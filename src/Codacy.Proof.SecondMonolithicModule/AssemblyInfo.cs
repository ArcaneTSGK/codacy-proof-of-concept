using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Codacy.Proof.SecondMonolithicModule.Tests.Unit")]
[assembly: InternalsVisibleTo("Codacy.Proof.SecondMonolithicModule.Tests.Integration")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Codacy.Proof.SecondMonolithicModule;

public class AssemblyInfo
{
    public static string ModuleName => "Codacy.Proof.SecondMonolithicModule";
}
