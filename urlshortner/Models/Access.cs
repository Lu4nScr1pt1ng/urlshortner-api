using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class Access
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Ip { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? LinkId { get; set; }

        public DateTime AccesedAt { get; set; }
    }
}
