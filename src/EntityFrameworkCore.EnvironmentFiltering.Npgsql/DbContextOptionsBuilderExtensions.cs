using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore.EnvironmentFiltering;

/// <summary>
/// Provides extension methods for configuring environment seeding annotations in <see cref="DbContextOptionsBuilder"/>.
/// </summary>
public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Enables the usage of environment seeding annotations in <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/>.</param>
    /// <returns>The <see cref="DbContextOptionsBuilder"/> with environment seeding annotations enabled.</returns>
    /// <remarks>
    /// This method replaces the default <see cref="IMigrationsSqlGenerator"/> and <see cref="IModelCustomizer"/> services with the environment filtering implementations.
    /// Use this method to enable the environment filtering for data seeding operations and migration generation.
    /// </remarks>
    public static DbContextOptionsBuilder UseEnvironmentSeedingAnnotations(this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IMigrationsSqlGenerator, EnvironmentFilteringSqlGenerator>();
        optionsBuilder.ReplaceService<IModelCustomizer, EnvironmentFilteringModelCustomizer>();
        return optionsBuilder;
    }
}