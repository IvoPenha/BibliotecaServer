using AutoMapper;
using BibliotecaServer.Core.Communication.Handlers;
using BibliotecaServer.Core.Communication.Mediator.Interfaces;
using BibliotecaServer.Core.Communication.Mediator;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using MediatR;
using System.Reflection;
using BibliotecaServer.Domain.Interfaces;
using BibliotecaServer.Infra.Repositories;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.Application.Services;
using BibliotecaServer.API.ViewModels.Usuario;
using BibliotecaServer.Domain.Entities;
using BibliotecaServer.API.ViewModels.Livro;
using Microsoft.EntityFrameworkCore;
using BibliotecaServer.Infra.Context;
using BibliotecaServer.Application.DTO;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<BibliotecaDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Mediator

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
builder.Services.AddScoped<DomainNotificationHandler>();

#endregion

#region Repositories

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

#endregion

#region Services

builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

#endregion

#region AutoMapper

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<UsuarioDTO, Usuario>().ReverseMap();
    cfg.CreateMap<CreateUsuarioViewModel, UsuarioDTO>().ReverseMap();
    cfg.CreateMap<UpdateUsuarioViewModel, UsuarioDTO>().ReverseMap();
    
    cfg.CreateMap<CreateLivroViewModel, LivroDTO>().ReverseMap();
    cfg.CreateMap<LivroDTO, Livro>().ReverseMap();
    cfg.CreateMap<UpdateLivroViewModel, LivroDTO>().ReverseMap();
    
    cfg.CreateMap<EmprestimoDTO, Emprestimo>().ReverseMap();

});

var mapper = autoMapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


#endregion

var app = builder.Build();

app.UseCors(app => app.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();