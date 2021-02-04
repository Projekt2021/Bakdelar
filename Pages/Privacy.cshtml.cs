using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakdelar.Classes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bakdelar.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            //var cookie = Request.Cookies["tempCookie"];
            //if (cookie == null || cookie == "")
            //{
            //    ShoppingBasketData basketData = new ShoppingBasketData
            //    {
            //        ShoppingItems = new List<Item>() { new Item("Form", 10, 1), new Item("Skål", 20, 2) }
            //    };
            //    string serializedBasket = JsonSerializer.Serialize(basketData);
            //    Response.Cookies.Append("tempCookie", serializedBasket);
            //}
            //else
            //{
            //    Response.Cookies.Append("tempCookie", "");
            //}
        }
    }
}
