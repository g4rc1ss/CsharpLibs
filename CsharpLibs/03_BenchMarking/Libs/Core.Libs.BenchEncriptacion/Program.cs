using BenchmarkDotNet.Running;

namespace Core.Libs.BenchEncriptacion {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchAES>();
        }
    }
}
