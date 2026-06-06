using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Contracts;

namespace CarRentalSystemAD.Services.Services;

public class RentalService : IRentalService
{
    private readonly ApplicationDbContext context;

    public RentalService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task RentCarAsync(
    int carId,
    string userId,
    DateTime startDate,
    DateTime endDate)
    {
        var car = await context.Cars.FindAsync(carId);

        if (car == null)
        {
            return;
        }

        var totalDays = (endDate - startDate).Days;

        if (totalDays <= 0)
        {
            totalDays = 1;
        }

        var rental = new Rental
        {
            CarId = carId,
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            TotalPrice = totalDays * car.PricePerDay,
            Status = "Active"
        };

        await context.Rentals.AddAsync(rental);

        await context.SaveChangesAsync();
    }
}