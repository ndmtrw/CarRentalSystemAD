using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystemAD.Web.Controllers;

public class CarsController : Controller
{
    private readonly ICarService carService;
    private readonly IReviewService reviewService;
    private readonly ICategoryService categoryService;

    public CarsController(ICarService carService, IReviewService reviewService, ICategoryService categoryService)
    {
        this.carService = carService;
        this.reviewService = reviewService;
        this.categoryService = categoryService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> All(string? searchTerm, string? sortBy, int page = 1)
    {
        var model = await carService.GetPagedAsync(searchTerm, sortBy, page, 6);
        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var car = await carService.GetByIdAsync(id);

        if (car == null)
        {
            return NotFound();
        }

        ViewBag.Reviews = await reviewService.GetByCarIdAsync(id);
        return View(car);
    }

    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Add()
    {
        ViewBag.Categories = await categoryService.GetAllAsync();
        return View(new CarFormModel());
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(CarFormModel model)
    {
        if (!await categoryService.ExistsAsync(model.CategoryId))
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Please select a valid category.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
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

        ViewBag.Categories = await categoryService.GetAllAsync();
        return View(car);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CarFormModel model)
    {
        if (!await categoryService.ExistsAsync(model.CategoryId))
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Please select a valid category.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            return View(model);
        }

        if (!await carService.ExistsAsync(id))
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
        if (!await carService.ExistsAsync(id))
        {
            return NotFound();
        }

        await carService.DeleteAsync(id);
        TempData["SuccessMessage"] = "Car deleted successfully.";
        return RedirectToAction(nameof(All));
    }
}
