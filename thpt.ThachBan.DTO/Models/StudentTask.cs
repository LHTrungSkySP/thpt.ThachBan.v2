﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace thpt.ThachBan.DTO.Models
{
    public partial class StudentTask
    {
        public StudentTask()
        {
            Student = new HashSet<Student>();
        }

        public Guid StudentTaskId { get; set; }
        public string StudentTaskName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}