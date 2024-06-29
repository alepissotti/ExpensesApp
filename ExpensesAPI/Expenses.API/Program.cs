using Expenses.API;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container using Startup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline using Startup
var env = app.Environment;
startup.Configure(app, env);

app.Run();
