# Environment Filtering for Entity Framework Core Migrations

This feature enhances Entity Framework Core by allowing the exclusion of specific migrations or data operations based on the current environment. It provides two approaches for environment filtering: using attributes on entity classes and using extension methods on the migration builder fluent API.

## Getting Started

To enable this feature, ensure that you call `UseEnvironmentSeedingAnnotations()` when configuring the DbContext

```csharp
public class Startup
{
	public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyAppDbContext>(x =>
            {
                x.UseNpgsql(connectionString);
                x.UseEnvironmentSeedingAnnotations();
            });
			
			/// other services
        }
}
```

## Attribute-Based Environment Filtering

The attribute-based approach utilizes attributes on entity classes to annotate them with the environments in which the associated data should be included. This approach applies to data seeding operations performed during migrations.

### Usage

Annotate the entity classes with the `[TestData]` attribute to specify the environments where the associated data should be included.

```csharp
[TestData]
public class Customer
{
    // Entity properties...
}
```

> **! Warning !** 
> 
> Attribute based filtering works by updating the model snapshot's annotation for the table data. If you enable this feature for an existing table, 
> it may not properly append the annotation to the snapshot since the entity hasn't actually changed. Verify the annotation exists in the snapshot, or you may 
> add it manually if it does not appear.

```csharp
[DbContext(typeof(MyAppDbContext))]
    partial class MyAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            //Other modelbuilder calls

            modelBuilder.Entity("EnvironmentSeedingExample.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasAnnotation("DataEnvironments", "Development,Staging"); //<<--- this must exist
                }
            }
        }
    }
```

## Operation-Based Environment Filtering

The operation-based approach allows for annotating specific migration operations to exclude them from specific environments. This approach applies to explicit data insertion or deletion operations in migrations.

### Usage

Use the `ExcludeProductionEnvironment()` or `ForEnvironments()` extension methods on the migration builder fluent API to exclude specific operations from the production environment.

```csharp
migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "OrderNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 23, 12, 9, 8, 144, DateTimeKind.Utc).AddTicks(6977), "ORD001" },
                    { 2, 2, new DateTime(2023, 6, 23, 12, 9, 8, 144, DateTimeKind.Utc).AddTicks(7028), "ORD002" }
                }).ExcludeProductionEnvironment();
```