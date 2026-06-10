using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CarRentalSystemAD.Tests;

public class RentalServiceTests
{
    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private static async Task<ApplicationDbContext> CreateContextWithCarAsync()
    {
        var context = CreateContext();
        context.Categories.Add(new Category { Id = 1, Name = "Economy" });
        context.Cars.Add(new Car { Id = 1, Brand = "Toyota", Model = "Yaris", Year = 2020, FuelType = "Petrol", Transmission = "Manual", Seats = 5, PricePerDay = 50, ImageUrl = "url", CategoryId = 1 });
        await context.SaveChangesAsync();
        return context;
    }

    [Test]
    public async Task IsCarAvailableAsyncReturnsTrueWhenNoRentals()
    {
        using var context = await CreateContextWithCarAsync();
        var service = new RentalService(context);

        var available = await service.IsCarAvailableAsync(1, DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));

        Assert.That(available, Is.True);
    }

    [Test]
    public async Task IsCarAvailableAsyncReturnsFalseWhenDatesOverlap()
    {
        using var context = await CreateContextWithCarAsync();
        var service = new RentalService(context);

        await service.RentCarAsync(1, "user1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(5));

        var available = await service.IsCarAvailableAsync(1, DateTime.Today.AddDays(3), DateTime.Today.AddDays(7));

        Assert.That(available, Is.False);
    }

    [Test]
    public async Task IsCarAvailableAsyncReturnsTrueForAdjacentPeriod()
    {
        using var context = await CreateContextWithCarAsync();
        var service = new RentalService(context);

        await service.RentCarAsync(1, "user1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));

        var available = await service.IsCarAvailableAsync(1, DateTime.Today.AddDays(3), DateTime.Today.AddDays(5));

        Assert.That(available, Is.True);
    }

    [Test]
    public async Task RentCarAsyncDoesNotCreateOverlappingRental()
    {
        using var context = await CreateContextWithCarAsync();
        var service = new RentalService(context);

        await service.RentCarAsync(1, "user1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(5));
        await service.RentCarAsync(1, "user2", DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));

        Assert.That(context.Rentals.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task RentCarAsyncCalculatesTotalPrice()
    {
        using var context = await CreateContextWithCarAsync();
        var service = new RentalService(context);

        await service.RentCarAsync(1, "user1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(4));

        var rental = context.Rentals.Single();
        Assert.That(rental.TotalPrice, Is.EqualTo(150));
    }

    [Test]
    public async Task IsValidDateRangeAsyncRejectsPastStartDate()
    {
        using var context = CreateContext();
        var service = new RentalService(context);

        var valid = await service.IsValidDateRangeAsync(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(2));

        Assert.That(valid, Is.False);
    }

    [Test]
    public async Task IsValidDateRangeAsyncRejectsEndBeforeStart()
    {
        using var context = CreateContext();
        var service = new RentalService(context);

        var valid = await service.IsValidDateRangeAsync(DateTime.Today.AddDays(3), DateTime.Today.AddDays(1));

        Assert.That(valid, Is.False);
    }
}
