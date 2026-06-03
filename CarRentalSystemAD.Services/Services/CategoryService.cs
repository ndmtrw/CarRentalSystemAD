using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext context;

    public CategoryService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<string>> GetAllNamesAsync()
    {
        return await context.Categories
            .Select(c => c.Name)
            .ToListAsync();
    }
}