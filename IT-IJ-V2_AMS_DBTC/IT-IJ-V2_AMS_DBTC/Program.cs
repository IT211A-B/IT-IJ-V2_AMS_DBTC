using IT_IJ_V2_AMS_DBTC.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<CourseService>();

builder.Services.AddHttpClient("AttendanceAPI", client =>
{client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");
pattern: "{controller=Attendance}/{action=Index}/{id?}");

app.Run();