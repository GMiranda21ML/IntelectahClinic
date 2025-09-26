using IntelectahClinic.Models;
using IntelectahClinic.Repository;
using IntelectahClinic.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<IntelectahClinicContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("IntelectahClinicConnection")));


builder.Services
    .AddIdentity<Paciente, IdentityRole>()
    .AddEntityFrameworkStores<IntelectahClinicContext>()
    .AddDefaultTokenProviders();

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
