using EcoTRack_.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoTRack_.NewModel
{
    public class User
    {
        [Key]
        public string Uid { get; set; } = GenerateId(); // Automatically generate a new ID on instantiation

        [ForeignKey("EcoTrackUser")]
        public string ID { get; set; } // Foreign key referencing AspNetUsers table (EcoTrackUser)

        public EcoTrackUser EcoTrackUser { get; set; } // Navigation property

        private static string GenerateId()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generate a random 6-digit number
        }
    }
}
