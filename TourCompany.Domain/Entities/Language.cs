using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Language : BaseEntity
    {
        
        public Language(string name)
        {
            _name = name;
        }
        
        public string Name => _name;

        private readonly string _name;
    }
}
