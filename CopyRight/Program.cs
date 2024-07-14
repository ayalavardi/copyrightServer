
using Bl.Blservice;
using Dal;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CopyRightContext>(options => options.UseSqlServer("Server=.;Database=CopyRight;TrustServerCertificate=True;Trusted_Connection=True;"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<GoogleDriveService>();

builder.Services.AddScoped(typeof(Bl.Interfaces.IUser), typeof(Service.UserService));
builder.Services.AddScoped(typeof(Dal.Interfaces.IUser), typeof(Dal.Service.UserService));
builder.Services.AddScoped(typeof(Bl.Interfaces.ILead), typeof(Service.LeadService));
builder.Services.AddScoped(typeof(Bl.Interfaces.ICustomer), typeof(Service.CustomerService));
builder.Services.AddScoped(typeof(Bl.Interfaces.ITasks), typeof(Service.TasksService));
builder.Services.AddScoped(typeof(Dal.Interfaces.ITasks), typeof(Dal.Service.TasksService));
builder.Services.AddScoped(typeof(Bl.Interfaces.IProject), typeof(Service.ProjectService));
builder.Services.AddScoped(typeof(Dal.Interfaces.IProject), typeof(Dal.Service.ProjectService));
builder.Services.AddScoped<DalManager>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
