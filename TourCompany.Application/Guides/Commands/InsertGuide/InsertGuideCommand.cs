using MediatR;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Guides.Commands.InsertGuide
{
    public class InsertGuideCommand : IRequest<Guide>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public int GenderID { get; set; }
        public IEnumerable<int> Languages { get; set; } = null!;
    }
    public class InsertGuideCommandHandler : IRequestHandler<InsertGuideCommand, Guide>
    {
        private readonly ITourCompanyDbContext _context;

        public InsertGuideCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<Guide> Handle(InsertGuideCommand request, CancellationToken cancellationToken)
        {
            var entity = new Guide()
            {
                Name = request.Name,
                Surname = request.Surname,
                TelephoneNumber = request.TelephoneNumber,
                GenderID = request.GenderID,
                IsActive = true,
                Languages = request.Languages
                                            .Select(id => new GuideLanguage()
                                            {
                                                LanguageID = id
                                            })
                                            .ToList()
            };
            var entry = _context.Guides.Add(entity);
            await _context.Instance.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }
    }
}
