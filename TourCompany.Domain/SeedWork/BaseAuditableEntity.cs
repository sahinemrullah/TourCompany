using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourCompany.Domain.SeedWork
{
    public class BaseAuditableEntity : BaseEntity
    {

        public BaseAuditableEntity(string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null)
        {
            if (string.IsNullOrWhiteSpace(createdBy))
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace.", nameof(createdBy));

            _createdBy = createdBy;
            _created = created;
            _updatedBy = updatedBy;
            _updated = updated;
        }

        #region Properties
        public string? UpdatedBy => _updatedBy;
        public DateTime? Updated => _updated;
        public DateTime Created => _created;
        public string CreatedBy => _createdBy;
        #endregion

        #region Methods
        public void Update(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));

            _updatedBy = userId;
            _updated = DateTime.Now;
        }
        #endregion

        #region Fields
        private readonly string _createdBy;
        private readonly DateTime _created;
        private string? _updatedBy;
        private DateTime? _updated;
        #endregion
    }
}
