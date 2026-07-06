using System;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event triggered when a sale is created.
/// </summary>
public record SaleCreatedEvent(Guid SaleId) : INotification;

/// <summary>
/// Event triggered when a sale is modified.
/// </summary>
public record SaleModifiedEvent(Guid SaleId) : INotification;

/// <summary>
/// Event triggered when a sale is cancelled.
/// </summary>
public record SaleCancelledEvent(Guid SaleId) : INotification;

/// <summary>
/// Event triggered when a sale item is cancelled.
/// </summary>
public record ItemCancelledEvent(Guid SaleId, Guid SaleItemId) : INotification;
