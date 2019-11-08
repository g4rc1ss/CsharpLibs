using BenchmarkDotNet.Running;

namespace BenchMarkingDirectoriosArchivos {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchDirectoriosArchivos>();
        }
    }
}
