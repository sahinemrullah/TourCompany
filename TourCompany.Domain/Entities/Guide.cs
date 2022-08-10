using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Guide : BaseAuditableEntity
    {

        public Guide(string name, string surname, string telephoneNumber, int genderID, bool isActive, string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null) : base(createdBy, created, updatedBy, updated)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException($"'{nameof(surname)}' cannot be null or whitespace.", nameof(surname));

            if (string.IsNullOrWhiteSpace(telephoneNumber))
                throw new ArgumentException($"'{nameof(telephoneNumber)}' cannot be null or whitespace.", nameof(telephoneNumber));

            _name = name;
            _surname = surname;
            _telephoneNumber = telephoneNumber;
            _genderID = genderID;
            _isActive = isActive;
            _languages = new();
        }

        #region Fiedls
        private string _name = null!;
        private string _surname = null!;
        private string _telephoneNumber = null!;
        private int _genderID;
        private bool _isActive;
        private List<GuideLanguage> _languages;
        #endregion

        #region Properties
        public string Name => _name;
        public string Surname => _surname;
        public string TelephoneNumber => _telephoneNumber;
        public int GenderID => _genderID;
        public bool IsActive => _isActive; 
        #endregion

        #region NavigationProperties
        public IReadOnlyCollection<GuideLanguage> Languages => _languages.AsReadOnly();
        public Gender Gender { get; set; } = null!; 
        #endregion
    }
}
