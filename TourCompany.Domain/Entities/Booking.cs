using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Booking : BaseAuditableEntity
    {
        public Booking(int tourID, int guideID, DateTime date, string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null) : base(createdBy, created, updatedBy, updated)
        {
            _tourID = tourID;
            _guideID = guideID;
            _date = date;
            _tourists = new();
        }

        #region Properties
        public int TourID { get => _tourID; set => _tourID = value; }
        public int GuideID { get => _guideID; set => _guideID = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public IReadOnlyCollection<TourParticipant> Tourists => _tourists.AsReadOnly();
        #endregion

        #region Methods
        public bool AddTourist(TourParticipant tourist)
        {
            var touristExists = (from t in _tourists
                                   where t.TouristID == tourist.TouristID
                                   select t).Any();
            if (!touristExists)
            {
                _tourists.Add(tourist);
                // TODO: Tourist Added Event
                return true;
            }
            else
            {
                // TODO: Tourist Exists Event
                return false;
            }
        }

        public bool RemoveTourist(TourParticipant tourist)
        {
            bool success = _tourists.Remove(tourist);
            if(success)
            {
                // TODO: Tourist Removed Event
            }
            else
            {
                // TODO: Tourist Not Exist Event
            }
            return success;
        }
        #endregion

        #region Fields
        private int _tourID;
        private int _guideID;
        private DateTime _date;
        private List<TourParticipant> _tourists;
        #endregion

        #region Navigation Properties
        public Tour Tour { get; set; } = null!;
        public Guide Guide { get; set; } = null!;
        #endregion
    }
}
