using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Net.Http;

namespace Bakdelar.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ProductID { get; set; }
        public void OnGet()
        {
            if(ProductID != string.Empty || ProductID != null)
            {
                ViewData["ProductID"] = ProductID;
            }
        }
        [BindProperty]
        public Classes.ProductFromJson Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string postURL = "https://localhost:44347/api/Products";
            using HttpClient httpClient = new HttpClient();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //httpClient
            var response = await httpClient.PostAsJsonAsync(postURL, Product);
            Classes.ProductFromJson postedProduct = response.Content.ReadFromJsonAsync< Classes.ProductFromJson>().Result;
            ProductID = postedProduct.Id.ToString();
            return RedirectToPage($"/Item", new { Id = postedProduct.Id });
        }
    }
}
