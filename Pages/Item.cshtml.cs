using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

namespace Bakdelar.Pages
{
    public class ItemModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public bool Error { get; set; } = false;
        public void OnGet()
        {


            var jsonText = GetProductInfo("https://localhost:44347/api/Products/" + Id);
            if (jsonText != string.Empty)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };


                Classes.ProductFromJson jsonProduct = JsonSerializer.Deserialize<Classes.ProductFromJson>(jsonText, options);
                ViewData["Title"] = jsonProduct.ProductName;
                ViewData["ProductDescription"] = "Vara: " + jsonProduct.ProductName + ", beskrivning: " + jsonProduct.ProductDescription;
            }
            else
            {
                Error = true;
            }
        }

        string GetProductInfo(string jsonDataURL)
        {
            string jsonText = "";
            try
            {
                jsonText = (new WebClient()).DownloadString(jsonDataURL);
            }
            catch(Exception e)
            {
                Redirect("/");
            }
            return jsonText;
        }
    }
}
