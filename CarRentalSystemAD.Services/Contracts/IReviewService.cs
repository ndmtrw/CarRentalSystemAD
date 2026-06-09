using CarRentalSystemAD.Services.Models;

namespace CarRentalSystemAD.Services.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewViewModel>> GetByCarIdAsync(int carId);
    Task AddAsync(ReviewFormModel model, string userId);
}
