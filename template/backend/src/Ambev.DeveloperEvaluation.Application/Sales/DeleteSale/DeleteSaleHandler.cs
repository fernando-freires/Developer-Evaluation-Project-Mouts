using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<DeleteSaleHandler> _logger;
    private readonly IMediator _mediator;

    public DeleteSaleHandler(
        ISaleRepository saleRepository,
        ILogger<DeleteSaleHandler> logger,
        IMediator mediator)
    {
        _saleRepository = saleRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _saleRepository.DeleteAsync(command.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale with id {command.Id} not found");

        _logger.LogInformation("Sale {SaleId} deleted successfully.", command.Id);

        // We treat deletion as cancellation for event purposes
        await _mediator.Publish(new SaleCancelledEvent(command.Id), cancellationToken);

        return new DeleteSaleResponse { Success = true };
    }
}
