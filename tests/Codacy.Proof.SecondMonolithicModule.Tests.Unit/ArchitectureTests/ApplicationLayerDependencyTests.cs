using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Codacy.Proof.SecondMonolithicModule.Tests.Unit.ArchitectureTests.Constants;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Codacy.Proof.SecondMonolithicModule.Tests.Unit.ArchitectureTests;
/// <summary>
/// The application layer is allowed to reference the domain layer.
/// It can also reference contracts and the SharedKernel project.
/// </summary>
public class ApplicationLayerDependencyTests
{
    private static readonly Architecture _architecture =
        new ArchLoader()
            .LoadAssemblies(typeof(AssemblyInfo).Assembly)
            .Build();

    const string ApplicationTypesAs = "Module Application Types";

    [Fact]
    public void ApplicationLayer_ShouldNotReferenceInfrastructure_WhenGivenModule()
    {
        // Arrange
        var applicationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.ApplicationNamespace}.*",
                useRegularExpressions: true)
            .As(ApplicationTypesAs);

        var infrastructureTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.InfrastructureNamespace}.*",
                useRegularExpressions: true)
            .As("Module Infrastructure Types");

        // Act
        var rule = applicationTypes.Should().NotDependOnAny(infrastructureTypes);

        // Assert
        rule.Check(_architecture);
    }

    [Fact]
    public void ApplicationLayer_ShouldNotReferencePresentation_WhenGivenModule()
    {
        // Arrange
        var applicationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.ApplicationNamespace}.*",
                useRegularExpressions: true)
            .As(ApplicationTypesAs);

        var presentationTypes = Types().That()
            .ResideInNamespace($"{AssemblyInfo.ModuleName}.{LayerNamespaces.PresentationNamespace}.*",
                useRegularExpressions: true)
            .As("Module Presentation Types");

        // Act
        var rule = applicationTypes.Should().NotDependOnAny(presentationTypes);

        // Assert
        rule.Check(_architecture);
    }
}

