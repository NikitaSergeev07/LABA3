using LABA3.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LABA3.Models;

public class AddStudentViewModel
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int UserId { get; set; } 
    
    public int CollegeId { get; set; } 

}