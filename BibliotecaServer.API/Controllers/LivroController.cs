using Microsoft.AspNetCore.Mvc;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.API.ViewModels;
using AutoMapper;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.API.ViewModels.Livro;
using BibliotecaServer.Application.Utilities;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using MediatR;

namespace BibliotecaServer.API.Controllers;

[ApiController]
public class LivroController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ILivroService _livrosService;
    public LivroController(
        INotificationHandler<DomainNotification> domainNotificationHandler,
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
            Message = "Livro criado com sucesso",
            Success = true,
            Data = livro.Value
        });
    }


    [HttpPut]
    [Route("/api/livros/{id}")]
    public async Task<IActionResult> UpdateLivro(int id, [FromBody] UpdateLivroViewModel livroViewModel )
    {
        var livroDTO = _mapper.Map<LivroDTO>(livroViewModel);

        var livro = await _livrosService.UpdateAsync(id,livroDTO);

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Livro atualizado com sucesso",
            Success = true,
            Data = livro.Value
        });

    }

    [HttpDelete]
    [Route("api/livro/{id}")]
    public async Task<IActionResult> DeleteLivro(int id)
    {
        await _livrosService.RemoveAsync(id);

        if (HasNotifications())
            return Result();

        return Ok();
    }

    [HttpGet]
    [Route("api/livro/{id}")]
    public async Task<IActionResult> GetLivro(int id)
    {
        var livro = await _livrosService.GetAsync(id);

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Livro retornado com sucesso",
            Success = true,
            Data = livro.Value
        });
    }

    [HttpGet]
    [Route("api/livros")]
    public async Task<IActionResult> GetAllLivros([FromQuery] LivroSearchParameters? searchParams )
    {
        if (searchParams != null)
        {
            var livrosFiltradosDTO = await _livrosService.SearchAsync(searchParams);

            if (HasNotifications())
                return Result();

            return Ok(new ResultViewModel
            {
                Message = "Livros retornados com sucesso",
                Success = true,
                Data = livrosFiltradosDTO
            });
        }

        var livrosDTO = await _livrosService.GetAllAsync();

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Livros retornados com sucesso",
            Success = true,
            Data = livrosDTO
        });
    }

}

