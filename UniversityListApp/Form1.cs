using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UniversityListApp
{
    public partial class Form1 : Form
    {
        private List<UniversityData> universities;
        private List<CountryData> countriesData;

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUniversityData();
            UpdateCountryData();

            CountriesDataGridView.DataSource = countriesData;
        }

        private void UpdateCountryData()
        {
            // countriesData.Clear();
            foreach (var country in countriesData)
            {
                country.SecureUniversityCount = 0;
                country.InsecureUniversityCount = 0;
            }

            foreach (var university in universities)
            {
                var countryData = countriesData.Find(c => c.CountryName == university.country);
                if (countryData != null)
                {
                    countryData.UniversityCount++;

                    if (university.web_pages.Any(url => url.StartsWith("https://")))
                    {
                        countryData.SecureUniversityCount++;
                    }
                    else
                    {
                        countryData.InsecureUniversityCount++;
                    }
                }
            }
        }

        private void LoadUniversityData()
        {
            string apiUrl = "http://universities.hipolabs.com/search";
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(apiUrl);
                universities = JsonConvert.DeserializeObject<List<UniversityData>>(json);
            }

            countriesData = ProcessUniversityData();
            
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            var countryWithLeastInsecure = countriesData.OrderBy(c => c.InsecureUniversityCount).First();
            var countryWithMostInsecure = countriesData.OrderByDescending(c => c.InsecureUniversityCount).First();
            var countryWithHighestInsecurePercentage = countriesData.OrderByDescending(c => (double)c.InsecureUniversityCount / c.UniversityCount * 100).First();
            var countryWithLowestInsecurePercentage = countriesData.OrderBy(c => (double)c.InsecureUniversityCount / c.UniversityCount * 100).First();

            statisticsLabel.Text = $"מדינה עם הכי מעט אוניברסיטאות לא מאובטחות: {countryWithLeastInsecure.CountryName} ({countryWithLeastInsecure.InsecureUniversityCount})\n" +
                                  $"מדינה עם הכי הרבה אוניברסיטאות לא מאובטחות: {countryWithMostInsecure.CountryName} ({countryWithMostInsecure.InsecureUniversityCount})\n" +
                                  $"מדינה עם האחוז הגבוה ביותר של אוניברסיטאות לא מאובטחות: {countryWithHighestInsecurePercentage.CountryName} ({countryWithHighestInsecurePercentage.InsecureUniversityCount} מתוך {countryWithHighestInsecurePercentage.UniversityCount}, {countryWithHighestInsecurePercentage.InsecureUniversityCount / countryWithHighestInsecurePercentage.UniversityCount * 100:0.00}%)\n" +
                                  $"מדינה עם האחוז הנמוך ביותר של אוניברסיטאות לא מאובטחות: {countryWithLowestInsecurePercentage.CountryName} ({countryWithLowestInsecurePercentage.InsecureUniversityCount} מתוך {countryWithLowestInsecurePercentage.UniversityCount}, {countryWithLowestInsecurePercentage.InsecureUniversityCount / countryWithLowestInsecurePercentage.UniversityCount * 100:0.00}%)";
        }

        private List<CountryData> ProcessUniversityData()
        {
            var groupedData = universities.GroupBy(u => u.country)
                                           .Select(g => new CountryData
                                           {
                                               CountryName = g.Key,
                                               CountryCode = g.First().alpha_two_code,
                                               UniversityCount = g.Count(),
                                               SecureUniversityCount = g.Count(u => u.web_pages.Any(url => url.StartsWith("https://"))),
                                               InsecureUniversityCount = g.Count(u => !u.web_pages.Any(url => url.StartsWith("https://")))
                                           });

            var countryWithLeastInsecure = groupedData.OrderBy(c => c.InsecureUniversityCount).First();
            var countryWithMostInsecure = groupedData.OrderByDescending(c => c.InsecureUniversityCount).First();
            var countryWithHighestInsecurePercentage = groupedData.OrderByDescending(c => (double)c.InsecureUniversityCount / c.UniversityCount * 100).First();
            var countryWithLowestInsecurePercentage = groupedData.OrderBy(c => (double)c.InsecureUniversityCount / c.UniversityCount * 100).First();

            statisticsLabel.Text = $"מדינה עם הכי מעט אוניברסיטאות לא מאובטחות: {countryWithLeastInsecure.CountryName} ({countryWithLeastInsecure.InsecureUniversityCount})\n" +
                                  $"מדינה עם הכי הרבה אוניברסיטאות לא מאובטחות: {countryWithMostInsecure.CountryName} ({countryWithMostInsecure.InsecureUniversityCount})\n" +
                                  $"מדינה עם האחוז הגבוה ביותר של אוניברסיטאות לא מאובטחות: {countryWithHighestInsecurePercentage.CountryName} ({countryWithHighestInsecurePercentage.InsecureUniversityCount} מתוך {countryWithHighestInsecurePercentage.UniversityCount}, {countryWithHighestInsecurePercentage.InsecureUniversityCount / countryWithHighestInsecurePercentage.UniversityCount * 100:0.00}%)\n" +
                                  $"מדינה עם האחוז הנמוך ביותר של אוניברסיטאות לא מאובטחות: {countryWithLowestInsecurePercentage.CountryName} ({countryWithLowestInsecurePercentage.InsecureUniversityCount} מתוך {countryWithLowestInsecurePercentage.UniversityCount}, {countryWithLowestInsecurePercentage.InsecureUniversityCount / countryWithLowestInsecurePercentage.UniversityCount * 100:0.00}%)";

            return groupedData.ToList();
        }


        private void UpdateUniversityList(string countryName)
        {
            universityList.Items.Clear();
            var selectedCountry = countriesData.Find(c => c.CountryName == countryName);
            if (selectedCountry != null)
            {
                foreach (var university in universities.Where(u => u.country == selectedCountry.CountryName))
                {
                    string universityText = university.name;
                    string secureUrl = university.web_pages.FirstOrDefault(url => url.StartsWith("https://"));
                    if (secureUrl != null)
                    {
                        universityText += $" - {secureUrl}";
                    }
                    else
                    {
                        universityText += $" - {university.web_pages.FirstOrDefault()}";
                    }

                    universityList.Items.Add(universityText);
                }
            }
        }

        //private void countriesList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (countriesList.SelectedIndex >= 0)
        //    {
        //        var selectedCountryName = countriesList.SelectedItem.ToString().Split('(')[0];
        //        UpdateUniversityList(selectedCountryName);
        //    }
        //}

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
            }
        }

        private void CountriesDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CountriesDataGridView.SelectedRows.Count == 1)
            {
                string countryName =  CountriesDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                UpdateUniversityList(countryName);
            }
        }
    }

    public class UniversityData
    {
        public string name { get; set; }
        public string country { get; set; }
        public string[] web_pages { get; set; }
        public string alpha_two_code { get; set; }
        public string state_province { get; set; }
    }

    public class CountryData
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int UniversityCount { get; set; }
        public int SecureUniversityCount { get; set; }
        public int InsecureUniversityCount { get; set; }
    }
}