# Practica Programada 2

## Juan Pablo Solís Benamburg - Carnet: FH21009671

### Comandos utilizados:
```bash
dotnet new sln -n PP2
new mvc -o Practica_Programada_2 -f net8.0
dotnet sln add Practica_Programada_2/
ni README.md
```
### Prompts
- Realizar validaciones personalizadas con .NET MVC:
```csharp
using System.ComponentModel.DataAnnotations;

namespace TuProyecto.Models
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
```

- Necesito que a la siguiente tabla: 

```html
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Formulario Binario</title>
</head>
<body>
    <h1>Formulario Web (Vista por defecto)</h1>
    <form action="/submit" method="post">
        <div>
            <label for="a">a</label>
            <input type="text" id="a" name="a">
        </div>
        <div>
            <label for="b">b</label>
            <input type="text" id="b" name="b">
        </div>
        <div>
            <button type="submit">Enviar</button>
        </div>
    </form>
</body>
</html>
```
Le coloques estilos, algo basico y moderno, tambien necesito que crees como una tabla con esta estructura para ahorrar algo de tiempo: 

| Operación | Binario   | Octal | Decimal | Hexadecimal |
|-----------|-----------|-------|---------|-------------|
| a         | 00001010  | 12    | 10      | A           |
| b         | 00000011  | 3     | 3       | 3           |
| a AND b   | 10        | 2     | 2       | 2           |
| a OR b    | 1011      | 13    | 11      | B           |
| a XOR b   | 1001      | 11    | 9       | 9           |
| a + b     | 1101      | 15    | 13      | D           |
| a • b     | 11110     | 36    | 30      | 1E          |


```html
<style>
    body {
      font-family: Arial, sans-serif;
      background-color: #f0f4f8;
      display: flex;
      justify-content: center;
      align-items: flex-start;
      min-height: 100vh;
      padding: 40px;
    }

    .container {
      background-color: #d6eaff;
      border-radius: 12px;
      padding: 30px;
      width: 500px;
      box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    }

    h1 {
      text-align: center;
      color: #003366;
    }

    form div {
      margin-bottom: 15px;
    }

    label {
      display: block;
      font-weight: bold;
      color: #002244;
      margin-bottom: 5px;
    }

    input[type="text"] {
      width: 100%;
      padding: 10px;
      border: 1px solid #99ccee;
      border-radius: 8px;
      font-size: 1rem;
    }

    button {
      background-color: #0066cc;
      color: #fff;
      font-size: 1rem;
      padding: 12px 20px;
      border: none;
      border-radius: 8px;
      cursor: pointer;
      width: 100%;
      transition: background 0.3s;
    }

    button:hover {
      background-color: #004c99;
    }

    table {
      margin-top: 30px;
      width: 100%;
      border-collapse: collapse;
      text-align: center;
    }

    table th, table td {
      border: 1px solid #004c99;
      padding: 8px;
    }

    table th {
      background-color: #0066cc;
      color: white;
    }

    table tr:nth-child(even) {
      background-color: #e6f2ff;
    }
  </style>


<table>
            <thead>
                <tr>
                    <th>Operación</th>
                    <th>Bin</th>
                    <th>Oct</th>
                    <th>Dec</th>
                    <th>Hex</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>a</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>b</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>a AND b</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>a OR b</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>a XOR b</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>a + b</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>a * b</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
```

- Ayudame con la logica para hallar el valor Binario Oct Dec Hex de a y b:

```csharp
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

    return View(model);
```
Cambios necesarios en el HTML:

```html
<tr>
    <td>a</td>
    <td>@ViewBag.A_Bin</td>
    <td>@ViewBag.A_Oct</td>
    <td>@ViewBag.A_Dec</td>
    <td>@ViewBag.A_Hex</td>
</tr>
    <tr>
    <td>b</td>
    <td>@ViewBag.B_Bin</td>
    <td>@ViewBag.B_Oct</td>
    <td>@ViewBag.B_Dec</td>
    <td>@ViewBag.B_Hex</td>
</tr>
```
- implementar la logica para realizar a AND b - a OR b - a XOR b

Crear métodos auxiliares en el HomeController (o como métodos privados)

```csharp
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
```
Integrar en el HttpPost Index

```csharp
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
```


```html
<tr>
    <td>a AND b</td>
    <td>@ViewBag.AND_Bin</td>
    <td>@ViewBag.AND_Oct</td>
    <td>@ViewBag.AND_Dec</td>
    <td>@ViewBag.AND_Hex</td>
</tr>
<tr>
    <td>a OR b</td>
    <td>@ViewBag.OR_Bin</td>
    <td>@ViewBag.OR_Oct</td>
    <td>@ViewBag.OR_Dec</td>
    <td>@ViewBag.OR_Hex</td>
</tr>
<tr>
    <td>a XOR b</td>
    <td>@ViewBag.XOR_Bin</td>
    <td>@ViewBag.XOR_Oct</td>
    <td>@ViewBag.XOR_Dec</td>
    <td>@ViewBag.XOR_Hex</td>
</tr>
```
- Realizar logica para a + b y a * b usando este mismo esquema

Modificar en el HttpPost Index

```csharp
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

```

```html
<tr>
    <td>a + b</td>
    <td>@ViewBag.SUM_Bin</td>
    <td>@ViewBag.SUM_Oct</td>
    <td>@ViewBag.SUM_Dec</td>
    <td>@ViewBag.SUM_Hex</td>
</tr>
<tr>
    <td>a * b</td>
    <td>@ViewBag.MUL_Bin</td>
    <td>@ViewBag.MUL_Oct</td>
    <td>@ViewBag.MUL_Dec</td>
    <td>@ViewBag.MUL_Hex</td>
</tr>

```

### Preguntas de la PP2:

- **¿Cuál es el número que resulta al multiplicar, si se introducen los valores máximos permitidos en a y b? Indíquelo en todas las bases (binaria, octal, decimal y hexadecimal).**
  
| Operación | Binario          | Octal   | Decimal | Hexadecimal |
|-----------|-----------------|---------|---------|-------------|
| a * b     | 1111111000000001 | 177001 | 65025   | FE01        |


- **¿Es posible hacer las operaciones en otra capa? Si sí, ¿en cuál sería?**
  - Se puede y seria lo mas recomendable, la capa recomendable para realizar esto seria en el Model ya que ahi es donde suelen llevarse datos y logicas de negocio.
