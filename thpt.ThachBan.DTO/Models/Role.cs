﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace thpt.ThachBan.DTO.Models
{
    public partial class Role
    {
        public Role()
        {
            License = new HashSet<License>();
        }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public int RoleGroup { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<License> License { get; set; }
    }
}