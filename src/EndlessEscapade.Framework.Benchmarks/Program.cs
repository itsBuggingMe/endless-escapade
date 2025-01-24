using System.Reflection;
using BenchmarkDotNet.Running;

namespace EndlessEscapade.Framework.Benchmarks;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
    }
}