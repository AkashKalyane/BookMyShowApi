using System;
using System.Collections.Generic;
using BookMyShow.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.DataAccessLayer.DataContext;

public partial class BookMyShowContext : DbContext
{
    public BookMyShowContext()
    {
    }

    public BookMyShowContext(DbContextOptions<BookMyShowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Theater> Theaters { get; set; }

    public virtual DbSet<TheaterScreen> TheaterScreens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserContact> UserContacts { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=LP198\\SQLEXPRESS; database=BookMyShow; trusted_connection=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK_Actor_ActorId");

            entity.ToTable("Actor");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.ActorChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Actor_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ActorCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actor_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.ActorDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Actor_DeletedBy_User_UserId");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK_Booking_BookingId");

            entity.ToTable("Booking");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.BookingChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Booking_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BookingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.BookingDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Booking_DeletedBy_User_UserId");

            entity.HasOne(d => d.Movie).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Booking_MovieId_Movie_MovieId");

            entity.HasOne(d => d.Slot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SlotId)
                .HasConstraintName("FK_Booking_SlotId_Slot_SlotId");

            entity.HasOne(d => d.TheaterScreen).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TheaterScreenId)
                .HasConstraintName("FK_Booking_TheaterScreenId_TheaterScreen_TheaterScreenId");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK_Director_DirectorId");

            entity.ToTable("Director");

            entity.HasIndex(e => e.Name, "UQ_Director_Name").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfMovies)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.DirectorChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Director_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DirectorCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Director_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.DirectorDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Director_DeletedBy_User_UserId");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_Genre_GenreId");

            entity.ToTable("Genre");

            entity.HasIndex(e => e.GenreName, "UQ_Genre_GenreName").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.GenreName)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.GenreChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Genre_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GenreCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Genre_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.GenreDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Genre_DeletedBy_User_UserId");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK_Movie_MovieId");

            entity.ToTable("Movie");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Grade)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.MovieName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseYear)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.MovieChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Movie_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MovieCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.MovieDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Movie_DeletedBy_User_UserId");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_DirectorId_Director_DirectorId");

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_GenreId_Genre_GenreId");

            entity.HasOne(d => d.MainActorFemaleNavigation).WithMany(p => p.MovieMainActorFemaleNavigations)
                .HasForeignKey(d => d.MainActorFemale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_MainActorFemale_Actor_ActorId");

            entity.HasOne(d => d.MainActorMaleNavigation).WithMany(p => p.MovieMainActorMaleNavigations)
                .HasForeignKey(d => d.MainActorMale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movie_MainActorMale_Actor_ActorId");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK_Slot_Slot_SlotId");

            entity.ToTable("Slot");

            entity.HasIndex(e => e.SlotName, "UQ_Slot_SlotName").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.SlotName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.SlotChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Slot_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SlotCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slot_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.SlotDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Slot_DeletedBy_User_UserId");
        });

        modelBuilder.Entity<Theater>(entity =>
        {
            entity.HasKey(e => e.TheaterId).HasName("PK_Theater_Theater_id");

            entity.ToTable("Theater");

            entity.HasIndex(e => e.TheaterName, "UQ_Theater_TheaterName").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.TheaterName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.TheaterChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_Theater_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TheaterCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Theater_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TheaterDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Theater_DeletedBy_User_UserId");
        });

        modelBuilder.Entity<TheaterScreen>(entity =>
        {
            entity.HasKey(e => e.TheaterScreenId).HasName("PK_TheaterScreen_TheaterScreenId");

            entity.ToTable("TheaterScreen");

            entity.HasIndex(e => new { e.TheaterId, e.ScreenName }, "UQ_TheaterScreen_TheaterId_ScreenName").IsUnique();

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.TheaterScreenChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_TheaterScreen_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TheaterScreenCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TheaterScreen_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.TheaterScreenDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_TheaterScreen_DeletedBy_User_UserId");

            entity.HasOne(d => d.Theater).WithMany(p => p.TheaterScreens)
                .HasForeignKey(d => d.TheaterId)
                .HasConstraintName("FK_TheaterScreen_TheaterId_Theater_TheaterId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User_UserId");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ_User_Email").IsUnique();

            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserContact>(entity =>
        {
            entity.HasKey(e => e.UserContactId).HasName("PK_UserContact_UserContactId");

            entity.ToTable("UserContact");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.UserContactChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_UserContact_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserContactCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserContact_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserContactDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_UserContact_DeletedBy_User_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserContactUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserContact_userId_User_userId");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailId).HasName("PK_UserDetail_UserDetailId");

            entity.ToTable("UserDetail");

            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.CreateOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FullNameFl)
                .HasMaxLength(41)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat([firstName],' ',[lastName]))", true)
                .HasColumnName("FullNameFL");
            entity.Property(e => e.FullNameFml)
                .HasMaxLength(62)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat([firstName],' ',[middleName],' ',[lastName]))", true)
                .HasColumnName("FullNameFML");
            entity.Property(e => e.FullNameLf)
                .HasMaxLength(41)
                .IsUnicode(false)
                .HasComputedColumnSql("(concat([lastName],' ',[firstName]))", true)
                .HasColumnName("FullNameLF");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.UserDetailChangedByNavigations)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK_UserDetail_ChangedBy_User_UserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UserDetailCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetail_CreatedBy_User_UserId");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.UserDetailDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_UserDetail_DeletedBy_User_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetailUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserDetail_UserId_User_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
