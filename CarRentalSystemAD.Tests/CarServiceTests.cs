using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Models;
using CarRentalSystemAD.Services.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarRentalSystemAD.Tests;

public class CarServiceTests
{
    [Test]
    public async Task AddAsyncShouldAddCar()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);

        context.Categories.Add(new Category { Id = 1, Name = "Economy" });
        await context.SaveChangesAsync();

        var service = new CarService(context);

        var model = new CarFormModel
        {
            Brand = "Toyota",
            Model = "Corolla",
            Year = 2020,
            FuelType = "Petrol",
            Transmission = "Manual",
            Seats = 5,
            PricePerDay = 50,
            ImageUrl = "url",
            CategoryId = 1
        };

        await service.AddAsync(model);

        Assert.That(context.Cars.Count(), Is.EqualTo(1));
    }
}