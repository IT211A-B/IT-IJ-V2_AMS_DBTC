using AMS_Backend_V2.Data;
using AMS_Backend_V2.Repositories.AttendanceRepo;
using AMS_Backend_V2.Repositories.CourseRepo;
using AMS_Backend_V2.Repositories.StudentRepo;
using AMS_Backend_V2.Repositories.TeacherRepo;
using AMS_Backend_V2.Services.AttendanceServe;
using AMS_Backend_V2.Services.CourseServe;
using AMS_Backend_V2.Services.StudentServe;
using AMS_Backend_V2.Services.TeacherServe;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IStudentServices, StudentService>();
builder.Services.AddScoped<ITeacherServices, TeacherService>();
builder.Services.AddScoped<ICourseServices, CourseService>();
builder.Services.AddScoped<IAttendanceServices, AttendanceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
