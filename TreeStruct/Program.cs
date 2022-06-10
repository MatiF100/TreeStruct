using TreeStruct.Database;
using TreeStruct.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages(options => options.Conventions.AddPageRoute("/Pages/TreeNodes/Index", ""));
builder.Services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/TreeNodes/Index", ""));


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TreeStructDbContext>(opts => opts.UseNpgsql(connectionString));

builder.Services.AddSession();
builder.Services.AddMemoryCache();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedExampleData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();


app.UseAuthorization();

app.MapRazorPages();


app.Run();
