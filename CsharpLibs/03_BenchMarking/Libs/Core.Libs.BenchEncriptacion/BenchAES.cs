using BenchmarkDotNet.Attributes;

namespace Core.Libs.BenchEncriptacion {
    [InProcess]
    public class BenchAES {
        [Benchmark()]
        public void BenchCifradoArchivos() {
            new TestEncriptacion.TestAES().CifradoArchivos();
        }

        [Benchmark()]
        public void BenchCifradoTexto() {
            new TestEncriptacion.TestAES().CifradoTexto();
        }

        [Benchmark()]
        public void BenchDescifradoArchivos() {
            new TestEncriptacion.TestAES().DecifradoArchivos();
        }

        [Benchmark()]
        public void BenchDescifrarTexto() {
            new TestEncriptacion.TestAES().DescifrarTexto();
        }
    }
}
