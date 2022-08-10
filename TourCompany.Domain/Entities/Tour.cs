using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Tour : BaseAuditableEntity
    {
        public Tour(string name, string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null) : base(createdBy, created, updatedBy, updated)
        {
            _name = name;
            _destinations = new();
        }

        #region Properties
        public string Name => _name;
        #endregion

        #region Methods
        public void SetName(string name)
        {
            _name = name;
        }

        public bool AddDestination(TourDestination destination)
        {
            var destinationExists = (from d in _destinations
                                     where d.DestinationID == destination.DestinationID
                                     select d).Any();
            if (!destinationExists)
            {
                _destinations.Add(destination);
                // TODO: Destination Added Event
                return true;
            }
            else
            {
                // TODO: Destination Exists Event
                return false;
            }
        }

        public bool RemoveTourist(TourDestination destination)
        {
            bool success = _destinations.Remove(destination);
            if (success)
            {
                // TODO: Destination Removed Event
            }
            else
            {
                // TODO: Destination Not Exist Event
            }
            return success;
        }
        #endregion

        #region Fields
        private string _name;
        private List<TourDestination> _destinations;
        #endregion

        #region Navigation Properties
        public IReadOnlyCollection<TourDestination> Destinations => _destinations.AsReadOnly();
        #endregion
    }
}
