namespace CarRentalSystemAD.Services.Models;

public class CarViewModel
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public decimal PricePerDay { get; set; }

    public string ImageUrl { get; set; } = null!;
}