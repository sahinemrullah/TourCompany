namespace TourCompany.Domain.Entities
{
    public class Country
    {
        private readonly string _name;

        public Country(string name)
        {
            _name = name;
        }
        public string Name => _name;
    }
}
