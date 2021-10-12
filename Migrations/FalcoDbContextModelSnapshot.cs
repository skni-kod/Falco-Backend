﻿// <auto-generated />
using System;
using FalcoBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FalcoBackEnd.Migrations
{
    [DbContext(typeof(FalcoDbContext))]
    partial class FalcoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConversationUser", b =>
                {
                    b.Property<int>("ConversationsConverastionId")
                        .HasColumnType("int");

                    b.Property<int>("OwnersId")
                        .HasColumnType("int");

                    b.HasKey("ConversationsConverastionId", "OwnersId");

                    b.HasIndex("OwnersId");

                    b.ToTable("ConversationUser");
                });

            modelBuilder.Entity("FalcoBackEnd.Models.Conversation", b =>
                {
                    b.Property<int>("ConverastionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ConverastionId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("FalcoBackEnd.Models.Message", b =>
                {
                    b.Property<int>("Message_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Author_id")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ConversationConverastionId")
                        .HasColumnType("int");

                    b.Property<int>("Conversation_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Message_id");

                    b.HasIndex("ConversationConverastionId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("FalcoBackEnd.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ConversationUser", b =>
                {
                    b.HasOne("FalcoBackEnd.Models.Conversation", null)
                        .WithMany()
                        .HasForeignKey("ConversationsConverastionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FalcoBackEnd.Models.User", null)
                        .WithMany()
                        .HasForeignKey("OwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FalcoBackEnd.Models.Message", b =>
                {
                    b.HasOne("FalcoBackEnd.Models.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationConverastionId");

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("FalcoBackEnd.Models.Conversation", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
