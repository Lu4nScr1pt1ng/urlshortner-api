using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Password { get; set; }
    }
}
