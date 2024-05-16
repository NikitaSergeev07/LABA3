namespace LABA3.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;
public class Student
{
    public Student()
    {
        Courses = new List<Course>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int UserId { get; set; } 
    [ForeignKey(nameof(UserId))]
    public StudentUser? User { get; set; }
    
    public int CollegeId { get; set; } 
    [ForeignKey(nameof(CollegeId))]
    public College? College { get; set; }
    
    public List<Course> Courses { get; set; }
}