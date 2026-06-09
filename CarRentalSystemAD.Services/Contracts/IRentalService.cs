using CarRentalSystemAD.Services.Models;

namespace CarRentalSystemAD.Services.Contracts;

public interface IRentalService
{
    Task RentCarAsync(int carId, string userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<RentalViewModel>> GetByUserIdAsync(string userId);
    Task<IEnumerable<RentalViewModel>> GetAllAsync();
    Task<bool> IsValidDateRangeAsync(DateTime startDate, DateTime endDate);
}
