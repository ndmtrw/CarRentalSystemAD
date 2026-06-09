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
            new Category { Id = 3, Name = "Luxury" },
            new Category { Id = 4, Name = "Van" }
        );

        builder.Entity<Car>().HasData(
            new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2022, FuelType = "Petrol", Transmission = "Automatic", Seats = 5, PricePerDay = 70, ImageUrl = "https://images.unsplash.com/photo-1549399542-7e3f8b79c341", CategoryId = 1, IsAvailable = true, IsDeleted = false },
            new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2023, FuelType = "Diesel", Transmission = "Automatic", Seats = 5, PricePerDay = 180, ImageUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e", CategoryId = 2, IsAvailable = true, IsDeleted = false },
            new Car { Id = 3, Brand = "Mercedes", Model = "S Class", Year = 2024, FuelType = "Hybrid", Transmission = "Automatic", Seats = 5, PricePerDay = 260, ImageUrl = "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8", CategoryId = 3, IsAvailable = true, IsDeleted = false },
            new Car { Id = 4, Brand = "Volkswagen", Model = "Touran", Year = 2021, FuelType = "Diesel", Transmission = "Manual", Seats = 7, PricePerDay = 95, ImageUrl = "https://images.unsplash.com/photo-1492144534655-ae79c964c9d7", CategoryId = 4, IsAvailable = true, IsDeleted = false }
        );
    }
}
