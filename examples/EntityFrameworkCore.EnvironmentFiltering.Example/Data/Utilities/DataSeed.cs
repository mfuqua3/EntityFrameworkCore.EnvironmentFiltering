using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.EnvironmentFiltering.Example.Data.Utilities;

public abstract class DataSeed<T> : IEntityTypeConfiguration<T> where T : class
{
    protected abstract IEnumerable<T> SeedData { get; }

    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasData(SeedData);
    }
}