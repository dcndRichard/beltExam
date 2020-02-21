﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using beltExam.Models;

namespace beltExam.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("beltExam.Models.FunThing", b =>
                {
                    b.Property<int>("FunThingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Desc")
                        .IsRequired();

                    b.Property<int>("Duration");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.Property<string>("hourMin");

                    b.HasKey("FunThingId");

                    b.HasIndex("UserId");

                    b.ToTable("FunThings");
                });

            modelBuilder.Entity("beltExam.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("FunThingId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.Property<bool>("isGoing");

                    b.HasKey("ParticipantId");

                    b.HasIndex("FunThingId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("beltExam.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("beltExam.Models.FunThing", b =>
                {
                    b.HasOne("beltExam.Models.User", "FunThingCreator")
                        .WithMany("FunThingsCreated")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("beltExam.Models.Participant", b =>
                {
                    b.HasOne("beltExam.Models.FunThing", "FunThing")
                        .WithMany("Participants")
                        .HasForeignKey("FunThingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("beltExam.Models.User", "Attendant")
                        .WithMany("FunThingsAttending")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}