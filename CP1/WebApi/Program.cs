using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System.Text;
using System.IO;

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

var list = new List<object>();

app.MapGet("/", () => Results.Redirect("/swagger"));

//app.MapPost("/", () => list);



app.MapPost("/", ([FromHeader] bool xml = false) =>
{
    if (xml)
    {
        var xmlSerializer = new XmlSerializer(list.GetType());
        using var stringWriter = new StringWriter();
        xmlSerializer.Serialize(stringWriter, list);
        return Results.Content(stringWriter.ToString(), "application/xml");
    }

    return Results.Ok(list);
});



//ChatGPT        
app.MapPut("/", ([FromForm] int quantity, [FromForm] string type) =>
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    if (type != "int" && type != "float")
        return Results.BadRequest(new { error = "'type' must be either 'int' or 'float'" });

    var random = new Random();

    if (type == "int")
        for (int i = 0; i < quantity; i++)
            list.Add(random.Next());
    else
        for (int i = 0; i < quantity; i++)
            list.Add(random.NextSingle());

    return Results.Ok(list);
}).DisableAntiforgery();

        //ChatGPT
app.MapDelete("/", ([FromForm] int quantity) =>
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    if (list.Count < quantity)
        return Results.BadRequest(new { error = "The list does not contain enough elements to delete" });

    for (int i = 0; i < quantity; i++)
        list.RemoveAt(0);

    return Results.Ok(list);
}).DisableAntiforgery();

        //ChatGPT
app.MapPatch("/", () =>
{
    list.Clear();
    return Results.Ok(new { message = "List cleared" });
}).DisableAntiforgery();

app.Run();
