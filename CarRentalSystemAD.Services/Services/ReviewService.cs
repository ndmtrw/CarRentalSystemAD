using CarRentalSystemAD.Data.Data;
using CarRentalSystemAD.Data.Models;
using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAD.Services.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext context;

    public ReviewService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<ReviewViewModel>> GetByCarIdAsync(int carId)
    {
        return await context.Reviews
            .Where(r => r.CarId == carId)
            .OrderByDescending(r => r.CreatedOn)
            .Select(r => new ReviewViewModel
            {
                Id = r.Id,
                Rating = r.Rating,
                Content = r.Content,
                UserName = r.User.UserName!,
                CreatedOn = r.CreatedOn
            })
            .ToListAsync();
    }

    public async Task AddAsync(ReviewFormModel model, string userId)
    {
        var review = new Review
        {
            CarId = model.CarId,
            UserId = userId,
            Rating = model.Rating,
            Content = model.Content,
            CreatedOn = DateTime.UtcNow
        };

        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
    }
}