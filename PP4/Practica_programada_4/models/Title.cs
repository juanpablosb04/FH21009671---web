using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
