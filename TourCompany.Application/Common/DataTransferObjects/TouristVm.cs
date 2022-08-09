namespace TourCompany.Application.Common.DataTransferObjects
{
    public class TouristVm
    {
        public int TouristID { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public GenderDto Gender { get; set; } = null!;
        public NationalityDto Nationality { get; set; } = null!;
        public CountryDto Country { get; set; } = null!;
    }
}