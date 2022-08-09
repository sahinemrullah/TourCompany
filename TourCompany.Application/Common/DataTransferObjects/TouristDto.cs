namespace TourCompany.Application.Common.DataTransferObjects
{
    public class TouristDto
    {
        public int TouristID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public GenderDto Gender { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
