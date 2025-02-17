using System.ComponentModel.DataAnnotations;

namespace JetstreamAPI.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required, MinLength(1)]
        public string ServiceName { get; set; } = string.Empty;
    }
}
