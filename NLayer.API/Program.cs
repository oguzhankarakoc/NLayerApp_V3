using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());


//Validasyonlar� kendimiz ekledi�imiz i�in default gelen validasyon filtresi modelini a�a��daki kodla inaktif hale getirdik, art�k kendi validasyon filtre modelimiz �al���yor... Ancak bu bask�lamay� MVC taraf�nda yap�lmas�na gerek yok. Zaten orada aktif a�a��daki kod blo�u...
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddDbContext<AppDbContext>(
    x =>
    {
        x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
        {
            option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
        });
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCustomException();

//app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

//app.MapRazorPages();

app.Run();
