using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets the sale id.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the product id (External Identity).
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the quantity of the product.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the discount applied to the item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets the total amount of the item (Quantity * UnitPrice - Discount).
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether the item was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    public SaleItem()
    {
    }

    /// <summary>
    /// Calculates the discount based on the quantity and sets the TotalAmount.
    /// Rules:
    /// - < 4 items: no discount
    /// - 4-9 items: 10% discount
    /// - 10-20 items: 20% discount
    /// - > 20 items: not allowed (should be validated before)
    /// </summary>
    public void CalculateDiscountAndTotal()
    {
        if (Quantity < 4)
        {
            Discount = 0;
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            Discount = (UnitPrice * Quantity) * 0.10m;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = (UnitPrice * Quantity) * 0.20m;
        }

        TotalAmount = (UnitPrice * Quantity) - Discount;
    }

    /// <summary>
    /// Cancels the item.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }
}
