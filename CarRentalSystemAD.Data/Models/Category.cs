using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Data.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string Name { get; set; } = null!;

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}