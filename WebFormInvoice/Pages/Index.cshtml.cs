using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebFormInvoice.Models;

namespace WebFormInvoice.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _config;
        public MasterData Data { get; set; }
        public List<InvoiceDue> ListOfInvoiceDue { get; set; }
        public string Number { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void OnGet()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_config.GetSection("WebAPIBaseAddress").Value);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/GetMasterData").Result;

                if (response.IsSuccessStatusCode)
                {
                    string a = response.Content.ReadAsStringAsync().Result;
                    Data = JsonConvert.DeserializeObject<MasterData>(a);
                }
            }
            catch { }

            #region InvoiceNo
            int nomor = Data != null ? Data.Number : 0;
            if (nomor == 0)
            {
                this.Number = "001";
            }
            else
            {
                this.Number = nomor.ToString();
                if (this.Number.Length == 1)
                {
                    this.Number = "00" + this.Number;
                }
                if (this.Number.Length == 2)
                {
                    this.Number = "0" + this.Number;
                }
            }
            #endregion

            #region Data Dummy
            ListOfInvoiceDue = new List<InvoiceDue>();
            ListOfInvoiceDue.Add(new InvoiceDue { Desc = "Immediately", ID = 1 });
            ListOfInvoiceDue.Add(new InvoiceDue { Desc = "Later", ID = 2 });

            if (Data == null)
            {
                Data = new MasterData();
            }

            if (Data.ListOfConsumen == null || Data.ListOfConsumen.Count == 0)
            {
                Data.ListOfConsumen = new List<Consumen>();
                Data.ListOfConsumen.Add(new Consumen { Alamat = "Jakarta", Nama = "Joe", ID = 1, URL = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Kalbe_Farma.svg/220px-Kalbe_Farma.svg.png" });
                Data.ListOfConsumen.Add(new Consumen { Alamat = "Bandung", Nama = "Asep", ID = 2, URL = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/251px-Google_2015_logo.svg.png" });
            }

            if (Data.ListOfLanguage == null || Data.ListOfLanguage.Count == 0)
            {
                Data.ListOfLanguage = new List<Language>();
                Data.ListOfLanguage.Add(new Language { Code = "English", Desc = "English(US)", ID = 1 });
                Data.ListOfLanguage.Add(new Language { Code = "Indonesia", Desc = "Indonesia(IDN)", ID = 1 });
            }

            if (Data.ListOfCurrency == null || Data.ListOfCurrency.Count == 0)
            {
                Data.ListOfCurrency = new List<Currency>();
                Data.ListOfCurrency.Add(new Currency { Code = "USD", Desc = "United State(USD)", ID = 1 });
                Data.ListOfCurrency.Add(new Currency { Code = "IDR", Desc = "Indonesia(IDR)", ID = 1 });
            }

            if (Data.ListOfUOM == null || Data.ListOfUOM.Count == 0)
            {
                Data.ListOfUOM = new List<UOM>();
                Data.ListOfUOM.Add(new UOM { Code = "hour", ID = 1 });
                Data.ListOfUOM.Add(new UOM { Code = "min", ID = 2 });
            }
            #endregion


        }
    }
}
