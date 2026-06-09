namespace CarRentalSystemAD.Services.Models;

public class CarViewModel
{
    public int Id { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public string FuelType { get; set; } = null!;
    public string Transmission { get; set; } = null!;
    public int Seats { get; set; }
    public decimal PricePerDay { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}
