using BenchmarkDotNet.Attributes;

namespace Core.Data.BenchMarkingLogs {
    [InProcess]
    public class BenchFicheroLog {
        [Benchmark()]
        public void BenchLogs() {
            new TestLogs.TestFicheroLog().Logs();
        }
    }
}
