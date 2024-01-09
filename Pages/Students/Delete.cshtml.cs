using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyUniversity.Data;
using MyUniversity.Models;

namespace MyUniversity.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly MyUniversity.Data.SchoolContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(SchoolContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        public string ErrorMessage {get; set;}
        public string SuccessDelete {get; set;}

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangerError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            
            if (saveChangerError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try Again", id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);

             if (student == null)
            {
                return NotFound();
            }
            
            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                SuccessDelete = String.Format("Deletion successful!");
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToAction("./Delete", new {id, saveChangesError = true});
            }   
        }
    }
}
