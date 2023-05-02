﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace thpt.ThachBan.DTO.Models
{
    public partial class thptThachBanContext : DbContext
    {
        public thptThachBanContext()
        {
        }

        public thptThachBanContext(DbContextOptions<thptThachBanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentManager> DepartmentManager { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Guardian> Guardian { get; set; }
        public virtual DbSet<License> License { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SocialPolicy> SocialPolicy { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentTask> StudentTask { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.AccountCode, "IX_Account")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.Property(e => e.ActionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('7cdca116-f57b-4a75-b1a3-744d84512f0c')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Class_Employee");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.DepartmentName, "IX_Department")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('7cdca116-f57b-4a75-b1a3-744d84512f0c')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<DepartmentManager>(entity =>
            {
                entity.Property(e => e.DepartmentManagerId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentManager)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_DepartmentManager_Department");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DepartmentManager)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_DepartmentManager_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.EmployeeCode, "IX_Employee")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('7cdca116-f57b-4a75-b1a3-744d84512f0c')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkEnded).HasColumnType("date");

                entity.Property(e => e.WorkStarted).HasColumnType("date");

                entity.HasMany(d => d.Subject)
                    .WithMany(p => p.Employee)
                    .UsingEntity<Dictionary<string, object>>(
                        "TeacherExpertise",
                        l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TeacherTask_Subject"),
                        r => r.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TeacherTask_Employee"),
                        j =>
                        {
                            j.HasKey("EmployeeId", "SubjectId").HasName("PK_TeacherTask_1");

                            j.ToTable("TeacherExpertise");
                        });
            });

            modelBuilder.Entity<Guardian>(entity =>
            {
                entity.Property(e => e.GuardianId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Career)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GuardianName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Relation).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Guardian)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Guardian_Student");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ActionId });

                entity.Property(e => e.SkipTime).HasColumnType("date");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.License)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_License_Action1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.License)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_License_Role");
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.Grade, e.SubjectId, e.Semester });

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('7cdca116-f57b-4a75-b1a3-744d84512f0c')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Result_Student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Result_Subject");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.ClassTime, e.Day });

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Class");

                entity.HasOne(d => d.Empoyee)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.EmpoyeeId)
                    .HasConstraintName("FK_Schedule_Employee");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Schedule_Subject");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("logo");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<SocialPolicy>(entity =>
            {
                entity.Property(e => e.SocialPolicyId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.SocialPolicyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.StudentCode, "IX_Student")
                    .IsUnique();

                entity.Property(e => e.StudentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('7cdca116-f57b-4a75-b1a3-744d84512f0c')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Nation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StudentCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Student_Class");

                entity.HasOne(d => d.SocialPolicy)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SocialPolicyId)
                    .HasConstraintName("FK_Student_SocialPolicy");

                entity.HasOne(d => d.StudentTask)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.StudentTaskId)
                    .HasConstraintName("FK_Student_StudentTask");
            });

            modelBuilder.Entity<StudentTask>(entity =>
            {
                entity.HasIndex(e => e.StudentTaskName, "IX_StudentTask")
                    .IsUnique();

                entity.Property(e => e.StudentTaskId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.StudentTaskName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("('7cdca116-f57b-4a75-b1a3-744d84512f0c')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.LessonAweek).HasColumnName("LessonAWeek");

                entity.Property(e => e.MaxLessonAday).HasColumnName("MaxLessonADay");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subject_Department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}