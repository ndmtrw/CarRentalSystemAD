using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalSystemAD.Data.Models;

public class Rental
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public Car Car { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Pending";
}