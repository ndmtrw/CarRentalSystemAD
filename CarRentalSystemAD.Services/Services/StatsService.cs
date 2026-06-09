using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Services.Services;

public class StatsService : IStatsService
{
    private readonly ApplicationDbContext context;

    public StatsService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<StatsViewModel> GetStatsAsync()
    {
        return new StatsViewModel
        {
            CarsCount = await context.Cars.CountAsync(c => !c.IsDeleted),
            RentalsCount = await context.Rentals.CountAsync(),
            CategoriesCount = await context.Categories.CountAsync(),
            ReviewsCount = await context.Reviews.CountAsync()
        };
    }
}
