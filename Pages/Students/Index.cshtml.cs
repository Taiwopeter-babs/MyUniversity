using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;

namespace MyUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string FirstNameSort { get; set; }
        public string LastNameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Student> Students { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter,
            string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            FirstNameSort = string.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
            LastNameSort = sortOrder == "last_name" ? "last_name_desc" : "last_name";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentsIQ = from student in _context.Students
                                             select student;

            if (!string.IsNullOrEmpty(searchString))
            {
                // The ToUpper() is unneccesary when it is called on a SQL server as a case-insensitive
                // match is done, SQLite is case-sensitive. Also IQueryable is preffered over Ienumerable
                // for performance reasons as the filtering is done by the database, but in the case of
                // the latter, all the rows are fetched first
                studentsIQ = studentsIQ.Where(s => s.LastName.ToLower().Contains(searchString.ToLower()) ||
                                                s.FirstName.ToLower().Contains(searchString.ToLower()));
            }
            // switch expression
            studentsIQ = sortOrder switch
            {
                "last_name_desc" => studentsIQ.OrderByDescending(s => s.LastName),
                "last_name" => studentsIQ.OrderBy(s => s.LastName),
                "first_name_desc" => studentsIQ.OrderByDescending(s => s.FirstName),
                "Date" => studentsIQ.OrderBy(s => s.EnrollmentDate),
                "date_desc" => studentsIQ.OrderByDescending(s => s.EnrollmentDate),
                _ => studentsIQ.OrderBy(s => s.FirstName),
            };
            var pageSize = Configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize
                );
        }
    }
}
