﻿// <auto-generated />
using System;
using ISI.infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISI.infrastructure.Migrations
{
    [DbContext(typeof(IsiDbContext))]
    partial class IsiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ISI.Domain.Entity.Reservation", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("reservation_code_pk");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("checkin");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("checkout");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("ReservationPassword")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("reservation_password");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("character varying(10)")
                        .HasColumnName("fK_reservation_room");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("fk_reservation_user");

                    b.HasKey("Id")
                        .HasName("reservation_code");

                    b.HasIndex("RoomNumber");

                    b.HasIndex("UserId");

                    b.ToTable("reservations", (string)null);
                });

            modelBuilder.Entity("ISI.Domain.Entity.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("room_number_pk");

                    b.Property<long>("ControllerId")
                        .HasColumnType("bigint");

                    b.Property<string>("RoomLock")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("access_code");

                    b.HasKey("Id")
                        .HasName("room_number_pk");

                    b.HasIndex("ControllerId")
                        .IsUnique();

                    b.ToTable("room", (string)null);
                });

            modelBuilder.Entity("ISI.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("ISI.Domain.ValueObject.Entry", b =>
                {
                    b.Property<string>("RoomNumber")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("room_number");

                    b.Property<string>("ReservationCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("reservation_code");

                    b.Property<DateTime>("AccessTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("access_time");

                    b.HasKey("RoomNumber", "ReservationCode", "AccessTime");

                    b.ToTable("room_history", (string)null);
                });

            modelBuilder.Entity("ISI.infrastructure.Models.ControllerModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("controller_address");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("LockCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)")
                        .HasColumnName("lock_code");

                    b.HasKey("Id");

                    b.ToTable("controller", (string)null);
                });

            modelBuilder.Entity("ISI.Domain.Entity.Reservation", b =>
                {
                    b.HasOne("ISI.Domain.Entity.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fK_reservation_room");

                    b.HasOne("ISI.Domain.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reservation_user");
                });

            modelBuilder.Entity("ISI.Domain.Entity.Room", b =>
                {
                    b.HasOne("ISI.infrastructure.Models.ControllerModel", null)
                        .WithOne()
                        .HasForeignKey("ISI.Domain.Entity.Room", "ControllerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_room_controller");
                });
#pragma warning restore 612, 618
        }
    }
}
