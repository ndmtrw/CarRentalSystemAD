using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalSystemAD.Web.Controllers;

[Authorize]
public class RentalsController : Controller
{
    private readonly IRentalService rentalService;
    private readonly ICarService carService;

    public RentalsController(
        IRentalService rentalService,
        ICarService carService)
    {
        this.rentalService = rentalService;
        this.carService = carService;
    }

    public async Task<IActionResult> Create(int carId)
    {
        var car = await carService.GetByIdAsync(carId);

        if (car == null)
        {
            return NotFound();
        }

        var model = new RentalFormModel
        {
            CarId = carId,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        await rentalService.RentCarAsync(
            model.CarId,
            userId,
            model.StartDate,
            model.EndDate);

        TempData["SuccessMessage"] = "Rental created successfully.";

        return RedirectToAction("All", "Cars");
    }
}