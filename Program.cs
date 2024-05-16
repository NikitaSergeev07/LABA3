using LABA3.Data;
using LABA3.Models.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




//добавление студента для отладки
/*
 * // Создаем область для резолвинга сервисов из построителя
   using (var scope = app.Services.CreateScope())
   {
       var services = scope.ServiceProvider;
       var dbContext = services.GetRequiredService<AppDbContext>();
   
       // Проверяем, существует ли колледж с указанным ID
       var collegeId = 1; // ID вашего колледжа
       var college = await dbContext.Colleges.FindAsync(collegeId);
       var userId = 1;
       var user = await dbContext.StudentUsers.FindAsync(userId);
       if (college == null)
       {
           Console.WriteLine("Колледж не найден.");
           return;
       }
       if (user == null)
       {
           Console.WriteLine("StudentUser не найден.");
           return;
       }
   
       // Создаем нового студента
       var student = new Student
       {
           Name = "Egor",
           Age = 21,
           College = college, 
           User = user
       };
   
       // Добавляем студента в контекст базы данных
       dbContext.Students.Add(student);
       await dbContext.SaveChangesAsync();
   
       Console.WriteLine("Студент успешно создан и привязан к колледжу.");
   }
*/