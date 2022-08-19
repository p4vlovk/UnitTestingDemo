namespace DemoCode.Tests;

using System;
using AutoFixture;
using Xunit;

public class ObjectGraphDemos
{
    [Fact]
    public void ManualCreation()
    {
        // Arrange
        var customer = new Customer { CustomerName = "Jason" };
        var order = new Order(customer)
        {
            Id = 42,
            OrderDate = DateTime.Now,
            Items =
            {
                new OrderItem
                {
                    ProductName = "Rubber duck",
                    Quantity = 2
                }
            }
        };

        // Act
        // Assert
    }

    [Fact]
    public void AutoCreation()
    {
        // Arrange
        var fixture = new Fixture();
        var order = fixture.Create<Order>();

        // Act
        // Assert
    }
}