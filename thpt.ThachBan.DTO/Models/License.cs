﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace thpt.ThachBan.DTO.Models
{
    public partial class License
    {
        public Guid RoleId { get; set; }
        public Guid ActionId { get; set; }
        public DateTime? SkipTime { get; set; }

        public virtual Action Action { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}