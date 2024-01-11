using MyUniversity.Models;

namespace MyUniversity.VModels;

public class InstructorIndexDataVM
{
    public IEnumerable<Instructor> Instructors { get; set; }
    public string SelectedInstructorName { get; set; }
    public IEnumerable<Course> Courses { get; set; }
    public IEnumerable<Enrollment> Enrollments { get; set; }
}