using System.Collections.Generic;
using EnvironmentSeedingExample.Data.EnvironmentFiltering;

namespace EnvironmentSeedingExample.Data.Entities;
[TestData]
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}