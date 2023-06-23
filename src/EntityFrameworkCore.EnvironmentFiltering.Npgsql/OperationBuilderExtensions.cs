using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCore.EnvironmentFiltering;

/// <summary>
/// Provides extension methods for excluding specific environments from migration operations.
/// </summary>
public static class OperationBuilderExtensions
{
    /// <summary>
    /// Excludes the production environment from the specified <paramref name="operation"/>.
    /// </summary>
    /// <typeparam name="T">The type of migration operation.</typeparam>
    /// <param name="operation">The operation builder.</param>
    /// <returns>The operation builder with excluded production environment.</returns>
    /// <remarks>
    /// This method adds an annotation to the migration operation indicating that it should be excluded from the production environment.
    /// Use this method to exclude specific migration operations from being executed in the production environment.
    /// </remarks>
    public static OperationBuilder<T> ExcludeProductionEnvironment<T>(this OperationBuilder<T> operation)
        where T : MigrationOperation
        => operation.ForEnvironments(Environments.Development, Environments.Staging);
    
    /// <summary>
    /// Excludes the specified <paramref name="environments"/> from the <paramref name="operation"/>.
    /// </summary>
    /// <typeparam name="T">The type of migration operation.</typeparam>
    /// <param name="operation">The operation builder.</param>
    /// <param name="environments">The environments to exclude.</param>
    /// <returns>The operation builder with excluded environments.</returns>
    /// <remarks>
    /// This method adds an annotation to the migration operation indicating the environments to exclude it from.
    /// Use this method to exclude specific migration operations from being executed in certain environments.
    /// </remarks>
    public static OperationBuilder<T> ForEnvironments<T>(this OperationBuilder<T> operation, params string[] environments)
        where T : MigrationOperation
    {
        return operation.Annotation("OperationEnvironments", string.Join(',', environments));
    }
}