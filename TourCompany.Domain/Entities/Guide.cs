namespace TourCompany.Domain.Entities
{
    public class Guide
    {
        public Guide()
        {
            Languages = new HashSet<GuideLanguage>();
            Tours = new HashSet<Booking>();
        }
        public int GuideID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public int GenderID { get; set; }
        public bool IsActive { get; set; }

        public Gender Gender { get; set; } = null!;
        public ICollection<GuideLanguage> Languages { get; set; }
        public ICollection<Booking> Tours { get; set; }
    }
}
