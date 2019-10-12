using BenchmarkDotNet.Running;

namespace BenchMarkingLogs {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchFicheroLog>();
        }
    }
}
