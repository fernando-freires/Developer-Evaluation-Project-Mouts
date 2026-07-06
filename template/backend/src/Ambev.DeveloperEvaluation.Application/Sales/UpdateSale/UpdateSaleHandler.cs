using MediatR;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<UpdateSaleHandler> logger,
        IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with id {command.Id} not found");

        _mapper.Map(command, existingSale);
        
        foreach (var item in existingSale.Items)
        {
            if (!item.IsCancelled)
                item.CalculateDiscountAndTotal();
        }

        existingSale.CalculateTotalAmount();
        existingSale.UpdatedAt = DateTime.UtcNow;

        await _saleRepository.UpdateAsync(existingSale, cancellationToken);

        _logger.LogInformation("Sale {SaleId} updated successfully.", existingSale.Id);
        
        await _mediator.Publish(new SaleModifiedEvent(existingSale.Id), cancellationToken);

        return new UpdateSaleResult { Id = existingSale.Id };
    }
}
