using ContactService.Application.Services;
using ContactService.Core.Interfaces.Services;
using ContactService.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using ReportService.Core.Interfaces;
using ReportService.Infrastructure.Consumer.Services;
using ReportService.Infrastructure.Database;
using ReportService.Infrastructure.Mapping;
using ReportService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ReportRequestProfile));

builder.Services.AddDbContext<ReportDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ReportDbConnection")));

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService.Application.Services.ReportService>();
builder.Services.AddScoped<IContactServiceClient, ContactServiceClient>();

builder.Services.AddSingleton<IHostedService, ReportServiceConsumer>();

builder.Services.AddHttpClient<IPersonService, PersonService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:55489");
});

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
