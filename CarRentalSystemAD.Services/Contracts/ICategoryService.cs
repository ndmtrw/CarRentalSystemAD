namespace CarRentalSystemAD.Services.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<string>> GetAllNamesAsync();
}