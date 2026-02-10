using JobTracker.Application.UseCases.JobApplications;
using JobTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: remover daqui
builder.Services.AddScoped<CreateApplication>();
builder.Services.AddScoped<ChangeStatus>();


builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
