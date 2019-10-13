using BenchmarkDotNet.Attributes;

namespace BenchMarkingDirectoriosArchivos {
    [InProcess]
    public class BenchDirectoriosArchivos {
        [Benchmark()]
        public void BenchOrdenar() {
            new TestDirectoriosArchivos.TestArchivos().Ordenar();
        }

        [Benchmark()]
        public void BenchCopiar() {
            new TestDirectoriosArchivos.TestDirectorios().Copiar();
        }
    }
}
