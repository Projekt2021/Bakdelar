using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bakdelar.Pages.Admin.Product
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            //if (HttpContext==null || SessionHelper.GetObjectFromJson<UserToken>(HttpContext.Session, "usertoken") == null)
            //    Response.Redirect("~/");
        }

        /// <summary>  
        /// Gets or sets product model property.  
        /// </summary>  
        [BindProperty]
        public IList<ProductView> ProductView { get; set; }
        public async Task OnGet()
        {
            Uri baseURL = new Uri("https://localhost:5001/api/product/GetAllProducts");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseURL.ToString());

                if (response.IsSuccessStatusCode)
                {
                    ProductView = JsonConvert.DeserializeObject<List<ProductView>>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    this.RedirectToPage("/index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error found!");
                }
            }
        }
    }
}
