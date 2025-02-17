using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetstreamAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required, MinLength(1)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public string Phone { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public string Priority { get; set; } = string.Empty;

        // Beziehung zu Service
        [Required]
        public int ServiceID { get; set; }

        [ForeignKey("ServiceID")]
        public virtual Service? Service { get; set; } = null!;

        public string Status { get; set; } = "Offen";

/*        // Beziehung zu Mitarbeiter (User)
        public int? AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public virtual User? AssignedUser { get; set; } = null!;*/

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }}