using ConnexionMongo.Models;
using ConnexionMongo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectPfe.Services;
using ProjectPfe.Services.libs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//générer tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Conexion Database
builder.Services.Configure<DbPfeDatabaseSettings>(
    builder.Configuration.GetSection("dbPfe"));
builder.Services.AddCors();
builder.Services.AddSingleton<IntegrationService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<CategorieService>();
builder.Services.AddSingleton<TitreService>();
builder.Services.AddSingleton<SousTitreService>();
builder.Services.AddSingleton<ParagrapheService>();
builder.Services.AddSingleton<SousParagrapheService>();
builder.Services.AddSingleton<TableauService>();
builder.Services.AddSingleton<LigneService>();
builder.Services.AddSingleton<ColonneService>();

builder.Services.AddSingleton<CategorieService>();

builder.Services.AddSingleton<GridFsStockTemplate>();


builder.Services.AddSingleton<TemplateWordService>();

builder.Services.AddSingleton<InputXmlService>();
/*builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
*/
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));


app.Run();





