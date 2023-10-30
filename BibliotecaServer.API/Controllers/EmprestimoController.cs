using Microsoft.AspNetCore.Mvc;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.Core.Communication.Handlers;
using BibliotecaServer.API.ViewModels;
using AutoMapper;
using BibliotecaServer.API.ViewModels.Usuario;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.API.ViewModels.Livro;
using BibliotecaServer.Application.Services;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using MediatR;

namespace BibliotecaServer.API.Controllers;

[ApiController]
public class EmprestimoController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IEmprestimoService _emprestimoService;
    public EmprestimoController(
        INotificationHandler<DomainNotification> domainNotificationHandler,
        IEmprestimoService emprestimoService,
        IMapper mapper
        ) : base(domainNotificationHandler)
    {
        _mapper = mapper;
        _emprestimoService = emprestimoService;
    }

    [HttpPost]
    [Route("/api/emprestar-livro")]
    public async Task<IActionResult> CreateAsync ([FromBody] RealizarEmprestimoViewModel emprestimoViewModel)
    {

        var emprestimo = await _emprestimoService.CreateAsync(emprestimoViewModel.LivroId, emprestimoViewModel.UsuarioId);

        if (!emprestimo.HasValue)
            return Result();

        return Created(new ResultViewModel
        {
            Message = "Emprestimo realizado com sucesso",
            Success = true,
            Data = emprestimo.Value
        });
    }


    [HttpPost]
    [Route("/api/devolver-livro")]
    public async Task<IActionResult> devolverLivro ([FromBody] DevolverLivroViewModel emprestimoViewModel )
    {
    

        var emprestimo = await _emprestimoService.Devolver(emprestimoViewModel.LivroId);


        if (!emprestimo.HasValue)
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Livro devolvido com sucesso",
            Success = true,
            Data = emprestimo.Value
        });

    }

    [HttpGet]
    [Route("api/emprestimos")]
    public async Task<IActionResult> GetAllEmprestimos()
    {
        var emprestimosDTO = await _emprestimoService.GetAllAsync();

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Emprestimo retornados com sucesso",
            Success = true,
            Data = emprestimosDTO
        });
    }

}

