using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using popfragg.Domain.Models;
using popfragg.Domain.Models.Nades;
using popfragg.Domain.Models.Users;

namespace popfragg.Infrastructure.Data;

public partial class RailwayContext : DbContext
{
    public RailwayContext()
    {
    }

    public RailwayContext(DbContextOptions<RailwayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<Nade> Nades { get; set; }

    public virtual DbSet<NadeAccess> NadeAccesses { get; set; }

    public virtual DbSet<NadeImage> NadeImages { get; set; }

    public virtual DbSet<NadeRating> NadeRatings { get; set; }

    public virtual DbSet<NadeTag> NadeTags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=mainline.proxy.rlwy.net;Port=30209;Database=railway;Username=postgres;Password=lfjNSSxgLQRlqDtjEcnnmPSdjurgVKVm;SSL Mode=Require;Trust Server Certificate=true;Include Error Detail=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("errors_pkey");

            entity.ToTable("errors", "logs");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CodeMessage).HasColumnName("code_message");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.HttpStatus).HasColumnName("http_status");
            entity.Property(e => e.Level)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Error'::character varying")
                .HasColumnName("level");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.MessageUser).HasColumnName("message_user");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.Properties)
                .HasColumnType("jsonb")
                .HasColumnName("properties");
            entity.Property(e => e.Source).HasColumnName("source");
            entity.Property(e => e.StackTrace).HasColumnName("stack_trace");
            entity.Property(e => e.TraceId).HasColumnName("trace_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Nade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nades_pkey");

            entity.ToTable("nades");

            entity.HasIndex(e => e.Map, "nades_map_index");

            entity.HasIndex(e => e.Side, "nades_side_index");

            entity.HasIndex(e => e.Type, "nades_type_index");

            entity.HasIndex(e => e.Visibility, "nades_visibility_index");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AccuracyLevel)
                .HasMaxLength(50)
                .HasColumnName("accuracyLevel");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Map)
                .HasMaxLength(50)
                .HasColumnName("map");
            entity.Property(e => e.Rating)
                .HasPrecision(8, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("rating");
            entity.Property(e => e.Side)
                .HasMaxLength(10)
                .HasColumnName("side");
            entity.Property(e => e.Technique)
                .HasMaxLength(100)
                .HasColumnName("technique");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .HasColumnName("videoUrl");
            entity.Property(e => e.Visibility)
                .HasMaxLength(10)
                .HasColumnName("visibility");

            entity.HasOne(d => d.Author).WithMany(p => p.Nades)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nades_userid_fkey");
        });

        modelBuilder.Entity<NadeAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nadeAccess_pkey");

            entity.ToTable("nadeAccess");

            entity.HasIndex(e => new { e.NadeId, e.UserId }, "nadeAccess_nadeId_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("grantedAt");
            entity.Property(e => e.NadeId).HasColumnName("nadeId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Nade).WithMany(p => p.NadeAccesses)
                .HasForeignKey(d => d.NadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nadeaccess_nadeid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.NadeAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nadeaccess_userid_fkey");
        });

        modelBuilder.Entity<NadeImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nadeImages_pkey");

            entity.ToTable("nadeImages");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ImageOrder).HasColumnName("imageOrder");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("imageUrl");
            entity.Property(e => e.NadeId).HasColumnName("nadeId");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("uploadedAt");

            entity.HasOne(d => d.Nade).WithMany(p => p.NadeImages)
                .HasForeignKey(d => d.NadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nadeimages_nadeid_fkey");
        });

        modelBuilder.Entity<NadeRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nadeRatings_pkey");

            entity.ToTable("nadeRatings");

            entity.HasIndex(e => new { e.NadeId, e.UserId }, "naderatings_unique_user_nade").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.NadeId).HasColumnName("nadeId");
            entity.Property(e => e.RatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ratedAt");
            entity.Property(e => e.Rating)
                .HasPrecision(8, 2)
                .HasColumnName("rating");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Nade).WithMany(p => p.NadeRatings)
                .HasForeignKey(d => d.NadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("naderatings_nadeid_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.NadeRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("naderatings_userid_foreign");
        });

        modelBuilder.Entity<NadeTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nadeTags_pkey");

            entity.ToTable("nadeTags");

            entity.HasIndex(e => new { e.NadeId, e.Tag }, "nadeTags_nadeId_tag_key").IsUnique();

            entity.HasIndex(e => e.Tag, "nadetags_tag_index");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.NadeId).HasColumnName("nadeId");
            entity.Property(e => e.Tag)
                .HasMaxLength(50)
                .HasColumnName("tag");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Nade).WithMany(p => p.NadeTags)
                .HasForeignKey(d => d.NadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nadetags_nadeid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Nickname, "displayname_1760482352656_index").IsUnique();

            entity.HasIndex(e => e.Email, "email_1760482312306_index");

            entity.HasIndex(e => e.Email, "email_1760482319943_index").IsUnique();

            entity.HasIndex(e => e.Nickname, "users_displayname_index");

            entity.HasIndex(e => e.Role, "users_role_index");

            entity.HasIndex(e => e.SteamId, "users_steamId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatarUrl");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(255)
                .HasColumnName("familyName");
            entity.Property(e => e.GivenName)
                .HasMaxLength(100)
                .HasComment("Primary Name")
                .HasColumnName("givenName");
            entity.Property(e => e.Nickname)
                .HasMaxLength(100)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(80)
                .HasColumnName("password");
            entity.Property(e => e.ProfileUrl)
                .HasMaxLength(255)
                .HasColumnName("profileUrl");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("'user'::character varying")
                .HasColumnName("role");
            entity.Property(e => e.SteamId)
                .HasMaxLength(50)
                .HasColumnName("steamId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
