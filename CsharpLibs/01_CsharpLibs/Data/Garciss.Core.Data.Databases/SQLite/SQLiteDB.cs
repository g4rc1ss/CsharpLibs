using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Garciss.Core.Data.Databases.SQLite {
    /// <summary>
    /// Clase para la creacion y uso de una base de datos SQLite
    /// </summary>
    public class SQLiteDB :Base {

        /// <summary>
        /// Elegimos la base de datos, sino se llamara "Database.db"
        /// </summary>
        /// <returns>
        /// devuelve el nombre de la base de datos
        /// </returns>
        public string DBName { get; set; } = $"Database.db";

        /// <summary>
        /// Creamos una instancia de la conexion privada para ser usada siempre en cada consulta
        /// De esta manera la propia libreria se asegura de cerrar las conexiones etc.
        /// </summary>
        private SQLiteConnection Conexion {
            get {
                return new SQLiteConnection(
                    string.Format($"Data Source={DBName};Version=3;")
                ).OpenAndReturn();
            }
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
            if (!File.Exists(DBName) && !File.Exists($"{DBName}.crypt"))
                return false;
            return true;
        }

        /// <summary>
        /// Metodo para crear la base de datos, tambien comprueba
        /// si existe o no la base de datos
        /// </summary>
        /// <param name="query">consulta SQL escrita como una cadena</param>
        /// <example>
        /// <code>
        /// var baseDatos = new SQLiteDB();
        /// baseDatos.CreateDatabase("CREATE TABLE EMPRESA(" +
        ///         "ID          INT       PRIMARY KEY      NOT NULL," +
        ///         "NOMBRE      TEXT                       NOT NULL," +
        ///         "EDAD        INT                        NOT NULL," +
        ///         "DIRECCION   TEXT                       NOT NULL," +
        ///         "SALARIO     REAL)"
        ///);
        ///     
        /// </code>
        /// </example>
        public void CreateDatabase(string query) {
            ExecuteCreateDatabase(query: query);
        }

        private void ExecuteCreateDatabase(string query) {
            ValidarSentencia(query, TiposSentenciaSql.Create);
            // Crea la base de datos con la tabla
            if (!IsCreateDatabase()) {
                SQLiteConnection.CreateFile(DBName);
                using (var connect = Conexion) {
                    using (var command = new SQLiteCommand(query, connect))
                        command.ExecuteNonQuery();
                    connect.Close();
                }
            }
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
        public DataTable Select(string query) {
            return ExecuteSelect(query: query);
        }

        private DataTable ExecuteSelect(string query) {
            ValidarSentencia(query, TiposSentenciaSql.Select);
            using (var connect = Conexion) using (var command = new SQLiteCommand(query, connect)) {
                var tabla = new DataTable();
                tabla.Load(command.ExecuteReader());
                return tabla;
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
        public int UpdateOrInsert(string query) {
            return ExecuteUpdateOrInsert(query: query);
        }

        private int ExecuteUpdateOrInsert(string query) {
            if (query.ToUpper().Contains("UPDATE"))
                ValidarSentencia(query, TiposSentenciaSql.Update);
            else if (query.ToUpper().Contains("INSERT INTO"))
                ValidarSentencia(query, TiposSentenciaSql.Insert);
            else
                ValidarSentencia(query, TiposSentenciaSql.Delete);
            using (var connect = Conexion) using (var command = new SQLiteCommand(query, connect))
                return command.ExecuteNonQuery();
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
        public int MaxID(string columna, string table) {
            return GetMaxCount(columna: columna, table: table);
        }

        private int GetMaxCount(string columna, string table) {
            try {
                using (var countID = ExecuteSelect($"SELECT COUNT({columna}) FROM {table}"))
                    return Convert.ToInt32(countID.Rows[0].ItemArray[0]) + 1;
            } catch (Exception) {
                return 1;
            }
        }
    }
}
