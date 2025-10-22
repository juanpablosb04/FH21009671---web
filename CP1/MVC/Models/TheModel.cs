namespace MVC.Models;
using System.ComponentModel.DataAnnotations;

public class TheModel
{
    [Required(ErrorMessage = "Poner una frase es obligatorio.")]
    [MinLength(5, ErrorMessage = "La longitud de la frase debe ser de 5 a 25 caracteres.")]
    [MaxLength(25, ErrorMessage = "La longitud de la frase debe ser de 5 a 25 caracteres.")]

    //ChatGPT
    public string Phrase { get; set; } = string.Empty;

    public Dictionary<char, int> Counts { get; set; } = [];

    public string Lower { get; set; } = string.Empty;

    public string Upper { get; set; } = string.Empty;
}
