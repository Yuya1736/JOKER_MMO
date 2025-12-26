using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

public class ServerBuildFliterAssembly : IFilterBuildAssemblies
{
    public int callbackOrder => 1;
    public static bool serverMode = false;

    // 过滤掉HotUpdate程序集
    public string[] OnFilterAssemblies(BuildOptions buildOptions, string[] assemblies)
    {
        if (serverMode)
        {
            var newAssemblies = assemblies.Where(ass =>
            {
                string assName = Path.GetFileNameWithoutExtension(ass);
                return !assName.Contains("HotUpdate"); // 这里的HotUpdate实际上就是客户端独享热更程序集
            });
            return newAssemblies.ToArray();
        }
        else
        {
            var newAssemblies = assemblies.Where(ass =>
            {
                string assName = Path.GetFileNameWithoutExtension(ass);
                return !assName.Contains("Server");
            });
            return newAssemblies.ToArray();
        }
    }
}
