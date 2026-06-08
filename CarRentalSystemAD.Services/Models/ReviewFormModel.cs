using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Services.Models;

public class ReviewFormModel
{
    public int CarId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 5)]
    public string Content { get; set; } = null!;
}