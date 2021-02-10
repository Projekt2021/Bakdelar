using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakdelar.Pages.Admin
{
    public class UploadImageModel : PageModel
    {
        private IWebHostEnvironment _environment;
        public UploadImageModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public Classes.ProductImage ProductImage { get; set; }


        [BindProperty]
        public IFormFile Image { get; set; }



        [BindProperty(SupportsGet = true)]
        public string ProductImageID { get; set; }

        public List<Classes.Product> Products { get; set; }





        public void OnGet()
        {
           //var getProductsURL = "https://localhost:44347/api/Products";
           //using HttpClient httpClient = new HttpClient();
           //Products = httpClient.GetFromJsonAsync<List<Classes.Product>>(getProductsURL).Result;
        }



        public async Task<IActionResult> OnPostAsync()
        {
            //where the image can be loaded from in a img tag
            var partialFilePath = "\\images\\products\\" + Image.FileName;

            //where the file is to be saved
            var file = _environment.WebRootPath + "\\images\\products\\" + Image.FileName;
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
            //api url for posting the productimage so its saved into the database
            string postURL = "https://localhost:44347/api/ProductImages";
            using HttpClient httpClient = new HttpClient();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //the partial path is stored in a product image object
            Classes.ProductImage productImage = new Classes.ProductImage() { ImageUrl = partialFilePath };
            //httpClient

            //the productimage is posted to the api
            var response = await httpClient.PostAsJsonAsync(postURL, productImage);

            //the posted product image is read to get the id it was assigned
            Classes.ProductImage postedProductImage = await response.Content.ReadFromJsonAsync<Classes.ProductImage>();



            string ImageID = postedProductImage.ProductImageID.ToString();

            //redirect to addproduct/ImageID
            return RedirectToPage("AddProduct", new { ProductImageID = ImageID });

        }
    }
}
