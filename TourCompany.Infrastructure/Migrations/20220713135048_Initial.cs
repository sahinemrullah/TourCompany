using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCompany.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "char(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(28)", maxLength: 28, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageID);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityID);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourID);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    GuideID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TelephoneNumber = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.GuideID);
                    table.ForeignKey(
                        name: "FK_Guides_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tourists",
                columns: table => new
                {
                    TouristID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    NationalityID = table.Column<int>(type: "int", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourists", x => x.TouristID);
                    table.ForeignKey(
                        name: "FK_Tourists_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tourists_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tourists_Nationalities_NationalityID",
                        column: x => x.NationalityID,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourDestinations",
                columns: table => new
                {
                    TourID = table.Column<int>(type: "int", nullable: false),
                    DestinationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDestinations", x => new { x.TourID, x.DestinationID });
                    table.ForeignKey(
                        name: "FK_TourDestinations_Destinations_DestinationID",
                        column: x => x.DestinationID,
                        principalTable: "Destinations",
                        principalColumn: "DestinationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourDestinations_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourID = table.Column<int>(type: "int", nullable: false),
                    GuideID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Guides_GuideID",
                        column: x => x.GuideID,
                        principalTable: "Guides",
                        principalColumn: "GuideID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Tours_TourID",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuideLanguages",
                columns: table => new
                {
                    GuideID = table.Column<int>(type: "int", nullable: false),
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideLanguages", x => new { x.GuideID, x.LanguageID });
                    table.ForeignKey(
                        name: "FK_GuideLanguages_Guides_GuideID",
                        column: x => x.GuideID,
                        principalTable: "Guides",
                        principalColumn: "GuideID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuideLanguages_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourParticipants",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    TouristID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourParticipants", x => new { x.BookingID, x.TouristID });
                    table.ForeignKey(
                        name: "FK_TourParticipants_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourParticipants_Tourists_TouristID",
                        column: x => x.TouristID,
                        principalTable: "Tourists",
                        principalColumn: "TouristID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { 1, "Afghanistan" },
                    { 2, "Albania" },
                    { 3, "Algeria" },
                    { 4, "Andorra" },
                    { 5, "Angola" },
                    { 6, "Antigua and Barbuda" },
                    { 7, "Argentina" },
                    { 8, "Armenia" },
                    { 9, "Australia" },
                    { 10, "Norfolk Island" },
                    { 11, "Austria" },
                    { 12, "Azerbaijan" },
                    { 13, "Bahrain" },
                    { 14, "Bangladesh" },
                    { 15, "Barbados" },
                    { 16, "Belarus" },
                    { 17, "Belgium" },
                    { 18, "Belize" },
                    { 19, "Benin" },
                    { 20, "Bhutan" },
                    { 21, "Bolivia" },
                    { 22, "Bosnia and Herzegovina" },
                    { 23, "Botswana" },
                    { 24, "Brazil" },
                    { 25, "Brunei" },
                    { 26, "Bulgaria" },
                    { 27, "Burkina Faso" },
                    { 28, "Burundi" },
                    { 29, "Cambodia" },
                    { 30, "Cameroon" },
                    { 31, "Canada" },
                    { 32, "Cape Verde" },
                    { 33, "Central African Republic" },
                    { 34, "Chad" },
                    { 35, "Chile" },
                    { 36, "China" },
                    { 37, "Hong Kong S.A.R." },
                    { 38, "Colombia" },
                    { 39, "Comoros" },
                    { 40, "Costa Rica" },
                    { 41, "Croatia" },
                    { 42, "Cuba" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { 43, "Cyprus" },
                    { 44, "Czech Republic" },
                    { 45, "Democratic Republic of the Congo" },
                    { 46, "Denmark" },
                    { 47, "Faroe Islands" },
                    { 48, "Greenland" },
                    { 49, "Djibouti" },
                    { 50, "Dominica" },
                    { 51, "Dominican Republic" },
                    { 52, "East Timor" },
                    { 53, "Ecuador" },
                    { 54, "Egypt" },
                    { 55, "El Salvador" },
                    { 56, "Equatorial Guinea" },
                    { 57, "Eritrea" },
                    { 58, "Estonia" },
                    { 59, "Ethiopia" },
                    { 60, "Federated States of Micronesia" },
                    { 61, "Fiji" },
                    { 62, "Aland" },
                    { 63, "Finland" },
                    { 64, "Clipperton Island" },
                    { 65, "France" },
                    { 66, "French Polynesia" },
                    { 67, "New Caledonia" },
                    { 68, "Saint Barthelemy" },
                    { 69, "Saint Martin" },
                    { 70, "Saint Pierre and Miquelon" },
                    { 71, "Wallis and Futuna" },
                    { 72, "Gabon" },
                    { 73, "Gambia" },
                    { 74, "Georgia" },
                    { 75, "Germany" },
                    { 76, "Ghana" },
                    { 77, "Greece" },
                    { 78, "Grenada" },
                    { 79, "Guatemala" },
                    { 80, "Guinea" },
                    { 81, "Guinea Bissau" },
                    { 82, "Guyana" },
                    { 83, "Haiti" },
                    { 84, "Honduras" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { 85, "Hungary" },
                    { 86, "Iceland" },
                    { 87, "India" },
                    { 88, "Indonesia" },
                    { 89, "Iran" },
                    { 90, "Iraq" },
                    { 91, "Ireland" },
                    { 92, "Israel" },
                    { 93, "Palestine" },
                    { 94, "Italy" },
                    { 95, "Ivory Coast" },
                    { 96, "Jamaica" },
                    { 97, "Japan" },
                    { 98, "Jordan" },
                    { 99, "Kazakhstan" },
                    { 100, "Kenya" },
                    { 101, "Kiribati" },
                    { 102, "Kosovo" },
                    { 103, "Kuwait" },
                    { 104, "Kyrgyzstan" },
                    { 105, "Laos" },
                    { 106, "Latvia" },
                    { 107, "Lebanon" },
                    { 108, "Lesotho" },
                    { 109, "Liberia" },
                    { 110, "Libya" },
                    { 111, "Liechtenstein" },
                    { 112, "Lithuania" },
                    { 113, "Luxembourg" },
                    { 114, "Macedonia" },
                    { 115, "Madagascar" },
                    { 116, "Malawi" },
                    { 117, "Malaysia" },
                    { 118, "Maldives" },
                    { 119, "Mali" },
                    { 120, "Malta" },
                    { 121, "Marshall Islands" },
                    { 122, "Mauritania" },
                    { 123, "Mauritius" },
                    { 124, "Mexico" },
                    { 125, "Moldova" },
                    { 126, "Monaco" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { 127, "Mongolia" },
                    { 128, "Montenegro" },
                    { 129, "Morocco" },
                    { 130, "Mozambique" },
                    { 131, "Myanmar" },
                    { 132, "Namibia" },
                    { 133, "Nauru" },
                    { 134, "Nepal" },
                    { 135, "Aruba" },
                    { 136, "Netherlands" },
                    { 137, "Sint Maarten" },
                    { 138, "Cook Islands" },
                    { 139, "New Zealand" },
                    { 140, "Niue" },
                    { 141, "Nicaragua" },
                    { 142, "Niger" },
                    { 143, "Nigeria" },
                    { 144, "North Korea" },
                    { 145, "Northern Cyprus" },
                    { 146, "Norway" },
                    { 147, "Oman" },
                    { 148, "Pakistan" },
                    { 149, "Palau" },
                    { 150, "Panama" },
                    { 151, "Papua New Guinea" },
                    { 152, "Paraguay" },
                    { 153, "Peru" },
                    { 154, "Philippines" },
                    { 155, "Poland" },
                    { 156, "Portugal" },
                    { 157, "Qatar" },
                    { 158, "Republic of Congo" },
                    { 159, "Serbia" },
                    { 160, "Romania" },
                    { 161, "Russia" },
                    { 162, "Rwanda" },
                    { 163, "Saint Kitts and Nevis" },
                    { 164, "Saint Lucia" },
                    { 165, "Saint Vincent and the Grenadines" },
                    { 166, "Samoa" },
                    { 167, "San Marino" },
                    { 168, "Sao Tome and Principe" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { 169, "Saudi Arabia" },
                    { 170, "Senegal" },
                    { 171, "Seychelles" },
                    { 172, "Sierra Leone" },
                    { 173, "Singapore" },
                    { 174, "Slovakia" },
                    { 175, "Slovenia" },
                    { 176, "Solomon Islands" },
                    { 177, "Somalia" },
                    { 178, "Somaliland" },
                    { 179, "South Africa" },
                    { 180, "South Korea" },
                    { 181, "South Sudan" },
                    { 182, "Spain" },
                    { 183, "Sri Lanka" },
                    { 184, "Sudan" },
                    { 185, "Suriname" },
                    { 186, "Swaziland" },
                    { 187, "Sweden" },
                    { 188, "Switzerland" },
                    { 189, "Syria" },
                    { 190, "Taiwan" },
                    { 191, "Tajikistan" },
                    { 192, "Thailand" },
                    { 193, "Togo" },
                    { 194, "Tonga" },
                    { 195, "Trinidad and Tobago" },
                    { 196, "Tunisia" },
                    { 197, "Turkey" },
                    { 198, "Turkmenistan" },
                    { 199, "Tuvalu" },
                    { 200, "Uganda" },
                    { 201, "Ukraine" },
                    { 202, "United Arab Emirates" },
                    { 203, "Anguilla" },
                    { 204, "Bermuda" },
                    { 205, "British Indian Ocean Territory" },
                    { 206, "British Virgin Islands" },
                    { 207, "Cayman Islands" },
                    { 208, "Falkland Islands" },
                    { 209, "Gibraltar" },
                    { 210, "Guernsey" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "Name" },
                values: new object[,]
                {
                    { 211, "Isle of Man" },
                    { 212, "Jersey" },
                    { 213, "Montserrat" },
                    { 214, "Pitcairn Islands" },
                    { 215, "Saint Helena" },
                    { 216, "Turks and Caicos Islands" },
                    { 217, "United Kingdom" },
                    { 218, "United Republic of Tanzania" },
                    { 219, "American Samoa" },
                    { 220, "Guam" },
                    { 221, "Northern Mariana Islands" },
                    { 222, "Puerto Rico" },
                    { 223, "United States Minor Outlying Islands" },
                    { 224, "United States of America" },
                    { 225, "Uruguay" },
                    { 226, "Uzbekistan" },
                    { 227, "Vanuatu" },
                    { 228, "Vatican" },
                    { 229, "Venezuela" },
                    { 230, "Vietnam" },
                    { 231, "Western Sahara" },
                    { 232, "Yemen" },
                    { 233, "Zambia" },
                    { 234, "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "Name" },
                values: new object[,]
                {
                    { 1, "Afrikaans" },
                    { 2, "Albanian" },
                    { 3, "Amharic" },
                    { 4, "Arabic" },
                    { 5, "Armenian" },
                    { 6, "Aymara" },
                    { 7, "Azerbaijani" },
                    { 8, "Bahasa Indonesia" },
                    { 9, "Bahasa Malaysia" },
                    { 10, "Belarusian" },
                    { 11, "Bengali" },
                    { 12, "Bosnian" },
                    { 13, "Bulgarian" },
                    { 14, "Burmanese" },
                    { 15, "Cantonese" },
                    { 16, "Catalan" },
                    { 17, "Chamorro" },
                    { 18, "Cook Island Maori" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "Name" },
                values: new object[,]
                {
                    { 19, "Creole" },
                    { 20, "Croatian" },
                    { 21, "Czech" },
                    { 22, "Danish" },
                    { 23, "Dari" },
                    { 24, "Dhivehi" },
                    { 25, "Dutch" },
                    { 26, "Dzongkha" },
                    { 27, "English" },
                    { 28, "Estonian" },
                    { 29, "Faroese" },
                    { 30, "Fijian" },
                    { 31, "Filipino" },
                    { 32, "Finnish" },
                    { 33, "French" },
                    { 34, "Georgian" },
                    { 35, "German" },
                    { 36, "Greek" },
                    { 37, "Greenlandic" },
                    { 38, "Guarani" },
                    { 39, "Hebrew" },
                    { 40, "Hindi" },
                    { 41, "Hiri Motu" },
                    { 42, "Hungarian" },
                    { 43, "Icelandic" },
                    { 44, "Indigenous" },
                    { 45, "Irish" },
                    { 46, "Italian" },
                    { 47, "Japanese" },
                    { 48, "Karakalpakstani" },
                    { 49, "Kazakh" },
                    { 50, "Khalkha Mongol" },
                    { 51, "Khmer" },
                    { 52, "Kinyarwanda" },
                    { 53, "Kirundi" },
                    { 54, "Kiswahili" },
                    { 55, "Korean" },
                    { 56, "Kurdish" },
                    { 57, "Kyrgyz" },
                    { 58, "Lao" },
                    { 59, "Latin" },
                    { 60, "Latvian" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "Name" },
                values: new object[,]
                {
                    { 61, "Lingala" },
                    { 62, "Lithuanian" },
                    { 63, "Luxembourgish" },
                    { 64, "Macedonian" },
                    { 65, "Malagasy" },
                    { 66, "Malay" },
                    { 67, "Maltese" },
                    { 68, "Maori" },
                    { 69, "Marshallese" },
                    { 70, "Melanesian pidgin" },
                    { 71, "Mirandese" },
                    { 72, "Moldovan" },
                    { 73, "Montenegrin" },
                    { 74, "Nauruan" },
                    { 75, "Ndebele" },
                    { 76, "Nepali" },
                    { 77, "Niuean" },
                    { 78, "Norwegian" },
                    { 79, "Palauan" },
                    { 80, "Pashto" },
                    { 81, "Persian" },
                    { 82, "Polish" },
                    { 83, "Polynesian" },
                    { 84, "Portuguese" },
                    { 85, "Quechua" },
                    { 86, "Regional Chinese" },
                    { 87, "Romanian" },
                    { 88, "Russian" },
                    { 89, "Samoan" },
                    { 90, "Sangho" },
                    { 91, "Sara" },
                    { 92, "Serbian" },
                    { 93, "Shona" },
                    { 94, "Sinhala" },
                    { 95, "Slovak" },
                    { 96, "Slovenian" },
                    { 97, "Somali" },
                    { 98, "Sotho" },
                    { 99, "Spanish" },
                    { 100, "Standard Chinese or Mandarin" },
                    { 101, "Swahili" },
                    { 102, "Swedish" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageID", "Name" },
                values: new object[,]
                {
                    { 103, "Tajik" },
                    { 104, "Tetum" },
                    { 105, "Thai" },
                    { 106, "Tigrinya" },
                    { 107, "Tok Pisin" },
                    { 108, "Tongan" },
                    { 109, "Turkish" },
                    { 110, "Turkmen" },
                    { 111, "Tuvaluan" },
                    { 112, "Ukrainian" },
                    { 113, "Urdu" },
                    { 114, "Uzbek" },
                    { 115, "Vietnamese" },
                    { 116, "siSwati" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityID", "Name" },
                values: new object[,]
                {
                    { 1, "Afghan" },
                    { 2, "Albanian" },
                    { 3, "Andorran" },
                    { 4, "Angolan" },
                    { 5, "Argentine" },
                    { 6, "Armenian" },
                    { 7, "Azerbaijani" },
                    { 8, "Bahamian" },
                    { 9, "Bahraini" },
                    { 10, "Belarusian" },
                    { 11, "Belgian" },
                    { 12, "Bermudian" },
                    { 13, "Bhutanese" },
                    { 14, "Botswanan" },
                    { 15, "Brazilian" },
                    { 16, "Bruneian" },
                    { 17, "Bulgarian" },
                    { 18, "Burundian" },
                    { 19, "Cambodian" },
                    { 20, "Cameroonian" },
                    { 21, "Cayman Islander" },
                    { 22, "Central African" },
                    { 23, "Chinese" },
                    { 24, "Citizen of Seychelles" },
                    { 25, "Citizen of Vanuatu" },
                    { 26, "Colombian" },
                    { 27, "Congolese (DRC)" },
                    { 28, "Cook Islander" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityID", "Name" },
                values: new object[,]
                {
                    { 29, "Cuban" },
                    { 30, "Cymraes" },
                    { 31, "Czech" },
                    { 32, "Danish" },
                    { 33, "Djiboutian" },
                    { 34, "Dutch" },
                    { 35, "East Timorese" },
                    { 36, "Ecuadorean" },
                    { 37, "English" },
                    { 38, "Equatorial Guinean" },
                    { 39, "Ethiopian" },
                    { 40, "Faroese" },
                    { 41, "Fijian" },
                    { 42, "Finnish" },
                    { 43, "French" },
                    { 44, "Gabonese" },
                    { 45, "Gambian" },
                    { 46, "Ghanaian" },
                    { 47, "Gibraltarian" },
                    { 48, "Greek" },
                    { 49, "Grenadian" },
                    { 50, "Guamanian" },
                    { 51, "Guinean" },
                    { 52, "Guyanese" },
                    { 53, "Haitian" },
                    { 54, "Honduran" },
                    { 55, "Icelandic" },
                    { 56, "Indian" },
                    { 57, "Iraqi" },
                    { 58, "Italian" },
                    { 59, "Irish" },
                    { 60, "Ivorian" },
                    { 61, "Jamaican" },
                    { 62, "Japanese" },
                    { 63, "Kazakh" },
                    { 64, "Kenyan" },
                    { 65, "Kosovan" },
                    { 66, "Kuwaiti" },
                    { 67, "Lao" },
                    { 68, "Latvian" },
                    { 69, "Libyan" },
                    { 70, "Liechtenstein citizen" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityID", "Name" },
                values: new object[,]
                {
                    { 71, "Macanese" },
                    { 72, "Macedonian" },
                    { 73, "Malaysian" },
                    { 74, "Maldivian" },
                    { 75, "Marshallese" },
                    { 76, "Martiniquais" },
                    { 77, "Mexican" },
                    { 78, "Micronesian" },
                    { 79, "Mongolian" },
                    { 80, "Montenegrin" },
                    { 81, "Mosotho" },
                    { 82, "Mozambican" },
                    { 83, "Namibian" },
                    { 84, "Nauruan" },
                    { 85, "Nicaraguan" },
                    { 86, "Nigerian" },
                    { 87, "North Korean" },
                    { 88, "Northern Irish" },
                    { 89, "Omani" },
                    { 90, "Pakistani" },
                    { 91, "Palauan" },
                    { 92, "Papua New Guinean" },
                    { 93, "Paraguayan" },
                    { 94, "Polish" },
                    { 95, "Portuguese" },
                    { 96, "Qatari" },
                    { 97, "Romanian" },
                    { 98, "Russian" },
                    { 99, "Salvadorean" },
                    { 100, "Sammarinese" },
                    { 101, "Saudi Arabian" },
                    { 102, "Scottish" },
                    { 103, "Sierra Leonean" },
                    { 104, "Slovenian" },
                    { 105, "Solomon Islander" },
                    { 106, "South Korean" },
                    { 107, "South Sudanese" },
                    { 108, "St Helenian" },
                    { 109, "St Lucian" },
                    { 110, "Surinamese" },
                    { 111, "Swazi" },
                    { 112, "Syrian" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityID", "Name" },
                values: new object[,]
                {
                    { 113, "Taiwanese" },
                    { 114, "Tajik" },
                    { 115, "Togolese" },
                    { 116, "Tongan" },
                    { 117, "Tunisian" },
                    { 118, "Turkish" },
                    { 119, "Tuvaluan" },
                    { 120, "Ugandan" },
                    { 121, "Ukrainian" },
                    { 122, "Vatican citizen" },
                    { 123, "Vincentian" },
                    { 124, "Wallisian" },
                    { 125, "Welsh" },
                    { 126, "Yemeni" },
                    { 127, "Zambian" },
                    { 128, "Zimbabwean" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GuideID",
                table: "Bookings",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourID",
                table: "Bookings",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_GuideLanguages_LanguageID",
                table: "GuideLanguages",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_GenderID",
                table: "Guides",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_TourDestinations_DestinationID",
                table: "TourDestinations",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Tourists_CountryID",
                table: "Tourists",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Tourists_GenderID",
                table: "Tourists",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Tourists_NationalityID",
                table: "Tourists",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_TourParticipants_TouristID",
                table: "TourParticipants",
                column: "TouristID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuideLanguages");

            migrationBuilder.DropTable(
                name: "TourDestinations");

            migrationBuilder.DropTable(
                name: "TourParticipants");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Tourists");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
