using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    public Guid Id { get; set; }

    public DeleteSaleCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteSaleResponse
{
    public bool Success { get; set; }
}
