using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Sistema_de_planos.Infraestructura.Datos;
using Sistema_de_planos.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlanosContext>(); //le digo cuál va a ser mi contexto

/*builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PlanosContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<User, PlanosContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
//app.UseIdentityServer();
app.UseAuthentication();
app.MapControllers();


app.UseCors(options =>
{
    //options.WithOrigins("http://localhost:4200/");
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();

});

app.Run();


