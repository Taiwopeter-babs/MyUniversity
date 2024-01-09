using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.VModels;

namespace MyUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly MyUniversity.Data.SchoolContext _context;

        public CreateModel(MyUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // using view model to create a new student
        [BindProperty]
        public StudentVM StudentVM { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newEntry = _context.Add(new Student());
            newEntry.CurrentValues.SetValues(StudentVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
