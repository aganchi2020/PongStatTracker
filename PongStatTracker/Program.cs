using System;
using PongStatTracker;
using PongStatTracker.Models;
using PongStatTracker.Data;
using PongStatTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConn")));


builder.Services.AddScoped<IQueryService, QueryService>();

var app = builder.Build();

app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.MapRazorPages();

app.Run();
