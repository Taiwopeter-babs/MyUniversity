using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniversity.Models;

public class Course
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ID {get; set;}
    public string Title {get; set;}
    public int Credits {get; set;}

    public ICollection<Enrollment> Enrollments {get; set;}
}