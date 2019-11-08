using BenchmarkDotNet.Running;

namespace Core.Common.BenchMarkingRespuesta {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<BenchRespuesta>();
        }
    }
}
