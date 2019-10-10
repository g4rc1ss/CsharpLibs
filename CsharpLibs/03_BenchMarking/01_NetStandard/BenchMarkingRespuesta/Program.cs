using BenchmarkDotNet.Running;

namespace BenchMarkingRespuesta {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<BenchRespuesta>();
        }
    }
}
