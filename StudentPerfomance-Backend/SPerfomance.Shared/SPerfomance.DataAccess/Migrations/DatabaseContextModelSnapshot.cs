﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPerfomance.DataAccess;

#nullable disable

namespace SPerfomance.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SPerfomance.Domain.Models.EducationDirections.EducationDirection", b =>
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

            modelBuilder.Entity("SPerfomance.Domain.Models.EducationPlans.EducationPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DirectionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Year", "SPerfomance.Domain.Models.EducationPlans.EducationPlan.Year#PlanYear", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Year")
                                .HasColumnType("INTEGER");
                        });

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("EducationPlans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.AssignmentSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SessionCloseDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SessionStartDate")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Number", "SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.AssignmentSession.Number#AssignmentSessionNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<byte>("Number")
                                .HasColumnType("INTEGER");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("State", "SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.AssignmentSession.State#AssignmentSessionState", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<bool>("State")
                                .HasColumnType("INTEGER");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Type", "SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.AssignmentSession.Type#AssignmentSessionSemesterType", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DisciplineId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("WeekId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("WeekId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.AssignmentWeek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("GroupId");

                    b.HasIndex("SessionId");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments.StudentAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Value", "SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments.StudentAssignment.Value#AssignmentValue", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<byte>("Value")
                                .HasColumnType("INTEGER");
                        });

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAssignments");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.SemesterPlans.SemesterPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SemesterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("SemesterId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SemesterPlans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Semesters.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("TEXT");

                    b.ComplexProperty<Dictionary<string, object>>("Number", "SPerfomance.Domain.Models.Semesters.Semester.Number#SemesterNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<byte>("Number")
                                .HasColumnType("INTEGER");
                        });

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.HasIndex("PlanId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.StudentGroups.StudentGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ActiveGroupSemesterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EducationPlanId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActiveGroupSemesterId");

                    b.HasIndex("EducationPlanId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AttachedGroupId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttachedGroupId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.TeacherDepartments.TeachersDepartments", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Acronymus")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Teachers.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("EntityNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "SPerfomance.Domain.Models.Teachers.Teacher.Name#TeacherName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Patronymic")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AttachedRoleId")
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

                    b.HasKey("Id");

                    b.HasIndex("EntityNumber")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.EducationDirections.EducationDirection", b =>
                {
                    b.OwnsOne("SPerfomance.Domain.Models.EducationDirections.ValueObjects.DirectionCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("EducationDirectionId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Code")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("EducationDirectionId");

                            b1.HasIndex("Code")
                                .IsUnique();

                            b1.ToTable("EducationDirections");

                            b1.WithOwner()
                                .HasForeignKey("EducationDirectionId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.EducationDirections.ValueObjects.DirectionName", "Name", b1 =>
                        {
                            b1.Property<Guid>("EducationDirectionId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("EducationDirectionId");

                            b1.ToTable("EducationDirections");

                            b1.WithOwner()
                                .HasForeignKey("EducationDirectionId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.EducationDirections.ValueObjects.DirectionType", "Type", b1 =>
                        {
                            b1.Property<Guid>("EducationDirectionId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("TEXT");

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

            modelBuilder.Entity("SPerfomance.Domain.Models.EducationPlans.EducationPlan", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.EducationDirections.EducationDirection", "Direction")
                        .WithMany("Plans")
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direction");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Assignment", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.SemesterPlans.SemesterPlan", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.AssignmentWeek", "Week")
                        .WithMany("Assignments")
                        .HasForeignKey("WeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.AssignmentWeek", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.StudentGroups.StudentGroup", "Group")
                        .WithMany("Weeks")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.AssignmentSession", "Session")
                        .WithMany("Weeks")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments.StudentAssignment", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Assignment", "Assignment")
                        .WithMany("StudentAssignments")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Models.Students.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.SemesterPlans.SemesterPlan", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.Semesters.Semester", "Semester")
                        .WithMany("Disciplines")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPerfomance.Domain.Models.Teachers.Teacher", "Teacher")
                        .WithMany("Disciplines")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("SPerfomance.Domain.Models.SemesterPlans.ValueObjects.DisciplineName", "Discipline", b1 =>
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

                    b.Navigation("Discipline")
                        .IsRequired();

                    b.Navigation("Semester");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Semesters.Semester", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.EducationPlans.EducationPlan", "Plan")
                        .WithMany("Semesters")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.StudentGroups.StudentGroup", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.Semesters.Semester", "ActiveGroupSemester")
                        .WithMany()
                        .HasForeignKey("ActiveGroupSemesterId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SPerfomance.Domain.Models.EducationPlans.EducationPlan", "EducationPlan")
                        .WithMany("Groups")
                        .HasForeignKey("EducationPlanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("SPerfomance.Domain.Models.StudentGroups.ValueObjects.StudentGroupName", "Name", b1 =>
                        {
                            b1.Property<Guid>("StudentGroupId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("StudentGroupId");

                            b1.HasIndex("Name")
                                .IsUnique();

                            b1.ToTable("Groups");

                            b1.WithOwner()
                                .HasForeignKey("StudentGroupId");
                        });

                    b.Navigation("ActiveGroupSemester");

                    b.Navigation("EducationPlan");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Students.Student", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.StudentGroups.StudentGroup", "AttachedGroup")
                        .WithMany("Students")
                        .HasForeignKey("AttachedGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SPerfomance.Domain.Models.Students.ValueObjects.StudentName", "Name", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Patronymic")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.Students.ValueObjects.StudentRecordbook", "Recordbook", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<ulong>("Recordbook")
                                .HasColumnType("INTEGER");

                            b1.HasKey("StudentId");

                            b1.HasIndex("Recordbook")
                                .IsUnique();

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.Students.ValueObjects.StudentState", "State", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("AttachedGroup");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Recordbook")
                        .IsRequired();

                    b.Navigation("State")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.TeacherDepartments.TeachersDepartments", b =>
                {
                    b.OwnsOne("SPerfomance.Domain.Models.TeacherDepartments.ValueObjects.DepartmentName", "Name", b1 =>
                        {
                            b1.Property<Guid>("TeachersDepartmentsId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("TeachersDepartmentsId");

                            b1.ToTable("Departments");

                            b1.WithOwner()
                                .HasForeignKey("TeachersDepartmentsId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Teachers.Teacher", b =>
                {
                    b.HasOne("SPerfomance.Domain.Models.TeacherDepartments.TeachersDepartments", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SPerfomance.Domain.Models.Teachers.ValueObjects.TeacherJobTitle", "JobTitle", b1 =>
                        {
                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("JobTitle")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("TeacherId");

                            b1.ToTable("Teachers");

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.Teachers.ValueObjects.TeacherWorkingState", "State", b1 =>
                        {
                            b1.Property<Guid>("TeacherId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("TeacherId");

                            b1.ToTable("Teachers");

                            b1.WithOwner()
                                .HasForeignKey("TeacherId");
                        });

                    b.Navigation("Department");

                    b.Navigation("JobTitle")
                        .IsRequired();

                    b.Navigation("State")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Users.User", b =>
                {
                    b.OwnsOne("SPerfomance.Domain.Models.Users.ValueObjects.UserEmail", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.Users.ValueObjects.UserRole", "Role", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Role")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("SPerfomance.Domain.Models.Users.ValueObjects.Username", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Patronymic")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Role")
                        .IsRequired();
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.EducationDirections.EducationDirection", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.EducationPlans.EducationPlan", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Semesters");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.AssignmentSession", b =>
                {
                    b.Navigation("Weeks");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Assignment", b =>
                {
                    b.Navigation("StudentAssignments");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.AssignmentWeek", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Semesters.Semester", b =>
                {
                    b.Navigation("Disciplines");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.StudentGroups.StudentGroup", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Weeks");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.TeacherDepartments.TeachersDepartments", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("SPerfomance.Domain.Models.Teachers.Teacher", b =>
                {
                    b.Navigation("Disciplines");
                });
#pragma warning restore 612, 618
        }
    }
}
