using System;
using Environment = Microsoft.Extensions.Hosting.Environments;

namespace EnvironmentSeedingExample.Data.EnvironmentFiltering;

[AttributeUsage(validOn: AttributeTargets.Class)]
public class TestDataAttribute : Attribute
{
    public string[] Environments { get; }

    public TestDataAttribute() : this(Environment.Development, Environment.Staging)
    {
    }

    public TestDataAttribute(params string[] environments)
    {
        Environments = environments;
    }
}