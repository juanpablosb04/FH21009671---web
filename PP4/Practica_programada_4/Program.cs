using Microsoft.EntityFrameworkCore;
using Practica_programada_4;
using Practica_programada_4.Models;

string dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "data");
string csvPath = Path.Combine("..", "data", "books.csv");

using var db = new AppDbContext();

// Crear carpeta Data/ si no existe
if (!Directory.Exists(dataFolder))
    Directory.CreateDirectory(dataFolder);

// Si no hay autores en la BD, cargamos desde el CSV
if (!db.Authors.Any())
{
    Console.WriteLine("\nLa base de datos está vacía, será llenada a partir del CSV.");
    Console.WriteLine("Procesando...");

    var lines = File.ReadAllLines(csvPath).Skip(1); // saltar encabezado

    foreach (var line in lines)
    {
        // Split CSV respetando comillas
        var fields = System.Text.RegularExpressions.Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

        // ✅ Validación para evitar IndexOutOfRange
        if (fields.Length < 3 || string.IsNullOrWhiteSpace(line))
            continue;

        string author = fields[0].Trim('"');
        string title = fields[1].Trim('"');
        string tags = fields[2];

        // Insertar autor (evita duplicados)
        var dbAuthor = db.Authors.FirstOrDefault(a => a.AuthorName == author);
        if (dbAuthor == null)
        {
            dbAuthor = new Author { AuthorName = author };
            db.Authors.Add(dbAuthor);
            db.SaveChanges();
        }

        // Insertar título
        var dbTitle = new Title
        {
            TitleName = title,
            AuthorId = dbAuthor.AuthorId
        };
        db.Titles.Add(dbTitle);
        db.SaveChanges();

        // Insertar tags
        foreach (var tagName in tags.Split("|"))
        {
            var dbTag = db.Tags.FirstOrDefault(t => t.TagName == tagName);
            if (dbTag == null)
            {
                dbTag = new Tag { TagName = tagName };
                db.Tags.Add(dbTag);
                db.SaveChanges();
            }

            db.TitlesTags.Add(new TitleTag
            {
                TitleId = dbTitle.TitleId,
                TagId = dbTag.TagId
            });
        }

        db.SaveChanges();
    }

    Console.WriteLine("✅ Importación completa.");
}
else
{
    Console.WriteLine("\nLa base de datos se está leyendo para crear los archivos TSV.");
    Console.WriteLine("Procesando...");

    var records = db.TitlesTags
        .Include(tt => tt.Title)
        .ThenInclude(t => t.Author)
        .Include(tt => tt.Tag)
        .Select(tt => new
        {
            AuthorName = tt.Title.Author.AuthorName,
            TitleName = tt.Title.TitleName,
            TagName = tt.Tag.TagName
        })
        .ToList();

    // Agrupar por primera letra del autor
    var grouped = records
        .OrderByDescending(r => r.AuthorName)
        .ThenByDescending(r => r.TitleName)
        .ThenByDescending(r => r.TagName)
        .GroupBy(r => r.AuthorName[0].ToString().ToUpper());

    foreach (var group in grouped)
    {
        string tsvFile = Path.Combine(dataFolder, $"{group.Key}.tsv");

        using (var writer = new StreamWriter(tsvFile))
        {
            writer.WriteLine("AuthorName\tTitleName\tTagName");

            foreach (var record in group)
            {
                writer.WriteLine($"{record.AuthorName}\t{record.TitleName}\t{record.TagName}");
            }
        }
    }

    Console.WriteLine("✅ TSV generados.");
}