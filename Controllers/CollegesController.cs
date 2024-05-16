using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LABA3.Data;
using LABA3.Models.Entities;
using LABA3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LABA3.Controllers;

public class CollegesController : Controller
{
    private readonly AppDbContext _context;

    public CollegesController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var colleges = await _context.Colleges.Include(s => s.Students).ToListAsync();
        return View(colleges);
    }
    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var college = await _context.Colleges
            .Include(c => c.Students)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (college == null)
        {
            return NotFound();
        }

        return View(college);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AddCollegeViewModel ViewModel)
    {
        var college = new College
        {
            Name = ViewModel.Name,
        };
        await _context.Colleges.AddAsync(college);
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

        var college = await _context.Colleges.FindAsync(id);
        if (college == null)
        {
            return NotFound();
        }

        return View(college);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(College viewModel)
    {
        var college = await _context.Colleges.FindAsync(viewModel.Id);
        if (college is not null)
        {
            college.Name = viewModel.Name;
            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(College viewModel)
    {
        var college = await _context.Colleges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
        if (college is not null)
        {
            _context.Colleges.Remove(viewModel);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Colleges");
    }
}