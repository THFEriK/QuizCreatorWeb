using Microsoft.AspNetCore.Authentication.Cookies;
using QuizCreatorWeb.Components;
using QuizCreatorWeb.Data;
using QuizCreatorWeb.Handlers;

var builder = WebApplication.CreateBuilder(args);
const string BaseUrl = "http://localhost:5189/";
//const string BaseUrl = "http://localhost:5472/";

builder.Services.AddSingleton(x => new HttpClient { BaseAddress = new Uri(BaseUrl) });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "QuizSiteCookie";
        options.LoginPath = "/signin";
        options.LogoutPath = "/signout";
        options.AccessDeniedPath = "/access-denied";
        options.Cookie.MaxAge = TimeSpan.FromDays(1);
    });
builder.Services.AddAuthorization();
builder.Services.AddLogging();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<QuizService>(client =>
{
    client.BaseAddress = new Uri($"{BaseUrl}api/quiz/");
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();
builder.Services.AddHttpClient<QuestionService>(client =>
{
    client.BaseAddress = new Uri($"{BaseUrl}api/question/");
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();
builder.Services.AddHttpClient<UserService>(client =>
{
    client.BaseAddress = new Uri($"{BaseUrl}api/user/");
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();
builder.Services.AddHttpClient<UserQuizService>(client =>
{
    client.BaseAddress = new Uri($"{BaseUrl}api/");
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();

builder.Services.AddTransient<JwtAuthorizationHandler>();

var app = builder.Build();

#if RELEASE
app.Urls.Add("http://0.0.0.0:5000");
#endif

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
