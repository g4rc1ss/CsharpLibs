using BenchmarkDotNet.Attributes;

namespace BenchMarkingRespuesta {
    [InProcess]
    public class BenchRespuesta {
        [Benchmark]
        public void BenchRespuestaConstructores() {
            new TestRespuesta.TestRespuesta().RespuestaConstrutores();
        }
    }
}
