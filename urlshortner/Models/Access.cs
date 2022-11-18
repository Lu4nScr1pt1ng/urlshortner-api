using System.ComponentModel.DataAnnotations;

namespace urlshortner.Models
{
    public class Access
    {
        public Access(int id, string? ip, string? city, string? region, string? country, string? organization, string? browser, string? linkId, string? creatorOfLinkId, DateTime accesedAt)
        {
            Id = id;
            Ip = ip;
            City = city;
            Region = region;
            Country = country;
            Organization = organization;
            Browser = browser;
            LinkId = linkId;
            CreatorOfLinkId = creatorOfLinkId;
            AccesedAt = accesedAt;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        public string? Ip { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? Country { get; set; }

        public string? Organization { get; set; }

        public string? Browser { get; set; }

        public string? OperatingSystem { get; set; }

        public string? LinkId { get; set; }

        public string? CreatorOfLinkId { get; set; }

        public DateTime AccesedAt { get; set; }
    }
}
