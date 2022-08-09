namespace TourCompany.Domain.Entities
{
    public class Language
    {
        public Language(int languageID, string name)
        {
            LanguageID = languageID;
            Name = name;
            Guides = new HashSet<GuideLanguage>();
        }
        public int LanguageID { get; set; }
        public string Name { get; set; }
        public ICollection<GuideLanguage> Guides { get; set; }
    }
}
