global using WebMS.Models.Entities;
using WebMS.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer<DB>($@"
    Data Source=(LocalDB)\MSSQLLocalDB;
    Initial Catalog=WebMSDB; 
    AttachDbFilename={builder.Environment.ContentRootPath}\DB.mdf;
");

builder.Services.AddScoped<SeatLayoutService>();
builder.Services.AddScoped<HallService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<PricingService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();

/* Note: Not to use
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
*/