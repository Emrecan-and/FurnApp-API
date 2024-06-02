using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FurnApp_API.Models
{
    public partial class FurnAppContext : DbContext
    {
        public FurnAppContext()
        {
        }

        public FurnAppContext(DbContextOptions<FurnAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<ProductColors> ProductColors { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-JEUIHMH\\SQLEXPRESS; Database=FurnApp;Trusted_Connection=True;");
            }
        }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("Address_Id");

                entity.Property(e => e.BuildingNumber)
                    .HasColumnName("Building_Number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.District)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HomeNumber)
                    .HasColumnName("Home_Number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasColumnName("Postal_Code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId).HasColumnName("Cart_Id");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.UsersId).HasColumnName("Users_Id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Cart__Product_Id__4E88ABD4");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Cart__Users_Id__4D94879B");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Categori__6DB38D6E214B5E79");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("Category_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasKey(e => e.ColorId)
                    .HasName("PK__Colors__795F1D5429A7B2A9");

                entity.Property(e => e.ColorId).HasColumnName("Color_Id");

                entity.Property(e => e.ColorName)
                    .HasColumnName("Color_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__F1E4607B628FFA14");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.AddressId).HasColumnName("Address_Id");

                entity.Property(e => e.CargoNo).HasColumnName("Cargo_No");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("Order_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.UsersId).HasColumnName("Users_Id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Orders__Address___4AB81AF0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Orders__Product___49C3F6B7");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Orders__Users_Id__48CFD27E");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");

                entity.Property(e => e.CardCvv).HasColumnName("Card_Cvv");

                entity.Property(e => e.CardMonth).HasColumnName("Card_Month");

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasColumnName("Card_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CardYear).HasColumnName("Card_Year");

                entity.Property(e => e.CargoCompany)
                    .HasColumnName("Cargo_Company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CargoPrice)
                    .HasColumnName("Cargo_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreditCardNo).HasColumnName("CreditCard_No");

                entity.Property(e => e.UsersId).HasColumnName("Users_Id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Payment__Users_I__45F365D3");
            });

            modelBuilder.Entity<ProductColors>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ColorId })
                    .HasName("PK__Product___CFA10A6FE49912E7");

                entity.ToTable("Product_Colors");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.ColorId).HasColumnName("Color_Id");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_C__Color__4316F928");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_C__Produ__4222D4EF");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__9834FBBAB844C9E8");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("Product_Description")
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasColumnName("Product_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPicture).HasColumnName("Product_Picture");

                entity.Property(e => e.ProductPrice)
                    .HasColumnName("Product_Price")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductStock).HasColumnName("Product_Stock");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__3F466844");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UsersId).HasColumnName("Users_Id");

                entity.Property(e => e.UsersAddress)
                    .HasColumnName("Users_Address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsersAuthorization).HasColumnName("Users_Authorization");

                entity.Property(e => e.UsersMail)
                    .HasColumnName("Users_Mail")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsersPassword)
                    .HasColumnName("Users_Password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsersTelNo).HasColumnName("Users_TelNo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
