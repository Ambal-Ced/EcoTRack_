using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoTRack_.NewModel
{
    public class Insight
    {
        [Key]
        public string Id { get; set; } = GenerateId(); // Automatically generate a new ID on instantiation

        [ForeignKey("User")]
        public string Uid { get; set; } // Foreign key referencing the User table

        [Required]
        public string InsightText { get; set; } // Required property for the insight text

        public User User { get; set; } // Navigation property

        private static string GenerateId()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generate a random 6-digit number
        }
    }
}
