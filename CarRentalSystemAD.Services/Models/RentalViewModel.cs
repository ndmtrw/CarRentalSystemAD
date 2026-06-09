namespace CarRentalSystemAD.Services.Models;

public class RentalViewModel
{
    public int Id { get; set; }
    public string CarName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = null!;
}
