using System.Reflection;

namespace Codacy.Proof.FirstMonolithicModule.Contracts;

public class AssemblyInfo
{
    public static string ModuleName => Assembly.GetExecutingAssembly().GetName().Name!;
}
