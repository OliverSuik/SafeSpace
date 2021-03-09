using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Pages.Admin
{
    public class UploadPlanModel : PageModel
    {
        [BindProperty]
        public string Upload { get; set; }
        public IActionResult OnPost()
        {
            return RedirectToPage("../CreateClassroom", new { plan = Upload });
        }
    }
}
