using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext context;

    public CategoryService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        return await context.Categories
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<CategoryFormModel?> GetByIdAsync(int id)
    {
        return await context.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryFormModel
            {
                Name = c.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(CategoryFormModel model)
    {
        var category = new Category
        {
            Name = model.Name
        };

        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, CategoryFormModel model)
    {
        var category = await context.Categories.FindAsync(id);

        if (category == null)
        {
            return;
        }

        category.Name = model.Name;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category == null)
        {
            return;
        }

        context.Categories.Remove(category);

        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Categories.AnyAsync(c => c.Id == id);
    }
}