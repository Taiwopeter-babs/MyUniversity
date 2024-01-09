using MyUniversity.Models.SchoolViewsModels;
using MyUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Models;

namespace MyUniversity.Pages;

public class AboutModel : PageModel
{
    private readonly SchoolContext _context;

    public AboutModel(SchoolContext context) => _context = context;

    public IList<EnrollmentDateGroup> Students { get; set; }

    public async Task OnGetAsync()
    {
        IQueryable<EnrollmentDateGroup> data =
            from student in _context.Students
            group student by student.EnrollmentDate into dateGroup
            select new EnrollmentDateGroup()
            {
                EnrollmentDate = dateGroup.Key,
                StudentCount = dateGroup.Count()
            };
        Students = await data.AsNoTracking().ToListAsync();
    }
}