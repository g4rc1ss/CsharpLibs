using BenchmarkDotNet.Running;

namespace BenchMarkingLogs {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<BenchFicheroLog>();
        }
    }
}
