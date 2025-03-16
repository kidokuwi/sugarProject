using sugarProject.DataModel;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<VisitorService>();
builder.Services.AddSingleton<LoginService>();

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(90);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMemoryCache();


var app = builder.Build();
ServiceProviderAccessor.ServiceProvider = app.Services;


//cookie
CookieOptions cookieOptions = new CookieOptions();
cookieOptions.Expires = DateTime.Now.AddHours(6);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

public static class ServiceProviderAccessor
{
    public static IServiceProvider ServiceProvider { get; set; }
}
public class VisitorService
{
    private int _visitorCount = 0;
    public int GetVisitorCount() => _visitorCount;

    public void IncrementVisitorCount() => _visitorCount++;
}
public class LoginService
{
	private int _loginCount = 0;
	public int GetloginCount() => _loginCount;

	public void IncrementloginCount() => _loginCount++;
}

