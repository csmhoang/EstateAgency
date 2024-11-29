using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public partial class RepositoryContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
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
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<Lease> Leases { get; set; } = null!;
        public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } = null!;
        public virtual DbSet<MaintenanceImage> MaintenanceImages { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Follow> Follows { get; set; } = null!;
        public virtual DbSet<SavePost> SavePosts { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public virtual DbSet<LeaseDetail> LeaseDetails { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartDetail> CartDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=RentalHouse;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Amenity>(entity => { });

            modelBuilder.Entity<Feedback>(entity => { });

            modelBuilder.Entity<Invoice>(entity => { });

            modelBuilder.Entity<InvoiceDetail>(entity => { });

            modelBuilder.Entity<Cart>(entity => { });

            modelBuilder.Entity<CartDetail>(entity => { });

            modelBuilder.Entity<Lease>(entity => { });

            modelBuilder.Entity<LeaseDetail>(entity => { });

            modelBuilder.Entity<Booking>(entity => { });

            modelBuilder.Entity<BookingDetail>(entity => { });

            modelBuilder.Entity<MaintenanceRequest>(entity => { });

            modelBuilder.Entity<MaintenanceImage>(entity => { });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId);

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId);
            });

            modelBuilder.Entity<Payment>(entity => { });

            modelBuilder.Entity<Reservation>(entity => { });

            modelBuilder.Entity<Post>(entity => { });

            modelBuilder.Entity<Photo>(entity => { });

            modelBuilder.Entity<Room>(entity => { });

            modelBuilder.Entity<SavePost>(entity => { });

            modelBuilder.Entity<Follow>(entity => { });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.Property(e => e.Id)
                    .HasMaxLength(36);

                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.Id)
                    .HasMaxLength(36);

                entity.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                entity.HasMany(e => e.Followers)
                    .WithOne(e => e.Followee)
                    .HasForeignKey(f => f.FolloweeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasMany(e => e.Followees)
                    .WithOne(e => e.Follower)
                    .HasForeignKey(f => f.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
            });

            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
