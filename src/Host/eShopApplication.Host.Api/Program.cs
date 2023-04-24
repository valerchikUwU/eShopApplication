using eShopApplication.Application.AppData.Account.Services;
using eShopApplication.Application.AppData.Accounts.Repository;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Application.AppData.Adverts.Service;
using eShopApplication.Application.AppData.Categories.Repository;
using eShopApplication.Application.AppData.Categories.Service;
using eShopApplication.Contracts.Adverts;
using eShopApplication.Infrastructure.DataAccess;
using eShopApplication.Infrastructure.DataAccess.Contexts.Account.Repositories;
using eShopApplication.Infrastructure.DataAccess.Contexts.Advert.Repository;
using eShopApplication.Infrastructure.DataAccess.Contexts.Category.Repositories;
using eShopApplication.Infrastructure.DataAccess.Interfaces;
using eShopApplication.Infrastructure.Repositories;
using eShopApplication.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using eShopApplication.Domain.Account;
using eShopApplication.Infrastructure.DataAccess.Contexts.Account.Handler;
using Microsoft.AspNetCore.Authorization;
using eShopApplication.Infrastructure.DataAccess.Contexts.Account.Requirement;
using eShopApplication.Application.AppData.AccountRole.Service;
using eShopApplication.Application.AppData.AccountRole.Repository;
using eShopApplication.Infrastructure.DataAccess.Contexts.AccountRole.Repository;
using System.Text.Json.Serialization;
using eShopApplication.Application.AppData.File.Repository;
using eShopApplication.Infrastructure.DataAccess.Contexts.File.Repository;
using eShopApplication.Application.AppData.File.Service;
using eShopApplication.Contracts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IDbContextOptionsConfigurator<eShopApplicationDbContext>, eShopApplicationDbContextConfiguration>();


builder.Services.AddDbContext<eShopApplicationDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
    ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<eShopApplicationDbContext>>()
        .Configure((DbContextOptionsBuilder<eShopApplicationDbContext>)dbOptions)));

builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<eShopApplicationDbContext>()));


builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();




// Add repositories to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdvertService, AdvertService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountRoleService, AccountRoleService>();
builder.Services.AddScoped<IFileService, FileService>();


//builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var sectetKey = builder.Configuration["Jwt:Key"];

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = false,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sectetKey))
    };
});

builder.Services.AddSingleton<IAuthorizationHandler, AdminRoleHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, UserRoleHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new AdminRoleRequirement("Admin")));
    options.AddPolicy("UserPolicy", policy => policy.Requirements.Add(new UserRoleRequirement("User")));
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Advert Api", Version = "V1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.  
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer secretKey'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

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
