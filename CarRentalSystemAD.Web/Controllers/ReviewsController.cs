using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalSystemAD.Web.Controllers;

[Authorize]
public class ReviewsController : Controller
{
    private readonly IReviewService reviewService;
    private readonly ICarService carService;

    public ReviewsController(IReviewService reviewService, ICarService carService)
    {
        this.reviewService = reviewService;
        this.carService = carService;
    }

    public async Task<IActionResult> Add(int carId)
    {
        if (!await carService.ExistsAsync(carId))
        {
            return NotFound();
        }

        return View(new ReviewFormModel { CarId = carId, Rating = 5 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ReviewFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await reviewService.AddAsync(model, userId);
        TempData["SuccessMessage"] = "Review added successfully.";
        return RedirectToAction("Details", "Cars", new { id = model.CarId });
    }
}
