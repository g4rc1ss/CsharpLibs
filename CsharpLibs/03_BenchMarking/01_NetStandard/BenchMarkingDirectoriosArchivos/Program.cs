using BenchmarkDotNet.Running;

namespace BenchMarkingDirectoriosArchivos {
    internal class Program {
        private static void Main(string[] _args) {
            BenchmarkRunner.Run<BenchDirectoriosArchivos>();
        }
    }
}
