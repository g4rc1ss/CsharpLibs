using BenchmarkDotNet.Running;

namespace BenchMarkingLogs {
    internal class Program {
        private static void Main(string[] _args) {
            BenchmarkRunner.Run<BenchFicheroLog>();
        }
    }
}
