using System;
using Environment = Microsoft.Extensions.Hosting.Environments;

namespace EnvironmentSeedingExample.Data.EnvironmentFiltering;

/// <summary>
/// Specifies the environments in which the associated data should be included.
/// </summary>
/// <remarks>
/// When the <see cref="TestDataAttribute"/> is applied to an entity class, all operations that insert or delete data from the annotated entity's table will be impacted.
/// The attribute applies a table-wide annotation to indicate the environments in which the associated data should be included.<br/>
/// <p/>
/// If you need more granular control to exclude specific operations only, consider using the <see cref="OperationBuilderExtensions"/> methods to annotate individual operations.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class TestDataAttribute : Attribute
{
    /// <summary>
    /// Gets the environments in which the associated data should be included.
    /// </summary>
    public string[] Environments { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestDataAttribute"/> class with default environments (Development and Staging).
    /// </summary>
    public TestDataAttribute() : this(Environment.Development, Environment.Staging)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestDataAttribute"/> class with the specified <paramref name="environments"/>.
    /// </summary>
    /// <param name="environments">The environments in which the associated data should be included.</param>
    public TestDataAttribute(params string[] environments)
    {
        Environments = environments;
    }
}