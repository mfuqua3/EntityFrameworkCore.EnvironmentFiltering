using System.Collections.Generic;
using EnvironmentSeedingExample.Data.Entities;
using EnvironmentSeedingExample.Data.Utilities;

namespace EnvironmentSeedingExample.Data.Seeds;

public class CustomerSeed : DataSeed<Customer>
{
    protected override IEnumerable<Customer> SeedData => new List<Customer>
    {
        new Customer { Id = 1, Name = "Chuck Norris", Email = "chuck@norris.com" },
        new Customer { Id = 2, Name = "Minnie Mouse", Email = "minnie@mouse.com" },
    };
}