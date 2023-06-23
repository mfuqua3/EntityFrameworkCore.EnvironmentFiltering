using System.Collections;
using EnvironmentSeedingExample.Data;
using EnvironmentSeedingExample.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnvironmentSeedingExample.Tests;

[TestFixture]
public class MigrationsValidationTests
{
    [OneTimeSetUp]
    public async Task EnsureFreshDatabase()
    {
        var app = new TestApplication();
        await using var context = GetContext(app);
        await context.Database.EnsureDeletedAsync();
    }
    
    [TestCaseSource(nameof(TestCases))]
    public async Task<bool> CheckForTestData<T>(string environment, T _) where T : class
    {
        var app = new TestApplication(environment);
        await using var migrationContext = GetContext(app);
        await migrationContext.Database.MigrateAsync();
        await using var queryContext = GetContext(app);
        return await queryContext.Set<T>().AnyAsync();
    }
    
    [TearDown]
    public async Task TearDownDatabase()
    {
        var app = new TestApplication();
        await using var context = GetContext(app);
        await context.Database.EnsureDeletedAsync();
    }

    private static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData(Environments.Development, new Product()).Returns(true);
            yield return new TestCaseData(Environments.Staging, new Product()).Returns(true);
            yield return new TestCaseData(Environments.Production, new Product()).Returns(true);
            yield return new TestCaseData(Environments.Development, new Order()).Returns(true);
            yield return new TestCaseData(Environments.Staging, new Order()).Returns(true);
            yield return new TestCaseData(Environments.Production, new Order()).Returns(false);
            yield return new TestCaseData(Environments.Development, new Customer()).Returns(true);
            yield return new TestCaseData(Environments.Staging, new Customer()).Returns(true);
            yield return new TestCaseData(Environments.Production, new Customer()).Returns(false);
        }
    }

    private MyAppDbContext GetContext(TestApplication application)
    {
        var provider = application.Services;
        var scope = provider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<MyAppDbContext>();
    }
}