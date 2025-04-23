using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FreeSweet.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contect> Contects { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<SecialOrder> SecialOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F194SCB;Database=FreeSweet;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07F509437A");

            entity.ToTable("Cart");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__Id__5AEE82B9");

            entity.HasOne(d => d.Id1).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__Id__59FA5E80");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3213E83F266DB368");

            entity.ToTable("CartItem");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Size).HasMaxLength(100);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.CartItem)
                .HasForeignKey<CartItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__Size__1CBC4616");

            entity.HasOne(d => d.Id1).WithOne(p => p.CartItem)
                .HasForeignKey<CartItem>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItem__id__1DB06A4F");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC076856CDA1");

            entity.ToTable("Category");

            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contect>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contect__3214EC071C8FFD61");

            entity.ToTable("Contect");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Msg).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC077EEA5561");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Id__5DCAEF64");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_Orders_Payment");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.Property(e => e.Size).HasMaxLength(200);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItem_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderItem_Product");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC076B532CC9");

            entity.ToTable("Payment");

            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethoud).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC072BA453E4");

            entity.ToTable("Product");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Img1)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Img2)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Img3)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Img4)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Id__571DF1D5");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipes__3214EC070561CA0A");

            entity.Property(e => e.CookTime).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Img1)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Img2)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Img3)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Img4)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Img5)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.PrepTime).HasMaxLength(100);
            entity.Property(e => e.TotalTime).HasMaxLength(100);
        });

        modelBuilder.Entity<SecialOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Secial_O__3214EC07DCD68ECB");

            entity.ToTable("Secial_Order");

            entity.Property(e => e.Date)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Img)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0730C455E1");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344048F8F2").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Img)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("IMG");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
