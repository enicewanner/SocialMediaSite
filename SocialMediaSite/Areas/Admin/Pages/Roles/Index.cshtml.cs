using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ID6NP.Areas.Admin.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public List<IdentityRole> Roles { get; set; }

        public void OnGet()
        {
            this.Roles = this.roleManager.Roles.ToList();
        }
    }
}
