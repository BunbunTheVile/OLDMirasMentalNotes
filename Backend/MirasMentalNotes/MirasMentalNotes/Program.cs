using MirasMentalNotes.Settings;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: "AllowAllOrigins",
            policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
    });
}

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowAllOrigins");
app.MapControllers();

AppSettings.Initialize();

if (app.Environment.IsProduction())
{
    Process.Start("cmd", "/c start http://localhost:5000");
}

app.Run();