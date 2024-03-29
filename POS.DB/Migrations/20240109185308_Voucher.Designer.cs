﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS.DB;

#nullable disable

namespace POS.DB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240109185308_Voucher")]
    partial class Voucher
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DiscountItemCategory", b =>
                {
                    b.Property<int>("DiscountsId")
                        .HasColumnType("int");

                    b.Property<int>("ForCategoriesId")
                        .HasColumnType("int");

                    b.HasKey("DiscountsId", "ForCategoriesId");

                    b.HasIndex("ForCategoriesId");

                    b.ToTable("DiscountItemCategory");
                });

            modelBuilder.Entity("DiscountLoyaltyProgram", b =>
                {
                    b.Property<int>("DiscountsId")
                        .HasColumnType("int");

                    b.Property<int>("ForLoyaltyProgramsId")
                        .HasColumnType("int");

                    b.HasKey("DiscountsId", "ForLoyaltyProgramsId");

                    b.HasIndex("ForLoyaltyProgramsId");

                    b.ToTable("DiscountLoyaltyProgram");
                });

            modelBuilder.Entity("ItemItemCategory", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ItemsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ItemsId");

                    b.HasIndex("ItemsId");

                    b.ToTable("ItemItemCategory");
                });

            modelBuilder.Entity("POS.DB.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Businesss");
                });

            modelBuilder.Entity("POS.DB.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DiscountInAmount")
                        .HasColumnType("decimal(6,2)");

                    b.Property<decimal>("DiscountInPct")
                        .HasColumnType("decimal(3,2)");

                    b.Property<int?>("ForSpecificItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.Property<int?>("minQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ForSpecificItemId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("POS.DB.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<int>("DefaultTaxId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUnavailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<TimeSpan?>("ServiceDuration")
                        .HasColumnType("time");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("DefaultTaxId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("POS.DB.Models.ItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("ItemCategories");
                });

            modelBuilder.Entity("POS.DB.Models.LoyaltyCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CardCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoyaltyPoints")
                        .HasColumnType("int");

                    b.Property<int?>("LoyaltyProgramId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LoyaltyProgramId");

                    b.HasIndex("UserId");

                    b.ToTable("LoyaltyCards");
                });

            modelBuilder.Entity("POS.DB.Models.LoyaltyProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("LoyaltyPrograms");
                });

            modelBuilder.Entity("POS.DB.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PendingUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TipAmount")
                        .HasColumnType("decimal(6,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("POS.DB.Models.OrderLine", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("LineId")
                        .HasColumnType("int");

                    b.Property<int>("AppliedTaxId")
                        .HasColumnType("int");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UnitCount")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("OrderId", "LineId");

                    b.HasIndex("AppliedTaxId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ItemId");

                    b.ToTable("OrdersLine");
                });

            modelBuilder.Entity("POS.DB.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsReserved")
                        .HasColumnType("bit");

                    b.Property<int>("ProvidingEmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ResEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ResStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProvidingEmployeeId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("POS.DB.Models.Tax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountPct")
                        .HasColumnType("decimal(3,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("POS.DB.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("POS.DB.Models.Voucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(6,2)");

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("OrderId");

                    b.ToTable("Voucher");
                });

            modelBuilder.Entity("POS.DB.Models.WorkingHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("DiscountItemCategory", b =>
                {
                    b.HasOne("POS.DB.Models.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.ItemCategory", null)
                        .WithMany()
                        .HasForeignKey("ForCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscountLoyaltyProgram", b =>
                {
                    b.HasOne("POS.DB.Models.Discount", null)
                        .WithMany()
                        .HasForeignKey("DiscountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.LoyaltyProgram", null)
                        .WithMany()
                        .HasForeignKey("ForLoyaltyProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ItemItemCategory", b =>
                {
                    b.HasOne("POS.DB.Models.ItemCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("POS.DB.Models.Discount", b =>
                {
                    b.HasOne("POS.DB.Models.Item", "ForSpecificItem")
                        .WithMany()
                        .HasForeignKey("ForSpecificItemId");

                    b.Navigation("ForSpecificItem");
                });

            modelBuilder.Entity("POS.DB.Models.Item", b =>
                {
                    b.HasOne("POS.DB.Models.Business", "Business")
                        .WithMany("Items")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.Tax", "DefaultTax")
                        .WithMany("Items")
                        .HasForeignKey("DefaultTaxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("DefaultTax");
                });

            modelBuilder.Entity("POS.DB.Models.ItemCategory", b =>
                {
                    b.HasOne("POS.DB.Models.Business", "Business")
                        .WithMany("ItemCategories")
                        .HasForeignKey("BusinessId");

                    b.Navigation("Business");
                });

            modelBuilder.Entity("POS.DB.Models.LoyaltyCard", b =>
                {
                    b.HasOne("POS.DB.Models.LoyaltyProgram", "LoyaltyProgram")
                        .WithMany("LoyaltyCards")
                        .HasForeignKey("LoyaltyProgramId");

                    b.HasOne("POS.DB.Models.User", "User")
                        .WithMany("LoyaltyCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoyaltyProgram");

                    b.Navigation("User");
                });

            modelBuilder.Entity("POS.DB.Models.LoyaltyProgram", b =>
                {
                    b.HasOne("POS.DB.Models.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("POS.DB.Models.Order", b =>
                {
                    b.HasOne("POS.DB.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("POS.DB.Models.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("POS.DB.Models.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("POS.DB.Models.OrderLine", b =>
                {
                    b.HasOne("POS.DB.Models.Tax", "AppliedTax")
                        .WithMany("OrderLines")
                        .HasForeignKey("AppliedTaxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.HasOne("POS.DB.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppliedTax");

                    b.Navigation("Discount");

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("POS.DB.Models.Reservation", b =>
                {
                    b.HasOne("POS.DB.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("POS.DB.Models.User", "ProvidingEmployee")
                        .WithMany()
                        .HasForeignKey("ProvidingEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.Item", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("ProvidingEmployee");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("POS.DB.Models.User", b =>
                {
                    b.HasOne("POS.DB.Models.Business", "Business")
                        .WithMany("Users")
                        .HasForeignKey("BusinessId");

                    b.Navigation("Business");
                });

            modelBuilder.Entity("POS.DB.Models.Voucher", b =>
                {
                    b.HasOne("POS.DB.Models.Business", "Business")
                        .WithMany("Vouchers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.Order", "Order")
                        .WithMany("Vouchers")
                        .HasForeignKey("OrderId");

                    b.Navigation("Business");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("POS.DB.Models.WorkingHours", b =>
                {
                    b.HasOne("POS.DB.Models.Business", "Business")
                        .WithMany("BusinessHours")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("POS.DB.Models.User", "Employee")
                        .WithMany("EmployeeWorkingHours")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("POS.DB.Models.Business", b =>
                {
                    b.Navigation("BusinessHours");

                    b.Navigation("ItemCategories");

                    b.Navigation("Items");

                    b.Navigation("Users");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("POS.DB.Models.LoyaltyProgram", b =>
                {
                    b.Navigation("LoyaltyCards");
                });

            modelBuilder.Entity("POS.DB.Models.Order", b =>
                {
                    b.Navigation("OrderLines");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("POS.DB.Models.Tax", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("POS.DB.Models.User", b =>
                {
                    b.Navigation("EmployeeWorkingHours");

                    b.Navigation("LoyaltyCards");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
