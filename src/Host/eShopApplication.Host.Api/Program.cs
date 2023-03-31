using eShopApplication.Application.AppData.Account.Services;
using eShopApplication.Application.AppData.Accounts.Repository;
using eShopApplication.Application.AppData.Adverts.Repository;
using eShopApplication.Application.AppData.Adverts.Service;
using eShopApplication.Infrastructure.DataAccess;
using eShopApplication.Infrastructure.DataAccess.Contexts.Account.Repositories;
using eShopApplication.Infrastructure.DataAccess.Contexts.Advert.Repository;
using eShopApplication.Infrastructure.DataAccess.Interfaces;
using eShopApplication.Infrastructure.Repositories;
using eShopApplication.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IDbContextOptionsConfigurator<eShopApplicationDbContext>, eShopApplicationDbContextConfiguration>();


builder.Services.AddDbContext<eShopApplicationDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
    ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<eShopApplicationDbContext>>()
        .Configure((DbContextOptionsBuilder<eShopApplicationDbContext>)dbOptions)));

builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<eShopApplicationDbContext>()));

// Add repositories to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdvertService, AdvertService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
