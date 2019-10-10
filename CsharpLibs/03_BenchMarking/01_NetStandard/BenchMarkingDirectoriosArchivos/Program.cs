using BenchmarkDotNet.Running;

namespace BenchMarkingDirectoriosArchivos {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<BenchDirectoriosArchivos>();
        }
    }
}
