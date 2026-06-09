using CarRentalSystemAD.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystemAD.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class RentalsController : Controller
{
    private readonly IRentalService rentalService;

    public RentalsController(IRentalService rentalService)
    {
        this.rentalService = rentalService;
    }

    public async Task<IActionResult> All()
    {
        return View(await rentalService.GetAllAsync());
    }
}
