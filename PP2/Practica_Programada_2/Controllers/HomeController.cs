using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Practica_Programada_2.Models;

namespace Practica_Programada_2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


    private string BinaryAND(string a, string b)
    {
        int length = Math.Max(a.Length, b.Length);
        a = a.PadLeft(length, '0');
        b = b.PadLeft(length, '0');

        char[] result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (a[i] == '1' && b[i] == '1') ? '1' : '0';
        }
        return new string(result);
    }

    private string BinaryOR(string a, string b)
    {
        int length = Math.Max(a.Length, b.Length);
        a = a.PadLeft(length, '0');
        b = b.PadLeft(length, '0');

        char[] result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (a[i] == '1' || b[i] == '1') ? '1' : '0';
        }
        return new string(result);
    }

    private string BinaryXOR(string a, string b)
    {
        int length = Math.Max(a.Length, b.Length);
        a = a.PadLeft(length, '0');
        b = b.PadLeft(length, '0');

        char[] result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (a[i] != b[i]) ? '1' : '0';
        }
        return new string(result);
    }










    [HttpPost]
    public IActionResult Index(BinaryInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Convertir a decimal
        int decA = Convert.ToInt32(model.A!, 2);
        int decB = Convert.ToInt32(model.B!, 2);

        // Enviar a la vista en ViewBag
        ViewBag.A_Bin = model.A!.PadLeft(8, '0');
        ViewBag.A_Oct = Convert.ToString(decA, 8);
        ViewBag.A_Dec = decA;
        ViewBag.A_Hex = Convert.ToString(decA, 16).ToUpper();

        ViewBag.B_Bin = model.B!.PadLeft(8, '0');
        ViewBag.B_Oct = Convert.ToString(decB, 8);
        ViewBag.B_Dec = decB;
        ViewBag.B_Hex = Convert.ToString(decB, 16).ToUpper();

        // Operaciones binarias
        string andResult = BinaryAND(model.A!, model.B!);
        string orResult = BinaryOR(model.A!, model.B!);
        string xorResult = BinaryXOR(model.A!, model.B!);

        int decAND = Convert.ToInt32(andResult, 2);
        int decOR = Convert.ToInt32(orResult, 2);
        int decXOR = Convert.ToInt32(xorResult, 2);

        ViewBag.AND_Bin = andResult.PadLeft(8, '0');
        ViewBag.AND_Oct = Convert.ToString(decAND, 8);
        ViewBag.AND_Dec = decAND;
        ViewBag.AND_Hex = Convert.ToString(decAND, 16).ToUpper();

        ViewBag.OR_Bin = orResult.PadLeft(8, '0');
        ViewBag.OR_Oct = Convert.ToString(decOR, 8);
        ViewBag.OR_Dec = decOR;
        ViewBag.OR_Hex = Convert.ToString(decOR, 16).ToUpper();

        ViewBag.XOR_Bin = xorResult.PadLeft(8, '0');
        ViewBag.XOR_Oct = Convert.ToString(decXOR, 8);
        ViewBag.XOR_Dec = decXOR;
        ViewBag.XOR_Hex = Convert.ToString(decXOR, 16).ToUpper();

        // OPERACIONES ARITMÉTICAS

        int sum = decA + decB;
        int mult = decA * decB;

        ViewBag.SUM_Bin = Convert.ToString(sum, 2).PadLeft(8, '0');
        ViewBag.SUM_Oct = Convert.ToString(sum, 8);
        ViewBag.SUM_Dec = sum;
        ViewBag.SUM_Hex = Convert.ToString(sum, 16).ToUpper();

        ViewBag.MUL_Bin = Convert.ToString(mult, 2).PadLeft(8, '0');
        ViewBag.MUL_Oct = Convert.ToString(mult, 8);
        ViewBag.MUL_Dec = mult;
        ViewBag.MUL_Hex = Convert.ToString(mult, 16).ToUpper();

        return View(model);
        
    }
}
