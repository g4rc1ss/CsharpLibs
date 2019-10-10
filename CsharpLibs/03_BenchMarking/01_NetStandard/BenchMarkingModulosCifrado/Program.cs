using BenchmarkDotNet.Running;

namespace BenchMarkingModulosCifrado {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<BenchAES>();
        }
    }
}
