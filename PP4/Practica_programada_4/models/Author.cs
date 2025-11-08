using System.ComponentModel.DataAnnotations;

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
