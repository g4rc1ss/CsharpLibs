using BenchmarkDotNet.Attributes;
using Core.Common.TestRespuesta;

namespace Core.Common.BenchMarkingRespuesta {
    [InProcess]
    public class BenchRespuesta {
        [Benchmark]
        public void BenchRespuestaConstructores() {
            new TestRespuesta.TestRespuesta().RespuestaConstrutores();
        }
    }
}
