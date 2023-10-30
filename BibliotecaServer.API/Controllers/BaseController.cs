using BibliotecaServer.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BibliotecaServer.Core.Communication.Handlers;
using BibliotecaServer.Core.Communication.Messages.Notifications;
using BibliotecaServer.Core.Enums;

namespace BibliotecaServer.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private readonly DomainNotificationHandler _domainNotificationHandler;

    protected BaseController(
        INotificationHandler<DomainNotification> domainNotificationHandler)
    {
        _domainNotificationHandler = (DomainNotificationHandler)domainNotificationHandler;
    }

    protected bool HasNotifications()
        => _domainNotificationHandler.HasNotifications();

    protected ObjectResult Created(dynamic responseObject)
        => StatusCode(201, responseObject);

    protected ObjectResult Result()
    {
        var notification = _domainNotificationHandler.Notifications.FirstOrDefault();

        return StatusCode(statusCode: GetStatusCodeByNotificationType(notification!.Type),
            new ResultViewModel
            {
                Message = notification.Message,
                Success = false,
                Data = new { }
            });
    }

    private int GetStatusCodeByNotificationType(DomainNotificationType errorType)
    {
        return errorType switch
        {
            //Unprocessable Entity
            ((DomainNotificationType.EmprestimoInvalid) or (DomainNotificationType.LivroInvalid) or (DomainNotificationType.UsuarioInvalid))
                => 422,

            //Not Found
            ((DomainNotificationType.EmprestimoNotFound) or (DomainNotificationType.LivroNotFound) or (DomainNotificationType.UsuarioNotFound))
                => 404,

            (_) => 500,
        };
    }
}