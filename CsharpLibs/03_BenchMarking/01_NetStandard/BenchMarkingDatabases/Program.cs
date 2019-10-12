using BenchmarkDotNet.Running;

namespace BenchMarkingDatabases {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchSQLite>();
        }
    }
}
