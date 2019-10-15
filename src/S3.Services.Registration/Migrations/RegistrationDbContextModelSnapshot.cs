﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using S3.Services.Registration.Utility;

namespace S3.Services.Registration.Migrations
{
    [DbContext(typeof(RegistrationDbContext))]
    partial class RegistrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("S3.Services.Registration.Domain.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Line2")
                        .HasMaxLength(100);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Address");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Address");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Class", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AssistantTeacherId");

                    b.Property<string>("Category");

                    b.Property<Guid?>("ClassTeacherId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("SchoolId");

                    b.Property<string>("Subjects");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("AssistantTeacherId");

                    b.HasIndex("ClassTeacherId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Parent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<bool>("IsSignedUp");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<string>("RegNumber")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Roles");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("RegNumber")
                        .IsUnique();

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.School", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.ScoresEntryTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ClassId")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("SubjectId");

                    b.Property<Guid>("TeacherId");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("ScoresEntryTasks");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<Guid?>("ClassId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<bool>("IsSignedUp");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.Property<bool>("OfferingAllClassSubjects");

                    b.Property<Guid?>("ParentId");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<string>("RegNumber")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Roles");

                    b.Property<Guid>("SchoolId");

                    b.Property<string>("Subjects");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("ParentId");

                    b.HasIndex("RegNumber")
                        .IsUnique();

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<double>("GradeLevel");

                    b.Property<bool>("IsSignedUp");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<string>("Position")
                        .HasMaxLength(50);

                    b.Property<string>("Roles");

                    b.Property<Guid>("SchoolId");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.ParentAddress", b =>
                {
                    b.HasBaseType("S3.Services.Registration.Domain.Address");

                    b.Property<Guid>("ParentId");

                    b.HasIndex("ParentId")
                        .IsUnique()
                        .HasFilter("[ParentId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("ParentAddress");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.SchoolAddress", b =>
                {
                    b.HasBaseType("S3.Services.Registration.Domain.Address");

                    b.Property<Guid>("SchoolId");

                    b.HasIndex("SchoolId")
                        .IsUnique()
                        .HasFilter("[SchoolId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("SchoolAddress");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.StudentAddress", b =>
                {
                    b.HasBaseType("S3.Services.Registration.Domain.Address");

                    b.Property<Guid>("StudentId");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("StudentAddress");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.TeacherAddress", b =>
                {
                    b.HasBaseType("S3.Services.Registration.Domain.Address");

                    b.Property<Guid>("TeacherId");

                    b.HasIndex("TeacherId")
                        .IsUnique()
                        .HasFilter("[TeacherId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("TeacherAddress");
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Class", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.Teacher", "AssistantTeacher")
                        .WithMany()
                        .HasForeignKey("AssistantTeacherId");

                    b.HasOne("S3.Services.Registration.Domain.Teacher", "ClassTeacher")
                        .WithMany()
                        .HasForeignKey("ClassTeacherId");

                    b.HasOne("S3.Services.Registration.Domain.School", "School")
                        .WithMany("Classes")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.ScoresEntryTask", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.Teacher", "Teacher")
                        .WithMany("ScoresEntryTasks")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Student", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId");

                    b.HasOne("S3.Services.Registration.Domain.Parent", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentId");

                    b.HasOne("S3.Services.Registration.Domain.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.Teacher", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.School", "School")
                        .WithMany("Teachers")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.ParentAddress", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.Parent", "Parent")
                        .WithOne("Address")
                        .HasForeignKey("S3.Services.Registration.Domain.ParentAddress", "ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.SchoolAddress", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.School", "School")
                        .WithOne("Address")
                        .HasForeignKey("S3.Services.Registration.Domain.SchoolAddress", "SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.StudentAddress", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("S3.Services.Registration.Domain.StudentAddress", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("S3.Services.Registration.Domain.TeacherAddress", b =>
                {
                    b.HasOne("S3.Services.Registration.Domain.Teacher", "Teacher")
                        .WithOne("Address")
                        .HasForeignKey("S3.Services.Registration.Domain.TeacherAddress", "TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
