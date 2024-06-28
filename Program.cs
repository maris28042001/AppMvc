
using AppMvc.Services;
using AppMvc.ExtendMethods;
using Microsoft.AspNetCore.Mvc.Razor;
using AppMvc.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppMvc.Models;
using AppMvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
var mailSetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppMvcConnectionString"));
});
builder.Services.Configure<RazorViewEngineOptions>(options =>{
    // /Views/Controller/Action.cshtml
    // /MyView/Controller/Action.cshtml

    //{0} -> Tên Action
    //{1} -> Tên Controller
    //{2} -> Tên Area
    options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
});
// builder.Services.AddSingleton<ProductService>();
// builder.Services.AddSingleton<ProductService, ProductService>();
// builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService),typeof(ProductService));
builder.Services.AddSingleton<PlanetService>();

// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true; 
});

builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath = "/Login/";
    options.LogoutPath = "/Logout/";
    options.AccessDeniedPath= "/khongduoctruycap.html/";
});

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        // Đọc thông tin Authentication:Google từ appsettings.json
        IConfigurationSection googleConfig = builder.Configuration.GetSection("Authentication:Google");
 
        // Thiết lập ClientID và ClientSecret để truy cập API google
        googleOptions.ClientId = googleConfig["ClientId"];
        googleOptions.ClientSecret = googleConfig["ClientSecret"];
        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
        googleOptions.CallbackPath = "/dang-nhap-tu-google";

    });

    builder.Services.AddSingleton<IdentityErrorDescriber,AppIdentityErrorDescriber>();
    builder.Services.AddAuthorization(options =>{
        options.AddPolicy("ViewManageMenu", builder =>{
            builder.RequireAuthenticatedUser();
            builder.RequireRole(RoleName.Administrator);
        });
    });


//Đăng ký identity
builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
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

app.AddStatusCodePage(); // Tuy bien Response loi: 400 - 599

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.MapRazorPages();

app.Run();
