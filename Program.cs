using api.Services;

var builder = WebApplication.CreateBuilder(args);
//Adding CORS Policy: We added builder.Services.AddCors to configure the CORS policy, 

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:3000") // Allow only the specified origin
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddSingleton<GoogleAnalyticsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();


