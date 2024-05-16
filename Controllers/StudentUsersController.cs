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
namespace LABA3.Controllers;

public class StudentUsersController : Controller
{
    private readonly AppDbContext _context;

    public StudentUsersController(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.StudentUsers.ToListAsync());
    }
    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studentUsers = await _context.StudentUsers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (studentUsers == null)
        {
            return NotFound();
        }

        return View(studentUsers);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AddStudentUserViewModel ViewModel)
    {
        var studentUser = new StudentUser
        {
            Login = ViewModel.Login,
            Password = ViewModel.Password
        };
        await _context.StudentUsers.AddAsync(studentUser);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var studentUser = await _context.StudentUsers.FindAsync(id);
        if (studentUser == null)
        {
            return NotFound();
        }
        return View(studentUser);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(StudentUser viewModel)
    {
        var studentUser = await _context.StudentUsers.FindAsync(viewModel.Id);
        if (studentUser is not null)
        {
            studentUser.Login = viewModel.Login;
            studentUser.Password = viewModel.Password;
            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(StudentUser viewModel)
    {
        var studentUser = await _context.StudentUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
        if (studentUser is not null)
        {
            _context.StudentUsers.Remove(viewModel);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index", "StudentUsers");
    }
}
