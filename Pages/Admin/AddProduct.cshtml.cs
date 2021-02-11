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
            //gets the product images from the api
            //ProductImages = ProductImages.Where(image => image.ProductID == null).ToList();
            Categories = httpClient.GetFromJsonAsync<List<Classes.Category>>("https://localhost:44347/api/Categories").Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //api urls for posting and getting
            string postProductURL = "https://localhost:44347/api/Products";
            string productImageURL = "https://localhost:44347/api/ProductImages/";
            using HttpClient httpClient = new HttpClient();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //the product image that was selected from the form
            Classes.ProductImage selectedProductImage = httpClient.GetFromJsonAsync<Classes.ProductImage>(productImageURL + ProductImageID).Result;

            //store the response to read the contents later
            var response = await httpClient.PostAsJsonAsync(postProductURL, Product);

            //store the posted product to redirect to the product page for the added product
            Classes.Product postedProduct = response.Content.ReadFromJsonAsync< Classes.Product>().Result;
            ProductID = postedProduct.Id;


            //code below is not working as intended at the moment (it does not set the product id)
            selectedProductImage.ProductID = ProductID;

            //store the productID in the the product image 
            await httpClient.PutAsJsonAsync(productImageURL + ProductImageID, selectedProductImage);


            //redirects to the newly added item 
            return RedirectToPage($"/Item", new { Id = postedProduct.Id });
        }
    }
}
