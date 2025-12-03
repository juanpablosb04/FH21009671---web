using Microsoft.AspNetCore.Mvc;
using Transportation.Interfaces;
using Transportation.Models;

namespace Transportation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // ChatGPT //
    public IActionResult Index([FromServices] IEnumerable<IAirplanes> airplanes)
{   
    using var db = new CarsContext();

    // --- EF: datos de Minnie Mouse ---
    var customer = db.Customers.First(c => c.LastName == "Mouse");
    var ownership = db.CustomerOwnerships.First(o => o.CustomerId == customer.CustomerId);
    var vin = db.CarVins.First(v => v.Vin == ownership.Vin);
    var model = db.Models.First(m => m.ModelId == vin.ModelId);
    var brand = db.Brands.First(b => b.BrandId == model.BrandId);

    ViewData["BrandModel"] = $"{brand.BrandName} - {model.ModelName}";

    var dealer = db.Dealers.First(d => d.DealerId == ownership.DealerId);
    ViewData["Dealer"] = $"{dealer.DealerName} - {dealer.DealerAddress}";

    // --- DI: Airbus y Boeing desde IEnumerable<IAirplanes> ---
    var boeing = airplanes.FirstOrDefault(a => a.GetBrand == "Boeing");
    var airbus = airplanes.FirstOrDefault(a => a.GetBrand == "Airbus");
    
    if (boeing == null || airbus == null)
    return Content("Error: servicios no registrados correctamente");

    ViewData["Airbus"] = $"{airbus.GetBrand}: {string.Join(" - ", airbus.GetModels)}";
    ViewData["Boeing"] = $"{boeing.GetBrand}: {string.Join(" - ", boeing.GetModels)}";

    return View();
}

}
