using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RateYourMovie.Models;

namespace RateYourMovie.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            var roleExist = await roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return View();
        }
    }
}