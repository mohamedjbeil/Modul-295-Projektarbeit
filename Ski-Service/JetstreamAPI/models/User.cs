using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JetstreamAPI.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; } = "Mitarbeiter";
    }
}
