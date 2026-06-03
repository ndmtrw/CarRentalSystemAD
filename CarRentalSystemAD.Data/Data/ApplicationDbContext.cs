using CarRentalSystemAD.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Data.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Rental> Rentals { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Economy" },
            new Category { Id = 2, Name = "SUV" },
            new Category { Id = 3, Name = "Luxury" }
        );

        builder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2022,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Seats = 5,
                PricePerDay = 70,
                ImageUrl = "https://images.unsplash.com/photo-1549399542-7e3f8b79c341",
                CategoryId = 1,
                IsAvailable = true,
                IsDeleted = false
            },
            new Car
            {
                Id = 2,
                Brand = "BMW",
                Model = "X5",
                Year = 2023,
                FuelType = "Diesel",
                Transmission = "Automatic",
                Seats = 5,
                PricePerDay = 180,
                ImageUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e",
                CategoryId = 2,
                IsAvailable = true,
                IsDeleted = false
            }
        );
    }
}