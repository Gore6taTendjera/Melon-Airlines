using Logic_Layer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Data_Access_Layer;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services;
using Logic_Layer.Services.Planes;
using Logic_Layer.Services.Payment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddScoped<IUserAccountDAL, UserAccountDAL>();
builder.Services.AddScoped<IIDDAL, DocumentDAL>();
builder.Services.AddScoped<IPassportDAL, DocumentDAL>();
builder.Services.AddScoped<ITicketsDAL, TicketsDAL>();
builder.Services.AddScoped<IPlaneDAL, PlaneDAL>();
builder.Services.AddScoped<IFlightDAL, FlightDAL>();
builder.Services.AddScoped<IImageDAL, ImageDAL>();

builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IIDService, IDService>();
builder.Services.AddScoped<IPassportService, PassportService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IPlaneService, PlaneService>();
builder.Services.AddScoped<IFlightService, FlightService>();


builder.Services.AddScoped<IPlaneSeatService, PlaneSeatsService>();
builder.Services.AddScoped<ISeatService, SeatService>();

builder.Services.AddScoped<ISeatAssignmentStrategy, A320SeatAssignmentStrategy>();
builder.Services.AddScoped<ISeatAssignmentStrategy, A380SeatAssignmentStrategy>();

builder.Services.AddScoped<ISeatGroupCreator, A320SeatGroupCreator>();
builder.Services.AddScoped<ISeatGroupCreator, A380SeatGroupCreator>();

builder.Services.AddScoped<ISeatTypeArrangement, SeatTypeArrangement>();

builder.Services.AddScoped<IPlaneSeatsServiceFactory, PlaneSeatsServiceFactory>();

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddTransient<IPaymentStrategy, PayPalPaymentStrategy>();
builder.Services.AddTransient<IPaymentStrategy, CreditCardPaymentStrategy>();

builder.Services.AddScoped<IImageService, ImageDBService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(s =>
{
    s.LoginPath = "/Login";
    s.LogoutPath = "/Index";
    s.ExpireTimeSpan = TimeSpan.FromSeconds(1200);
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1200);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
