using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalSystemAD.Web.Controllers;

[Authorize]
public class RentalsController : Controller
{
    private readonly IRentalService rentalService;
    private readonly ICarService carService;

    public RentalsController(IRentalService rentalService, ICarService carService)
    {
        this.rentalService = rentalService;
        this.carService = carService;
    }

    public async Task<IActionResult> Create(int carId)
    {
        if (!await carService.ExistsAsync(carId))
        {
            return NotFound();
        }

        return View(new RentalFormModel
        {
            CarId = carId,
            StartDate = DateTime.Today.AddDays(1),
            EndDate = DateTime.Today.AddDays(2)
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalFormModel model)
    {
        if (!await carService.ExistsAsync(model.CarId))
        {
            return NotFound();
        }

        if (!await rentalService.IsValidDateRangeAsync(model.StartDate, model.EndDate))
        {
            ModelState.AddModelError(string.Empty, "End date must be after start date and start date cannot be in the past.");
        }
        else if (!await rentalService.IsCarAvailableAsync(model.CarId, model.StartDate, model.EndDate))
        {
            ModelState.AddModelError(string.Empty, "This car is already rented for the selected period. Please choose different dates.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await rentalService.RentCarAsync(model.CarId, userId, model.StartDate, model.EndDate);
        TempData["SuccessMessage"] = "Rental created successfully.";
        return RedirectToAction(nameof(Mine));
    }

    public async Task<IActionResult> Mine()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var rentals = await rentalService.GetByUserIdAsync(userId);
        return View(rentals);
    }
}
