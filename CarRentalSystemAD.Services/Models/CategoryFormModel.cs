using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Services.Models;

public class CategoryFormModel
{
    [Required]
    [StringLength(60, MinimumLength = 2)]
    public string Name { get; set; } = null!;
}
