﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PAM.Data;
using Xunit;

namespace PAM.Test.Data
{
    [Collection(nameof(DataServiceCollection))]
    public class OrganizationServiceTests
    {
        readonly IConfiguration _configuration;
        readonly DbContextOptions<AppDbContext> _dbContextOptions;
        readonly ILogger<OrganizationService> _logger;

        public OrganizationServiceTests(DataServiceFixture dataServiceFixture)
        {
            _configuration = dataServiceFixture.Configuration;
            _dbContextOptions = dataServiceFixture.DbContextOptions;
            _logger = dataServiceFixture.LoggerFactory.CreateLogger<OrganizationService>();
        }

        [Fact]
        public void GetBureausTest()
        {
            using (var dbContext = new AppDbContext(_dbContextOptions, _configuration))
            {
                var orgnizationService = new OrganizationService(dbContext, _logger);
                Assert.Equal(14, orgnizationService.GetBureaus().Count);
            }
        }
    }
}
