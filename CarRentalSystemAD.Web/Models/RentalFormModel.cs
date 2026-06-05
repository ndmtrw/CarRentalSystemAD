using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAD.Web.Models;

public class RentalFormModel
{
    public int CarId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}