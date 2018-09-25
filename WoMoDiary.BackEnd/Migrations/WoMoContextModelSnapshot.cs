﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WoMoDiary.BackEnd;

namespace WoMoDiary.BackEnd.Migrations
{
    [DbContext(typeof(WoMoContext))]
    partial class WoMoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WoMoDiary.Domain.Place", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Altitude");

                    b.Property<string>("AssetName");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset>("LastEdit");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<short>("Rating");

                    b.Property<Guid>("TripFk");

                    b.Property<Guid?>("TripId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("WoMoDiary.Domain.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Description");

                    b.Property<DateTimeOffset?>("LastEdit");

                    b.Property<string>("Name");

                    b.Property<Guid>("UserFk");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("WoMoDiary.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Email");

                    b.Property<byte[]>("Hash");

                    b.Property<DateTimeOffset?>("LastEdit");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Salt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WoMoDiary.Domain.Place", b =>
                {
                    b.HasOne("WoMoDiary.Domain.Trip")
                        .WithMany("Places")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("WoMoDiary.Domain.Trip", b =>
                {
                    b.HasOne("WoMoDiary.Domain.User")
                        .WithMany("Trips")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
