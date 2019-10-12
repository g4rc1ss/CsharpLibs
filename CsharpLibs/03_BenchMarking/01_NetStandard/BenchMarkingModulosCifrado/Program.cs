using BenchmarkDotNet.Running;

namespace BenchMarkingModulosCifrado {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchAES>();
        }
    }
}
