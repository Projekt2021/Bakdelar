using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            }
        }
    }
}
