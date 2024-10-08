using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoTRack_.NewModel
{
    public class Electrate
    {
        [Key]
        public string Id { get; set; } = GenerateId(); // Automatically generate a new ID on instantiation

        [ForeignKey("User")]
        public string Uid { get; set; } // Foreign key referencing the User table

        [Column(TypeName = "decimal(18, 2)")]
        public decimal kwr { get; set; }

        [Column(TypeName = "decimal(38, 2)")]
        public decimal totalbill { get; set; }

        [Required]
        public DateTime date { get; set; } // Use DateTime instead of string

        public User User { get; set; } // Navigation property

        private static string GenerateId()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generate a random 6-digit number
        }
    }
}
