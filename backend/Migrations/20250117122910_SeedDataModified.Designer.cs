﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250117122910_SeedDataModified")]
    partial class SeedDataModified
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademicProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("AcademicPrograms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "",
                            FacultyId = 1,
                            Name = "BSc Computer Science"
                        },
                        new
                        {
                            Id = 2,
                            Code = "",
                            FacultyId = 2,
                            Name = "BSc Physics"
                        },
                        new
                        {
                            Id = 3,
                            Code = "",
                            FacultyId = 3,
                            Name = "MBA"
                        },
                        new
                        {
                            Id = 4,
                            Code = "",
                            FacultyId = 4,
                            Name = "BA English"
                        });
                });

            modelBuilder.Entity("Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniversityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Faculties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Faculty of Engineering",
                            UniversityId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Faculty of Science",
                            UniversityId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Faculty of Business",
                            UniversityId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Faculty of Arts",
                            UniversityId = 2
                        });
                });

            modelBuilder.Entity("PDF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PDFs");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicProgramId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreditHours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AcademicProgramId");

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AcademicProgramId = 1,
                            Code = "",
                            CreditHours = 3,
                            Name = "Data Structures"
                        },
                        new
                        {
                            Id = 2,
                            AcademicProgramId = 1,
                            Code = "",
                            CreditHours = 3,
                            Name = "Operating Systems"
                        },
                        new
                        {
                            Id = 3,
                            AcademicProgramId = 2,
                            Code = "",
                            CreditHours = 4,
                            Name = "Quantum Mechanics"
                        },
                        new
                        {
                            Id = 4,
                            AcademicProgramId = 3,
                            Code = "",
                            CreditHours = 3,
                            Name = "Financial Management"
                        },
                        new
                        {
                            Id = 5,
                            AcademicProgramId = 4,
                            Code = "",
                            CreditHours = 2,
                            Name = "English Literature"
                        });
                });

            modelBuilder.Entity("SubjectPDF", b =>
                {
                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("PDFId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId", "PDFId");

                    b.HasIndex("PDFId");

                    b.ToTable("SubjectPDFs");
                });

            modelBuilder.Entity("University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserPDF", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PDFId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PDFId");

                    b.HasIndex("PDFId");

                    b.ToTable("UserPDFs");
                });

            modelBuilder.Entity("AcademicProgram", b =>
                {
                    b.HasOne("Faculty", "Faculty")
                        .WithMany("AcademicPrograms")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Faculty", b =>
                {
                    b.HasOne("University", "University")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.HasOne("AcademicProgram", "AcademicProgram")
                        .WithMany("Subjects")
                        .HasForeignKey("AcademicProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicProgram");
                });

            modelBuilder.Entity("SubjectPDF", b =>
                {
                    b.HasOne("PDF", "PDF")
                        .WithMany("SubjectPDFs")
                        .HasForeignKey("PDFId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subject", "Subject")
                        .WithMany("SubjectPDFs")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PDF");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("UserPDF", b =>
                {
                    b.HasOne("PDF", "PDF")
                        .WithMany("UserPDFs")
                        .HasForeignKey("PDFId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("UserPDFs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PDF");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicProgram", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("Faculty", b =>
                {
                    b.Navigation("AcademicPrograms");
                });

            modelBuilder.Entity("PDF", b =>
                {
                    b.Navigation("SubjectPDFs");

                    b.Navigation("UserPDFs");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.Navigation("SubjectPDFs");
                });

            modelBuilder.Entity("University", b =>
                {
                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("UserPDFs");
                });
#pragma warning restore 612, 618
        }
    }
}