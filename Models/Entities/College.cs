namespace LABA3.Models.Entities;

public class College
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
}