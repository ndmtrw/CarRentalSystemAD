using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Services.Services;

public class RentalService : IRentalService
{
    private readonly ApplicationDbContext context;

    public RentalService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task RentCarAsync(int carId, string userId, DateTime startDate, DateTime endDate)
    {
        var car = await context.Cars.FindAsync(carId);

        if (car == null
            || car.IsDeleted
            || !await IsValidDateRangeAsync(startDate, endDate)
            || !await IsCarAvailableAsync(carId, startDate, endDate))
        {
            return;
        }

        var totalDays = Math.Max(1, (endDate.Date - startDate.Date).Days);
        var rental = new Rental
        {
            CarId = carId,
            UserId = userId,
            StartDate = startDate.Date,
            EndDate = endDate.Date,
            TotalPrice = totalDays * car.PricePerDay,
            Status = "Active"
        };

        await context.Rentals.AddAsync(rental);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RentalViewModel>> GetByUserIdAsync(string userId)
    {
        return await context.Rentals
            .Include(r => r.Car)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.StartDate)
            .Select(r => new RentalViewModel
            {
                Id = r.Id,
                CarName = r.Car.Brand + " " + r.Car.Model,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                TotalPrice = r.TotalPrice,
                Status = r.Status
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<RentalViewModel>> GetAllAsync()
    {
        return await context.Rentals
            .Include(r => r.Car)
            .OrderByDescending(r => r.StartDate)
            .Select(r => new RentalViewModel
            {
                Id = r.Id,
                CarName = r.Car.Brand + " " + r.Car.Model,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                TotalPrice = r.TotalPrice,
                Status = r.Status
            })
            .ToListAsync();
    }

    public Task<bool> IsValidDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return Task.FromResult(startDate.Date >= DateTime.Today && endDate.Date > startDate.Date);
    }

    public async Task<bool> IsCarAvailableAsync(int carId, DateTime startDate, DateTime endDate)
    {
        return !await context.Rentals.AnyAsync(r =>
            r.CarId == carId
            && r.Status == "Active"
            && r.StartDate < endDate.Date
            && startDate.Date < r.EndDate);
    }
}
