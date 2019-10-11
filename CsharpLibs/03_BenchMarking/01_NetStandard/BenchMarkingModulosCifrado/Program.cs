using BenchmarkDotNet.Running;

namespace BenchMarkingModulosCifrado {
    internal class Program {
        private static void Main(string[] _args) {
            BenchmarkRunner.Run<BenchAES>();
        }
    }
}
