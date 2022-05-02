﻿// <auto-generated />
using System;
using EFCData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCData.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20220427222521_tattiMigrations")]
    partial class tattiMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Entities.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WriterUsername")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("WriterUsername");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Entities.Models.Forum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForumDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ForumName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("Entities.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("SubForumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WrittenByUsername")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubForumId");

                    b.HasIndex("WrittenByUsername");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Entities.Models.SubForum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ForumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnedByUsername")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ForumId");

                    b.HasIndex("OwnedByUsername");

                    b.ToTable("SubForums");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Models.Comment", b =>
                {
                    b.HasOne("Entities.Models.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("Entities.Models.User", "Writer")
                        .WithMany()
                        .HasForeignKey("WriterUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("Entities.Models.Post", b =>
                {
                    b.HasOne("Entities.Models.SubForum", null)
                        .WithMany("AllPosts")
                        .HasForeignKey("SubForumId");

                    b.HasOne("Entities.Models.User", "WrittenBy")
                        .WithMany()
                        .HasForeignKey("WrittenByUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WrittenBy");
                });

            modelBuilder.Entity("Entities.Models.SubForum", b =>
                {
                    b.HasOne("Entities.Models.Forum", null)
                        .WithMany("AllSubForums")
                        .HasForeignKey("ForumId");

                    b.HasOne("Entities.Models.User", "OwnedBy")
                        .WithMany()
                        .HasForeignKey("OwnedByUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnedBy");
                });

            modelBuilder.Entity("Entities.Models.Forum", b =>
                {
                    b.Navigation("AllSubForums");
                });

            modelBuilder.Entity("Entities.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Entities.Models.SubForum", b =>
                {
                    b.Navigation("AllPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
