using BenchmarkDotNet.Running;

namespace Core.Data.BenchMarkingDatabases {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchSQLite>();
        }
    }
}
