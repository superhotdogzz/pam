﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAM.Data;
using PAM.Models;
using PAM.Services;

namespace PAM.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IADService _adService;
        private readonly UserService _userService;
        private readonly ILogger _logger;

        public AccountController(IADService adService, UserService userService, ILogger<AccountController> logger)
        {
            _adService = adService;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            Employee employee = _adService.GetEmployee(username, password);
            if (employee == null)
            {
                return View();
            }

            Employee user = _userService.GetEmployeeByUsername(employee.Username);
            if (user != null)
            {
                user.Title = employee.Title;
                user.Department = employee.Department;
                user.Phone = employee.Phone;
                employee = user;
            }
            employee = _userService.SaveEmployee(employee);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(employee.ToClaimsIdentity()),
                new AuthenticationProperties());

            _logger.LogInformation($"User {employee.Username} logged in at {DateTime.UtcNow}.");

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"User {User.Identity.Name} logged out at {DateTime.UtcNow}.");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }
    }
}
