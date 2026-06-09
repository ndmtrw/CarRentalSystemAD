namespace CarRentalSystemAD.Services.Models;

public class ReviewViewModel
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}
