﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyUniversity.Data;

#nullable disable

namespace MyUniversity.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20240109082749_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("MyUniversity.Models.Course", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Credits")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("MyUniversity.Models.Enrollment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment", (string)null);
                });

            modelBuilder.Entity("MyUniversity.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("MyUniversity.Models.Enrollment", b =>
                {
                    b.HasOne("MyUniversity.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyUniversity.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MyUniversity.Models.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("MyUniversity.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
