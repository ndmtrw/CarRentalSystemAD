using CarRentalSystemAD.Services.Models;

namespace CarRentalSystemAD.Services.Contracts;

public interface IStatsService
{
    Task<StatsViewModel> GetStatsAsync();
}
