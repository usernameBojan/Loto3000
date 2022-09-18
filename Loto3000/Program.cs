var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddInfrastracture(builder.Configuration);
builder.Services.AddConfiguration();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddPolicies();
builder.Services.AddLogger(builder.Configuration);
builder.Services.AddCorsPolicy();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Cors.CorsPolicy);

app.UseGlobalExceptionHandler();
app.UseCustomLogger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();