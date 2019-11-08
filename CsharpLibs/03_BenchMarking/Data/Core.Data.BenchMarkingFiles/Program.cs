using BenchmarkDotNet.Running;

namespace Core.Data.BenchMarkingFiles {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchFiles>();
        }
    }
}
