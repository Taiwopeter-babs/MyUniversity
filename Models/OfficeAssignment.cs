using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniversity.Models;

public class OfficeAssignment
{
    public int ID { get; set; }
    public int InstructorID { get; set; }

    [Required]
    [StringLength(60)]
    [Display(Name = "Office Location")]
    public string Location { get; set; }

    public Instructor Instructor { get; set; }
}