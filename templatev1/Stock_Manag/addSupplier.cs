using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Stock_Manag
{
    public partial class addSupplier : Form
    {
        controller.supplierController controller;
        public addSupplier()
        {
            InitializeComponent();
            controller = new controller.supplierController();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (controller.addSupplier(lblSupplierNumber.Text.ToString(), tbName.Text.ToString(), tbPhone.Text.ToString(), tbAddress.Text.ToString(),cmbCountry.Text.ToString()) && tbName.Text.ToString() != "" && tbPhone.Text.ToString() != "" && tbAddress.Text.ToString() != "" && cmbCountry.Text.ToString() != "")
            {
                MessageBox.Show("Supplier added.");
                Form viewSupplier = new viewSupplier();
                this.Hide();
                viewSupplier.StartPosition = FormStartPosition.Manual;
                viewSupplier.Location = this.Location;
                viewSupplier.ShowDialog();
                this.Close();
            }
            else
            {
                if (tbName.Text.ToString() == "" || tbPhone.Text.ToString() == "" || tbAddress.Text.ToString() == "" || cmbCountry.Text.ToString() =="")
                {
                    MessageBox.Show("Please don't leave blank");
                }
                else
                {
                    MessageBox.Show("Please try again.");
                }
            }

        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countCountry = controller.getSupplierNumFromSameCountry(cmbCountry.Text.ToString());
            countCountry++;
            string countryCode = getCountryCode(cmbCountry.Text.ToString());
            string id = $"SID{countryCode}";

            if (countCountry <= 9) {
                id += $"0000{countCountry}";
            }else if (countCountry <= 99)
            {
                id += $"000{countCountry}";
            }else if (countCountry <= 999)
            {
                id += $"00{countCountry}";
            }else if (countCountry <= 9999)
            {
                id += $"0{countCountry}";
            }
            else
            {
                id += $"{countCountry}";
            }
            lblSupplierNumber.Text = id;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("All unsaved change will be lost!\nAre you sure you want to cancel editing?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Form viewSupplier = new viewSupplier();
                this.Hide();
                viewSupplier.StartPosition = FormStartPosition.Manual;
                viewSupplier.Location = this.Location;
                viewSupplier.ShowDialog();
                this.Close();
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private string getCountryCode(string country)
        {
            string cc = "";
            switch (country)
            {
                case "Afghanistan": return "AF";
                case "Albania": return "AL";
                case "Algeria": return "DZ";
                case "Andorra": return "AD";
                case "Angola": return "AO";
                case "Antigua and Barbuda": return "AG";
                case "Argentina": return "AR";
                case "Armenia": return "AM";
                case "Australia": return "AU";
                case "Austria": return "AT";
                case "Azerbaijan": return "AZ";
                case "The Bahamas": return "BS";
                case "Bahrain": return "BH";
                case "Bangladesh": return "BD";
                case "Barbados": return "BB";
                case "Belarus": return "BY";
                case "Belgium": return "BE";
                case "Belize": return "BZ";
                case "Benin": return "BJ";
                case "Bhutan": return "BT";
                case "Bolivia": return "BO";
                case "Bosnia and Herzegovina": return "BA";
                case "Botswana": return "BW";
                case "Brazil": return "BR";
                case "Brunei": return "BN";
                case "Bulgaria": return "BG";
                case "Burkina Faso": return "BF";
                case "Burundi": return "BI";
                case "Cabo Verde": return "CV";
                case "Cambodia": return "KH";
                case "Cameroon": return "CM";
                case "Canada": return "CA";
                case "Central African Republic": return "CF";
                case "Chad": return "TD";
                case "Chile": return "CL";
                case "China": return "CN";
                case "Colombia": return "CO";
                case "Comoros": return "KM";
                case "Congo, Democratic Republic of the": return "CD";
                case "Congo, Republic of the": return "CG";
                case "Costa Rica": return "CR";
                case "Côte d’Ivoire": return "CI";
                case "Croatia": return "HR";
                case "Cuba": return "CU";
                case "Cyprus": return "CY";
                case "Czech Republic": return "CZ";
                case "Denmark": return "DK";
                case "Djibouti": return "DJ";
                case "Dominica": return "DM";
                case "Dominican Republic": return "DO";
                case "East Timor (Timor-Leste)": return "TL";
                case "Ecuador": return "EC";
                case "Egypt": return "EG";
                case "El Salvador": return "SV";
                case "Equatorial Guinea": return "GQ";
                case "Eritrea": return "ER";
                case "Estonia": return "EE";
                case "Eswatini": return "SZ";
                case "Ethiopia": return "ET";
                case "Fiji": return "FJ";
                case "Finland": return "FI";
                case "France": return "FR";
                case "Gabon": return "GA";
                case "The Gambia": return "GM";
                case "Georgia": return "GE";
                case "Germany": return "DE";
                case "Ghana": return "GH";
                case "Greece": return "GR";
                case "Grenada": return "GD";
                case "Guatemala": return "GT";
                case "Guinea": return "GN";
                case "Guinea-Bissau": return "GW";
                case "Guyana": return "GY";
                case "Haiti": return "HT";
                case "Honduras": return "HN";
                case "Hungary": return "HU";
                case "Iceland": return "IS";
                case "India": return "IN";
                case "Indonesia": return "ID";
                case "Iran": return "IR";
                case "Iraq": return "IQ";
                case "Ireland": return "IE";
                case "Israel": return "IL";
                case "Italy": return "IT";
                case "Jamaica": return "JM";
                case "Japan": return "JP";
                case "Jordan": return "JO";
                case "Kazakhstan": return "KZ";
                case "Kenya": return "KE";
                case "Kiribati": return "KI";
                case "North Korea": return "KP";
                case "South Korea": return "KR";
                case "Kosovo": return "XK";
                case "Kuwait": return "KW";
                case "Kyrgyzstan": return "KG";
                case "Laos": return "LA";
                case "Latvia": return "LV";
                case "Lebanon": return "LB";
                case "Lesotho": return "LS";
                case "Liberia": return "LR";
                case "Libya": return "LY";
                case "Liechtenstein": return "LI";
                case "Lithuania": return "LT";
                case "Luxembourg": return "LU";
                case "Madagascar": return "MG";
                case "Malawi": return "MW";
                case "Malaysia": return "MY";
                case "Maldives": return "MV";
                case "Mali": return "ML";
                case "Malta": return "MT";
                case "Marshall Islands": return "MH";
                case "Mauritania": return "MR";
                case "Mauritius": return "MU";
                case "Mexico": return "MX";
                case "Micronesia, Federated States of": return "FM";
                case "Moldova": return "MD";
                case "Monaco": return "MC";
                case "Mongolia": return "MN";
                case "Montenegro": return "ME";
                case "Morocco": return "MA";
                case "Mozambique": return "MZ";
                case "Myanmar (Burma)": return "MM";
                case "Namibia": return "NA";
                case "Nauru": return "NR";
                case "Nepal": return "NP";
                case "Netherlands": return "NL";
                case "New Zealand": return "NZ";
                case "Nicaragua": return "NI";
                case "Niger": return "NE";
                case "Nigeria": return "NG";
                case "North Macedonia": return "MK";
                case "Norway": return "NO";
                case "Oman": return "OM";
                case "Pakistan": return "PK";
                case "Palau": return "PW";
                case "Panama": return "PA";
                case "Papua New Guinea": return "PG";
                case "Paraguay": return "PY";
                case "Peru": return "PE";
                case "Philippines": return "PH";
                case "Poland": return "PL";
                case "Portugal": return "PT";
                case "Qatar": return "QA";
                case "Romania": return "RO";
                case "Russia": return "RU";
                case "Rwanda": return "RW";
                case "Saint Kitts and Nevis": return "KN";
                case "Saint Lucia": return "LC";
                case "Saint Vincent and the Grenadines": return "VC";
                case "Samoa": return "WS";
                case "San Marino": return "SM";
                case "Sao Tome and Principe": return "ST";
                case "Saudi Arabia": return "SA";
                case "Senegal": return "SN";
                case "Serbia": return "RS";
                case "Seychelles": return "SC";
                case "Sierra Leone": return "SL";
                case "Singapore": return "SG";
                case "Slovakia": return "SK";
                case "Slovenia": return "SI";
                case "Solomon Islands": return "SB";
                case "Somalia": return "SO";
                case "South Africa": return "ZA";
                case "Spain": return "ES";
                case "Sri Lanka": return "LK";
                case "Sudan": return "SD";
                case "Sudan, South": return "SS";
                case "Suriname": return "SR";
                case "Sweden": return "SE";
                case "Switzerland": return "CH";
                case "Syria": return "SY";
                case "Tajikistan": return "TJ";
                case "Tanzania": return "TZ";
                case "Thailand": return "TH";
                case "Togo": return "TG";
                case "Tonga": return "TO";
                case "Trinidad and Tobago": return "TT";
                case "Tunisia": return "TN";
                case "Turkey": return "TR";
                case "Turkmenistan": return "TM";
                case "Tuvalu": return "TV";
                case "Uganda": return "UG";
                case "Ukraine": return "UA";
                case "United Arab Emirates": return "AE";
                case "United Kingdom": return "UK";
                case "United States": return "US";
                case "Uruguay": return "UY";
                case "Uzbekistan": return "UZ";
                case "Vanuatu": return "VU";
                case "Vatican City": return "VA";
                case "Venezuela": return "VE";
                case "Vietnam": return "VN";
                case "Yemen": return "YE";
                case "Zambia": return "ZM";
                case "Zimbabwe": return "ZW";
                default: return cc;
            }
        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void addSupplier_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
