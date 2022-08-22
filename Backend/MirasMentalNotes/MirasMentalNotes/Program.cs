using MirasMentalNotes.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: "AllowSpecificOrigins",
            policy =>
            {
                policy.WithOrigins("http://localhost:4200");
                policy.AllowAnyMethod();
            });
    });
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();
app.MapControllers();

AppSettings.Initialize();

app.Run();
