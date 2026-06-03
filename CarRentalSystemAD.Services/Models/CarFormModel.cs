using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Services.Models;

public class CarFormModel
{
    [Required]
    public string Brand { get; set; } = null!;

    [Required]
    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string FuelType { get; set; } = null!;

    public string Transmission { get; set; } = null!;

    public int Seats { get; set; }

    public decimal PricePerDay { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int CategoryId { get; set; }
}