using AutoMapper;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.Core.Communication.Mediator.Interfaces;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using BibliotecaServer.Core.Enums;
using BibliotecaServer.Core.Structs;
using BibliotecaServer.Core.Validations.Message;
using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Domain.Interfaces;

namespace BibliotecaServer.Application.Services;
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMediatorHandler mediator, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Optional<UsuarioDTO>> CreateAsync(UsuarioDTO usuarioDTO)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDTO);

        usuario.Validate();
        if (!usuario.IsValid)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                ErrorMessages.UsuarioInvalid(usuario.ErrorsToString()),
                DomainNotificationType.UsuarioInvalid
                ));
            return new Optional<UsuarioDTO>();
        }

        var usuarioCreated = await _usuarioRepository.CreateAsync(usuario);

        return _mapper.Map<UsuarioDTO>(usuarioCreated);
    }


    public async Task<Optional<IList<UsuarioDTO>>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        var usuariosDTO = _mapper.Map<IList<UsuarioDTO>>(usuarios);

        return new Optional<IList<UsuarioDTO>>(usuariosDTO);

    }

    public async Task<Optional<UsuarioDTO>> GetAsync(int id)
    {
        var usuario = await _usuarioRepository.GetAsync(id);

        if (usuario == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.UsuarioNotFound, DomainNotificationType.UsuarioNotFound));
            return new Optional<UsuarioDTO>();
        }

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task RemoveAsync(int id)
    {
        var usuario = await _usuarioRepository.GetAsync(id);

        if (usuario == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.UsuarioNotFound, DomainNotificationType.UsuarioNotFound));
            return;
        }

        await _usuarioRepository.RemoveAsync(id);
    }

    public async Task<Optional<UsuarioDTO>> UpdateAsync(int id, UsuarioDTO usuarioDTO)
    {
        if (id != usuarioDTO.Id)
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.IdMismatch, DomainNotificationType.IdMismatch));

        var usuarioExists = await _usuarioRepository.GetAsync(usuarioDTO.Id);
        if (usuarioExists == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.UsuarioNotFound, DomainNotificationType.UsuarioNotFound));
            return new Optional<UsuarioDTO>();
        }

        var usuario = _mapper.Map<Usuario>(usuarioDTO);

        usuario.Validate();
        if (!usuario.IsValid)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.UsuarioInvalid(usuario.ErrorsToString()),DomainNotificationType.UsuarioInvalid));
            return new Optional<UsuarioDTO>();
        }

        var usuarioUpdated = await _usuarioRepository.UpdateAsync(usuario);

        return _mapper.Map<UsuarioDTO>(usuarioUpdated);

    }
}
