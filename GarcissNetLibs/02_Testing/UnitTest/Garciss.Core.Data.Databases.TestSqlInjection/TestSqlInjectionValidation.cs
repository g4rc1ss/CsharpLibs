using Garciss.Core.Data.Databases.MockSqlInjection;
using Garciss.Core.Data.Databases.SqlInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Data.Databases.TestSqlInjection {
    [TestClass]
    public class TestSqlInjectionValidation {

        [TestMethod]
        public void ValidarSentencia() {
            SqlInjectionValidation.ValidarSentencia(SqlExamples.SELECT_SQL, TiposSentenciaSql.Select);
            SqlInjectionValidation.ValidarSentencia(SqlExamples.UPDATE_SQL, TiposSentenciaSql.Update);
            SqlInjectionValidation.ValidarSentencia(SqlExamples.INSERT_SQL, TiposSentenciaSql.Insert);
            SqlInjectionValidation.ValidarSentencia(SqlExamples.DELETE_SQL, TiposSentenciaSql.Delete);
            SqlInjectionValidation.ValidarSentencia(SqlExamples.CREATE_SQL, TiposSentenciaSql.Create);
        }

        [TestMethod]
        public void LimpiarParametros() {
            var sqlLimpia = SqlInjectionValidation.LimpiarParametrosSql(SqlExamples.QUERY_CLEAN);
            Assert.IsTrue(sqlLimpia.Equals(@"SELECTFROMsysobjects"));
        }
    }
}
