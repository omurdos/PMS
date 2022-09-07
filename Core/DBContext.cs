using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class DBContext : IdentityDbContext<User,IdentityRole, string>
    {

        private string LocalDBConnection = "Server=localhost;Port=3306;Database=PMS;Uid=root;Pwd=;";

        public virtual DbSet<Entities.Action> Actions { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Emirate> Emirates { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Shop> Shops{ get; set; }
        public virtual DbSet<Status> Statuses { get; set; }


        public DBContext()
        {

        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            foreach (var fk in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Cascade;
            }
            ConfigureRelations(builder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(LocalDBConnection, ServerVersion.Parse("10.4.22"));
        }

        private void ConfigureRelations(ModelBuilder builder) {
            //Actions for loging all users actions.
            builder.Entity<Entities.Action>().HasKey(c => c.Id);
            builder.Entity<Entities.Action>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Entities.Action>().Property(c => c.Text).IsRequired();
            //Device
            builder.Entity<Device>().HasKey(c => c.Id);
            builder.Entity<Device>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Device>().Property(c => c.Lat).IsRequired();
            builder.Entity<Device>().Property(c => c.Lng).IsRequired();
            builder.Entity<Device>().Property(c => c.IMEI1).IsRequired();
            builder.Entity<Device>().Property(c => c.ApplicationVersion).IsRequired();
            builder.Entity<Device>().Property(c => c.SIMCardSerialNumber).IsRequired();
            builder.Entity<Device>().Property(c => c.IsDeployed).IsRequired();
            builder.Entity<Device>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<Device>().HasOne(d => d.Shop).WithMany(s => s.Devices).HasForeignKey(w => w.ShopId);
            builder.Entity<Device>().HasOne(d => d.User).WithMany(s => s.Devices).HasForeignKey(w => w.UserId);
            builder.Entity<Device>().HasOne(d => d.Emirate).WithMany(s => s.Devices).HasForeignKey(w => w.EmirateId);
            builder.Entity<Device>().HasOne(d => d.Status).WithMany(s => s.Devices).HasForeignKey(w => w.StatusId);
            builder.Entity<Device>().HasOne(d => d.Model).WithMany(s => s.Devices).HasForeignKey(w => w.ModelId);
            //Emirate
            builder.Entity<Emirate>().HasKey(c => c.Id);
            builder.Entity<Emirate>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Emirate>().Property(c => c.Name).IsRequired();
            builder.Entity<Emirate>().Property(c => c.IsActive).HasDefaultValue(true);
            //Manufacturer
            builder.Entity<Manufacturer>().HasKey(c => c.Id);
            builder.Entity<Manufacturer>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Manufacturer>().Property(c => c.Name).IsRequired();
            builder.Entity<Manufacturer>().Property(c => c.IsActive).HasDefaultValue(true);
            //Model
            builder.Entity<Model>().HasKey(c => c.Id);
            builder.Entity<Model>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Model>().Property(c => c.Name).IsRequired();
            builder.Entity<Model>().Property(c => c.IsActive).HasDefaultValue(true);
            //Provider
            builder.Entity<Provider>().HasKey(c => c.Id);
            builder.Entity<Provider>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Provider>().Property(c => c.Name).IsRequired();
            builder.Entity<Provider>().Property(c => c.IsActive).HasDefaultValue(true);
            //Shop
            builder.Entity<Shop>().HasKey(c => c.Id);
            builder.Entity<Shop>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Shop>().Property(c => c.Name).IsRequired();
            builder.Entity<Shop>().Property(c => c.PhoneNumber).IsRequired();
            builder.Entity<Shop>().Property(c => c.IsActive).HasDefaultValue(true);
            //Status
            builder.Entity<Status>().HasKey(c => c.Id);
            builder.Entity<Status>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Status>().Property(c => c.Name).IsRequired();
            builder.Entity<Status>().Property(c => c.IsActive).HasDefaultValue(true);
        }
    }
}
