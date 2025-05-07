using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;

using System.Text.Json;
using System.Xml;


namespace ScoringSystem_web_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BaseCondition> ConditionStrategies { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseCondition>()
            .Property(u => u.Properties)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, (JsonSerializerOptions)null),
                new ValueComparer<Dictionary<string, object>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToDictionary(entry => entry.Key, entry => entry.Value)
                )
            )
            .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<BaseCondition>()
                .HasDiscriminator<string>("ConditionType")
                .HasValue<AgeCondition>("AgeCondition")
                .HasValue<SalaryCondition>("SalaryCondition")
                .HasValue<TotalLoansCondition>("TotalLoansCondition");
        }



    }
}
