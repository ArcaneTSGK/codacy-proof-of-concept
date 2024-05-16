using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Codacy.Proof.FirstMonolithicModule.Tests.Unit.ArchitectureTests.Constants;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Codacy.Proof.FirstMonolithicModule.Tests.Unit.ArchitectureTests;

/// <summary>
/// The domain layer should have no dependencies on any other layer.
/// It is only allowed to reference the SharedKernel if needed.
/// </summary>
public class DomainDependencyTests
{
    private static readonly Architecture _architecture =
        new ArchLoader()
            .LoadAssemblies(typeof(AssemblyInfo).Assembly,
                typeof(Contracts.AssemblyInfo).Assembly)
            .Build();

    const string DomainTypesAs = "Module Domain Types";

    [Fact]
    public void DomainLayer_ShouldNotReferenceInfrastructure_WhenGivenModule()
    {
        // Arrange
        var domainTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.DomainNamespace}.*",
                useRegularExpressions: true)
            .As(DomainTypesAs);

        var infrastructureTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.InfrastructureNamespace}.*",
                useRegularExpressions: true)
            .As("Module Infrastructure Types");

        // Act
        var rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);

        // Assert
        rule.Check(_architecture);
    }

    [Fact]
    public void DomainLayer_ShouldNotReferenceApplication_WhenGivenModule()
    {
        // Arrange
        var domainTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.DomainNamespace}.*",
                useRegularExpressions: true)
            .As(DomainTypesAs);

        var applicationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.ApplicationNamespace}.*",
                useRegularExpressions: true)
            .As("Module Application Types");

        // Act
        var rule = domainTypes.Should().NotDependOnAny(applicationTypes);

        // Assert
        rule.Check(_architecture);
    }

    [Fact]
    public void DomainLayer_ShouldNotReferencePresentation_WhenGivenModule()
    {
        // Arrange
        var domainTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.DomainNamespace}.*",
                useRegularExpressions: true)
            .As(DomainTypesAs);

        var presentationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.PresentationNamespace}.*",
                useRegularExpressions: true)
            .As("Module Presentation Types");

        // Act
        var rule = domainTypes.Should().NotDependOnAny(presentationTypes);

        // Assert
        rule.Check(_architecture);
    }

    [Fact]
    public void DomainLayer_ShouldNotReferenceContracts_WhenGivenModule()
    {
        // Arrange
        var domainTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.DomainNamespace}.*",
                useRegularExpressions: true)
            .As(DomainTypesAs);

        var contractTypes = Types().That()
            .ResideInNamespace($"{Contracts.AssemblyInfo.ModuleName}.*",
                useRegularExpressions: true)
            .As("Module Contracts Types");

        // Act
        var rule = domainTypes.Should().NotDependOnAny(contractTypes);

        // Assert
        rule.Check(_architecture);
    }
}
