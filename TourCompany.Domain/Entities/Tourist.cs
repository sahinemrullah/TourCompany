namespace TourCompany.Domain.Entities
{
    public class Tourist
    {
        public Tourist()
        {
            Tours = new HashSet<TourParticipant>();
            Invoices = new HashSet<Invoice>();
        }
        public int TouristID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public int NationalityID { get; set; }
        public int CountryID { get; set; }
        public int GenderID { get; set; }

        public Gender Gender { get; set; } = null!;
        public Nationality Nationality { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public ICollection<TourParticipant> Tours { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
