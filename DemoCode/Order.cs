namespace DemoCode;

using System;
using System.Collections.Generic;

public class Order
{
    public Order(Customer customer)
    {
        this.Customer = customer;
        this.Items = new List<OrderItem>();
    }

    public int Id { get; set; }

    public Customer Customer { get; set; }

    public List<OrderItem> Items { get; set; }

    public DateTime OrderDate { get; set; }

    public override string ToString() => $"{this.Id}-{this.Customer.CustomerName}";
}