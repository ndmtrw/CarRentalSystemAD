using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystemAD.Web.Controllers;

public class CarsController : Controller
{
    private readonly ICarService carService;

    public CarsController(ICarService carService)
    {
        this.carService = carService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        var cars = await carService.GetAllAsync();

        return View(cars);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var car = await carService.GetByIdAsync(id);

        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }

    [Authorize(Roles = "Administrator")]
    public IActionResult Add()
    {
        return View(new CarFormModel());
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(CarFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await carService.AddAsync(model);

        TempData["SuccessMessage"] = "Car added successfully.";

        return RedirectToAction(nameof(All));
    }

    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Edit(int id)
    {
        var car = await carService.GetFormModelByIdAsync(id);

        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CarFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var exists = await carService.ExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        await carService.EditAsync(id, model);

        TempData["SuccessMessage"] = "Car edited successfully.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var exists = await carService.ExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        await carService.DeleteAsync(id);

        TempData["SuccessMessage"] = "Car deleted successfully.";

        return RedirectToAction(nameof(All));
    }
}