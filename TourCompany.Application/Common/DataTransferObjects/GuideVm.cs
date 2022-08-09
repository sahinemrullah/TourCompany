namespace TourCompany.Application.Common.DataTransferObjects
{
    public class GuideVm
    {
        public int GuideID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public GenderDto Gender { get; set; } = null!;
        public IEnumerable<LanguageDto> Languages { get; set; } = null!;
    }
}
