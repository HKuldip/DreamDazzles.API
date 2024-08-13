using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DreamDazzle.Model.Data
{
    public class MainDBContext : IdentityDbContext<User>
    {
        public MainDBContext(DbContextOptions<MainDBContext> options) : base(options) { }

        #region All Table

        //public DbSet<Form> Forms { get; set; }
        public DbSet<Product> Product { get; set; }

        #endregion
        /// <summary>
        /// Configures the MainContext
        /// </summary>
        /// <param name="optionsBuilder">Options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /// <summary>
        /// Creating models for MainContext
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Records to be created on data migration
            //List<PropertyType> propertyTypes = new List<PropertyType>()
            //{
            //    new PropertyType() { Id = 1, Name = "Clean Air" },
            //    new PropertyType() { Id = 2, Name = "Facility Assessment" },
            //    new PropertyType() { Id = 3, Name = "Due Diligence" }
            //};

            //_ = builder.Entity<PropertyType>().HasData(propertyTypes);

            //builder.Entity<FormQuestion>()
            //    .HasKey(x => new { x.FormId, x.QuestionId });

            base.OnModelCreating(builder);
        }
    }
}
