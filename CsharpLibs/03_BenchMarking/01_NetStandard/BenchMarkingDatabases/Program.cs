using BenchmarkDotNet.Running;

namespace BenchMarkingDatabases {
    internal class Program {
        private static void Main(string[] _args) {
            BenchmarkRunner.Run<BenchSQLite>();
        }
    }
}
