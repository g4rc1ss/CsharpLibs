using BenchmarkDotNet.Attributes;

namespace BenchMarkingDatabases {
    public class BenchSQLite {
        [Benchmark()]
        public void BenchConectar() => new TestDatabases.TestSQLite().Conectar();
    }
}
