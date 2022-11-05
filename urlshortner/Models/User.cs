using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 22 caracteres")]
        [MaxLength(22, ErrorMessage = "Este campo deve conter entre 3 e 22 caracteres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        [MinLength(6, ErrorMessage = "Este campo deve conter entre 6 e 60 caracteres")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 6 e 60 caracteres")]
        public string? Password { get; set; }
    }
}
