using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Extensions.Hosting;

namespace EnvironmentSeedingExample.Data.EnvironmentFiltering;

public static class OperationBuilderExtensions
{
    public static OperationBuilder<T> ExcludeProductionEnvironment<T>(this OperationBuilder<T> operation)
        where T : MigrationOperation
        => operation.ForEnvironments(Environments.Development, Environments.Staging);
    
    public static OperationBuilder<T> ForEnvironments<T>(this OperationBuilder<T> operation, params string[] environments)
        where T : MigrationOperation
    {
        return operation.Annotation("OperationEnvironments", string.Join(',', environments));
    }
}