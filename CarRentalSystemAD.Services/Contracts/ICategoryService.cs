using CarRentalSystemAD.Services.Models;

namespace CarRentalSystemAD.Services.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllAsync();
    Task<CategoryFormModel?> GetByIdAsync(int id);
    Task AddAsync(CategoryFormModel model);
    Task EditAsync(int id, CategoryFormModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
