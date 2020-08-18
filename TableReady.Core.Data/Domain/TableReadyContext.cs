//Author: Gustavo Moises (Developer)
//Date: 8/7/2020
//Thead Project PROJ—207-OOS
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TableReady.Core.Data.Domain
{
    public partial class TableReadyContext : DbContext
    {
        public TableReadyContext()
        {
        }

        public TableReadyContext(DbContextOptions<TableReadyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authentication> Authentication { get; set; }
        public virtual DbSet<AuthenticationMatrix> AuthenticationMatrix { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Layouts> Layouts { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<ReservationEntry> ReservationEntry { get; set; }
        public virtual DbSet<ReservationEntryTable> ReservationEntryTable { get; set; }
        public virtual DbSet<RestaurantEmployees> RestaurantEmployees { get; set; }
        public virtual DbSet<RestaurantOwners> RestaurantOwners { get; set; }
        public virtual DbSet<Restaurants> Restaurants { get; set; }
        public virtual DbSet<TableAvailabilityDates> TableAvailabilityDates { get; set; }
        public virtual DbSet<TableGroups> TableGroups { get; set; }
        public virtual DbSet<TableInGroups> TableInGroups { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WaitlistEntry> WaitlistEntry { get; set; }
        public virtual DbSet<WaitlistEntryTable> WaitlistEntryTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;;Database=TableReady;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authentication>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuthenticationMatrix>(entity =>
            {
                entity.HasKey(e => new { e.AuthenticationId, e.RestaurantId })
                    .HasName("PK__Authenti__C9E5C890837267E7");

                entity.Property(e => e.AuthenticationId).HasColumnName("AuthenticationID");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Authentication)
                    .WithMany(p => p.AuthenticationMatrix)
                    .HasForeignKey(d => d.AuthenticationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Matrix_Auth_FK");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.AuthenticationMatrix)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Matrix_Rest_FK");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("CustomerID_PK");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerFirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerLastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserCustomer_FK");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("EmployeeID_PK");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserEmployee_FK");
            });

            modelBuilder.Entity<Layouts>(entity =>
            {
                entity.HasKey(e => e.LayoutId)
                    .HasName("LayoutID_PK");

                entity.Property(e => e.LayoutImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LayoutName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Layouts)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LayoutRestaurant_FK");
            });

            modelBuilder.Entity<Owners>(entity =>
            {
                entity.HasKey(e => e.OwnerId)
                    .HasName("OwnerID_PK");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserOwner_FK");
            });

            modelBuilder.Entity<ReservationEntry>(entity =>
            {
                entity.HasKey(e => e.ReservationId)
                    .HasName("ReservationID_PK");

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.CheckinDateTime).HasColumnType("datetime");

                entity.Property(e => e.CheckoutDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerMessage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EntryOrigin)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReservationDateTime).HasColumnType("datetime");

                entity.Property(e => e.ReservationStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.SeatingDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ReservationEntry)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerID_FK");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.ReservationEntry)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RestaurantID_FK");
            });

            modelBuilder.Entity<ReservationEntryTable>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.ReservationId })
                    .HasName("RET_Comp_PK");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationEntryTable)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ReservationID_FK");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.ReservationEntryTable)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TableID_FK");
            });

            modelBuilder.Entity<RestaurantEmployees>(entity =>
            {
                entity.HasKey(e => new { e.RestaurantId, e.EmployeeId })
                    .HasName("PK__Restaura__80E8486A77DBDA2F");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.NewRequestFlag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RequestStatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('On Hold')");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Inactive')");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RestaurantEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RE_Employees_FK");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantEmployees)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RE_Restaurant_FK");
            });

            modelBuilder.Entity<RestaurantOwners>(entity =>
            {
                entity.HasKey(e => new { e.RestaurantId, e.OwnerId })
                    .HasName("PK__Restaura__1F5C74CCD3E10B55");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RequestStatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('On Hold')");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Inactive')");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.RestaurantOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RO_Owners_FK");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantOwners)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RO_Restaurant_FK");
            });

            modelBuilder.Entity<Restaurants>(entity =>
            {
                entity.HasKey(e => e.RestaurantId)
                    .HasName("RestaurantID_PK");

                entity.Property(e => e.RestaurantName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.LayoutActiveNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.LayoutActive)
                    .HasConstraintName("RestaurantLayout_FK");
            });

            modelBuilder.Entity<TableAvailabilityDates>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.Datetime, e.TableGroupId })
                    .HasName("PK__TableAva__43A821D9F91DCEB2");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.Datetime)
                    .HasColumnName("_Datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.TableGroupId).HasColumnName("TableGroupID");

                entity.Property(e => e.AvailabilityStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CheckOutStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CleaningStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TableAvailabilityDates)
                    .HasForeignKey(d => new { d.TableId, d.TableGroupId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TAD_TableID_FK");
            });

            modelBuilder.Entity<TableGroups>(entity =>
            {
                entity.HasKey(e => e.TableGroupId)
                    .HasName("TableGroupID_PK");

                entity.Property(e => e.TableGroupId).HasColumnName("TableGroupID");

                entity.Property(e => e.LayoutId).HasColumnName("LayoutID");

                entity.Property(e => e.TableGroupImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableGroupName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableGroupPosX).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TableGroupPosY).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Layout)
                    .WithMany(p => p.TableGroups)
                    .HasForeignKey(d => d.LayoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LayoutID_FK");
            });

            modelBuilder.Entity<TableInGroups>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.TableGroupId })
                    .HasName("PK__TableInG__34A9C0AC9E7F7485");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TableGroupId).HasColumnName("TableGroupID");

                entity.Property(e => e.RestaurantArea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantZone)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TablePosX).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TablePosY).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.TableGroup)
                    .WithMany(p => p.TableInGroups)
                    .HasForeignKey(d => d.TableGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TableGroupID_FK");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TableInGroups)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TIG_TableID_FK");
            });

            modelBuilder.Entity<Tables>(entity =>
            {
                entity.HasKey(e => e.TableId)
                    .HasName("TableID_PK");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.TableImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableImageAvailable)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableImageCheckout)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableImageCleaning)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableImageUnavailable)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TableSize).HasColumnType("text");

                entity.Property(e => e.TableType).HasColumnType("text");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Tables)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("TableRestaurant_FK");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("UserID_PK");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AuthenticationId).HasColumnName("AuthenticationID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Authentication)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AuthenticationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AuthenticationUser_FK");
            });

            modelBuilder.Entity<WaitlistEntry>(entity =>
            {
                entity.Property(e => e.WaitlistEntryId).HasColumnName("WaitlistEntryID");

                entity.Property(e => e.CheckinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CheckoutDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EntryOrigin)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.SeatingDate).HasColumnType("datetime");

                entity.Property(e => e.WaitlistStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.WaitlistEntry)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerWaitID_FK");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.WaitlistEntry)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RestaurantWaitID_FK");
            });

            modelBuilder.Entity<WaitlistEntryTable>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.WaitlistEntryId })
                    .HasName("WET_Comp_PK");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.WaitlistEntryId).HasColumnName("WaitlistEntryID");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.WaitlistEntryTable)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TableWaitID_FK");

                entity.HasOne(d => d.WaitlistEntry)
                    .WithMany(p => p.WaitlistEntryTable)
                    .HasForeignKey(d => d.WaitlistEntryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WaitlistEntryID_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
