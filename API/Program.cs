using API.Service;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

//Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage("Data Source=.;initial catalog=HangfireMailProject;Trusted_Connection=True;"));
builder.Services.AddHangfireServer();

builder.Services.AddSingleton<EmailService>();
var serviceProvider = builder.Services.BuildServiceProvider();
RecurringJobs.ConfigureRecurringJobs(serviceProvider);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

app.UseHangfireDashboard("/job", new DashboardOptions
{
    Authorization = new[]
{
    new HangfireCustomBasicAuthenticationFilter
    {
         User = _configuration.GetSection("HangfireSettings:UserName").Value,
         Pass = _configuration.GetSection("HangfireSettings:Password").Value
    }
    }
});

app.UseHangfireServer(new BackgroundJobServerOptions());

GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

app.Run();