using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            
            
        


            modelBuilder.Entity<User>().HasData(
               new User()
               {
                   Id = 1,
                 
                   FirstName = "user",
                   LastName = "user",
                   UserTel="25252525",
                   DateOfBirth = "Null",
                   Password = "user",
                   EmailID="user@gmail.com",
                   ActivationCode = Guid.NewGuid(),
                   Avatar = "avatar.jpg",
                   Admin = true,
                   Username = "user",
                   Gender = "male",



               }
               );

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("UserID");

             
                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.EmailID)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50);

                entity.Property(e => e.UserTel)
                    .IsRequired()
                    .HasColumnName("UserTel")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.Property(e => e.Admin)
                    .IsRequired()
                    .HasColumnName("admin");

            });
        }

       
       
        public DbSet<User> User { get; set; }
        public DbSet<PortfolioItem> portfolioItems { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Abonner> abonnement { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Msg> Messages { get; set; }
        public DbSet<Image> Images { get; set; }



    }
}
