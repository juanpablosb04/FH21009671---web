using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        ViewBag.Valid = ModelState.IsValid;

            // ChatGPT
        if (ModelState.IsValid)
    {
        var chars = model.Phrase.Where(c => c != ' ');

        model.Counts = chars
            .GroupBy(c => c)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

        model.Lower = new string(chars.Select(c => char.ToLower(c)).ToArray());
        model.Upper = new string(chars.Select(c => char.ToUpper(c)).ToArray());
    }

    return View(model);
    }
}
