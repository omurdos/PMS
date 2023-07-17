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
    public class DBContext : IdentityDbContext<User, Role, string>
    {

        //remote password 1Izp1YVb92
        private string LocalDBConnection = "Server=localhost;Port=3306;Database=pos_management_db;Uid=root;Pwd=;ConvertZeroDateTime=True;";
        private string remoteDBConnection = "Server=localhost;Port=3306;Database=pos_management_db;Uid=root;Pwd=;ConvertZeroDateTime=True;";
        //private string remoteDBConnection = "Server=86.98.71.233;Port=8088;Database=pos_management_db;Uid=root";
        //private string remoteDBConnection = "Server=localhost;Port=3306;Database=pos_management_db;Uid=root;";

        public virtual DbSet<Entities.Action> Actions { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Emirate> Emirates { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<DeviceModel> DeviceModels { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ApplicationCategory> ApplicaionCategories { get; set; }
        public virtual DbSet<Application> Applicaions{ get; set; }

        public DBContext()
        {

        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
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
            //optionsBuilder.UseMySql(remoteDBConnection, ServerVersion.Parse("10.4.22"));
        }

        private void ConfigureRelations(ModelBuilder builder)
        {
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
            builder.Entity<Device>().HasOne(d => d.Shop).WithMany(s => s.Devices).HasForeignKey(w => w.ShopId).IsRequired(false);
            builder.Entity<Device>().HasOne(d => d.User).WithMany(s => s.Devices).HasForeignKey(w => w.UserId).IsRequired(false);
            builder.Entity<Device>().HasOne(d => d.Emirate).WithMany(s => s.Devices).HasForeignKey(w => w.EmirateId).IsRequired(false);
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
            builder.Entity<Manufacturer>().HasOne(d => d.Provider).WithMany(s => s.Manufacturers).HasForeignKey(w => w.ProviderId);

            //Model
            builder.Entity<DeviceModel>().HasKey(c => c.Id);
            builder.Entity<DeviceModel>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<DeviceModel>().Property(c => c.Name).IsRequired();
            builder.Entity<DeviceModel>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<DeviceModel>().HasOne(d => d.Manufacturer).WithMany(s => s.DeviceModels).HasForeignKey(w => w.ManufacturerId);
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
            //Applications
            builder.Entity<Application>().HasKey(c => c.Id);
            builder.Entity<Application>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Application>().Property(c => c.Version).IsRequired();
            builder.Entity<Application>().Property(c => c.IsActive).HasDefaultValue(true);
            //ApplicationCategories
            builder.Entity<ApplicationCategory>().HasKey(c => c.Id);
            builder.Entity<ApplicationCategory>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ApplicationCategory>().Property(c => c.Name).IsRequired();


            //     builder.Entity<ManufacturerProvider>()
            //.HasKey(bc => new { bc.ManufacturerId, bc.ProviderId });
            //     builder.Entity<ManufacturerProvider>()
            //         .HasOne(bc => bc.Provider)
            //         .WithMany(b => b.ManufacturerProviders)
            //         .HasForeignKey(bc => bc.ProviderId);
            //     builder.Entity<ManufacturerProvider>()
            //         .HasOne(bc => bc.Manufacturer)
            //         .WithMany(c => c.ManufacturerProviders)
            //         .HasForeignKey(bc => bc.ManufacturerId);

            //Manufacturer & Provider relation configuration
            // builder.Entity<Provider>()
            //.HasMany(p => p.Manufacturers)
            //.WithMany(p => p.Providers)
            //.UsingEntity<ManufacturerProvider>(
            //    j => j
            //        .HasOne(pt => pt.Provider)
            //        .WithMany(t => t.ManufacturerProviders)
            //        .HasForeignKey(pt => pt.ProviderId),
            //    j => j
            //        .HasOne(pt => pt.Manufacturer)
            //        .WithMany(p => p.ManufacturerProviders)
            //        .HasForeignKey(pt => pt.ManufacturerId),
            //    j =>
            //    {

            //        j.HasKey(t => new { t.ProviderId, t.ManufacturerId });
            //    });
        }
    }
}
