using Microsoft.AspNetCore.Mvc;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.Core.Communication.Handlers;
using BibliotecaServer.API.ViewModels;
using AutoMapper;
using BibliotecaServer.API.ViewModels.Usuario;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using MediatR;

namespace BibliotecaServer.API.Controllers;

[ApiController]
public class UsuarioController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(
        INotificationHandler<DomainNotification> domainNotificationHandler,
        IUsuarioService usuarioService,
        IMapper mapper
        ) : base(domainNotificationHandler)
    {
        _mapper = mapper;
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [Route("/api/usuarios")]
    public async Task<IActionResult> CreateAsync ([FromBody] CreateUsuarioViewModel usuarioViewModel)
    {
        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuarioViewModel);

        var usuario = await _usuarioService.CreateAsync(usuarioDTO);

        if (HasNotifications())
            return Result();

        return Created(new ResultViewModel
        {
            Message = "Usuário criado com sucesso",
            Success = true,
            Data = usuario.Value
        });
    }


    [HttpPut]
    [Route("/api/usuarios/{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioViewModel usuarioViewModel )
    {
        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuarioViewModel);

        var usuario = await _usuarioService.UpdateAsync(id, usuarioDTO);

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
    [Route("api/usuario/{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        await _usuarioService.RemoveAsync(id);

        if (HasNotifications())
            return Result();

        return Ok();
    }

    [HttpGet]
    [Route("api/usuarios/{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _usuarioService.GetAsync(id);

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
    [Route("api/usuarios")]
    public async Task<IActionResult> GetAllUsuarios()
    {
        var usuarios = await _usuarioService.GetAllAsync();

        if (HasNotifications())
            return Result();

        return Ok(new ResultViewModel
        {
            Message = "Usuários retornados com sucesso",
            Success = true,
            Data = usuarios.Value
        });
    }

}

