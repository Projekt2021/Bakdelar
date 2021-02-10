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
        public string ImageLink { get; set; }

        [BindProperty]
        public int ProductID { get; set; }
        [BindProperty]
        public string ProductImageID { get; set; }
        public List<Classes.ProductImage> ProductImages { get; set; }
        public List<Classes.Category> Categories { get; set; }

        [BindProperty]
        public Classes.Product Product { get; set; }
        public void OnGet()
        {
            using HttpClient httpClient = new HttpClient();
            ProductImages = httpClient.GetFromJsonAsync<List<Classes.ProductImage>>("https://localhost:44347/api/ProductImages").Result;
            ProductImages = ProductImages.Where(image => image.ProductID == null).ToList();
            Categories = httpClient.GetFromJsonAsync<List<Classes.Category>>("https://localhost:44347/api/Categories").Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string postProductURL = "https://localhost:44347/api/Products";
            string productImageURL = "https://localhost:44347/api/ProductImages/";
            using HttpClient httpClient = new HttpClient();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //httpClient
            Classes.ProductImage selectedProductImage = httpClient.GetFromJsonAsync<Classes.ProductImage>(productImageURL + ProductImageID).Result;

            var response = await httpClient.PostAsJsonAsync(postProductURL, Product);
            Classes.Product postedProduct = response.Content.ReadFromJsonAsync< Classes.Product>().Result;
            ProductID = postedProduct.Id;
            selectedProductImage.ProductID = ProductID;
            
            await httpClient.PostAsJsonAsync(productImageURL + ProductImageID, selectedProductImage);
            return RedirectToPage($"/Item", new { Id = postedProduct.Id });
        }
    }
}
