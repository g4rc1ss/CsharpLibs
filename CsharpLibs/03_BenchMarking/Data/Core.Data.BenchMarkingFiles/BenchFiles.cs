using BenchmarkDotNet.Attributes;

namespace Core.Data.BenchMarkingFiles {
    [InProcess]
    public class BenchFiles {
        [Benchmark()]
        public void BenchOrdenar() {
            new TestFiles.TestArchivos().Ordenar();
        }

        [Benchmark()]
        public void BenchCopiar() {
            new TestFiles.TestDirectorios().Copiar();
        }
    }
}
