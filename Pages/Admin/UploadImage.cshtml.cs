using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public IFormFile Image { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ImageLocation { get; set; }
        public string AddProductLink { get; set; }
        public void OnGet()
        {
            AddProductLink = "~/Admin/AddProduct?Image=" + ImageLocation;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var partialFilePath = "\\images\\products\\" + Image.FileName;
            var file = _environment.WebRootPath + "\\images\\products\\" + Image.FileName;
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
            return RedirectToPage("AddProduct", new { ImageLink = partialFilePath });

        }
    }
}
