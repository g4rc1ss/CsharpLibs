using BenchmarkDotNet.Running;

namespace Core.Data.BenchMarkingLogs {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchFicheroLog>();
        }
    }
}
