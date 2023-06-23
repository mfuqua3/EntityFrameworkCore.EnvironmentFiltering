using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnvironmentSeedingExample.Data.EnvironmentFiltering;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseEnvironmentSeedingAnnotations(this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IMigrationsSqlGenerator, EnvironmentFilteringSqlGenerator>();
        optionsBuilder.ReplaceService<IModelCustomizer, EnvironmentFilteringModelCustomizer>();
        return optionsBuilder;
    }
}