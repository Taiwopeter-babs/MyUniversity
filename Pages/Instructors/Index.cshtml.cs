using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Models.SchoolViewsModels;
using MyUniversity.VModels;

namespace MyUniversity.Pages.Instructors;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;

    public IndexModel(SchoolContext context)
    {
        _context = context;
    }

    public InstructorIndexDataVM InstructorData { get; set; }
    public int InstructorID { get; set; }
    public int CourseID { get; set; }
    public string SelectedCourseName { get; set; }

    public async Task OnGetAsync(int? id, int? courseID)
    {
        InstructorData = new InstructorIndexDataVM();

        InstructorData.Instructors = await _context.Instructors
        .Include(ins => ins.OfficeAssignment)
        .Include(ins => ins.Courses)
        .ThenInclude(c => c.Department)
        .OrderBy(ins => ins.LastName)
        .ToListAsync();

        // load courses for selected instructor
        if (id != null)
        {
            InstructorID = id.Value;
            Instructor instructor = InstructorData.Instructors
            .Where(ins => ins.ID == InstructorID)
            .SingleOrDefault();

            InstructorData.Courses = instructor.Courses;
            InstructorData.SelectedInstructorName = instructor.FullName;
        }

        // load students enrolled in selected course
        if (courseID != null)
        {
            // CourseID = courseID.Value;
            IEnumerable<Enrollment> StudentsEnrolled = await _context.Enrollments
            .Where(en => en.CourseID == courseID.Value)
            .Include(en => en.Student)
            .ToListAsync();

            Course selectedCourse = InstructorData.Courses
            .Where(c => c.ID == courseID.Value).SingleOrDefault();

            InstructorData.Enrollments = StudentsEnrolled;
            SelectedCourseName = selectedCourse.Title;

        }
    }
}
