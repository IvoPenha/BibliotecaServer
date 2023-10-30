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
public class EmprestimoService : IEmprestimoService
{
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly ILivroRepository _livroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public EmprestimoService(IEmprestimoRepository emprestimoRepository, IMediatorHandler mediator, IMapper mapper, ILivroRepository livroRepository, IUsuarioRepository usuarioRepository )
    {
        _emprestimoRepository = emprestimoRepository;
        _mediator = mediator;
        _mapper = mapper;
        _livroRepository = livroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Optional<EmprestimoDTO>> CreateAsync(int idLivro, int idUsuario )
    {
        var livro = await _livroRepository.GetAsync(idLivro);
        if(livro== null) { await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotFound, DomainNotificationType.LivroNotFound));return new Optional<EmprestimoDTO>();}

        var usuario = await _usuarioRepository.GetAsync(idUsuario);
        if(usuario == null) { await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.UsuarioNotFound, DomainNotificationType.UsuarioNotFound));return new Optional<EmprestimoDTO>();}

        if (livro.Disponibilidade == false)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotAvailable, DomainNotificationType.LivroNotAvailable));
            return new Optional<EmprestimoDTO>();
        }

        var dataEmprestimo = DateOnly.FromDateTime(DateTime.Now);
        var dataDevolucao = dataEmprestimo.AddDays(15);

        var emprestimoDTO = new EmprestimoDTO
        {
            LivroId = idLivro,
            UsuarioId = idUsuario,
            DataEmprestimo = dataEmprestimo,
            DataDevolucao = dataDevolucao
        };

        var emprestimo = _mapper.Map<Emprestimo>(emprestimoDTO);
        emprestimo.Validate();
        if (!emprestimo.IsValid)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(
                ErrorMessages.UsuarioInvalid(emprestimo.ErrorsToString()),
                DomainNotificationType.EmprestimoInvalid
                ));
            return new Optional<EmprestimoDTO>();
        }

        var emprestimoCreated = await _emprestimoRepository.CreateAsync(emprestimo);

        livro.Disponibilidade = false;

        await _livroRepository.UpdateAsync(livro);            


        return _mapper.Map<EmprestimoDTO>(emprestimoCreated);
    }

    public async Task<Optional<LivroDTO>> Devolver (int idLivro)
    {
        var livro = await _livroRepository.GetAsync(idLivro);

        if(livro == null) {await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotFound, DomainNotificationType.LivroNotFound));return new Optional<LivroDTO>();}

        if(livro.Disponibilidade == true)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroAlreadyAvailable, DomainNotificationType.LivroAlreadyAvailable));
            return new Optional<LivroDTO>();
        }

        var emprestimo = await _emprestimoRepository.getEmprestimoByLivroID(idLivro);

        if (emprestimo == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.LivroNotFound, DomainNotificationType.LivroNotFound));
            return new Optional<LivroDTO>();
        }

        if(emprestimo.DataDevolucao < DateOnly.FromDateTime(DateTime.Now))
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.EmprestimoLate, DomainNotificationType.EmprestimoLate));
            return new Optional<LivroDTO>();
        }


        livro.Disponibilidade = true;

        var livroUpdated = await _livroRepository.UpdateAsync(livro);            

        return _mapper.Map<LivroDTO>(livroUpdated);
    }

    public async Task<Optional<IList<EmprestimoDTO>>> GetAllAsync()
    {
        var allEmprestimo = await _emprestimoRepository.GetAllAsync();
        var allEmprestimoDTO = _mapper.Map<IList<EmprestimoDTO>>(allEmprestimo);
        return new Optional<IList<EmprestimoDTO>>(allEmprestimoDTO);
    }

    public async Task<Optional<EmprestimoDTO>> GetAsync(int id)
    {
        var emprestimo = await _emprestimoRepository.GetAsync(id);

        if (emprestimo == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.EmprestimoNotFound, DomainNotificationType.EmprestimoNotFound));
            return new Optional<EmprestimoDTO>();
        }
        
        return _mapper.Map<EmprestimoDTO>(emprestimo);
    }

    public async Task RemoveAsync(int id)
    {
        var emprestimo = await _emprestimoRepository.GetAsync(id);

        if (emprestimo == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.EmprestimoNotFound, DomainNotificationType.EmprestimoNotFound));
            return;
        }

        await _emprestimoRepository.RemoveAsync(id);
    }

    public async Task<Optional<EmprestimoDTO>> UpdateAsync(EmprestimoDTO emprestimoDTO)
    {
        var emprestimoExists = await _emprestimoRepository.GetAsync(emprestimoDTO.Id);
        if (emprestimoExists == null)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.EmprestimoNotFound, DomainNotificationType.EmprestimoNotFound));
            return new Optional<EmprestimoDTO>();
        }
        var emprestimo = _mapper.Map<Emprestimo>(emprestimoDTO);
        emprestimo.Validate();
        if (!emprestimo.IsValid)
        {
            await _mediator.PublishDomainNotificationAsync(new DomainNotification(ErrorMessages.UsuarioInvalid(emprestimo.ErrorsToString()),DomainNotificationType.EmprestimoInvalid));
            return new Optional<EmprestimoDTO>();
        }

        var userUpdated = await _emprestimoRepository.UpdateAsync(emprestimo);

        return _mapper.Map<EmprestimoDTO>(userUpdated);

    }

}
