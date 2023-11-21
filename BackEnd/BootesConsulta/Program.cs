using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Database.Repository.Observatories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//Add Repositories
builder.Services.AddScoped<IMeteorsRepository, MeteorsRepository>();
builder.Services.AddScoped<IObservatoriesRepository, ObservatoriesRepository>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
