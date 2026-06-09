using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Data.Models;

public class Review
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public Car Car { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(500)]
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
