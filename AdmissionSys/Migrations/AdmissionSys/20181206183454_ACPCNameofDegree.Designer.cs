﻿// <auto-generated />
using System;
using AdmissionSys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AdmissionSys.Migrations.AdmissionSys
{
    [DbContext(typeof(AdmissionSysContext))]
    [Migration("20181206183454_ACPCNameofDegree")]
    partial class ACPCNameofDegree
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AdmissionSys.Models.AcademicRecord", b =>
                {
                    b.Property<int>("AcademicRecordID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ACPCMeritNumber")
                        .HasMaxLength(15);

                    b.Property<string>("BoardOrUni")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<double>("CalcPercentage");

                    b.Property<string>("ExamName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LangOfInstruction")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("MarksObtained")
                        .IsRequired();

                    b.Property<string>("MarksOrGrade");

                    b.Property<decimal>("MeritNumber");

                    b.Property<DateTime>("MonthYearOfPass");

                    b.Property<string>("NameOfDegree")
                        .HasMaxLength(70);

                    b.Property<string>("ObtainedOutOfOrCGPA")
                        .IsRequired();

                    b.Property<string>("RollNumber")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SchoolOrCollegeName")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<DateTime>("ScoreValidity");

                    b.Property<string>("SpecializationOrMainSubject")
                        .HasMaxLength(40);

                    b.Property<int?>("StudentID");

                    b.HasKey("AcademicRecordID");

                    b.HasIndex("StudentID");

                    b.ToTable("AcademicRecord");
                });

            modelBuilder.Entity("AdmissionSys.Models.ApplicationList", b =>
                {
                    b.Property<int>("ApplicationListID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AcademicRecAdded");

                    b.Property<bool>("AdmissionConfirmed");

                    b.Property<string>("ApplicationCategory")
                        .IsRequired();

                    b.Property<bool>("AttendInterview");

                    b.Property<bool>("ConfirmFeesPayment");

                    b.Property<bool>("CounsellingDone");

                    b.Property<bool>("FormVerified");

                    b.Property<string>("PrioAreaOfResearch");

                    b.Property<string>("ProgramsID");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<int?>("StudentID");

                    b.HasKey("ApplicationListID");

                    b.HasIndex("ProgramsID");

                    b.HasIndex("StudentID");

                    b.ToTable("ApplicationList");
                });

            modelBuilder.Entity("AdmissionSys.Models.Documents", b =>
                {
                    b.Property<string>("DocumentsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DocumentPath");

                    b.Property<int?>("DocumentType");

                    b.Property<bool>("DocumentUploaded");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("StudentID");

                    b.HasKey("DocumentsID");

                    b.HasIndex("StudentID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("AdmissionSys.Models.Fees", b =>
                {
                    b.Property<int>("FeesID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeesAmount");

                    b.Property<string>("FeesType")
                        .IsRequired();

                    b.Property<string>("ProgramsID");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("FeesID");

                    b.HasIndex("ProgramsID");

                    b.ToTable("Fees");
                });

            modelBuilder.Entity("AdmissionSys.Models.Programs", b =>
                {
                    b.Property<string>("ProgramsID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DocsRequired");

                    b.Property<int>("ElegibilityCriteria");

                    b.Property<int>("IntakeCapacity");

                    b.Property<bool>("IsIntakeOpen");

                    b.Property<string>("ProgramName")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("ProgramsID");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("AdmissionSys.Models.Student", b =>
                {
                    b.Property<int>("StudentID");

                    b.Property<string>("AadharNumber");

                    b.Property<string>("ApplicantsMobileNumber")
                        .IsRequired();

                    b.Property<int>("BloodGroup");

                    b.Property<int?>("Caste");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<decimal>("FamilyIncome");

                    b.Property<bool>("FillPersonalInfo");

                    b.Property<int>("Gender");

                    b.Property<string>("GuardianMobileNumber")
                        .IsRequired();

                    b.Property<string>("GuardianOccupation")
                        .HasMaxLength(20);

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("PersonalMessage");

                    b.Property<bool>("PhysicallyHandicapStatus");

                    b.Property<string>("PlaceOfBirth")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<int>("RelationWithGuardian");

                    b.Property<string>("ResidencePhone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("State");

                    b.Property<string>("StudentAddress")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("StudentGuardianName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("StudentPhoto")
                        .IsRequired();

                    b.Property<string>("StudentSignature")
                        .IsRequired();

                    b.Property<string>("userID");

                    b.HasKey("StudentID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("AdmissionSys.Models.AcademicRecord", b =>
                {
                    b.HasOne("AdmissionSys.Models.Student", "Student")
                        .WithMany("AcademicRecords")
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("AdmissionSys.Models.ApplicationList", b =>
                {
                    b.HasOne("AdmissionSys.Models.Programs", "Programs")
                        .WithMany("ApplicationLists")
                        .HasForeignKey("ProgramsID");

                    b.HasOne("AdmissionSys.Models.Student", "Student")
                        .WithMany("ApplicationLists")
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("AdmissionSys.Models.Documents", b =>
                {
                    b.HasOne("AdmissionSys.Models.Student", "Student")
                        .WithMany("Documents")
                        .HasForeignKey("StudentID");
                });

            modelBuilder.Entity("AdmissionSys.Models.Fees", b =>
                {
                    b.HasOne("AdmissionSys.Models.Programs", "Programs")
                        .WithMany("Fees")
                        .HasForeignKey("ProgramsID");
                });
#pragma warning restore 612, 618
        }
    }
}
