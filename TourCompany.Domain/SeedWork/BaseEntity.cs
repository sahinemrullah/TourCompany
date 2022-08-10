using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourCompany.Domain.SeedWork
{
    public class BaseEntity
    {
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        public int ID { get => _id; protected set => _id = value; }

        private readonly List<INotification> _domainEvents = new();
        private int _id;

        public void AddDomainEvent(INotification e)
        {
            _domainEvents.Add(e);
        }
        public void RemoveDomainEvent(INotification e)
        {
            _domainEvents.Remove(e);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
