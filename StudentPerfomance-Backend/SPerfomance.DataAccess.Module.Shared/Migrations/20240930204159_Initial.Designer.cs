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
    [Migration("20240930204159_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Disciplines.Discipline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("TeacherId");

                    b.ToTable("Disciplines", (string)null);
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationDirections.EducationDirection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

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

                    b.ToTable("EducationPlans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.SemesterPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("LinkedDisciplineId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LinkedSemesterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LinkedDisciplineId");

                    b.HasIndex("LinkedSemesterId");

                    b.ToTable("SemesterPlans", (string)null);
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

                    b.HasIndex("PlanId");

                    b.ToTable("Semesters", (string)null);
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGrades.StudentGrade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DisciplineId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("GradeDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Value", "SPerfomance.Domain.Module.Shared.Entities.StudentGrades.StudentGrade.Value#GradeValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Grades", (string)null);
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EducationPlanId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup.Name#GroupName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("Name");
                        });

                    b.HasKey("Id");

                    b.HasIndex("EducationPlanId");

                    b.ToTable("Groups", (string)null);
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

                    b.ComplexProperty<Dictionary<string, object>>("Name", "SPerfomance.Domain.Module.Shared.Entities.Students.Student.Name#StudentName", b1 =>
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

                    b.ComplexProperty<Dictionary<string, object>>("Recordbook", "SPerfomance.Domain.Module.Shared.Entities.Students.Student.Recordbook#StudentRecordBook", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<ulong>("Recordbook")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Recordbook");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("State", "SPerfomance.Domain.Module.Shared.Entities.Students.Student.State#StudentState", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("State");
                        });

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students", (string)null);
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

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("Departments", (string)null);
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

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Disciplines.Discipline", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher", "Teacher")
                        .WithMany("Disciplines")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Teacher");
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
                        .WithMany()
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
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Disciplines.Discipline", "LinkedDiscipline")
                        .WithMany()
                        .HasForeignKey("LinkedDisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Semesters.Semester", "LinkedSemester")
                        .WithMany("Contracts")
                        .HasForeignKey("LinkedSemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LinkedDiscipline");

                    b.Navigation("LinkedSemester");
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

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGrades.StudentGrade", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Disciplines.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Students.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.Teachers.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", "EducationPlan")
                        .WithMany()
                        .HasForeignKey("EducationPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationPlan");
                });

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.Students.Student", b =>
                {
                    b.HasOne("SPerfomance.Domain.Module.Shared.Entities.StudentGroups.StudentGroup", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
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

            modelBuilder.Entity("SPerfomance.Domain.Module.Shared.Entities.EducationPlans.EducationPlan", b =>
                {
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
