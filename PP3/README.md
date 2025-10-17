# Practica Programada 3

## Juan Pablo Solís Benamburg - Carnet: FH21009671

### Comandos utilizados:
```bash
dotnet new sln -n PP3
dotnet new webapi -o Practica_Programada_3 -f net8.0
dotnet sln add Practica_Programada_3/
ni README.md
cd Practica_Programada_3/
```
### Prompts
- Necesito crear el siguiente endpoint para una minimal API: 

Endpoint /include
Este Endpoint será de tipo POST y permitirá incluir una palabra en una posición específica de una oración. Dicha posición se basa en la cantidad de palabras que tenga dicha oración y su índice empieza en cero. Estos serían sus parámetros:

| Parámetro | Representación          | Tipo   | Desde  | Validaciones                     |
|-----------|------------------------|--------|--------|---------------------------------|
| position  | La posición            | int    | Route  | Igual o mayor que cero           |
| value     | La palabra a incluir   | string | Query  | Longitud mayor que cero          |
| text      | La oración             | string | Form   | Longitud mayor que cero          |
| xml       | El formato del resultado | bool   | Headers | Ninguna ya que es opcional      |

Si el parámetro position es mayor que el largo de palabras de la oración, entonces la palabra se debe agregar al final de la oración.

```csharp
using System.Xml.Serialization;

var myApi = app.MapGroup("/api");

myApi.MapPost("/include/{position:int}", 
    ([FromRoute] int position, 
     [FromQuery] string value, 
     [FromForm] string text, 
     [FromHeader] bool xml = false) =>
{
    // Validaciones
    if (position < 0)
        return Results.BadRequest(new { error = "'position' must be 0 or higher" });

    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });

    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    // Lógica de inserción
    var words = text.Split(' ').ToList();
    if (position >= words.Count)
        words.Add(value);
    else
        words.Insert(position, value);

    var newText = string.Join(" ", words);

    // Respuesta
    if (xml)
    {
        var result = new Result { Ori = text, New = newText };
        var xmlSerializer = new XmlSerializer(typeof(Result));
        using var stringWriter = new StringWriter();
        xmlSerializer.Serialize(stringWriter, result);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }
    else
    {
        return Results.Ok(new { ori = text, @new = newText });
    }
})
.DisableAntiforgery();

// Clase para XML
public class Result
{
    public string Ori { get; set; }
    public string New { get; set; }
}

```

- Ahora, ayúdame a realizar el /replace: 

Este Endpoint será de tipo PUT y permitirá reemplazar las palabras de una longitud específica dentro de una oración, por otra palabra. Es decir, buscará todas las palabras de dicha longitud y las reemplazará para una nueva palabra indicada. Estos son sus parámetros:

| Parámetro | Representación          | Tipo   | Desde  | Validaciones                     |
|-----------|------------------------|--------|--------|---------------------------------|
| length    | La longitud            | int    | Route  | Mayor que cero                   |
| value     | La palabra por la que se reemplazará | string | Query  | Longitud mayor que cero          |
| text      | La oración             | string | Form   | Longitud mayor que cero          |
| xml       | El formato del resultado | bool   | Headers | Ninguna ya que es opcional      |


```csharp
// Endpoint /replace
app.MapPut("/replace/{length:int}",
    ([FromRoute] int length,
     [FromQuery] string value,
     [FromForm] string text,
     [FromHeader] bool xml = false) =>
{
    // Validaciones
    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be greater than 0" });

    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });

    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    // Lógica de reemplazo
    var words = text.Split(' ');
    for (int i = 0; i < words.Length; i++)
    {
        if (words[i].Length == length)
            words[i] = value;
    }

    var newText = string.Join(" ", words);

    // Respuesta JSON o XML
    if (xml)
    {
        var result = new Result { Ori = text, New = newText };
        var serializer = new XmlSerializer(typeof(Result));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, result);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }
    else
    {
        return Results.Ok(new { ori = text, @new = newText });
    }
}).DisableAntiforgery();

```
- Ahora, ayúdame a realizar el /erase:

Este Endpoint será de tipo DELETE y permitirá eliminar las palabras de una longitud específica dentro de una oración. Estos serían los parámetros:

| Parámetro | Representación          | Tipo   | Desde  | Validaciones                     |
|-----------|------------------------|--------|--------|---------------------------------|
| length    | La longitud            | int    | Route  | Mayor que cero                   |
| text      | La oración             | string | Form   | Longitud mayor que cero          |
| xml       | El formato del resultado | bool   | Headers | Ninguna ya que es opcional      |


```csharp
// Endpoint /erase
app.MapDelete("/erase/{length:int}",
    ([FromRoute] int length,
     [FromForm] string text,
     [FromHeader] bool xml = false) =>
{
    // Validaciones
    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be greater than 0" });

    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    // Lógica de borrado
    var words = text.Split(' ')
                    .Where(w => w.Length != length)
                    .ToArray();

    var newText = string.Join(" ", words);

    // Respuesta JSON o XML
    if (xml)
    {
        var result = new Result { Ori = text, New = newText };
        var serializer = new XmlSerializer(typeof(Result));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, result);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }
    else
    {
        return Results.Ok(new { ori = text, @new = newText });
    }
}).DisableAntiforgery();

```

### Preguntas de la PP3:

- **¿Es posible enviar valores en el Body (por ejemplo, en el Form) del Request de tipo GET?**
  - Es posible, pero desde mi punto de vista no me parece que sea recomendable; incluso dudo de que sea posible en muchos frameworks, ya que el GET es para recuperar datos o información.



- **¿Qué ventajas y desventajas observa con el Minimal API si se compara con la opción de utilizar Controllers?**
  - En resumen, la ventaja principal es que es mucho más ligero y simplista para cosas pequeñas, tipo microservicios. En cuanto a la desventaja, es que, evidentemente, si se desea usar en sistemas más complejos y grandes, es muy probable que se quede corto; pero esto es evitable si se realiza un análisis previo de lo que se necesita. El problema es cuando la API, que ya usa Minimal API, presenta una escalabilidad muy grande y no puede rendir como sería necesario.