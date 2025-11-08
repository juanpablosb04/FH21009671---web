# Practica Programada 4

## Juan Pablo Solís Benamburg - Carnet: FH21009671

### Comandos utilizados:
```bash
dotnet new sln -n Practica_programada_4
dotnet new console -n Practica_programada_4 -f net8.0
dotnet sln add Practica_Programada_4/
ni README.md
cd Practica_Programada_4/
dotnet add package Microsoft.EntityFrameworkCore --version 9.*
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.*
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.*
mkdir data
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

### Prompts
- Necesito crear dbContext del problema planteado previamente.

```csharp
using Microsoft.EntityFrameworkCore;
using Practica_programada_4.Models;
using System.IO;

using Microsoft.EntityFrameworkCore;
using System.IO;
using YourProject.Models;

namespace YourProject
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TitleTag> TitlesTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "data", "books.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // Nombre de tabla TitlesTags (Fluent API)
            model.Entity<TitleTag>().ToTable("TitlesTags");

            // Orden de columnas en Title
            model.Entity<Title>().Property(t => t.TitleId).HasColumnOrder(0);
            model.Entity<Title>().Property(t => t.AuthorId).HasColumnOrder(1);
            model.Entity<Title>().Property(t => t.TitleName).HasColumnOrder(2);
        }
    }
}

```

- Ahora necesito importar el CSV a la base de datos (es decir books.db).

```csharp
✅ Paso 1: Modelos

👉 En la carpeta Models/, crea estos archivos:

📄 Author.cs

namespace Practica_programada_4.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public ICollection<Title> Titles { get; set; }
    }
}

📄 Title.cs
namespace Practica_programada_4.Models
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public string TitleName { get; set; }

        public Author Author { get; set; }
        public ICollection<TitleTag> TitlesTags { get; set; }
    }
}

📄 Tag.cs
namespace Practica_programada_4.Models
{
    public class TitleTag
    {
        [Key]
        public int TitleTagId { get; set; }

        [Required]
        public int TitleId { get; set; }

        [Required]
        public int TagId { get; set; }

        public Title Title { get; set; }
        public Tag Tag { get; set; }
    }
}

📄 TitleTag.cs (tabla puente para many-to-many)
namespace Practica_programada_4.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string TagName { get; set; }

        public ICollection<TitleTag> TitlesTags { get; set; }
    }
}

```

👉 Reemplaza el contenido de Program.cs por esto:

```csharp
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

```

- Ahora necesito generar archivos .tsv

```csharp
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

    // Agrupar por primera letra del nombre del autor
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

    Console.WriteLine("Listo.");
}


```

### Preguntas de la PP4:

- **¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?**

    - Creo que usar Code First con una base NoSQL sería algo más complicado, porque en NoSQL no existe el concepto de tablas ni relaciones estrictas como en SQL. Con Database First creo que sería todavía más difícil, porque en NoSQL no hay un esquema fijo de base de datos para “leer” y después generar el modelo en el código.

    - Con respecto a las Foreign Keys, sí creo que habría complicaciones, ya que en Mongo lo normal es no usar Foreign Keys.



- **¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué? Por ejemplo: Pipe (|) con extensión .pipe.**
    - Creo que otro buen carácter sería |. En cuanto a la extensión, yo le pondría .psv (Pipe-Separated Values), porque es parecida a .csv pero deja claro que los valores están separados por “|” en lugar de comas.
