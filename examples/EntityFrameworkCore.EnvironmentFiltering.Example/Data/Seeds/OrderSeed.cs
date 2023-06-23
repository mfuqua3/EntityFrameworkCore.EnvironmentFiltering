using System;
using System.Collections.Generic;
using EntityFrameworkCore.EnvironmentFiltering.Example.Data.Entities;
using EntityFrameworkCore.EnvironmentFiltering.Example.Data.Utilities;

namespace EntityFrameworkCore.EnvironmentFiltering.Example.Data.Seeds;

public class OrderSeed : DataSeed<Order>
{
    protected override IEnumerable<Order> SeedData => new List<Order>
    {
        new Order { Id = 1, OrderNumber = "ORD001", OrderDate = DateTime.UtcNow, CustomerId = 1 },
        new Order { Id = 2, OrderNumber = "ORD002", OrderDate = DateTime.UtcNow, CustomerId = 2 },
    };
}