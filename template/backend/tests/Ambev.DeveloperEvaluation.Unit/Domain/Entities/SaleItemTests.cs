using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void CalculateDiscountAndTotal_QuantityLessThan4_ShouldHaveNoDiscount()
    {
        // Arrange
        var item = new SaleItem { Quantity = 3, UnitPrice = 100 };

        // Act
        item.CalculateDiscountAndTotal();

        // Assert
        Assert.Equal(0, item.Discount);
        Assert.Equal(300, item.TotalAmount);
    }

    [Fact]
    public void CalculateDiscountAndTotal_Quantity4To9_ShouldHave10PercentDiscount()
    {
        // Arrange
        var item = new SaleItem { Quantity = 5, UnitPrice = 100 };

        // Act
        item.CalculateDiscountAndTotal();

        // Assert
        Assert.Equal(50, item.Discount); // 10% of 500
        Assert.Equal(450, item.TotalAmount);
    }

    [Fact]
    public void CalculateDiscountAndTotal_Quantity10To20_ShouldHave20PercentDiscount()
    {
        // Arrange
        var item = new SaleItem { Quantity = 15, UnitPrice = 100 };

        // Act
        item.CalculateDiscountAndTotal();

        // Assert
        Assert.Equal(300, item.Discount); // 20% of 1500
        Assert.Equal(1200, item.TotalAmount);
    }
}
