using BugFixer.Domain.Entities.Account;
using BugFixer.Domain.Entities.SiteSetting;
using Microsoft.EntityFrameworkCore;

namespace BugFixer.Data.Context
{
    public class BugFixerDbContext : DbContext
    {
        #region Ctor

        public BugFixerDbContext(DbContextOptions<BugFixerDbContext> options) : base(options)
        {
            
        }

        #endregion

        #region DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data

            var date = DateTime.MinValue;
            modelBuilder.Entity<EmailSetting>().HasData(new EmailSetting()
            {
                CreateDate = date,
                DisplayName = "BugFixer",
                EnableSSL = true,
                From = "aspaad.mkh@gmail.com",
                Id = 1,
                IsDefault = true,
                IsDeleted = false,
                Password = "1234HuneR",
                Port = 587,
                SMTP = "smtp.gmail.com"
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
