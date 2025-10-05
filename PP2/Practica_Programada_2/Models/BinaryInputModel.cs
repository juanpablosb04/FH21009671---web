using System.ComponentModel.DataAnnotations;

namespace Practica_Programada_2.Models
{
    public class BinaryInputModel
    {
        [Required(ErrorMessage = "El valor de a es obligatorio.")]
        [RegularExpression("^[01]+$", ErrorMessage = "El valor de a solo puede contener 0 y 1.")]
        [MaxLength(8, ErrorMessage = "El valor de a no puede tener más de 8 caracteres.")]
        [MinLength(2, ErrorMessage = "El valor de a debe tener al menos 2 caracteres.")]
        [MultipleOfTwoLength(ErrorMessage = "La longitud de a debe ser múltiplo de 2 (2, 4, 6 u 8).")]
        public string? A { get; set; }

        [Required(ErrorMessage = "El valor de b es obligatorio.")]
        [RegularExpression("^[01]+$", ErrorMessage = "El valor de b solo puede contener 0 y 1.")]
        [MaxLength(8, ErrorMessage = "El valor de b no puede tener más de 8 caracteres.")]
        [MinLength(2, ErrorMessage = "El valor de b debe tener al menos 2 caracteres.")]
        [MultipleOfTwoLength(ErrorMessage = "La longitud de b debe ser múltiplo de 2 (2, 4, 6 u 8).")]
        public string? B { get; set; }
    }

    // Validador personalizado
    public class MultipleOfTwoLengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string str)
            {
                return str.Length % 2 == 0;
            }
            return false;
        }
    }
}
