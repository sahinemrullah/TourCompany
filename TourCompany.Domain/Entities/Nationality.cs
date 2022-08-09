namespace TourCompany.Domain.Entities
{
    public class Nationality
    {
        public Nationality(int nationalityID, string name)
        {
            NationalityID = nationalityID;
            Name = name;
            Tourists = new HashSet<Tourist>();
        }
        public int NationalityID { get; set; }
        public string Name { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
    }
}
