using Amazon.Runtime;
using Amazon.S3;
using Faahi.Controllers.Application;
using Faahi.Mapper;
using Faahi.Service.Auth;
using Faahi.Service.CoBusiness;
using Faahi.Service.countries;
using Faahi.Service.Email;
using Faahi.Service.im_products;
using Faahi.Service.im_products.category;
using Faahi.Service.im_products.im_purchase;
using Faahi.Service.im_products.im_tags;
using Faahi.Service.PartyService;
using Faahi.Service.Store;
using Faahi.Service.table_key;
using Faahi.Service.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Text;

// Configure Serilog
var builder = WebApplication.CreateBuilder(args);

var logPath = Path.Combine(builder.Environment.ContentRootPath, "Logs", "log-.txt");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Optional: change to Information in production
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 500_000_000; // 500 MB
});

// ? Proper CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Key"]!)),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.MaxDepth = 200; // Increase max depth if you have deeply nested objects
        options.JsonSerializerOptions.WriteIndented = true; // Optional: makes the JSON output easier to read
    });

//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<Itable_key, table_key>();
builder.Services.AddScoped<ICoBusinessservice, CoBusinessService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<Iim_products,im_product>();
builder.Services.AddScoped<IPartyService,PartyService>();
builder.Services.AddScoped<ICategory,CategoryService>();
builder.Services.AddScoped<Iim_tags,im_tags>();
builder.Services.AddScoped<IUser,User_service>();
builder.Services.AddScoped<Iim_purchase,im_purchase_service>();
builder.Services.AddScoped<Istore,store_service>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<Iavl_countries,avl_countries_service>();
builder.Services.AddMemoryCache();

var wasabiOptions = builder.Configuration.GetSection("Wasabi");
var accessKey = wasabiOptions["AccessKey"];
var secretKey = wasabiOptions["SecretKey"];
var serviceUrl = wasabiOptions["ServiceUrl"];

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var credentials = new BasicAWSCredentials(accessKey, secretKey);

    var config = new AmazonS3Config
    {
        ServiceURL = serviceUrl, // Wasabi endpoint
        ForcePathStyle = true    // Required for Wasabi
    };

    return new AmazonS3Client(credentials, config);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.MapScalarApiReference();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Faahi v1");
    });
}
app.Use(async (context, next) =>
{
    // Try to get base URL from custom header or Origin
    var baseUrl = context.Request.Headers["X-Client-BaseUrl"].FirstOrDefault()
               ?? context.Request.Headers["Origin"].FirstOrDefault();

    if (!string.IsNullOrEmpty(baseUrl))
    {
        context.Items["BaseUrl"] = baseUrl;
    }

    await next();
});
app.UseHttpsRedirection();

// ? CORS must be before authentication/authorization
app.UseCors(myAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();  
