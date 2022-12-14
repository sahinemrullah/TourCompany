using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class NationalityConfiguration : IEntityTypeConfiguration<Nationality>
    {
        public void Configure(EntityTypeBuilder<Nationality> builder)
        {
            builder.Property(n => n.Name)
                .HasColumnType("varchar")
                .HasMaxLength(21);

            int nationalityID = 1;
            var nationalities = new Nationality[]
            {
                new Nationality(nationalityID++, "Afghan"),
                new Nationality(nationalityID++, "Albanian"),
                new Nationality(nationalityID++, "Andorran"),
                new Nationality(nationalityID++, "Angolan"),
                new Nationality(nationalityID++, "Argentine"),
                new Nationality(nationalityID++, "Armenian"),
                new Nationality(nationalityID++, "Azerbaijani"),
                new Nationality(nationalityID++, "Bahamian"),
                new Nationality(nationalityID++, "Bahraini"),
                new Nationality(nationalityID++, "Belarusian"),
                new Nationality(nationalityID++, "Belgian"),
                new Nationality(nationalityID++, "Bermudian"),
                new Nationality(nationalityID++, "Bhutanese"),
                new Nationality(nationalityID++, "Botswanan"),
                new Nationality(nationalityID++, "Brazilian"),
                new Nationality(nationalityID++, "Bruneian"),
                new Nationality(nationalityID++, "Bulgarian"),
                new Nationality(nationalityID++, "Burundian"),
                new Nationality(nationalityID++, "Cambodian"),
                new Nationality(nationalityID++, "Cameroonian"),
                new Nationality(nationalityID++, "Cayman Islander"),
                new Nationality(nationalityID++, "Central African"),
                new Nationality(nationalityID++, "Chinese"),
                new Nationality(nationalityID++, "Citizen of Seychelles"),
                new Nationality(nationalityID++, "Citizen of Vanuatu"),
                new Nationality(nationalityID++, "Colombian"),
                new Nationality(nationalityID++, "Congolese (DRC)"),
                new Nationality(nationalityID++, "Cook Islander"),
                new Nationality(nationalityID++, "Cuban"),
                new Nationality(nationalityID++, "Cymraes"),
                new Nationality(nationalityID++, "Czech"),
                new Nationality(nationalityID++, "Danish"),
                new Nationality(nationalityID++, "Djiboutian"),
                new Nationality(nationalityID++, "Dutch"),
                new Nationality(nationalityID++, "East Timorese"),
                new Nationality(nationalityID++, "Ecuadorean"),
                new Nationality(nationalityID++, "English"),
                new Nationality(nationalityID++, "Equatorial Guinean"),
                new Nationality(nationalityID++, "Ethiopian"),
                new Nationality(nationalityID++, "Faroese"),
                new Nationality(nationalityID++, "Fijian"),
                new Nationality(nationalityID++, "Finnish"),
                new Nationality(nationalityID++, "French"),
                new Nationality(nationalityID++, "Gabonese"),
                new Nationality(nationalityID++, "Gambian"),
                new Nationality(nationalityID++, "Ghanaian"),
                new Nationality(nationalityID++, "Gibraltarian"),
                new Nationality(nationalityID++, "Greek"),
                new Nationality(nationalityID++, "Grenadian"),
                new Nationality(nationalityID++, "Guamanian"),
                new Nationality(nationalityID++, "Guinean"),
                new Nationality(nationalityID++, "Guyanese"),
                new Nationality(nationalityID++, "Haitian"),
                new Nationality(nationalityID++, "Honduran"),
                new Nationality(nationalityID++, "Icelandic"),
                new Nationality(nationalityID++, "Indian"),
                new Nationality(nationalityID++, "Iraqi"),
                new Nationality(nationalityID++, "Italian"),
                new Nationality(nationalityID++, "Irish"),
                new Nationality(nationalityID++, "Ivorian"),
                new Nationality(nationalityID++, "Jamaican"),
                new Nationality(nationalityID++, "Japanese"),
                new Nationality(nationalityID++, "Kazakh"),
                new Nationality(nationalityID++, "Kenyan"),
                new Nationality(nationalityID++, "Kosovan"),
                new Nationality(nationalityID++, "Kuwaiti"),
                new Nationality(nationalityID++, "Lao"),
                new Nationality(nationalityID++, "Latvian"),
                new Nationality(nationalityID++, "Libyan"),
                new Nationality(nationalityID++, "Liechtenstein citizen"),
                new Nationality(nationalityID++, "Macanese"),
                new Nationality(nationalityID++, "Macedonian"),
                new Nationality(nationalityID++, "Malaysian"),
                new Nationality(nationalityID++, "Maldivian"),
                new Nationality(nationalityID++, "Marshallese"),
                new Nationality(nationalityID++, "Martiniquais"),
                new Nationality(nationalityID++, "Mexican"),
                new Nationality(nationalityID++, "Micronesian"),
                new Nationality(nationalityID++, "Mongolian"),
                new Nationality(nationalityID++, "Montenegrin"),
                new Nationality(nationalityID++, "Mosotho"),
                new Nationality(nationalityID++, "Mozambican"),
                new Nationality(nationalityID++, "Namibian"),
                new Nationality(nationalityID++, "Nauruan"),
                new Nationality(nationalityID++, "Nicaraguan"),
                new Nationality(nationalityID++, "Nigerian"),
                new Nationality(nationalityID++, "North Korean"),
                new Nationality(nationalityID++, "Northern Irish"),
                new Nationality(nationalityID++, "Omani"),
                new Nationality(nationalityID++, "Pakistani"),
                new Nationality(nationalityID++, "Palauan"),
                new Nationality(nationalityID++, "Papua New Guinean"),
                new Nationality(nationalityID++, "Paraguayan"),
                new Nationality(nationalityID++, "Polish"),
                new Nationality(nationalityID++, "Portuguese"),
                new Nationality(nationalityID++, "Qatari"),
                new Nationality(nationalityID++, "Romanian"),
                new Nationality(nationalityID++, "Russian"),
                new Nationality(nationalityID++, "Salvadorean"),
                new Nationality(nationalityID++, "Sammarinese"),
                new Nationality(nationalityID++, "Saudi Arabian"),
                new Nationality(nationalityID++, "Scottish"),
                new Nationality(nationalityID++, "Sierra Leonean"),
                new Nationality(nationalityID++, "Slovenian"),
                new Nationality(nationalityID++, "Solomon Islander"),
                new Nationality(nationalityID++, "South Korean"),
                new Nationality(nationalityID++, "South Sudanese"),
                new Nationality(nationalityID++, "St Helenian"),
                new Nationality(nationalityID++, "St Lucian"),
                new Nationality(nationalityID++, "Surinamese"),
                new Nationality(nationalityID++, "Swazi"),
                new Nationality(nationalityID++, "Syrian"),
                new Nationality(nationalityID++, "Taiwanese"),
                new Nationality(nationalityID++, "Tajik"),
                new Nationality(nationalityID++, "Togolese"),
                new Nationality(nationalityID++, "Tongan"),
                new Nationality(nationalityID++, "Tunisian"),
                new Nationality(nationalityID++, "Turkish"),
                new Nationality(nationalityID++, "Tuvaluan"),
                new Nationality(nationalityID++, "Ugandan"),
                new Nationality(nationalityID++, "Ukrainian"),
                new Nationality(nationalityID++, "Vatican citizen"),
                new Nationality(nationalityID++, "Vincentian"),
                new Nationality(nationalityID++, "Wallisian"),
                new Nationality(nationalityID++, "Welsh"),
                new Nationality(nationalityID++, "Yemeni"),
                new Nationality(nationalityID++, "Zambian"),
                new Nationality(nationalityID++, "Zimbabwean"),
            };
            builder.HasData(nationalities);
        }
    }
}
