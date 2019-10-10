using BenchmarkDotNet.Attributes;

namespace BenchMarkingModulosCifrado {
    [InProcess]
    public class BenchAES {
        [Benchmark()]
        public void BenchCifradoArchivos() => new TestModulosCifrado.TestAES().CifradoArchivos();

        [Benchmark()]
        public void BenchCifradoTexto() => new TestModulosCifrado.TestAES().CifradoTexto();

        [Benchmark()]
        public void BenchDescifradoArchivos() => new TestModulosCifrado.TestAES().DecifradoArchivos();

        [Benchmark()]
        public void BenchDescifrarTexto() => new TestModulosCifrado.TestAES().DescifrarTexto();
    }
}
