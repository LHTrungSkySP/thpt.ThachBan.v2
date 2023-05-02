﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace thpt.ThachBan.DTO.Models
{
    public partial class Student
    {
        public Student()
        {
            Guardian = new HashSet<Guardian>();
            Result = new HashSet<Result>();
        }

        public Guid StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nation { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public int? CodeOfSeat { get; set; }
        public Guid? StudentTaskId { get; set; }
        public Guid? ClassId { get; set; }
        public Guid? SocialPolicyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual Class Class { get; set; }
        public virtual SocialPolicy SocialPolicy { get; set; }
        public virtual StudentTask StudentTask { get; set; }
        public virtual ICollection<Guardian> Guardian { get; set; }
        public virtual ICollection<Result> Result { get; set; }
    }
}