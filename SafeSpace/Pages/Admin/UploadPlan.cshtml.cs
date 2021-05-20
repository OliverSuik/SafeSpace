using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Pages.Admin
{
    public class UploadPlanModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Please select a classroom.")]
        public int SelectedPlanId { get; set; }
        public SelectList SelectPlan { get; set; }
        public void OnGet()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "pyhavaimu_auditoorium_1", Value = "1"});
            items.Add(new SelectListItem { Text = "pyhavaimu_auditoorium_2", Value = "2" });
            SelectPlan = new SelectList(items, "Value", "Text");
        }
        public IActionResult OnPost()
        {
            string[] plans = { "pyhavaimu_auditoorium_1", "pyhavaimu_auditoorium_2" };
            return RedirectToPage("../CreateClassroom", new { fileName = plans[SelectedPlanId - 1] });
        }

    }
}
