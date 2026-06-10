using CarRentalSystemAD.Services.Contracts;
using CarRentalSystemAD.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystemAD.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class CategoriesController : Controller
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    public async Task<IActionResult> All()
    {
        return View(await categoryService.GetAllAsync());
    }

    public IActionResult Add()
    {
        return View(new CategoryFormModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(CategoryFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await categoryService.AddAsync(model);
        TempData["SuccessMessage"] = "Category added successfully.";
        return RedirectToAction(nameof(All));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!await categoryService.ExistsAsync(id))
        {
            return NotFound();
        }

        await categoryService.EditAsync(id, model);
        TempData["SuccessMessage"] = "Category edited successfully.";
        return RedirectToAction(nameof(All));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await categoryService.ExistsAsync(id))
        {
            return NotFound();
        }

        if (await categoryService.DeleteAsync(id))
        {
            TempData["SuccessMessage"] = "Category deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "This category cannot be deleted because it is used by existing cars.";
        }

        return RedirectToAction(nameof(All));
    }
}
