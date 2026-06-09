using CarRentalSystemAD.Services.Models;

namespace CarRentalSystemAD.Services.Contracts;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetAllAsync(string? searchTerm = null, string? sortBy = null);
    Task<CarQueryModel> GetPagedAsync(string? searchTerm, string? sortBy, int page, int pageSize);
    Task<CarViewModel?> GetByIdAsync(int id);
    Task<CarFormModel?> GetFormModelByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(CarFormModel model);
    Task EditAsync(int id, CarFormModel model);
    Task DeleteAsync(int id);
}
