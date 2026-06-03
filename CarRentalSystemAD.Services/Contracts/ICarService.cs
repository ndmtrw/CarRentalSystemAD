using CarRentalSystemAD.Services.Models;

namespace CarRentalSystemAD.Services.Contracts;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetAllAsync();

    Task<CarViewModel?> GetByIdAsync(int id);

    Task AddAsync(CarFormModel model);

    Task EditAsync(int id, CarFormModel model);

    Task DeleteAsync(int id);
}