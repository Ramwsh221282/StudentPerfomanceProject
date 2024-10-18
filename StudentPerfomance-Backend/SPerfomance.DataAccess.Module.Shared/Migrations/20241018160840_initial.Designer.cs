﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPerfomance.DataAccess.Module.Shared;

#nullable disable

namespace SPerfomance.DataAccess.Module.Shared.Migrations
{
    [DbContext(typeof(ApplicationDb))]
    [Migration("20241018160840_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.EducationDirection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("EducationDirections");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DirectionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("EducationPlans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.SemesterPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AttachedTeacherId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SemesterId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AttachedTeacherId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("SemesterId");

                    b.ToTable("SemesterPlans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Semesters.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Number", "SPerfomance.Domain.Module.Shared.Entities.Semesters.Semester.Number#SemesterNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<byte>("Value")
                                .HasColumnType("INTEGER");
                        });

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("PlanId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EducationPlanId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EducationPlanId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.TeachersDepartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher.Name#TeacherName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Surname");

                            b1.Property<string>("Thirdname")
                                .HasColumnType("TEXT")
                                .HasColumnName("Thirdname");
                        });

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "SPerfomance.Domain.Module.Shared.Entities.Users.User.Email#EmailValueObject", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.EducationDirection", b =>
                {
                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects.DirectionCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("EducationDirectionId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("DirectionCode");

                            b1.HasKey("EducationDirectionId");

                            b1.HasIndex("Code")
                                .IsUnique();

                            b1.ToTable("EducationDirections");

                            b1.WithOwner()
                                .HasForeignKey("EducationDirectionId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects.DirectionName", "Name", b1 =>
                        {
                            b1.Property<Guid>("EducationDirectionId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("DirectionName");

                            b1.HasKey("EducationDirectionId");

                            b1.ToTable("EducationDirections");

                            b1.WithOwner()
                                .HasForeignKey("EducationDirectionId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects.DirectionType", "Type", b1 =>
                        {
                            b1.Property<Guid>("EducationDirectionId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("DirectionType");

                            b1.HasKey("EducationDirectionId");

                            b1.ToTable("EducationDirections");

                            b1.WithOwner()
                                .HasForeignKey("EducationDirectionId");
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.EducationDirection", "Direction")
                        .WithMany("Plans")
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects.YearOfCreation", "Year", b1 =>
                        {
                            b1.Property<Guid>("EducationPlanId")
                                .HasColumnType("TEXT");

                            b1.Property<uint>("Year")
                                .HasColumnType("INTEGER")
                                .HasColumnName("YearOfCreation");

                            b1.HasKey("EducationPlanId");

                            b1.ToTable("EducationPlans");

                            b1.WithOwner()
                                .HasForeignKey("EducationPlanId");
                        });

                    b.Navigation("Direction");

                    b.Navigation("Year")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.SemesterPlan", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher", "AttachedTeacher")
                        .WithMany("Disciplines")
                        .HasForeignKey("AttachedTeacherId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Semesters.Semester", "Semester")
                        .WithMany("Contracts")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects.Discipline", "Discipline", b1 =>
                        {
                            b1.Property<Guid>("SemesterPlanId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("SemesterPlanId");

                            b1.ToTable("SemesterPlans");

                            b1.WithOwner()
                                .HasForeignKey("SemesterPlanId");
                        });

                    b.Navigation("AttachedTeacher");

                    b.Navigation("Discipline")
                        .IsRequired();

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Semesters.Semester", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", "Plan")
                        .WithMany("Semesters")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", "EducationPlan")
                        .WithMany("Groups")
                        .HasForeignKey("EducationPlanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects.GroupName", "Name", b1 =>
                        {
                            b1.Property<Guid>("StudentGroupId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");

                            b1.HasKey("StudentGroupId");

                            b1.HasIndex("Name")
                                .IsUnique();

                            b1.ToTable("Groups");

                            b1.WithOwner()
                                .HasForeignKey("StudentGroupId");
                        });

                    b.Navigation("EducationPlan");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Students.Student", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects.StudentName", "Name", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Surname");

                            b1.Property<string>("Thirdname")
                                .HasColumnType("TEXT")
                                .HasColumnName("Thirdname");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects.StudentRecordBook", "Recordbook", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<ulong>("Recordbook")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Recordbook");

                            b1.HasKey("StudentId");

                            b1.HasIndex("Recordbook")
                                .IsUnique();

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Students.ValueObjects.StudentState", "State", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("State");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("Group");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Recordbook")
                        .IsRequired();

                    b.Navigation("State")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.TeachersDepartment", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects.JobTitle", "JobTitle", b1 =>
                        {
                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("JobTitle");

                            b1.HasKey("TeacherId");

                            b1.ToTable("Teachers");

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects.WorkingCondition", "Condition", b1 =>
                        {
                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("WorkingCondition");

                            b1.HasKey("TeacherId");

                            b1.ToTable("Teachers");

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.Navigation("Condition")
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("JobTitle")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Users.User", b =>
                {
                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects.UserRole", "Role", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects.Username", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Thirdname")
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Role")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.EducationDirection", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Semesters");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Semesters.Semester", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.TeachersDepartment", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher", b =>
                {
                    b.Navigation("Disciplines");
                });
#pragma warning restore 612, 618
        }
    }
}
