namespace EZMoney.Models
{
    public class Company
    {
        /// <summary>
        /// Id of the company
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        public string name { get; set; } = "";

        /// <summary>
        /// Website of the company
        /// </summary>
        public string website { get; set; } = "";

        /// <summary>
        /// Logo of the company
        /// </summary>
        public string logoUrl { get; set; } = "";

        /// <summary>
        /// First line address of the company
        /// </summary>
        public string address1 { get; set; } = "";

        /// <summary>
        /// Second line address of the company
        /// </summary>
        public string address2 { get; set; } = "";

        /// <summary>
        /// City of the company
        /// </summary>
        public string city { get; set; } = "";

        /// <summary>
        /// State of the company
        /// </summary>
        public string state { get; set; } = "";

        /// <summary>
        /// Zip code of the company
        /// </summary>
        public string zip { get; set; } = "";

        /// <summary>
        /// Employer Identification Number of the company
        /// </summary>
        public string ein { get; set; } = "";

        public Company()
        {

        }

        public Company(int id, string name, string website, string logoUrl, string address1, string address2, string city, string state, string zip, string ein)
        {
            this.id = id;
            this.name = name;
            this.website = website;
            this.logoUrl = logoUrl;
            this.address1 = address1;
            this.address2 = address2;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.ein = ein;
        }

        /// <summary>
        /// Save the company in the database
        /// </summary>
        public bool save()
        {
            return true;
        }

        /// <summary>
        /// Update the company in the database
        /// </summary>
        public bool update()
        {
            return true;
        }

        /// <summary>
        /// Delete the company in the database
        /// </summary>
        public bool delete()
        {
            return true;
        }
    }
}
