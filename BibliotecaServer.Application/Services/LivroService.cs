using AutoMapper;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.Application.Interfaces;
using BibliotecaServer.Application.Utilities;
using BibliotecaServer.Core.Communication.Mediator.Interfaces;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using BibliotecaServer.Core.Enums;
using BibliotecaServer.Core.Structs;
using BibliotecaServer.Core.Validations.Message;
using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Domain.Interfaces;

namespace BibliotecaServer.Application.Services;
public class LivroService : ILivroService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public LivroService(ILivroRepository livroRepository, IMediatorHandler mediator, IMapper mapper)
    {
        _livroRepository = livroRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Optional<LivroDTO>> CreateAsync(LivroDTO livroDTO)
    {
        var livro = _mapper.Map<Livro>(livroDTO);

        livro.Validate();
        if (!livro.IsValid)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                ErrorMessages.LivroInvalid(livro.ErrorsToString()),
                DomainNotificationType.LivroInvalid
                ));
            return new Optional<LivroDTO>();
        }

        var livroCreated = await _livroRepository.CreateAsync(livro);

        return _mapper.Map<LivroDTO>(livroCreated);
    }

    public async Task<IList<LivroDTO>> GetAllAsync()
    {
        var livros = await _livroRepository.GetAllAsync();

        return _mapper.Map<IList<LivroDTO>>(livros);
    }

    public async Task<Optional<LivroDTO>> GetAsync(int id)
    {
        var livro = await _livroRepository.GetAsync(id);

        if (livro == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotFound,DomainNotificationType.LivroNotFound));
            return new Optional<LivroDTO>();
        }

        return _mapper.Map<LivroDTO>(livro);
    }

    public async Task RemoveAsync(int id)
    {
        var livro = await _livroRepository.GetAsync(id);

        if (livro == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotFound,DomainNotificationType.LivroNotFound));
            return;
        }

        await _livroRepository.RemoveAsync(id);
    }

  
    public async Task<IList<LivroDTO>> SearchAsync(LivroSearchParameters parametros)
    {
        var livros = await _livroRepository.SearchAsync(l =>
            (string.IsNullOrEmpty(parametros.Titulo) || l.Titulo.Contains(parametros.Titulo)) &&
            (string.IsNullOrEmpty(parametros.Autor) || l.Autor.Contains(parametros.Autor)) &&
            (string.IsNullOrEmpty(parametros.Ano) || l.Ano.ToString().Contains(parametros.Ano)));

        return _mapper.Map<IList<LivroDTO>>(livros);
    }
    public async Task<Optional<LivroDTO>> UpdateAsync(int id,LivroDTO livroDTO)
    {
        if(id != livroDTO.Id)
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.IdMismatch,DomainNotificationType.IdMismatch));


        var livroExists = await _livroRepository.GetAsync(livroDTO.Id);
        if (livroExists == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotFound,DomainNotificationType.LivroNotFound));
            return new Optional<LivroDTO>();
        }
        var livro = _mapper.Map<Livro>(livroDTO);
        livro.Validate();
        if (!livro.IsValid)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroInvalid(livro.ErrorsToString()),DomainNotificationType.LivroInvalid));
            return new Optional<LivroDTO>();
        }

        var livroUpdated = await _livroRepository.UpdateAsync(livro);

        return _mapper.Map<LivroDTO>(livroUpdated);

    }
}
