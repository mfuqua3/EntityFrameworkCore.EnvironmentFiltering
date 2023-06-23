using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EnvironmentSeedingExample.Data.EnvironmentFiltering;

public class EnvironmentFilteringModelCustomizer : ModelCustomizer
{
    public EnvironmentFilteringModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
    {
    }

    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetCustomAttribute<TestDataAttribute>() is { } testDataAttribute)
            {
                entityType.SetOrRemoveAnnotation(EnvironmentFilteringConstants.DataEnvironments,
                    string.Join(',', testDataAttribute.Environments));
            }
        }

        base.Customize(modelBuilder, context);
    }
}