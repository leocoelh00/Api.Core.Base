using Api.Core.Base.API.Extensions;
using Api.Core.Base.Application.Extensions;
using Api.Core.Base.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddBasicAuthentication();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwaggerConfiguration();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
