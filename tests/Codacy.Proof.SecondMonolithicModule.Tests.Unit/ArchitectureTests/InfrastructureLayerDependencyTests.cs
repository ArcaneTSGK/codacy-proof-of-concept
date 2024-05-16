using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Codacy.Proof.SecondMonolithicModule.Tests.Unit.ArchitectureTests.Constants;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Codacy.Proof.SecondMonolithicModule.Tests.Unit.ArchitectureTests;

/// <summary>
/// The infrastructure layer is allowed to reference the domain layer.
/// The infrastructure layer is allowed to reference the application layer.
/// The infrastructure layer is allowed to reference the SharedKernel project.
/// </summary>
public class InfrastructureLayerDependencyTests
{
    private static readonly Architecture _architecture =
        new ArchLoader()
            .LoadAssemblies(typeof(AssemblyInfo).Assembly,
            typeof(Contracts.AssemblyInfo).Assembly)
            .Build();

    [Fact]
    public void InfrastructureLayer_ShouldNotReferencePresentation_WhenGivenModule()
    {
        // Arrange
        var infrastructureTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.InfrastructureNamespace}.*",
                useRegularExpressions: true)
            .As("Module Infrastructure Types");

        var presentationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.PresentationNamespace}.*",
                useRegularExpressions: true)
            .As("Module Presentation Types");

        // Act
        var rule = infrastructureTypes.Should().NotDependOnAny(presentationTypes);

        // Assert
        rule.Check(_architecture);
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotReferenceContracts_WhenGivenModule()
    {
        // Arrange
        var infrastructureTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.InfrastructureNamespace}.*",
                useRegularExpressions: true)
            .As("Module Infrastructure Types");

        var contractsTypes = Types().That()
            .ResideInNamespace($"{Contracts.AssemblyInfo.ModuleName}.*",
                useRegularExpressions: true)
            .As("Module Contracts Types");

        // Act
        var rule = infrastructureTypes.Should().NotDependOnAny(contractsTypes);

        // Assert
        rule.Check(_architecture);
    }
}
