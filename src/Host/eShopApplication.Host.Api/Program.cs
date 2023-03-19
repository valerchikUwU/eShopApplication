using eShopApplication.Infrastructure.DataAccess;
using eShopApplication.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IDbContextOptionsConfigurator<BoardDbContext>, BoardDbContextConfiguration>();


builder.Services.AddDbContext<BoardDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
    ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BoardDbContext>>()
        .Configure((DbContextOptionsBuilder<BoardDbContext>)dbOptions)));

builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BoardDbContext>()));

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
