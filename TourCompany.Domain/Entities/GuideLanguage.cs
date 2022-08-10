namespace TourCompany.Domain.Entities
{
    public class GuideLanguage
    {
        public GuideLanguage(int guideID, int languageID)
        {
            _guideID = guideID;
            _languageID = languageID;
        }

        public int GuideID => _guideID;
        public int LanguageID => _languageID;

        private readonly int _guideID;
        private readonly int _languageID;
        
        public Language Language { get; set; } = null!; 
    }
}
