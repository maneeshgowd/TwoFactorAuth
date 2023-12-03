using Microsoft.EntityFrameworkCore;
using TwoFactorAuth.DataModels;

namespace TwoFactorAuth.DBContext
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<RegisterModel> Users => Set<RegisterModel>();
        public DbSet<OtpModel> UsersOtp => Set<OtpModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterModel>().HasKey(k => k.Email);
        }
    }
}
