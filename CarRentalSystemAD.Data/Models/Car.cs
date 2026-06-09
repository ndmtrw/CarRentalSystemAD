using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalSystemAD.Data.Models;

public class Car
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Brand { get; set; } = null!;

    [Required]
    [StringLength(80)]
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

    [Column(TypeName = "decimal(18,2)")]
    [Range(1, 10000)]
    public decimal PricePerDay { get; set; }

    [Required]
    [StringLength(500)]
    public string ImageUrl { get; set; } = null!;

    public bool IsAvailable { get; set; } = true;

    public bool IsDeleted { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
