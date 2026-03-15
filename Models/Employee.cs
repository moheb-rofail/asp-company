using System.ComponentModel.DataAnnotations;

namespace MyMvcProject.Models;

public class Employee
{
    public int Id { get; set; }

    [Display(Name="Full Name")]
    [Required]
    [MinLength(3, ErrorMessage = "Full Name must be greater than 2 letters")]
    [MaxLength(50, ErrorMessage = "Full Name must be less than 50 letters")]
    public string Name { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Display(Name="Department")]
    [Required]
    public int DepartmentId { get; set; }
}