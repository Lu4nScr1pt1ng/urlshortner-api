using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class Access
    {
        public Access(int id, string? ip, string? linkId, DateTime accesedAt, string? creatorOfLinkId)
        {
            Id = id;
            Ip = ip;
            LinkId = linkId;
            AccesedAt = accesedAt;
            CreatorOfLinkId = creatorOfLinkId;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Ip { get; set; }

        public string? LinkId { get; set; }

        public string? CreatorOfLinkId { get; set; }

        public DateTime AccesedAt { get; set; }
    }
}
