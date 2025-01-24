using System.Reflection;
using BenchmarkDotNet.Running;

namespace EndlessEscapade.Benchmarks;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
    }
}