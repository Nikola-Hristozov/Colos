using AutoMapper;
using Color.Services.CouponAPI;
using Color.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.MapControllers();

await ApplyMigration();

app.Run();

async Task ApplyMigration()
{
    await using var scope = app.Services.CreateAsyncScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if ((await _db.Database.GetPendingMigrationsAsync()).Any())
    {
        _db.Database.Migrate();
    }
}
