using BenchmarkDotNet.Running;

namespace BenchMarkingRespuesta {
    internal class Program {
        private static void Main(string[] _args) {
            BenchmarkRunner.Run<BenchRespuesta>();
        }
    }
}
