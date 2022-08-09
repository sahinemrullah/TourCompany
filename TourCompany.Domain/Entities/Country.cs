namespace TourCompany.Domain.Entities
{
    public class Country
    {
        public Country(int countryID, string name)
        {
            CountryID = countryID;
            Name = name;
            Tourists = new HashSet<Tourist>();
        }
        public int CountryID { get; set; }
        public string Name { get; set; }
        public ICollection<Tourist> Tourists { get; set; }
    }
}
