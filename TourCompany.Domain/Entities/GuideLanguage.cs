namespace TourCompany.Domain.Entities
{
    public class GuideLanguage
    {
        public int GuideID { get; set; }
        public int LanguageID { get; set; }
        public Language Language { get; set; } = null!;
        public Guide Guide { get; set; } = null!;
    }
}
