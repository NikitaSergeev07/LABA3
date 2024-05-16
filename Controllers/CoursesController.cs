using LABA3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LABA3.Models;
using LABA3.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LABA3.Controllers;

public class CoursesController : Controller
{
    private readonly AppDbContext _context;
    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.Include(c => c.Students).ToListAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.Include(c => c.Students).FirstOrDefaultAsync(m => m.Id == id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AddCourseViewModel ViewModel)
    {
        var course = new Course
        {
            Name = ViewModel.Name,
        };
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Course viewModel)
    {
        var course = await _context.Courses.FindAsync(viewModel.Id);
        if (course is not null)
        {
            course.Name = viewModel.Name;
            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Course viewModel)
    {
        var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
        if (course is not null)
        {
            _context.Courses.Remove(viewModel);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Courses");
    }
}