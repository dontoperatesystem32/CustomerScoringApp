using Microsoft.EntityFrameworkCore;
using ScoringSystem_web_api.Data;
using ScoringSystem_web_api;
using ScoringSystem_web_api.Repository;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Services;
using ScoringSystem_web_api.Services.Abstraction;
using ScoringSystem_web_api.Models.Abstraction;
using ScoringSystem_web_api.Services.ScoringService;

using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

using Prometheus;

var builder = WebApplication.CreateBuilder(args);


//rate limiting

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: key => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10, // Allow 10 requests
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 5
            });
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});




// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IScoringService, ScoringService>();

//repos
//builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IHistoryRecordRepository, HistoryRecordRepository>();
builder.Services.AddScoped<IHistoryConditionRecordRepository, HistoryConditionRecordRepository>();

builder.Services.AddScoped<IOptionalAmountCalcService, OptionalAmountCalcService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 20,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            });
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


//seed

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



app.UseHttpMetrics();   // one-line request metrics
app.MapMetrics();       // /metrics


app.UseHttpsRedirection();

app.UseAuthorization();

// Use rate limiting
app.UseRateLimiter();

app.MapControllers();

////migrate

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();      // Applies any pending migrations
}



app.Run();
