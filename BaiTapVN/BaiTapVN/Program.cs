var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Tuan02}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "index",
    pattern: "Index/{action=Index}/{id?}",
    defaults: new { controller = "Tuan02", action = "Index" });

app.MapControllerRoute(
    name: "Profile",
    pattern: "Profile/{action=Profile}/{id?}",
    defaults: new { controller = "Tuan02", action = "Profile" });



app.Run();
