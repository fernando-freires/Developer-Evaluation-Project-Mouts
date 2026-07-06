using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateSale()
    {
        // Arrange
        var mockRepository = Substitute.For<ISaleRepository>();
        var mockMapper = Substitute.For<IMapper>();
        var mockLogger = Substitute.For<ILogger<CreateSaleHandler>>();
        var mockMediator = Substitute.For<IMediator>();

        var command = new CreateSaleCommand
        {
            SaleNumber = "123",
            CustomerId = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemCommand>
            {
                new CreateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100 }
            }
        };

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 5, UnitPrice = 100 }
            }
        };

        mockMapper.Map<Sale>(command).Returns(sale);
        mockRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(sale));

        var handler = new CreateSaleHandler(mockRepository, mockMapper, mockLogger, mockMediator);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sale.Id, result.Id);
        await mockRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await mockMediator.Received(1).Publish(Arg.Any<Ambev.DeveloperEvaluation.Domain.Events.SaleCreatedEvent>(), Arg.Any<CancellationToken>());
    }
}
