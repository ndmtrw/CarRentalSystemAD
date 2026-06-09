using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Services.Models;

public class CarFormModel
{
    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Brand { get; set; } = null!;

    [Required]
    [StringLength(80, MinimumLength = 1)]
    public string Model { get; set; } = null!;

    [Range(1990, 2035)]
    public int Year { get; set; }

    [Required]
    [StringLength(30)]
    public string FuelType { get; set; } = null!;

    [Required]
    [StringLength(30)]
    public string Transmission { get; set; } = null!;

    [Range(1, 20)]
    public int Seats { get; set; }

    [Range(1, 10000)]
    public decimal PricePerDay { get; set; }

    [Required]
    [StringLength(500)]
    public string ImageUrl { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
