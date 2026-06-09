using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarRentalSystemAD.Tests;

public class StatsServiceTests
{
    [Test]
    public async Task GetStatsAsyncReturnsCorrectCounts()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.Categories.Add(new Category { Id = 1, Name = "Economy" });
        context.Cars.Add(new Car { Id = 1, Brand = "Toyota", Model = "Yaris", Year = 2020, FuelType = "Petrol", Transmission = "Manual", Seats = 5, PricePerDay = 50, ImageUrl = "url", CategoryId = 1 });
        await context.SaveChangesAsync();

        var service = new StatsService(context);
        var stats = await service.GetStatsAsync();

        Assert.That(stats.CarsCount, Is.EqualTo(1));
        Assert.That(stats.CategoriesCount, Is.EqualTo(1));
    }
}
