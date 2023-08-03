using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosIdentity.src.Contexts;
using UsuariosIdentity.src.Models.User;
using UsuariosIdentity.src.Services;
using UsuariosIdentity.src.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("connectionDatabase");
builder.Services.AddDbContext<UserContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

//added mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//added identity and scoped 

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ServicesUser>();
builder.Services.AddScoped<TokenServices>();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.GetTempPath()));

//added services Authentication token
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false, 
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication(); 

app.MapControllers();

app.Run();
