namespace CarRentalSystemAD.Services.Contracts;

public interface IRentalService
{
    Task RentCarAsync(int carId, string userId, DateTime startDate, DateTime endDate);
}