using System;
using System.Collections.Generic;
using System.Data; //must include in every controller file

//must include in every controller file 

namespace controller
{
    public class supplierController : abstractController
    {
        private static readonly Dictionary<string, string> CountryCodes = new Dictionary<string, string>
        {
            { "Afghanistan", "AF" },
            { "Albania", "AL" },
            { "Algeria", "DZ" },
            { "Andorra", "AD" },
            { "Angola", "AO" },
            { "Antigua and Barbuda", "AG" },
            { "Argentina", "AR" },
            { "Armenia", "AM" },
            { "Australia", "AU" },
            { "Austria", "AT" },
            { "Azerbaijan", "AZ" },
            { "The Bahamas", "BS" },
            { "Bahrain", "BH" },
            { "Bangladesh", "BD" },
            { "Barbados", "BB" },
            { "Belarus", "BY" },
            { "Belgium", "BE" },
            { "Belize", "BZ" },
            { "Benin", "BJ" },
            { "Bhutan", "BT" },
            { "Bolivia", "BO" },
            { "Bosnia and Herzegovina", "BA" },
            { "Botswana", "BW" },
            { "Brazil", "BR" },
            { "Brunei", "BN" },
            { "Bulgaria", "BG" },
            { "Burkina Faso", "BF" },
            { "Burundi", "BI" },
            { "Cabo Verde", "CV" },
            { "Cambodia", "KH" },
            { "Cameroon", "CM" },
            { "Canada", "CA" },
            { "Central African Republic", "CF" },
            { "Chad", "TD" },
            { "Chile", "CL" },
            { "China", "CN" },
            { "Colombia", "CO" },
            { "Comoros", "KM" },
            { "Congo, Democratic Republic of the", "CD" },
            { "Congo, Republic of the", "CG" },
            { "Costa Rica", "CR" },
            { "Côte d’Ivoire", "CI" },
            { "Croatia", "HR" },
            { "Cuba", "CU" },
            { "Cyprus", "CY" },
            { "Czech Republic", "CZ" },
            { "Denmark", "DK" },
            { "Djibouti", "DJ" },
            { "Dominica", "DM" },
            { "Dominican Republic", "DO" },
            { "East Timor (Timor-Leste)", "TL" },
            { "Ecuador", "EC" },
            { "Egypt", "EG" },
            { "El Salvador", "SV" },
            { "Equatorial Guinea", "GQ" },
            { "Eritrea", "ER" },
            { "Estonia", "EE" },
            { "Eswatini", "SZ" },
            { "Ethiopia", "ET" },
            { "Fiji", "FJ" },
            { "Finland", "FI" },
            { "France", "FR" },
            { "Gabon", "GA" },
            { "The Gambia", "GM" },
            { "Georgia", "GE" },
            { "Germany", "DE" },
            { "Ghana", "GH" },
            { "Greece", "GR" },
            { "Grenada", "GD" },
            { "Guatemala", "GT" },
            { "Guinea", "GN" },
            { "Guinea-Bissau", "GW" },
            { "Guyana", "GY" },
            { "Haiti", "HT" },
            { "Honduras", "HN" },
            { "Hungary", "HU" },
            { "Iceland", "IS" },
            { "India", "IN" },
            { "Indonesia", "ID" },
            { "Iran", "IR" },
            { "Iraq", "IQ" },
            { "Ireland", "IE" },
            { "Israel", "IL" },
            { "Italy", "IT" },
            { "Jamaica", "JM" },
            { "Japan", "JP" },
            { "Jordan", "JO" },
            { "Kazakhstan", "KZ" },
            { "Kenya", "KE" },
            { "Kiribati", "KI" },
            { "North Korea", "KP" },
            { "South Korea", "KR" },
            { "Kosovo", "XK" },
            { "Kuwait", "KW" },
            { "Kyrgyzstan", "KG" },
            { "Laos", "LA" },
            { "Latvia", "LV" },
            { "Lebanon", "LB" },
            { "Lesotho", "LS" },
            { "Liberia", "LR" },
            { "Libya", "LY" },
            { "Liechtenstein", "LI" },
            { "Lithuania", "LT" },
            { "Luxembourg", "LU" },
            { "Madagascar", "MG" },
            { "Malawi", "MW" },
            { "Malaysia", "MY" },
            { "Maldives", "MV" },
            { "Mali", "ML" },
            { "Malta", "MT" },
            { "Marshall Islands", "MH" },
            { "Mauritania", "MR" },
            { "Mauritius", "MU" },
            { "Mexico", "MX" },
            { "Micronesia, Federated States of", "FM" },
            { "Moldova", "MD" },
            { "Monaco", "MC" },
            { "Mongolia", "MN" },
            { "Montenegro", "ME" },
            { "Morocco", "MA" },
            { "Mozambique", "MZ" },
            { "Myanmar (Burma)", "MM" },
            { "Namibia", "NA" },
            { "Nauru", "NR" },
            { "Nepal", "NP" },
            { "Netherlands", "NL" },
            { "New Zealand", "NZ" },
            { "Nicaragua", "NI" },
            { "Niger", "NE" },
            { "Nigeria", "NG" },
            { "North Macedonia", "MK" },
            { "Norway", "NO" },
            { "Oman", "OM" },
            { "Pakistan", "PK" },
            { "Palau", "PW" },
            { "Panama", "PA" },
            { "Papua New Guinea", "PG" },
            { "Paraguay", "PY" },
            { "Peru", "PE" },
            { "Philippines", "PH" },
            { "Poland", "PL" },
            { "Portugal", "PT" },
            { "Qatar", "QA" },
            { "Romania", "RO" },
            { "Russia", "RU" },
            { "Rwanda", "RW" },
            { "Saint Kitts and Nevis", "KN" },
            { "Saint Lucia", "LC" },
            { "Saint Vincent and the Grenadines", "VC" },
            { "Samoa", "WS" },
            { "San Marino", "SM" },
            { "Sao Tome and Principe", "ST" },
            { "Saudi Arabia", "SA" },
            { "Senegal", "SN" },
            { "Serbia", "RS" },
            { "Seychelles", "SC" },
            { "Sierra Leone", "SL" },
            { "Singapore", "SG" },
            { "Slovakia", "SK" },
            { "Slovenia", "SI" },
            { "Solomon Islands", "SB" },
            { "Somalia", "SO" },
            { "South Africa", "ZA" },
            { "Spain", "ES" },
            { "Sri Lanka", "LK" },
            { "Sudan", "SD" },
            { "Sudan, South", "SS" },
            { "Suriname", "SR" },
            { "Sweden", "SE" },
            { "Switzerland", "CH" },
            { "Syria", "SY" },
            { "Tajikistan", "TJ" },
            { "Tanzania", "TZ" },
            { "Thailand", "TH" },
            { "Togo", "TG" },
            { "Tonga", "TO" },
            { "Trinidad and Tobago", "TT" },
            { "Tunisia", "TN" },
            { "Turkey", "TR" },
            { "Turkmenistan", "TM" },
            { "Tuvalu", "TV" },
            { "Uganda", "UG" },
            { "Ukraine", "UA" },
            { "United Arab Emirates", "AE" },
            { "United Kingdom", "UK" },
            { "United States", "US" },
            { "Uruguay", "UY" },
            { "Uzbekistan", "UZ" },
            { "Vanuatu", "VU" },
            { "Vatican City", "VA" },
            { "Venezuela", "VE" },
            { "Vietnam", "VN" },
            { "Yemen", "YE" },
            { "Zambia", "ZM" },
            { "Zimbabwe", "ZW" }
        };

