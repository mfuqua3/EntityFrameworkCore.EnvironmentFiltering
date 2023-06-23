using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCore.EnvironmentFiltering;

public class EnvironmentFilteringSqlGenerator : SqlServerMigrationsSqlGenerator
{
    private readonly string _environment;

    public EnvironmentFilteringSqlGenerator(
        MigrationsSqlGeneratorDependencies dependencies,
        ICommandBatchPreparer commandBatchPreparer) : base(dependencies, commandBatchPreparer)
    {
        _environment = dependencies.CurrentContext.Context.GetService<IHostEnvironment>().EnvironmentName;
    }

    protected override void Generate(MigrationOperation operation, IModel? model, MigrationCommandListBuilder builder)
    {
        var operationAnnotation = operation.GetAnnotations().FirstOrDefault(x => x.Name == EnvironmentFilteringConstants.OperationEnvironments);
        if (operationAnnotation is { Value: string operationEnvironments })
        {
            var environmentsArray = operationEnvironments.Split(',');
            if (!environmentsArray.Contains(_environment))
            {
                return;
            }
        }

        base.Generate(operation, model, builder);
    }

    protected override void Generate(InsertDataOperation operation, IModel? model, MigrationCommandListBuilder builder,
        bool terminate = true)
    {
        var entity = model?.GetEntityTypes().SingleOrDefault(x => x.GetTableName() == operation.Table);
        var entityAnnotation = entity?.GetAnnotations().FirstOrDefault(x => x.Name == EnvironmentFilteringConstants.DataEnvironments);
        if (entityAnnotation is { Value: string dataEnvironments })
        {
            var environmentsArray = dataEnvironments.Split(',');
            if (!environmentsArray.Contains(_environment))
            {
                return;
            }
        }

        base.Generate(operation, model, builder, terminate);
    }

    protected override void Generate(DeleteDataOperation operation, IModel? model, MigrationCommandListBuilder builder)
    {
        var entity = model?.GetEntityTypes().SingleOrDefault(x => x.GetTableName() == operation.Table);
        var entityAnnotation = entity?.GetAnnotations().FirstOrDefault(x => x.Name == EnvironmentFilteringConstants.DataEnvironments);
        if (entityAnnotation is { Value: string dataEnvironments })
        {
            var environmentsArray = dataEnvironments.Split(',');
            if (!environmentsArray.Contains(_environment))
            {
                return;
            }
        }

        base.Generate(operation, model, builder);
    }
}