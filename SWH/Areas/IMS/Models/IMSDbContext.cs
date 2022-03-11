using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using SWH.Areas.HR.Models;

namespace SWH.Areas.IMS.Models
{
    public class IMSDbContext:DbContext
    {
        public IMSDbContext() : base("SWHConnection") { }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingItems> BookingItems { get; set; }
        public DbSet<BookingHall> BookingHall { get; set; }
        public DbSet<Gulsaze> Gulsaze { get; set; }
        public DbSet<GulsazeItem> GulsazeItem { get; set; }
        public DbSet<GulsaziMenu> GulsaziMenu { get; set; }
        public DbSet<CategoryItems> CategoryItems { get; set; }
        public DbSet<Category> Category{get;set;}
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Production> Production { get; set; }
        public DbSet<ProductionItem> ProductionItem { get; set; }
        public DbSet<ProductionMenu> ProductionMenu { get; set; }
        public DbSet<MenuItemsSetting> MenuItemsSetting { get; set; }
        public DbSet<CategoryClass> CategoryClass { get; set; }
        public DbSet<BookingPayment> BookingPayment { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<BookingCancel> BookingCancel { get; set; }
        public DbSet<GulsazeExpensesMenu> GulsazeExpensesMenu { get; set; }
        public DbSet<GulsazeExpenses> GulsazeExpenses { get; set; }
        public DbSet<BookingType> BookingType { get; set; }
        //HR Tables

        public DbSet<USERINFO> USERINFO { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<CHECKINOUT> CHECKINOUT { get; set; }
        public DbSet<WorkHour> WorkHour { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<Exceptions> Exception { get; set; }
        public DbSet<UserPrepaid> UserPrepaid { get; set; }
        public DbSet<PayrolDetail> PayrolDetail { get; set; }
        public DbSet<Shift> Shift { get; set; }
    }
}