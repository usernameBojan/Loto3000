﻿// <auto-generated />
using System;
using Loto3000.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221021225659_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Loto3000.Domain.Entities.Combination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("Combination");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Draw", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DrawNumbersString")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("DrawTime")
                        .HasMaxLength(64)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SessionEnd")
                        .HasMaxLength(64)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SessionStart")
                        .HasMaxLength(64)
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Draws");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DrawNumbersString = "",
                            DrawTime = new DateTime(2022, 10, 31, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            SessionEnd = new DateTime(2022, 10, 31, 20, 0, 0, 0, DateTimeKind.Unspecified),
                            SessionStart = new DateTime(2022, 10, 22, 20, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.DrawNumbers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DrawId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrawId");

                    b.ToTable("DrawNumbers");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.NonregisteredPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DepositAmount")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NonregisteredPlayer");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.SuperAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SuperAdmin");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "123456789101112",
                            Role = "SuperAdmin",
                            Username = "SystemAdministrator"
                        });
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CombinationNumbersString")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DrawId")
                        .HasColumnType("int");

                    b.Property<int>("NumbersGuessed")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Prize")
                        .HasColumnType("int");

                    b.Property<DateTime>("TicketCreatedTime")
                        .HasMaxLength(256)
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DrawId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Tickets");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Ticket");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.TransactionTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("DepositAmount")
                        .HasColumnType("float");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Transactions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TransactionTracker");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("Loto3000.Domain.Entities.User");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.NonregisteredPlayerTicket", b =>
                {
                    b.HasBaseType("Loto3000.Domain.Entities.Ticket");

                    b.Property<int?>("NonregisteredPlayerId")
                        .HasColumnType("int");

                    b.HasIndex("NonregisteredPlayerId")
                        .IsUnique()
                        .HasFilter("[NonregisteredPlayerId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("NonregisteredPlayerTicket");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.NonregisteredPlayerTransaction", b =>
                {
                    b.HasBaseType("Loto3000.Domain.Entities.TransactionTracker");

                    b.Property<int?>("NonregisteredPlayerId")
                        .HasColumnType("int");

                    b.HasIndex("NonregisteredPlayerId")
                        .IsUnique()
                        .HasFilter("[NonregisteredPlayerId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("NonregisteredPlayerTransaction");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Player", b =>
                {
                    b.HasBaseType("Loto3000.Domain.Entities.User");

                    b.Property<double>("Credits")
                        .HasMaxLength(50)
                        .HasColumnType("float");

                    b.Property<DateTime>("DateOfBirth")
                        .HasMaxLength(256)
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("ForgotPasswordCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ForgotPasswordCodeCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Combination", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.Ticket", null)
                        .WithMany("CombinationNumbers")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.DrawNumbers", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.Draw", null)
                        .WithMany("DrawNumbers")
                        .HasForeignKey("DrawId");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.Draw", "Draw")
                        .WithMany("Tickets")
                        .HasForeignKey("DrawId");

                    b.HasOne("Loto3000.Domain.Entities.Player", "Player")
                        .WithMany("Tickets")
                        .HasForeignKey("PlayerId");

                    b.Navigation("Draw");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.TransactionTracker", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.Player", "Player")
                        .WithMany("Transactions")
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Admin", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Loto3000.Domain.Entities.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.NonregisteredPlayerTicket", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.NonregisteredPlayer", "NonregisteredPlayer")
                        .WithOne("Ticket")
                        .HasForeignKey("Loto3000.Domain.Entities.NonregisteredPlayerTicket", "NonregisteredPlayerId");

                    b.Navigation("NonregisteredPlayer");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.NonregisteredPlayerTransaction", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.NonregisteredPlayer", "NonregisteredPlayer")
                        .WithOne("Transaction")
                        .HasForeignKey("Loto3000.Domain.Entities.NonregisteredPlayerTransaction", "NonregisteredPlayerId");

                    b.Navigation("NonregisteredPlayer");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Player", b =>
                {
                    b.HasOne("Loto3000.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Loto3000.Domain.Entities.Player", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Draw", b =>
                {
                    b.Navigation("DrawNumbers");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.NonregisteredPlayer", b =>
                {
                    b.Navigation("Ticket");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Ticket", b =>
                {
                    b.Navigation("CombinationNumbers");
                });

            modelBuilder.Entity("Loto3000.Domain.Entities.Player", b =>
                {
                    b.Navigation("Tickets");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}