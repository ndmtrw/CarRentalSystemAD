using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Services.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext context;

    public CarService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<CarViewModel>> GetAllAsync()
    {
        return await context.Cars
            .Where(c => !c.IsDeleted)
            .Select(c => new CarViewModel
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                PricePerDay = c.PricePerDay,
                ImageUrl = c.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<CarViewModel?> GetByIdAsync(int id)
    {
        return await context.Cars
            .Where(c => c.Id == id)
            .Select(c => new CarViewModel
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                PricePerDay = c.PricePerDay,
                ImageUrl = c.ImageUrl
            })
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(CarFormModel model)
    {
        var car = new Car
        {
            Brand = model.Brand,
            Model = model.Model,
            Year = model.Year,
            FuelType = model.FuelType,
            Transmission = model.Transmission,
            Seats = model.Seats,
            PricePerDay = model.PricePerDay,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId
        };

        await context.Cars.AddAsync(car);
        await context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, CarFormModel model)
    {
        var car = await context.Cars.FindAsync(id);

        if (car == null)
        {
            return;
        }

        car.Brand = model.Brand;
        car.Model = model.Model;
        car.Year = model.Year;
        car.FuelType = model.FuelType;
        car.Transmission = model.Transmission;
        car.Seats = model.Seats;
        car.PricePerDay = model.PricePerDay;
        car.ImageUrl = model.ImageUrl;
        car.CategoryId = model.CategoryId;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var car = await context.Cars.FindAsync(id);

        if (car == null)
        {
            return;
        }

        car.IsDeleted = true;

        await context.SaveChangesAsync();
    }
}