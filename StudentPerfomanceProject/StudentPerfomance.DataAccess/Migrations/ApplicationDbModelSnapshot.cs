﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentPerfomance.DataAccess;

#nullable disable

namespace StudentPerfomance.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDb))]
    partial class ApplicationDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Discipline", b =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.EducationDirection", b =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.EducationPlan", b =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Number", "StudentPerfomance.Domain.Entities.Semester.Number#SemesterNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<byte>("Value")
                                .HasColumnType("INTEGER");
                        });

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PlanId");

                    b.ToTable("Semesters", (string)null);
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.SemesterPlan", b =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "StudentPerfomance.Domain.Entities.Student.Name#StudentName", b1 =>
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

                    b.ComplexProperty<Dictionary<string, object>>("Recordbook", "StudentPerfomance.Domain.Entities.Student.Recordbook#StudentRecordBook", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<ulong>("Recordbook")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Recordbook");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("State", "StudentPerfomance.Domain.Entities.Student.State#StudentState", b1 =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.StudentGrade", b =>
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

                    b.ComplexProperty<Dictionary<string, object>>("Value", "StudentPerfomance.Domain.Entities.StudentGrade.Value#GradeValue", b1 =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.StudentGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EducationPlanId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "StudentPerfomance.Domain.Entities.StudentGroup.Name#GroupName", b1 =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "StudentPerfomance.Domain.Entities.Teacher.Name#TeacherName", b1 =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.TeachersDepartment", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments", (string)null);
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Discipline", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Disciplines")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.EducationDirection", b =>
                {
                    b.OwnsOne("StudentPerfomance.Domain.ValueObjects.EducationDirections.DirectionCode", "Code", b1 =>
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

                    b.OwnsOne("StudentPerfomance.Domain.ValueObjects.EducationDirections.DirectionName", "Name", b1 =>
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

                    b.OwnsOne("StudentPerfomance.Domain.ValueObjects.EducationDirections.DirectionType", "Type", b1 =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.EducationPlan", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.EducationDirection", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("StudentPerfomance.Domain.ValueObjects.EducationPlans.YearOfCreation", "Year", b1 =>
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

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Semester", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.StudentGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPerfomance.Domain.Entities.EducationPlan", "Plan")
                        .WithMany("Semesters")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.SemesterPlan", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.Discipline", "LinkedDiscipline")
                        .WithMany()
                        .HasForeignKey("LinkedDisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPerfomance.Domain.Entities.Semester", "LinkedSemester")
                        .WithMany("Contracts")
                        .HasForeignKey("LinkedSemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LinkedDiscipline");

                    b.Navigation("LinkedSemester");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Student", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.StudentGroup", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.StudentGrade", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPerfomance.Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentPerfomance.Domain.Entities.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.StudentGroup", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.EducationPlan", "EducationPlan")
                        .WithMany()
                        .HasForeignKey("EducationPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationPlan");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("StudentPerfomance.Domain.Entities.TeachersDepartment", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.EducationPlan", b =>
                {
                    b.Navigation("Semesters");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Semester", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.StudentGroup", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Disciplines");
                });

            modelBuilder.Entity("StudentPerfomance.Domain.Entities.TeachersDepartment", b =>
                {
                    b.Navigation("Teachers");
                });
#pragma warning restore 612, 618
        }
    }
}
