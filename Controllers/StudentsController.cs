using LABA3.Data;
using LABA3.Models;
using LABA3.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace LABA3.Controllers;

public class StudentsController : Controller
{
    private readonly AppDbContext _context;
    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var students = _context.Students;
        return View(await students.ToListAsync());
    } 
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.Include(e => e.College)
            .Include(e => e.User)
            .Include(c => c.Courses)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }
    
    public IActionResult Create()
    {
        ViewData["CollegeId"] = new SelectList(_context.Colleges, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.StudentUsers, "Id", "Id");
        ViewData["Courses"] = _context.Courses.ToList(); 
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AddStudentViewModel ViewModel, List<int> selectedCourses)
    {
        var student = new Student
        {
            Name = ViewModel.Name,
            Age = ViewModel.Age,
            UserId = ViewModel.UserId,
            CollegeId = ViewModel.CollegeId
        };
        if (selectedCourses != null)
        {
            foreach (var courseId in selectedCourses)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course != null)
                {
                    student.Courses.Add(course);
                }
            }
        }
    
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _context.Students.Include(s => s.Courses).FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
    
        ViewData["CollegeId"] = new SelectList(_context.Colleges, "Id", "Id", student.CollegeId);
        ViewData["UserId"] = new SelectList(_context.StudentUsers, "Id", "Id", student.UserId);
        ViewData["Courses"] = _context.Courses.ToList();
    
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Student viewModel, List<int> selectedCourses)
    {
        var student = await _context.Students
            .Include(s => s.Courses) 
            .FirstOrDefaultAsync(s => s.Id == viewModel.Id);
        if (student is not null)
        {
            student.Name = viewModel.Name;
            student.Age = viewModel.Age;
            student.UserId = viewModel.UserId;
            student.CollegeId = viewModel.CollegeId;
            student.Courses.Clear();
            if (selectedCourses != null)
            {
                foreach (var courseId in selectedCourses)
                {
                    var course = await _context.Courses.FindAsync(courseId);
                    if (course != null)
                    {
                        student.Courses.Add(course);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Students");
    }

}