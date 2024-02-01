using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SharpMelody.Music.Data;
using SharpMelody.Music.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<LibraryService>();
builder.Services.AddTransient<MusicDbContext>();

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.File(AppContext.BaseDirectory + $"/logs/{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}_log.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

app.MapGet("/", () => "SharpMelody!");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();

app.Run();