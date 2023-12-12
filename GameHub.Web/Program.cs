using GameHub.Web.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InitSettings(builder.Configuration);
builder.Services.InitProviders();
builder.Services.InitRepositories();
builder.Services.InitServices();
builder.Services.InitMapper();
builder.Services.InitValidation();
builder.Services.InitJwt(builder.Configuration);
builder.Services.InitSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
