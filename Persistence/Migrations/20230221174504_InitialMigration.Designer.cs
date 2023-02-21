﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(GymXptoDbContext))]
    [Migration("20230221174504_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Schedule.ExerciseSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("SupersetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TargetRepetitions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupersetId");

                    b.ToTable("ExerciseSet");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("RestBetweenSets")
                        .HasColumnType("int");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Routine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Routines");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "um treino diferente para cada dia da semana",
                            Title = "Rotina do BestOf"
                        },
                        new
                        {
                            Id = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "um treino de lower body e um treino de upper body",
                            Title = "Rotina do BodyStation"
                        },
                        new
                        {
                            Id = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "treino construído com base em exemplos retirados da internet",
                            Title = "A minha rotina"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Superset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Superset");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoutineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("Workout");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.ExerciseSet", b =>
                {
                    b.HasOne("Domain.Entities.Schedule.Superset", null)
                        .WithMany("ExercisesInSuperset")
                        .HasForeignKey("SupersetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Group", b =>
                {
                    b.HasOne("Domain.Entities.Schedule.Workout", null)
                        .WithMany("ExerciseSequence")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Superset", b =>
                {
                    b.HasOne("Domain.Entities.Schedule.Group", null)
                        .WithMany("Sets")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Workout", b =>
                {
                    b.HasOne("Domain.Entities.Schedule.Routine", null)
                        .WithMany("Workouts")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Group", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Routine", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Superset", b =>
                {
                    b.Navigation("ExercisesInSuperset");
                });

            modelBuilder.Entity("Domain.Entities.Schedule.Workout", b =>
                {
                    b.Navigation("ExerciseSequence");
                });
#pragma warning restore 612, 618
        }
    }
}