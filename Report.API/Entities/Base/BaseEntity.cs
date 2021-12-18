﻿using System.ComponentModel.DataAnnotations;

namespace Report.API.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid UUID { get; set; }
    }
}
