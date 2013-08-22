using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3HomeBrewShed.Utils
{
    //-- USAGE INFORMATION --//
    //-- Use in controller
    //-- ViewBag.CountryList = new SelectList(Countries.CountryList(), "Abbreviations", "Name");
    //------------------------------------------------------------------------------------------
    //-- Use in Page
    //-- @Html.DropDownList("StateList", (SelectList)ViewBag.CountryList, "- please select -")

    public class Countries
    {
        public string Name { get; private set; }
        public string TwoLetterCode { get; private set; }
        public string ThreeLetterCode { get; private set; }
        public string NumericCode { get; private set; }
        public string Abbreviations { get; set; }
        static List<Countries> country;

        private Countries(string name, string twoLetterCode, string threeLetterCode, string numericCode)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            NumericCode = numericCode;
        }

        public Countries(string ab, string name)
        {
            Name = name;
            Abbreviations = ab;
        }

        /// <summary>
        /// ISO 3166-1 country codes
        /// Data source: http://en.wikipedia.org/wiki/ISO_3166-1
        /// </summary>
        static Countries()
        {
            #region Country Data
            country = new List<Countries>(248);
                   
            country.Add(new Countries("Afghanistan", "AF", "AFG", "004"));
            country.Add(new Countries("Åland Islands", "AX", "ALA", "248"));
            country.Add(new Countries("Albania", "AL", "ALB", "008"));
            country.Add(new Countries("Algeria", "DZ", "DZA", "012"));
            country.Add(new Countries("American Samoa", "AS", "ASM", "016"));
            country.Add(new Countries("Andorra", "AD", "AND", "020"));
            country.Add(new Countries("Angola", "AO", "AGO", "024"));
            country.Add(new Countries("Anguilla", "AI", "AIA", "660"));
            country.Add(new Countries("Antarctica", "AQ", "ATA", "010"));
            country.Add(new Countries("Antigua and Barbuda", "AG", "ATG", "028"));
            country.Add(new Countries("Argentina", "AR", "ARG", "032"));
            country.Add(new Countries("Armenia", "AM", "ARM", "051"));
            country.Add(new Countries("Aruba", "AW", "ABW", "533"));
            country.Add(new Countries("Australia", "AU", "AUS", "036"));
            country.Add(new Countries("Austria", "AT", "AUT", "040"));
            country.Add(new Countries("Azerbaijan", "AZ", "AZE", "031"));
            country.Add(new Countries("Bahamas", "BS", "BHS", "044"));
            country.Add(new Countries("Bahrain", "BH", "BHR", "048"));
            country.Add(new Countries("Bangladesh", "BD", "BGD", "050"));
            country.Add(new Countries("Barbados", "BB", "BRB", "052"));
            country.Add(new Countries("Belarus", "BY", "BLR", "112"));
            country.Add(new Countries("Belgium", "BE", "BEL", "056"));
            country.Add(new Countries("Belize", "BZ", "BLZ", "084"));
            country.Add(new Countries("Benin", "BJ", "BEN", "204"));
            country.Add(new Countries("Bermuda", "BM", "BMU", "060"));
            country.Add(new Countries("Bhutan", "BT", "BTN", "064"));
            country.Add(new Countries("Bolivia, Plurinational State of", "BO", "BOL", "068"));
            country.Add(new Countries("Bonaire, Sint Eustatius and Saba", "BQ", "BES", "535"));
            country.Add(new Countries("Bosnia and Herzegovina", "BA", "BIH", "070"));
            country.Add(new Countries("Botswana", "BW", "BWA", "072"));
            country.Add(new Countries("Bouvet Island", "BV", "BVT", "074"));
            country.Add(new Countries("Brazil", "BR", "BRA", "076"));
            country.Add(new Countries("British Indian Ocean Territory", "IO", "IOT", "086"));
            country.Add(new Countries("Brunei Darussalam", "BN", "BRN", "096"));
            country.Add(new Countries("Bulgaria", "BG", "BGR", "100"));
            country.Add(new Countries("Burkina Faso", "BF", "BFA", "854"));
            country.Add(new Countries("Burundi", "BI", "BDI", "108"));
            country.Add(new Countries("Cambodia", "KH", "KHM", "116"));
            country.Add(new Countries("Cameroon", "CM", "CMR", "120"));
            country.Add(new Countries("Canada", "CA", "CAN", "124"));
            country.Add(new Countries("Cape Verde", "CV", "CPV", "132"));
            country.Add(new Countries("Cayman Islands", "KY", "CYM", "136"));
            country.Add(new Countries("Central African Republic", "CF", "CAF", "140"));
            country.Add(new Countries("Chad", "TD", "TCD", "148"));
            country.Add(new Countries("Chile", "CL", "CHL", "152"));
            country.Add(new Countries("China", "CN", "CHN", "156"));
            country.Add(new Countries("Christmas Island", "CX", "CXR", "162"));
            country.Add(new Countries("Cocos (Keeling) Islands", "CC", "CCK", "166"));
            country.Add(new Countries("Colombia", "CO", "COL", "170"));
            country.Add(new Countries("Comoros", "KM", "COM", "174"));
            country.Add(new Countries("Congo", "CG", "COG", "178"));
            country.Add(new Countries("Congo, the Democratic Republic of the", "CD", "COD", "180"));
            country.Add(new Countries("Cook Islands", "CK", "COK", "184"));
            country.Add(new Countries("Costa Rica", "CR", "CRI", "188"));
            country.Add(new Countries("Côte d'Ivoire", "CI", "CIV", "384"));
            country.Add(new Countries("Croatia", "HR", "HRV", "191"));
            country.Add(new Countries("Cuba", "CU", "CUB", "192"));
            country.Add(new Countries("Curaçao", "CW", "CUW", "531"));
            country.Add(new Countries("Cyprus", "CY", "CYP", "196"));
            country.Add(new Countries("Czech Republic", "CZ", "CZE", "203"));
            country.Add(new Countries("Denmark", "DK", "DNK", "208"));
            country.Add(new Countries("Djibouti", "DJ", "DJI", "262"));
            country.Add(new Countries("Dominica", "DM", "DMA", "212"));
            country.Add(new Countries("Dominican Republic", "DO", "DOM", "214"));
            country.Add(new Countries("Ecuador", "EC", "ECU", "218"));
            country.Add(new Countries("Egypt", "EG", "EGY", "818"));
            country.Add(new Countries("El Salvador", "SV", "SLV", "222"));
            country.Add(new Countries("Equatorial Guinea", "GQ", "GNQ", "226"));
            country.Add(new Countries("Eritrea", "ER", "ERI", "232"));
            country.Add(new Countries("Estonia", "EE", "EST", "233"));
            country.Add(new Countries("Ethiopia", "ET", "ETH", "231"));
            country.Add(new Countries("Falkland Islands (Malvinas)", "FK", "FLK", "238"));
            country.Add(new Countries("Faroe Islands", "FO", "FRO", "234"));
            country.Add(new Countries("Fiji", "FJ", "FJI", "242"));
            country.Add(new Countries("Finland", "FI", "FIN", "246"));
            country.Add(new Countries("France", "FR", "FRA", "250"));
            country.Add(new Countries("French Guiana", "GF", "GUF", "254"));
            country.Add(new Countries("French Polynesia", "PF", "PYF", "258"));
            country.Add(new Countries("French Southern Territories", "TF", "ATF", "260"));
            country.Add(new Countries("Gabon", "GA", "GAB", "266"));
            country.Add(new Countries("Gambia", "GM", "GMB", "270"));
            country.Add(new Countries("Georgia", "GE", "GEO", "268"));
            country.Add(new Countries("Germany", "DE", "DEU", "276"));
            country.Add(new Countries("Ghana", "GH", "GHA", "288"));
            country.Add(new Countries("Gibraltar", "GI", "GIB", "292"));
            country.Add(new Countries("Greece", "GR", "GRC", "300"));
            country.Add(new Countries("Greenland", "GL", "GRL", "304"));
            country.Add(new Countries("Grenada", "GD", "GRD", "308"));
            country.Add(new Countries("Guadeloupe", "GP", "GLP", "312"));
            country.Add(new Countries("Guam", "GU", "GUM", "316"));
            country.Add(new Countries("Guatemala", "GT", "GTM", "320"));
            country.Add(new Countries("Guernsey", "GG", "GGY", "831"));
            country.Add(new Countries("Guinea", "GN", "GIN", "324"));
            country.Add(new Countries("Guinea-Bissau", "GW", "GNB", "624"));
            country.Add(new Countries("Guyana", "GY", "GUY", "328"));
            country.Add(new Countries("Haiti", "HT", "HTI", "332"));
            country.Add(new Countries("Heard Island and McDonald Islands", "HM", "HMD", "334"));
            country.Add(new Countries("Holy See (Vatican City State)", "VA", "VAT", "336"));
            country.Add(new Countries("Honduras", "HN", "HND", "340"));
            country.Add(new Countries("Hong Kong", "HK", "HKG", "344"));
            country.Add(new Countries("Hungary", "HU", "HUN", "348"));
            country.Add(new Countries("Iceland", "IS", "ISL", "352"));
            country.Add(new Countries("India", "IN", "IND", "356"));
            country.Add(new Countries("Indonesia", "ID", "IDN", "360"));
            country.Add(new Countries("Iran, Islamic Republic of", "IR", "IRN", "364"));
            country.Add(new Countries("Iraq", "IQ", "IRQ", "368"));
            country.Add(new Countries("Ireland", "IE", "IRL", "372"));
            country.Add(new Countries("Isle of Man", "IM", "IMN", "833"));
            country.Add(new Countries("Israel", "IL", "ISR", "376"));
            country.Add(new Countries("Italy", "IT", "ITA", "380"));
            country.Add(new Countries("Jamaica", "JM", "JAM", "388"));
            country.Add(new Countries("Japan", "JP", "JPN", "392"));
            country.Add(new Countries("Jersey", "JE", "JEY", "832"));
            country.Add(new Countries("Jordan", "JO", "JOR", "400"));
            country.Add(new Countries("Kazakhstan", "KZ", "KAZ", "398"));
            country.Add(new Countries("Kenya", "KE", "KEN", "404"));
            country.Add(new Countries("Kiribati", "KI", "KIR", "296"));
            country.Add(new Countries("Korea, Democratic People's Republic of", "KP", "PRK", "408"));
            country.Add(new Countries("Korea, Republic of", "KR", "KOR", "410"));
            country.Add(new Countries("Kuwait", "KW", "KWT", "414"));
            country.Add(new Countries("Kyrgyzstan", "KG", "KGZ", "417"));
            country.Add(new Countries("Lao People's Democratic Republic", "LA", "LAO", "418"));
            country.Add(new Countries("Latvia", "LV", "LVA", "428"));
            country.Add(new Countries("Lebanon", "LB", "LBN", "422"));
            country.Add(new Countries("Lesotho", "LS", "LSO", "426"));
            country.Add(new Countries("Liberia", "LR", "LBR", "430"));
            country.Add(new Countries("Libya", "LY", "LBY", "434"));
            country.Add(new Countries("Liechtenstein", "LI", "LIE", "438"));
            country.Add(new Countries("Lithuania", "LT", "LTU", "440"));
            country.Add(new Countries("Luxembourg", "LU", "LUX", "442"));
            country.Add(new Countries("Macao", "MO", "MAC", "446"));
            country.Add(new Countries("Macedonia, the former Yugoslav Republic of", "MK", "MKD", "807"));
            country.Add(new Countries("Madagascar", "MG", "MDG", "450"));
            country.Add(new Countries("Malawi", "MW", "MWI", "454"));
            country.Add(new Countries("Malaysia", "MY", "MYS", "458"));
            country.Add(new Countries("Maldives", "MV", "MDV", "462"));
            country.Add(new Countries("Mali", "ML", "MLI", "466"));
            country.Add(new Countries("Malta", "MT", "MLT", "470"));
            country.Add(new Countries("Marshall Islands", "MH", "MHL", "584"));
            country.Add(new Countries("Martinique", "MQ", "MTQ", "474"));
            country.Add(new Countries("Mauritania", "MR", "MRT", "478"));
            country.Add(new Countries("Mauritius", "MU", "MUS", "480"));
            country.Add(new Countries("Mayotte", "YT", "MYT", "175"));
            country.Add(new Countries("Mexico", "MX", "MEX", "484"));
            country.Add(new Countries("Micronesia, Federated States of", "FM", "FSM", "583"));
            country.Add(new Countries("Moldova, Republic of", "MD", "MDA", "498"));
            country.Add(new Countries("Monaco", "MC", "MCO", "492"));
            country.Add(new Countries("Mongolia", "MN", "MNG", "496"));
            country.Add(new Countries("Montenegro", "ME", "MNE", "499"));
            country.Add(new Countries("Montserrat", "MS", "MSR", "500"));
            country.Add(new Countries("Morocco", "MA", "MAR", "504"));
            country.Add(new Countries("Mozambique", "MZ", "MOZ", "508"));
            country.Add(new Countries("Myanmar", "MM", "MMR", "104"));
            country.Add(new Countries("Namibia", "NA", "NAM", "516"));
            country.Add(new Countries("Nauru", "NR", "NRU", "520"));
            country.Add(new Countries("Nepal", "NP", "NPL", "524"));
            country.Add(new Countries("Netherlands", "NL", "NLD", "528"));
            country.Add(new Countries("New Caledonia", "NC", "NCL", "540"));
            country.Add(new Countries("New Zealand", "NZ", "NZL", "554"));
            country.Add(new Countries("Nicaragua", "NI", "NIC", "558"));
            country.Add(new Countries("Niger", "NE", "NER", "562"));
            country.Add(new Countries("Nigeria", "NG", "NGA", "566"));
            country.Add(new Countries("Niue", "NU", "NIU", "570"));
            country.Add(new Countries("Norfolk Island", "NF", "NFK", "574"));
            country.Add(new Countries("Northern Mariana Islands", "MP", "MNP", "580"));
            country.Add(new Countries("Norway", "NO", "NOR", "578"));
            country.Add(new Countries("Oman", "OM", "OMN", "512"));
            country.Add(new Countries("Pakistan", "PK", "PAK", "586"));
            country.Add(new Countries("Palau", "PW", "PLW", "585"));
            country.Add(new Countries("Palestinian Territory, Occupied", "PS", "PSE", "275"));
            country.Add(new Countries("Panama", "PA", "PAN", "591"));
            country.Add(new Countries("Papua New Guinea", "PG", "PNG", "598"));
            country.Add(new Countries("Paraguay", "PY", "PRY", "600"));
            country.Add(new Countries("Peru", "PE", "PER", "604"));
            country.Add(new Countries("Philippines", "PH", "PHL", "608"));
            country.Add(new Countries("Pitcairn", "PN", "PCN", "612"));
            country.Add(new Countries("Poland", "PL", "POL", "616"));
            country.Add(new Countries("Portugal", "PT", "PRT", "620"));
            country.Add(new Countries("Puerto Rico", "PR", "PRI", "630"));
            country.Add(new Countries("Qatar", "QA", "QAT", "634"));
            country.Add(new Countries("Réunion", "RE", "REU", "638"));
            country.Add(new Countries("Romania", "RO", "ROU", "642"));
            country.Add(new Countries("Russian Federation", "RU", "RUS", "643"));
            country.Add(new Countries("Rwanda", "RW", "RWA", "646"));
            country.Add(new Countries("Saint Barthélemy", "BL", "BLM", "652"));
            country.Add(new Countries("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", "654"));
            country.Add(new Countries("Saint Kitts and Nevis", "KN", "KNA", "659"));
            country.Add(new Countries("Saint Lucia", "LC", "LCA", "662"));
            country.Add(new Countries("Saint Martin (French part)", "MF", "MAF", "663"));
            country.Add(new Countries("Saint Pierre and Miquelon", "PM", "SPM", "666"));
            country.Add(new Countries("Saint Vincent and the Grenadines", "VC", "VCT", "670"));
            country.Add(new Countries("Samoa", "WS", "WSM", "882"));
            country.Add(new Countries("San Marino", "SM", "SMR", "674"));
            country.Add(new Countries("Sao Tome and Principe", "ST", "STP", "678"));
            country.Add(new Countries("Saudi Arabia", "SA", "SAU", "682"));
            country.Add(new Countries("Senegal", "SN", "SEN", "686"));
            country.Add(new Countries("Serbia", "RS", "SRB", "688"));
            country.Add(new Countries("Seychelles", "SC", "SYC", "690"));
            country.Add(new Countries("Sierra Leone", "SL", "SLE", "694"));
            country.Add(new Countries("Singapore", "SG", "SGP", "702"));
            country.Add(new Countries("Sint Maarten (Dutch part)", "SX", "SXM", "534"));
            country.Add(new Countries("Slovakia", "SK", "SVK", "703"));
            country.Add(new Countries("Slovenia", "SI", "SVN", "705"));
            country.Add(new Countries("Solomon Islands", "SB", "SLB", "090"));
            country.Add(new Countries("Somalia", "SO", "SOM", "706"));
            country.Add(new Countries("South Africa", "ZA", "ZAF", "710"));
            country.Add(new Countries("South Georgia and the South Sandwich Islands", "GS", "SGS", "239"));
            country.Add(new Countries("South Sudan", "SS", "SSD", "728"));
            country.Add(new Countries("Spain", "ES", "ESP", "724"));
            country.Add(new Countries("Sri Lanka", "LK", "LKA", "144"));
            country.Add(new Countries("Sudan", "SD", "SDN", "729"));
            country.Add(new Countries("Suriname", "SR", "SUR", "740"));
            country.Add(new Countries("Svalbard and Jan Mayen", "SJ", "SJM", "744"));
            country.Add(new Countries("Swaziland", "SZ", "SWZ", "748"));
            country.Add(new Countries("Sweden", "SE", "SWE", "752"));
            country.Add(new Countries("Switzerland", "CH", "CHE", "756"));
            country.Add(new Countries("Syrian Arab Republic", "SY", "SYR", "760"));
            country.Add(new Countries("Taiwan, Province of China", "TW", "TWN", "158"));
            country.Add(new Countries("Tajikistan", "TJ", "TJK", "762"));
            country.Add(new Countries("Tanzania, United Republic of", "TZ", "TZA", "834"));
            country.Add(new Countries("Thailand", "TH", "THA", "764"));
            country.Add(new Countries("Timor-Leste", "TL", "TLS", "626"));
            country.Add(new Countries("Togo", "TG", "TGO", "768"));
            country.Add(new Countries("Tokelau", "TK", "TKL", "772"));
            country.Add(new Countries("Tonga", "TO", "TON", "776"));
            country.Add(new Countries("Trinidad and Tobago", "TT", "TTO", "780"));
            country.Add(new Countries("Tunisia", "TN", "TUN", "788"));
            country.Add(new Countries("Turkey", "TR", "TUR", "792"));
            country.Add(new Countries("Turkmenistan", "TM", "TKM", "795"));
            country.Add(new Countries("Turks and Caicos Islands", "TC", "TCA", "796"));
            country.Add(new Countries("Tuvalu", "TV", "TUV", "798"));
            country.Add(new Countries("Uganda", "UG", "UGA", "800"));
            country.Add(new Countries("Ukraine", "UA", "UKR", "804"));
            country.Add(new Countries("United Arab Emirates", "AE", "ARE", "784"));
            country.Add(new Countries("United Kingdom", "GB", "GBR", "826"));
            country.Add(new Countries("United States", "US", "USA", "840"));
            country.Add(new Countries("United States Minor Outlying Islands", "UM", "UMI", "581"));
            country.Add(new Countries("Uruguay", "UY", "URY", "858"));
            country.Add(new Countries("Uzbekistan", "UZ", "UZB", "860"));
            country.Add(new Countries("Vanuatu", "VU", "VUT", "548"));
            country.Add(new Countries("Venezuela, Bolivarian Republic of", "VE", "VEN", "862"));
            country.Add(new Countries("Viet Nam", "VN", "VNM", "704"));
            country.Add(new Countries("Virgin Islands, British", "VG", "VGB", "092"));
            country.Add(new Countries("Virgin Islands, U.S.", "VI", "VIR", "850"));
            country.Add(new Countries("Wallis and Futuna", "WF", "WLF", "876"));
            country.Add(new Countries("Western Sahara", "EH", "ESH", "732"));
            country.Add(new Countries("Yemen", "YE", "YEM", "887"));
            country.Add(new Countries("Zambia", "ZM", "ZMB", "894"));
            country.Add(new Countries("Zimbabwe", "ZW", "ZWE", "716"));
            #endregion
        }

        /// <summary>
        /// CountryList
        /// </summary>
        /// <returns>list of countries with 2 letter codes</returns>
        public static List<Countries> CountryList()
        {
            List<Countries> countryList = new List<Countries>(country.Count);
            foreach (var item in country)
            {
                countryList.Add(new Countries(item.TwoLetterCode,item.Name));             
                
            }
            return countryList;
        }

        /// <summary>
        /// CountryThreeCodeList
        /// </summary>
        /// <returns>list of countries with 3 letter codes</returns>
        public static List<Countries> CountryThreeCodeList()
        {
            List<Countries> countryList = new List<Countries>(country.Count);
            foreach (var item in country)
            {
                countryList.Add(new Countries(item.ThreeLetterCode, item.Name));
            }
            return countryList;
        }

        /// <summary>
        /// CountryNumericCodeList
        /// </summary>
        /// <returns>list of countries with numeric codes</returns>
        public static List<Countries> CountryNumericCodeList()
        {
            List<Countries> countryList = new List<Countries>(country.Count);
            foreach (var item in country)
            {
                countryList.Add(new Countries(item.NumericCode, item.Name));
            }
            return countryList;
        }

    }


}