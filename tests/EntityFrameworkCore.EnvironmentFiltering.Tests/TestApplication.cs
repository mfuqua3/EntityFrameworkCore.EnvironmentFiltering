using EntityFrameworkCore.EnvironmentFiltering.Example;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkCore.EnvironmentFiltering.Tests;

public class TestApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public TestApplication(string? environment = null)
    {
        _environment = environment ?? Environments.Development;
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.UseEnvironment(_environment);
    }
}