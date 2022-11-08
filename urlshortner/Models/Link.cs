using System.ComponentModel.DataAnnotations;
using urlshortner.Utils;

namespace urlshortner.Models
{
    public class Link
    {
        public Link(string? id, string redirectLink, string? userId, DateTime createdAt)
        {
            Id = id;
            RedirectLink = redirectLink;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? RedirectLink { get; set; }

        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