        private Database _db = new Database();

        public DataTable GetSupplierList() =>
            ExecuteQuery("SELECT * FROM supplier");

        public int CountSupplier() =>
            int.Parse(ExecuteQuery("SELECT COUNT(*) FROM supplier").Rows[0][0].ToString());

        public Boolean DeleteSupplier(string id)
        {
            try
            {
                _ = _db.ExecuteNonQueryCommandAsync($"DELETE FROM supplier WHERE supplierID = '{id}'",
                    null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetSupplierName(string id) =>
            ExecuteQuery($"SELECT name FROM supplier WHERE supplierID = '{id}'").Rows[0][0].ToString();

        public string GetSupplierCountry(string id) =>
            ExecuteQuery($"SELECT country FROM supplier WHERE supplierID = '{id}'").Rows[0][0].ToString();

        public string GetSupplierPhone(string id) =>
            ExecuteQuery($"SELECT phone FROM supplier WHERE supplierID = '{id}'").Rows[0][0].ToString();

        public string GetSupplierAddress(string id) =>
            ExecuteQuery($"SELECT address FROM supplier WHERE supplierID = '{id}'").Rows[0][0].ToString();

        public Boolean UpdateSupplier(string id, string name, string phone, string address)
        {
            try
            {
                _ = _db.ExecuteNonQueryCommandAsync(
                    "UPDATE supplier SET name = @name, phone = @phone, address = @address WHERE supplierID = @id",
                    new Dictionary<string, object>
                        { { "@name", name }, { "@phone", phone }, { "@address", address }, { "@id", id } });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetSupplierNumFromSameCountry(string country) =>
            int.Parse(ExecuteQuery($"SELECT COUNT(*) FROM supplier WHERE country = '{country}'").Rows[0][0].ToString());

        public Boolean AddSupplier(string id, string name, string phone, string address, string country)
        {
            try
            {
                _ = _db.ExecuteNonQueryCommandAsync(
                    "INSERT INTO supplier VALUES(@id, @name, @phone, @address, @country)",
                    new Dictionary<string, object>
                    {
                        { "@id", id }, { "@name", name }, { "@phone", phone }, { "@address", address },
                        { "@country", country }
                    });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetCountryCode(string country)
        {
            return CountryCodes.ContainsKey(country) ? CountryCodes[country] : CountryCodes["default"];
        }

        private DataTable ExecuteQuery(string sqlQuery) =>
            _db.ExecuteDataTableAsync(sqlQuery).Result;
    }
}