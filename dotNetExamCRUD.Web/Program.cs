using CustomerCRUDWebApp.Domain.CustomerManagement.Entities;
using dotNetExamCRUD.Domain.ContactManagement;
using dotNetExamCRUD.Domain.ContactManagement.Interfaces;
using dotNetExamCRUD.Domain.ContactManagement.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

static void AddSampleData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ContactManagementDBContext>();

    db.Contacts.Add(new Contact("John", "Smith", "ABC Corporation", "john.smith@abccorp.com", "1234567890"));
    db.Contacts.Add(new Contact("Emily", "Johnson", "XYZ Industries", "emily.johnson@xyzindustries.com", "9876543210"));
    db.Contacts.Add(new Contact("Michael", "Williams", "LMN Enterprises", "michael.williams@lmnenterprises.com", "5551234567"));
    db.Contacts.Add(new Contact("Sarah", "Brown", "PQR Group", "sarah.brown@pqrgroup.com", "7893451234"));
    db.Contacts.Add(new Contact("David", "Miller", "RST Inc.", "david.miller@rstinc.com", "2345678901"));
    db.SaveChanges();
}

var builder = WebApplication.CreateBuilder(args);
var assembly = AppDomain.CurrentDomain.Load("dotNetExamCRUD.Application");
builder.Services.AddMediatR(assembly);
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddDbContext<ContactManagementDBContext>
(o => o.UseInMemoryDatabase("ContactDb"));

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ContactManagementDBContext>();
// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


AddSampleData(app);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("corsapp");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
