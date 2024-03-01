using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IManager;
using PatientForm.WebApi.IRepository;
using PatientForm.WebApi.Manager;
using PatientForm.WebApi.Repository;
using PatientWebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PatientDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DB"), x => x.MigrationsAssembly("PatientForm.EntityModel")));

#region Manager & Repository
builder.Services.AddTransient<IPatientsManager, PatientsManager>();

builder.Services.AddTransient<IAllergieRepository, AllergieRepository>();
builder.Services.AddTransient<IAllergieDetailRepository, AllergieDetailRepository>();
builder.Services.AddTransient<IDiseaseRepository, DiseaseRepository>();
builder.Services.AddTransient<INcdRepository, NcdRepository>();
builder.Services.AddTransient<INcdDetailRepository, NcdDetailRepository>();
builder.Services.AddTransient<IPatientsRepository, PatientsRepository>();
#endregion

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
