using IntelectahClinic.Models;
using IntelectahClinic.Repository;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<IntelectahClinicContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("IntelectahClinicConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5502")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddIdentity<Paciente, IdentityRole>()
    .AddEntityFrameworkStores<IntelectahClinicContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; 
    
    options.LoginPath = "/user/login";
    options.AccessDeniedPath = "/";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<PacienteUserService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<AgendamentoService>();
builder.Services.AddScoped<EspecialidadeService>();
builder.Services.AddScoped<UnidadeService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
}); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();