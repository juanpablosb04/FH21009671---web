using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app. MapGet("/", () => Results.Redirect("/swagger/index.html"));

// Endpoint para incluir palabra en el texto
app.MapPost("/include/{position:int}",
    ([FromRoute] int position, 
     [FromQuery] string value, 
     [FromForm] string text, 
     [FromHeader] bool xml = false) =>
{
    if (position < 0)
        return Results.BadRequest(new { error = "'position' must be 0 or higher" });

    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });

    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    var words = text.Split(' ').ToList();
    if (position >= words.Count)
        words.Add(value);
    else
        words.Insert(position, value);

    var newText = string.Join(" ", words);

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


// Endpoint para reemplazar palabras en el texto

app.MapPut("/replace/{length:int}",
    ([FromRoute] int length,
     [FromQuery] string value,
     [FromForm] string text,
     [FromHeader] bool xml = false) =>
{
    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be greater than 0" });

    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });

    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    var words = text.Split(' ');
    for (int i = 0; i < words.Length; i++)
    {
        if (words[i].Length == length)
            words[i] = value;
    }

    var newText = string.Join(" ", words);

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




//Endpoint para eliminar palabras en el texto

app.MapDelete("/erase/{length:int}",
    ([FromRoute] int length,
     [FromForm] string text,
     [FromHeader] bool xml = false) =>
{
    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be greater than 0" });

    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    var words = text.Split(' ')
                    .Where(w => w.Length != length)
                    .ToArray();

    var newText = string.Join(" ", words);

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


app.Run();

// Clase para XML
public class Result
{
    public string Ori { get; set; }
    public string New { get; set; }
}





