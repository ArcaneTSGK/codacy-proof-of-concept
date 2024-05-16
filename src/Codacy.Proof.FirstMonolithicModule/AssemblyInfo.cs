using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Codacy.Proof.FirstMonolithicModule.Tests.Unit")]
[assembly: InternalsVisibleTo("Codacy.Proof.FirstMonolithicModule.Tests.Integration")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Codacy.Proof.FirstMonolithicModule;

public class AssemblyInfo
{
    public static string ModuleName => "Codacy.Proof.FirstMonolithicModule";
}
