namespace TourCompany.Application.Common.DataTransferObjects
{
    public class GuideDto
    {
        public int GuideID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
    }
}
