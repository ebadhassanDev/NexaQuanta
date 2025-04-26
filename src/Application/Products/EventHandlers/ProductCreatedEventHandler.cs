using Microsoft.Extensions.Logging;
using NexaQuanta.Domain.Events;

namespace NexaQuanta.Application.Products.EventHandlers;
public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("NexaQuanta Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
