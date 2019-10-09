using BenchmarkDotNet.Running;

namespace BenchMarkingDatabases {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<BenchSQLite>();
        }
    }
}
