using System.Reflection;

namespace Codacy.Proof.SecondMonolithicModule.Contracts;

public class AssemblyInfo
{
    public static string ModuleName => Assembly.GetExecutingAssembly().GetName().Name!;
}
