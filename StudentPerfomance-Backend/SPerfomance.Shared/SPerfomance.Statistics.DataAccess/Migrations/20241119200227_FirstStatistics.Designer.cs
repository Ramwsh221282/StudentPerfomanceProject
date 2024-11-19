﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPerfomance.Statistics.DataAccess;

#nullable disable

namespace SPerfomance.Statistics.DataAccess.Migrations
{
    [DbContext(typeof(StatisticsDatabaseContext))]
    [Migration("20241119200227_FirstStatistics")]
    partial class FirstStatistics
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.ControlWeekReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RowNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ControlWeekReports");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.CourseStatisticsReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<int>("Course")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DirectionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Perfomance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("RootId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("CourseStatisticsReportEntities");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DirectionCodeStatisticsReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<string>("DirectionCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Perfomance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("RootId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("DirectionCodeStatisticsReportEntities");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DirectionTypeStatisticsReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<string>("DirectionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Perfomance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("RootId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("DirectionTypeStatisticsReportEntities");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DisciplinesStatisticsReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<string>("DisciplineName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Perfomance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("RootId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeacherName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TeacherPatronymic")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeacherSurname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("DisciplinesStatisticsReports");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.GroupStatisticsReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<byte>("Course")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DirectionCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DirectionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Perfomance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("RootId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("GroupStatisticsReports");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.StudentStatisticsReportEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Average")
                        .HasColumnType("REAL");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Perfomance")
                        .HasColumnType("REAL");

                    b.Property<ulong>("Recordbook")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RootId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentPatronymic")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentSurname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.ToTable("StudentStatisticsReports");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.CourseStatisticsReportEntity", b =>
                {
                    b.HasOne("SPerfomance.Statistics.DataAccess.EntityModels.ControlWeekReportEntity", "Root")
                        .WithMany("CourseParts")
                        .HasForeignKey("RootId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Root");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DirectionCodeStatisticsReportEntity", b =>
                {
                    b.HasOne("SPerfomance.Statistics.DataAccess.EntityModels.ControlWeekReportEntity", "Root")
                        .WithMany("DirectionCodeReport")
                        .HasForeignKey("RootId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Root");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DirectionTypeStatisticsReportEntity", b =>
                {
                    b.HasOne("SPerfomance.Statistics.DataAccess.EntityModels.ControlWeekReportEntity", "Root")
                        .WithMany("DirectionTypeReport")
                        .HasForeignKey("RootId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Root");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DisciplinesStatisticsReportEntity", b =>
                {
                    b.HasOne("SPerfomance.Statistics.DataAccess.EntityModels.GroupStatisticsReportEntity", "Root")
                        .WithMany("Parts")
                        .HasForeignKey("RootId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Root");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.GroupStatisticsReportEntity", b =>
                {
                    b.HasOne("SPerfomance.Statistics.DataAccess.EntityModels.ControlWeekReportEntity", "Root")
                        .WithMany("GroupParts")
                        .HasForeignKey("RootId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Root");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.StudentStatisticsReportEntity", b =>
                {
                    b.HasOne("SPerfomance.Statistics.DataAccess.EntityModels.DisciplinesStatisticsReportEntity", "Root")
                        .WithMany("Parts")
                        .HasForeignKey("RootId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Root");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.ControlWeekReportEntity", b =>
                {
                    b.Navigation("CourseParts");

                    b.Navigation("DirectionCodeReport");

                    b.Navigation("DirectionTypeReport");

                    b.Navigation("GroupParts");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.DisciplinesStatisticsReportEntity", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("SPerfomance.Statistics.DataAccess.EntityModels.GroupStatisticsReportEntity", b =>
                {
                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}