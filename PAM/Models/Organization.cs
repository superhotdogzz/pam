﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAM.Models
{
    [Table("Locations")]
    public class Location
    {
        public int LocationId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    [Table("BureauTypes")]
    public class BureauType
    {
        public int BureauTypeId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string DisplayCode { get; set; }
    }

    [Table("Bureaus")]
    public class Bureau
    {
        public int BureauId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        public int? BureauTypeId { get; set; }
        public BureauType BureauType { get; set; }

        public int? DisplayOrder { get; set; }
    }

    [Table("UnitTypes")]
    public class UnitType
    {
        public int UnitTypeId { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string DisplayCode { get; set; }

        public string Description { get; set; }

        public int? DisplayOrder { get; set; }
    }

    [Table("Units")]
    public class Unit
    {
        public int UnitId { get; set; }

        [Required]
        public string Name { get; set; }

        public int BureauId { get; set; }
        public Bureau Bureau { get; set; }

        public int? UnitTypeId { get; set; }
        public UnitType UnitType { get; set; }

        public int? ParentId { get; set; }
        public Unit Parent { get; set; }

        [InverseProperty(nameof(Parent))]
        public ICollection<Unit> Children { get; set; }

        public int? DisplayOrder { get; set; }

        public ICollection<UnitSystem> Systems { get; set; }
    }
}
