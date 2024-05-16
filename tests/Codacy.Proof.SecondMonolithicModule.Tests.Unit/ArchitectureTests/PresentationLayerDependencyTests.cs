using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Codacy.Proof.SecondMonolithicModule.Tests.Unit.ArchitectureTests.Constants;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Codacy.Proof.SecondMonolithicModule.Tests.Unit.ArchitectureTests;

/// <summary>
/// Presentation layer is allowed to reference the application layer.
/// Presentation layer is allowed to reference the domain layer.
/// Presentation layer is allowed to reference the SharedKernel project.
/// Presentation layer is allowed to reference the contracts project.
/// </summary>

public class PresentationLayerDependencyTests
{
    private static readonly Architecture _architecture =
        new ArchLoader()
            .LoadAssemblies(typeof(AssemblyInfo).Assembly)
            .Build();

    [Fact]
    public void Presentation_ShouldNotReferenceInfrastructure_WhenGivenModule()
    {
        // Arrange
        var presentationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.PresentationNamespace}.*",
                useRegularExpressions: true)
            .As("Module Presentation Types");

        var infrastructureTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.InfrastructureNamespace}.*",
                useRegularExpressions: true)
            .As("Module Infrastructure Types");

        // Act
        var rule = presentationTypes.Should().NotDependOnAny(infrastructureTypes);

        // Assert
        rule.Check(_architecture);
    }
}
