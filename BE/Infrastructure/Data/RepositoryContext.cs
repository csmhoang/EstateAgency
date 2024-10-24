﻿using System;
using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static Dapper.SqlMapper;

namespace Infrastructure.Data
{
    public partial class RepositoryContext : DbContext
    {
        public RepositoryContext()
        {
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amenity> Amenities { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Lease> Leases { get; set; } = null!;
        public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=RentalHouse;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.HasIndex(e => e.AmenityCode, "UQ__Amenitie__300F6CA3021E4D38")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AmenityCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.HasIndex(e => e.FeedbackCode, "UQ__Feedback__BCE37B663CEFBD99")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.FeedbackCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostId).HasMaxLength(36);

                entity.Property(e => e.TenantId).HasMaxLength(36);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(e => e.InvoiceCode, "UQ__Invoices__0D9D7FF34AA36732")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.InvoiceCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.LeaseId).HasMaxLength(36);
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.HasIndex(e => e.LeaseCode, "UQ__Leases__D568E4B4702D9CD2")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LeaseCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.RoomId).HasMaxLength(36);

                entity.Property(e => e.SignedDate).HasColumnType("datetime");

                entity.Property(e => e.SignedOnline).HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TenantId).HasMaxLength(36);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasIndex(e => e.MaintenanceRequestCode, "UQ__Maintena__CBAB82F6EC2CDA6A")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MaintenanceRequestCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.LeaseId).HasMaxLength(36);

                entity.Property(e => e.InvoiceId).HasMaxLength(36);

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.MessageCode, "UQ__Messages__54E8229FE4E7C357")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MessageCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.ReceiverId).HasMaxLength(36);

                entity.Property(e => e.SenderId).HasMaxLength(36);

                entity.Property(e => e.SentAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId);

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.PaymentCode, "UQ__Payments__106D3BA8FEBBB92E")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.PaymentCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LeaseId).HasMaxLength(36);

                entity.Property(e => e.InvoiceId).HasMaxLength(36);

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasIndex(e => e.ReservationCode, "UQ__Reservat__2081C0BBCBCC7940")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ReservationCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReservationDate).HasColumnType("date");

                entity.Property(e => e.RoomId).HasMaxLength(36);

                entity.Property(e => e.TenantId).HasMaxLength(36);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.PostCode, "UQ__Posts__5K9D52454DASDASE")
                   .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.PostCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.AvailableFrom).HasColumnType("date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.RoomId).HasMaxLength(36);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(e => e.RoomCode, "UQ__Rooms__4F9D52313B1CAD3E")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.RoomCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Province).HasMaxLength(100);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.District).HasMaxLength(100);

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.Ward).HasMaxLength(100);

                entity.Property(e => e.LandlordId).HasMaxLength(36);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");

                entity.HasMany(d => d.Amenities)
                    .WithMany(p => p.Rooms)
                    .UsingEntity<Dictionary<string, object>>(
                        "RoomAmenity",
                        l => l.HasOne<Amenity>().WithMany().HasForeignKey("AmenityId").HasConstraintName("FK__RoomAmeni__Ameni__4E88ABD4"),
                        r => r.HasOne<Room>().WithMany().HasForeignKey("RoomId").HasConstraintName("FK__RoomAmeni__RoomI__4D94879B"),
                        j =>
                        {
                            j.HasKey("RoomId", "AmenityId").HasName("PK__RoomAmen__9AC496696494BAEA");

                            j.ToTable("RoomAmenities");

                            j.IndexerProperty<string>("RoomId").HasMaxLength(36);

                            j.IndexerProperty<string>("AmenityId").HasMaxLength(36);
                        });
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserCode, "UQ__Users__1DF52D0C64B859D5")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserCode)
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");

                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne<User>()
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .IsRequired();

                entity.HasOne<Role>()
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
