using Microsoft.AspNetCore.Mvc;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.Core.Communication.Handlers;
using BibliotecaServer.API.ViewModels;
using AutoMapper;
using BibliotecaServer.API.ViewModels.Usuario;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.API.ViewModels.Livro;
using BibliotecaServer.Application.Services;

namespace BibliotecaServer.API.Controllers;

[ApiController]
public class LivroController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ILivroService _livrosService;
    public LivroController(
        DomainNotificationHandler domainNotificationHandler,
        ILivroService livroService,
        IMapper mapper
        ) : base(domainNotificationHandler)
    {
        _mapper = mapper;
        _livrosService = livroService;
    }

    [HttpPost]
    [Route("/api/livro")]
    public async Task<IActionResult> CreateAsync ([FromBody] CreateLivroViewModel livroViewModel)
    {
        var livroDTO = _mapper.Map<LivroDTO>(livroViewModel);

        var livro = await _livrosService.CreateAsync(livroDTO);

        if (HasNotifications())
            return Result();

        return Created(new ResultViewModel
        {
            Message = "Usuário criado com sucesso",
            Success = true,
            Data = livro.Value
        });
    }


    [HttpPut]
    [Route("/api/livros/{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateLivroViewModel livroViewModel )
    {
        var livroDTO = _mapper.Map<LivroDTO>(livroViewModel);
        if (id != livroDTO.Id)
        {
            return Result();
        }

        var usuario = await _livrosService.UpdateAsync(livroDTO);

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Usuário atualizado com sucesso",
            Success = true,
            Data = usuario.Value
        });

    }

    [HttpDelete]
    [Route("api/livro/{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        await _livrosService.RemoveAsync(id);

        if (HasNotifications())
            return Result();

        return Ok();
    }

    [HttpGet]
    [Route("api/livro/{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _livrosService.GetAsync(id);

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Usuário retornado com sucesso",
            Success = true,
            Data = usuario.Value
        });
    }

    [HttpGet]
    [Route("api/livros")]
    public async Task<IActionResult> GetAllUsuarios()
    {
        var livrosDTO = await _livrosService.GetAllAsync();

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Usuários retornados com sucesso",
            Success = true,
            Data = livrosDTO
        });
    }

}

