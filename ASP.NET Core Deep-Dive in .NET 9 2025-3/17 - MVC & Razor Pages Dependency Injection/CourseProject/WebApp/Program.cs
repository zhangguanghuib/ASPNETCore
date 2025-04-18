using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
        ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddMinutes(10).ToString());
    }
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapRazorPages();
});

app.Run();
