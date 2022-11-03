using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? RedirectLink { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
