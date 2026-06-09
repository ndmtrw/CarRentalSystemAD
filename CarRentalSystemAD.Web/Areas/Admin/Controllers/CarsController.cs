using CarRentalSystemAD.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystemAD.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class CarsController : Controller
{
    private readonly ICarService carService;

    public CarsController(ICarService carService)
    {
        this.carService = carService;
    }

    public async Task<IActionResult> All()
    {
        return View(await carService.GetAllAsync());
    }
}
