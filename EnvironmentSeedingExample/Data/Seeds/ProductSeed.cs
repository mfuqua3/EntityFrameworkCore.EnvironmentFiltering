using System.Collections.Generic;
using EnvironmentSeedingExample.Data.Entities;
using EnvironmentSeedingExample.Data.Utilities;

namespace EnvironmentSeedingExample.Data.Seeds;

public class ProductSeed : DataSeed<Product>
{
    protected override IEnumerable<Product> SeedData => new List<Product>
    {
        new Product { Id = 1, Name = "Laughing Gas in a Can", Price = 10.99m },
        new Product { Id = 2, Name = "Invisible Ink Pen", Price = 19.99m },
    };
}