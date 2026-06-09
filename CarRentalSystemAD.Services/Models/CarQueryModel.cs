namespace CarRentalSystemAD.Services.Models;

public class CarQueryModel
{
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public IEnumerable<CarViewModel> Cars { get; set; } = new List<CarViewModel>();
}
