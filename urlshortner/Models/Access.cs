using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class Access
    {
        public Access(int id, string? ip, string? linkId, DateTime accesedAt)
        {
            Id = id;
            Ip = ip;
            LinkId = linkId;
            AccesedAt = accesedAt;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Ip { get; set; }

        public string? LinkId { get; set; }

        public DateTime AccesedAt { get; set; }
    }
}
