using MirasMentalNotes.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

Settings.Initialize();

app.Run();
