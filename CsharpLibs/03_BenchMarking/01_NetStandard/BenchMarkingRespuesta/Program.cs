using BenchmarkDotNet.Running;

namespace BenchMarkingRespuesta {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchRespuesta>();
        }
    }
}
