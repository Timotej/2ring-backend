using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace project
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SinglePositionDuration> PositionsDuration { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new DbContextOptionsBuilder().EnableSensitiveDataLogging(true);

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(30);
            builder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Entity<Employee>().Property(p => p.Address).HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.DateOfBirth).IsRequired();
            builder.Entity<Employee>().Property(p => p.Salary).IsRequired();
            builder.Entity<Employee>().Property(p => p.Archived);
            builder.Entity<Employee>().HasMany(p => p.PositionsDuration).WithOne().HasForeignKey(p => p.EmployeeId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Employee>().HasData
            (
                new Employee { Id = 100, FirstName = "Mark", LastName = "Wahlberg", Address = "Hanacka 20", Salary = 1000, DateOfBirth = DateTime.Now.Date.AddYears(-30), Archived = false },
                new Employee { Id = 101, FirstName = "Jon", LastName = "Snow", Address = "Hanacka 40", Salary = 1500, DateOfBirth = DateTime.Now.Date.AddYears(-40), Archived = false },
                new Employee { Id = 102, FirstName = "Maros", LastName = "Kramar", Address = "Hanacka 60", Salary = 2000, DateOfBirth = DateTime.Now.Date.AddYears(-50), Archived = false },
                new Employee { Id = 103, FirstName = "Julo", LastName = "Trulo", Address = "Cavojskeho 22", Salary = 2500, DateOfBirth = DateTime.Now.Date.AddYears(-26), Archived = true }
            );

            builder.Entity<Position>().ToTable("Positions");
            builder.Entity<Position>().HasKey(p => p.Id);
            builder.Entity<Position>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Position>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Position>().Property(p => p.IsDeleted).IsRequired();

            builder.Entity<Position>().HasData
            (
                new Position { Id = 100, Name = "Tester", IsDeleted = false },
                new Position { Id = 101, Name = "Programmer", IsDeleted = false },
                new Position { Id = 102, Name = "Support", IsDeleted = false },
                new Position { Id = 103, Name = "Analyst", IsDeleted = false },
                new Position { Id = 104, Name = "Dealer", IsDeleted = false }
            );


            builder.Entity<SinglePositionDuration>().ToTable("PositionDurations");
            builder.Entity<SinglePositionDuration>().HasKey(p => p.Id);
            builder.Entity<SinglePositionDuration>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SinglePositionDuration>().Property(p => p.EmployeeId).IsRequired();
            builder.Entity<SinglePositionDuration>().Property(p => p.PositionId).IsRequired();
            builder.Entity<SinglePositionDuration>().Property(p => p.StartDate).IsRequired();
            builder.Entity<SinglePositionDuration>().Property(p => p.EndDate);
            builder.Entity<SinglePositionDuration>().HasData
            (
                new SinglePositionDuration { Id = 100, EmployeeId = 100, PositionId = 100, StartDate = DateTime.Now.Date.AddYears(-5), EndDate = DateTime.Now.Date.AddYears(-4) },
                new SinglePositionDuration { Id = 101, EmployeeId = 100, PositionId = 101, StartDate = DateTime.Now.Date.AddYears(-4), EndDate = DateTime.Now.Date.AddYears(-3) },
                new SinglePositionDuration { Id = 102, EmployeeId = 100, PositionId = 103, StartDate = DateTime.Now.Date.AddYears(-3) },
                new SinglePositionDuration { Id = 103, EmployeeId = 101, PositionId = 104, StartDate = DateTime.Now.Date.AddYears(-3), EndDate = DateTime.Now.Date.AddYears(-2) },
                new SinglePositionDuration { Id = 104, EmployeeId = 101, PositionId = 102, StartDate = DateTime.Now.Date.AddYears(-2), EndDate = DateTime.Now.Date.AddYears(-1) },
                new SinglePositionDuration { Id = 105, EmployeeId = 101, PositionId = 103, StartDate = DateTime.Now.Date.AddYears(-1), },
                new SinglePositionDuration { Id = 106, EmployeeId = 102, PositionId = 104, StartDate = DateTime.Now.Date.AddYears(-1), },
                new SinglePositionDuration { Id = 107, EmployeeId = 103, PositionId = 104, StartDate = DateTime.Now.Date.AddYears(-1), EndDate = DateTime.Now.Date.AddYears(-1) }
            );
        }
    }
}