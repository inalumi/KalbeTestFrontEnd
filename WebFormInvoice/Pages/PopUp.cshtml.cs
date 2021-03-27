using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class PopUpModel : PageModel
    {
        public string RequestId { get; set; }

        public List<PurchaseOrder> ListOfPurchaseOrder { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<PopUpModel> _logger;

        private readonly IConfiguration _config;

        public PopUpModel(ILogger<PopUpModel> logger, IConfiguration config)
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

                HttpResponseMessage response = client.GetAsync("api/GetPurchaseOrder").Result;

                if (response.IsSuccessStatusCode)
                {
                    string a = response.Content.ReadAsStringAsync().Result;
                    ListOfPurchaseOrder = JsonConvert.DeserializeObject<List<PurchaseOrder>>(a);
                }
            }
            catch { }

            #region Data Dummy
            if (ListOfPurchaseOrder == null || ListOfPurchaseOrder.Count == 0)
            {
                ListOfPurchaseOrder = new List<PurchaseOrder>();
                ListOfPurchaseOrder.Add(new PurchaseOrder { Amount = 2, PONo = "PO-123" });
                ListOfPurchaseOrder.Add(new PurchaseOrder { Amount = 3, PONo = "PO-124" });
                ListOfPurchaseOrder.Add(new PurchaseOrder { Amount = 4, PONo = "PO-125" });
            }
            #endregion

        }
    }
}
