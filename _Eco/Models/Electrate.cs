namespace _Eco.Models
{
    public class Electrate
    {
        public int Id { get; set; }
        public float kwh { get; set; }
        public float totalbill { get; set; }
        public string date { get; set; }
        // Add this property to store the ID of the user who created the data
        public string UserId { get; set; }
    }
}
