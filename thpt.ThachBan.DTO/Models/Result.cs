﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace thpt.ThachBan.DTO.Models
{
    public partial class Result
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public int Grade { get; set; }
        public int Semester { get; set; }
        public double? Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}