using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Models;
using CarRentalSystemAD.Services.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarRentalSystemAD.Tests;

public class CarServiceTests
{
    private ApplicationDbContext context = null!;
    private CarService service = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new ApplicationDbContext(options);
        context.Categories.Add(new Category { Id = 10, Name = "Test" });
        context.Cars.Add(new Car { Id = 10, Brand = "Audi", Model = "A4", Year = 2020, FuelType = "Petrol", Transmission = "Automatic", Seats = 5, PricePerDay = 100, ImageUrl = "url", CategoryId = 10 });
        context.SaveChanges();
        service = new CarService(context);
    }

    [Test]
    public async Task ExistsAsyncReturnsTrueForExistingCar()
    {
        Assert.That(await service.ExistsAsync(10), Is.True);
    }

    [Test]
    public async Task AddAsyncAddsCar()
    {
        await service.AddAsync(new CarFormModel { Brand = "BMW", Model = "M3", Year = 2021, FuelType = "Petrol", Transmission = "Manual", Seats = 4, PricePerDay = 200, ImageUrl = "url", CategoryId = 10 });
        Assert.That(await context.Cars.CountAsync(), Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteAsyncSoftDeletesCar()
    {
        await service.DeleteAsync(10);
        Assert.That(await service.ExistsAsync(10), Is.False);
    }
}
