using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bakdelar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public bool Error { get; set; } = false;
        public List<Classes.Product> Products { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {


            var jsonText = GetProductInfo("https://localhost:44347/api/Products?adminToken=dXNlcmlzYWRtaW5zaG93ZnVsbHByb2R1Y3Q=");
            if (jsonText != string.Empty)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                Products = JsonSerializer.Deserialize<List<Classes.Product>>(jsonText, options);
            }
            else
            {
                Error = true;
            }
        }

        string GetProductInfo(string jsonDataURL)
        {
            string jsonText = "";
            using var webClient = new WebClient();
            try
            {
                jsonText = webClient.DownloadString(jsonDataURL);
            }
            catch (Exception e)
            {
                Redirect("/");
            }
            return jsonText;
        }
    }
}
