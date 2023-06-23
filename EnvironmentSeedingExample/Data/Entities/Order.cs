﻿using System;

namespace EnvironmentSeedingExample.Data.Entities;
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}