using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Garciss.Core.Data.Databases.SqlInjection;
using Microsoft.Data.Sqlite;

namespace Garciss.Core.Data.Databases.SQLite {
    /// <summary>
    /// Clase para la creacion y uso de una base de datos SQLite
    /// </summary>
    public sealed class SQLiteDB : IDisposable {

        /// <summary>
        /// Nombre de la BBDD
        /// </summary>
        /// <returns>
        /// devuelve el nombre de la base de datos
        /// </returns>
        public string DBName { get; private set; }

        /// <summary>
        /// Creamos una instancia de la conexion privada para ser usada siempre en cada consulta
        /// De esta manera la propia libreria se asegura de cerrar las conexiones etc.
        /// </summary>
        private SqliteConnection Connection => new($"Data Source={DBName}");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dBName"></param>
        public SQLiteDB(string dBName) {
            DBName = dBName;
            Connection.Open();
        }

        /// <summary>
        /// Metodo para comprobar si existe la base de datos
        /// </summary>
        /// <returns>
        /// retorna true o false dependiendo de si existe o no
        /// </returns>
        /// <example>
        /// <code>
        /// var baseDatos = new SQLiteDB();
        /// if (baseDatos.IsCreateDatabase())
        ///     return true;
        /// </code>
        /// </example>
        public bool IsCreateDatabase() {
            if (!File.Exists(DBName)) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Ejecuta la sentencia SELECT
        /// </summary>
        /// <returns>
        /// Retorna un DataTable que podra ser leido de diferentes formas, 
        /// Hay un ejemplo de lectura
        /// </returns>
        /// <param name="query">Consulta SQL en formato cadena</param>
        /// <example>
        /// <code>
        /// using (var read = baseDatos.Select("SELECT * from EMPRESA")) {
        ///     foreach (DataRow row in read.Rows) {
        ///         var id = row.Field<int/>("ID");
        ///         var nombre = row.Field<string/>("NOMBRE");
        ///         var edad = row.Field<int/>("EDAD");
        ///         var direccion = row.Field<string/>("DIRECCION");
        ///         var salario = row.Field<double/>("SALARIO");
        ///     }
        /// }    
        /// </code>
        /// </example>
        public async Task<IDataReader> SelectAsync(string query) {
            return await ExecuteSelectAsync(query);
        }

        private async Task<IDataReader> ExecuteSelectAsync(string query) {
            using (var command = Connection.CreateCommand()) {
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync()) {
                    return reader;
                }
            }
        }

        /// <summary>
        /// Se usa para ejecutar la instruccion Insert, Update o Delete
        /// </summary>
        /// <returns>
        /// Retorna el numero de elementos que ha sido retocado
        /// </returns>
        /// <param name="query">Consulta SQL en formato cadena</param>
        /// <example>
        /// <code>
        /// using (var conexion = baseDatos.Conexion()) {
        ///     baseDatos.UpdateOrInsert(
        ///         query: "INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
        ///                $"VALUES ({id}, '{nombre}', {edad}, '{direccion}', {salario})"
        ///     );
        ///
        ///     baseDatos.UpdateOrInsert(
        ///         query: "UPDATE EMPRESA set SALARIO = 4500.00 where ID=1"
        ///     );            
        /// }
        /// </code>
        /// </example>
        public async Task<int> NonQueryAsync(string query) {
            return await ExecuteNonQueryAsync(query);
        }

        private async Task<int> ExecuteNonQueryAsync(string query) {
            using (var command = Connection.CreateCommand()) {
                command.CommandText = query;
                return await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Realiza una consulta para obtener el maximo id de la base de datos.
        /// Su intencion es usarlo para hacer la insercion de datos autonumérica
        /// </summary>
        /// <returns>
        /// Retorna el numero de elementos que contiene la tabla consultada +1
        /// </returns>
        /// <param name="columna">El nombre de la columna con el que vamos a obtener el max</param>
        /// <param name="table">El nombre de la tabla para realizar la consulta</param>
        /// <example>
        /// <code>
        /// int id = baseDatos.MaxID("ID", "EMPRESA");
        /// </code>
        /// </example>
        public async Task<int> MaxIDAsync(string columna, string table) {
            return await GetMaxCountAsync(columna, table);
        }

        private async Task<int> GetMaxCountAsync(string columna, string table) {
            try {
                using (var countID = await ExecuteSelectAsync($"SELECT COUNT({columna}) FROM {table}")) {
                    return Convert.ToInt32(countID.GetSchemaTable().Rows[0].ItemArray[0]) + 1;
                }
            } catch (Exception) {
                return 1;
            }
        }

        private void ValidarSentencias(string query) {
            switch (query.ToUpper()) {
                case var sql when sql.StartsWith("UPDATE"):
                    SqlInjectionValidation.ValidarSentencia(query, TiposSentenciaSql.Update);
                    break;
                case var sql when sql.StartsWith("INSERT INTO"):
                    SqlInjectionValidation.ValidarSentencia(query, TiposSentenciaSql.Insert);
                    break;
                case var sql when sql.StartsWith("DELETE"):
                    SqlInjectionValidation.ValidarSentencia(query, TiposSentenciaSql.Delete);
                    break;
                case var sql when sql.StartsWith("CREATE"):
                    SqlInjectionValidation.ValidarSentencia(query, TiposSentenciaSql.Create);
                    break;
                case var sql when sql.StartsWith("SELECT"):
                    SqlInjectionValidation.ValidarSentencia(query, TiposSentenciaSql.Select);
                    break;
                default:
                    SqlInjectionValidation.ValidarSentencia(query, TiposSentenciaSql.None);
                    break;
            }

        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (disposing) {
                Connection.Close();
            }
        }
    }
}
