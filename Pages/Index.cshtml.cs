using Bakdelar.Classes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bakdelar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        /// <summary>  
        /// Gets or sets login model property.  
        /// </summary>  
        [BindProperty]
        public LoginView LoginModel { get; set; }

        /// <summary>  
        /// GET: /Index  
        /// </summary>  
        /// <returns>Returns - Appropriate page </returns>  
        public IActionResult OnGet()
        {
            try
            {
                //// Verification.  
                //if (this.User.Identity.IsAuthenticated)
                //{
                //    // Home Page.  
                //    return this.RedirectToPage("/Home/Index");
                //}
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info.  
            return this.Page();
        }

        /// <summary>  
        /// POST: /Index/LogIn  
        /// </summary>  
        /// <returns>Returns - Appropriate page </returns>  
        public async Task<IActionResult> OnPostLogIn()
        {
            try
            {
                // Verification.  
                if (ModelState.IsValid)
                {
                    // Initialization.  
                    Uri baseURL = new Uri("https://localhost:5001/api/authenticate/login");

                    using (HttpClient client = new HttpClient())
                    {
                        var jObject = JsonConvert.SerializeObject(this.LoginModel);

                        var stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(baseURL.ToString(), stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = JsonConvert.DeserializeObject<UserToken>(await response.Content.ReadAsStringAsync());
                            
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "usertoken", responseContent);

                            if (responseContent.AdminAccess)
                            {
                                return this.RedirectToPage("/Admin/Product/index");
                            }
                            else
                            {
                                return this.RedirectToPage("/Checkout");
                            }                                                                                                                                         
                        }
                        else
                        {
                            // Setting.  
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        }
                    }
                    //// Any parameters? Get value, and then add to the client 
                    //string key = HttpUtility.ParseQueryString(baseURL.Query).Get("key");
                    //if (key != "")
                    //{
                    //    client.DefaultRequestHeaders.Add("api-key", key);
                    //}
                      
                        // Verification.  
                        //if (loginInfo != null && loginInfo.Count() > 0)
                        //{
                        //    // Initialization.  
                        //    var logindetails = loginInfo.First();

                        //    // Login In.  
                        //    await this.SignInUser(logindetails.Username, false);

                        //    // Info.  
                        //    return this.RedirectToPage("/Home/Index");
                        //}
                    
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info.  
            return this.Page();
        }
    }
}
