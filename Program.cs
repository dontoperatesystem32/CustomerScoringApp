using Microsoft.EntityFrameworkCore;
using ScoringSystem_web_api.Data;
using ScoringSystem_web_api;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Repository;
using ScoringSystem_web_api.Models.ConditionModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();


var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() =>
{
    // Put your startup code here
    Console.WriteLine("Application has started!");
    using (var scope = app.Services.CreateScope())
    {
        var conditionRepository = scope.ServiceProvider.GetRequiredService<IConditionRepository>();
        // Call the method on the instance
        conditionRepository.CreateCondition("AgeCondition");
        conditionRepository.CreateCondition("SalaryCondition");
        conditionRepository.CreateCondition("TotalLoansCondition");

    }

});


//seed xuynalari

//if (args.Length == 1 && args[0].ToLower() == "seeddata")
//    SeedData(app);

//void SeedData(IHost app)
//{
//    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

//    using (var scope = scopedFactory.CreateScope())
//    {
//        var service = scope.ServiceProvider.GetService<Seed>();
//        service.SeedDataContext();
//}
//}

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
