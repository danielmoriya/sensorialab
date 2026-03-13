using NetArchTest.Rules;
using Xunit;
using Sensorialab.Domain.Entities;

namespace Sensorialab.Tests.Architecture;

public class ArchitectureTests
{
    private const string DomainNamespace = "Sensorialab.Domain";
    private const string ApplicationNamespace = "Sensorialab.Application";
    private const string InfrastructureNamespace = "Sensorialab.Infrastructure";
    private const string ApiNamespace = "Sensorialab.Api";

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
    {
        var result = Types.InAssembly(typeof(BaseEntity).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace, ApiNamespace)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Infrastructure()
    {
        var result = Types.InAssembly(typeof(Application.Interfaces.IAppDbContext).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny(InfrastructureNamespace, ApiNamespace)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
