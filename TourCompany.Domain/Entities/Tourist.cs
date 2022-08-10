using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Tourist : BaseAuditableEntity
    {
        #region Fields
        private string _name = null!;
        private string _surname = null!;
        private DateTime _birthDate;
        private int _nationalityID;
        private int _countryID;
        private int _genderID;
        private readonly List<TourParticipant> _tours; 
        #endregion

        public Tourist(string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null) : base(createdBy, created, updatedBy, updated)
        {
            _tours = new();
        }

        #region Properties
        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public DateTime BirthDate { get => _birthDate; set => _birthDate = value; }
        public int NationalityID { get => _nationalityID; set => _nationalityID = value; }
        public int CountryID { get => _countryID; set => _countryID = value; }
        public int GenderID { get => _genderID; set => _genderID = value; }
        #endregion

        #region NavigationProperties
        public Gender Gender { get; set; } = null!;
        public Nationality Nationality { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public IReadOnlyCollection<TourParticipant> Tours => _tours.AsReadOnly();
        #endregion
    }
}
