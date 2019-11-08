using BenchmarkDotNet.Attributes;

namespace Core.Data.BenchMarkingDatabases {
    [InProcess] // Para crear un proceso aparte, se usa para resolver problemas con los antivirus
    public class BenchSQLite {
        [Benchmark()]
        public void BenchConectar() {
            new TestDatabases.TestSQLite().Conectar();
        }

        [Benchmark()]
        public void BenchSelect() {
            new TestDatabases.TestSQLite().Select();
        }

        [Benchmark()]
        public void BenchUpdateInsertDelete() {
            new TestDatabases.TestSQLite().UpdateInsertDelete();
        }

        [Benchmark()]
        public void BenchCrearConectar() {
            new TestDatabases.TestSQLite().CrearConectar();
        }
    }
}
