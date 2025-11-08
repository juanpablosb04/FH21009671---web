using System.ComponentModel.DataAnnotations;

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
