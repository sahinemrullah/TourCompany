using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourCompany.Application.Common.Exceptions;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Guides.Commands.UpdateGuide
{
    public class UpdateGuideCommand : IRequest<int>
    {
        public int GuideID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public Gender Gender { get; set; } = null!;
        public IEnumerable<int> Languages { get; set; } = null!;
    }
    public class UpdateGuideCommandHandler : IRequestHandler<UpdateGuideCommand, int>
    {
        private readonly ITourCompanyDbContext _context;

        public UpdateGuideCommandHandler(ITourCompanyDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateGuideCommand request, CancellationToken cancellationToken)
        {
            var guide = _context.Guides.Where(g => g.GuideID == request.GuideID).FirstOrDefault();

            if (guide == null)
                throw new NotFoundException(nameof(Guide), request.GuideID);

            EntityEntry<Guide> entry = _context.Instance.Entry(guide);
            entry.CurrentValues.SetValues(request);

            entry.Collection(t => t.Languages).Load();
            var oldLanguages = guide.Languages.AsQueryable();
            var newLanguages = request.Languages.AsQueryable();
            var destinationsToBeDeleted = oldLanguages
                                        .ExceptBy(newLanguages, gl => gl.LanguageID);

            var destinationsToBeInserted = newLanguages
                                            .Except(oldLanguages
                                                        .Select(l => l.LanguageID))
                                            .Select(l => new GuideLanguage()
                                            {
                                                GuideID = request.GuideID,
                                                LanguageID = l
                                            });
            _context.GuideLanguages.RemoveRange(destinationsToBeDeleted);
            _context.GuideLanguages.AddRange(destinationsToBeInserted);

            await _context.Instance.SaveChangesAsync(cancellationToken);

            return guide.GuideID;
        }
    }
}
