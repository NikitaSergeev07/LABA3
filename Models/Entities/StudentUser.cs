using System.ComponentModel.DataAnnotations.Schema;

namespace LABA3.Models.Entities;

public class StudentUser
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    //// Required reference navigation to principal
    public Student Student { get; set; }
}