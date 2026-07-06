using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleEventHandlers :
    INotificationHandler<SaleCreatedEvent>,
    INotificationHandler<SaleModifiedEvent>,
    INotificationHandler<SaleCancelledEvent>,
    INotificationHandler<ItemCancelledEvent>
{
    private readonly ILogger<SaleEventHandlers> _logger;

    public SaleEventHandlers(ILogger<SaleEventHandlers> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Published: SaleCreated for SaleId {SaleId}", notification.SaleId);
        return Task.CompletedTask;
    }

    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Published: SaleModified for SaleId {SaleId}", notification.SaleId);
        return Task.CompletedTask;
    }

    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Published: SaleCancelled for SaleId {SaleId}", notification.SaleId);
        return Task.CompletedTask;
    }

    public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Published: ItemCancelled for SaleId {SaleId}, ItemId {SaleItemId}", notification.SaleId, notification.SaleItemId);
        return Task.CompletedTask;
    }
}
