using MediatR;
using BibliotecaServer.Core.Communication.Mediator.Interfaces;
using BibliotecaServer.Core.Communication.Messages.Notifications;

namespace BibliotecaServer.Core.Communication.Mediator;
public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishDomainNotificationAsync<T>(T appNotification) 
        where T : DomainNotification
        => await _mediator.Publish(appNotification);
}

