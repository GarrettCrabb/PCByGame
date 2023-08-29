namespace PCByGame.Models
{
    public class Pc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Motherboard { get; set; }
        public string CPU { get; set; }
        public string Ram { get; set; }
        public string GPU { get; set; }
        public string PSU { get; set; }
        public string Storage { get; set; }
        public string CaseName { get; set; }
        public string Cost { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
