using BenchmarkDotNet.Attributes;

namespace BenchMarkingLogs {
    [InProcess]
    public class BenchFicheroLog {
        [Benchmark()]
        public void BenchLogs() {
            new TestLogs.TestFicheroLog().Logs();
        }
    }
}
