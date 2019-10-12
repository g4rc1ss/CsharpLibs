using System;
using System.Data.SQLite;
using System.IO;

namespace Databases.SQLite {
    /// <summary>
    /// Clase para la creacion y uso de una base de datos SQLite
    /// </summary>
    public class SQLiteDB {
        #region Propiedades & Variables
        /// <summary>
        /// Elegimos la base de datos, sino se llamara "Database.db"
        /// </summary>
        /// <returns>
        /// devuelve el nombre de la base de datos
        /// </returns>
        public string DBName { get; set; } = $"Database.db";
        #endregion


        #region Metodos/Funciones

        /// <summary>
        /// Metodo para comprobar si existe la base de datos
        /// </summary>
        /// <returns>
        /// retorna true o false dependiendo de si existe o no
        /// </returns>
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
        public void CreateDatabase(string query) {
            ExecuteCreateDatabase(query: query);
        }

        private void ExecuteCreateDatabase(string query) {
            // Crea la base de datos y registra usuario solo una vez
            if (!IsCreateDatabase()) {
                SQLiteConnection.CreateFile(DBName);

                using (var connect = ConnectionToDatabase())
                using (var command = new SQLiteCommand(query, connect)) {
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// Establece la conexion con la base de datos
        /// </summary>
        /// <returns>
        /// Retorna la conexion establecida con la base de datos
        /// </returns>
        public SQLiteConnection Conexion() {
            return ConnectionToDatabase();
        }

        private SQLiteConnection ConnectionToDatabase() {
            return new SQLiteConnection(
                string.Format($"Data Source={DBName};Version=3;")
            ).OpenAndReturn();
        }

        /// <summary>
        /// Ejecuta la sentencia SELECT
        /// </summary>
        /// <returns>
        /// Retorna en un objeto la consulta SELECT que se leera como un array
        /// ej: nombreObjeto["nombreColumna"];
        /// </returns>
        /// <param name="query">Consulta SQL en formato cadena</param>
        /// <param name="connect">Se envia la informacion de la conexion de la base de datos</param>
        public SQLiteDataReader Select(string query, SQLiteConnection connect) {
            return ExecuteSelect(query: query, connect: connect);
        }

        private SQLiteDataReader ExecuteSelect(string query, SQLiteConnection connect) {
            using (var command = new SQLiteCommand(query, connect))

                return command.ExecuteReader();
        }

        /// <summary>
        /// Se usa para ejecutar la instruccion Insert, Update o Delete
        /// </summary>
        /// <returns>
        /// Retorna el numero de elementos que ha sido retocado
        /// </returns>
        /// <param name="query">Consulta SQL en formato cadena</param>
        /// <param name="connect">Se envia la informacion de la conexion de la base de datos</param>
        public int UpdateOrInsert(string query, SQLiteConnection connect) {
            return ExecuteUpdateOrInsert(query: query, connect: connect);
        }

        private int ExecuteUpdateOrInsert(string query, SQLiteConnection connect) {
            using (var command = new SQLiteCommand(query, connect))
                return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Se usa para ejecutar la instruccion Insert, Update o Delete
        /// </summary>
        /// <returns>
        /// Retorna el numero de elementos que ha sido retocado
        /// </returns>
        /// <param name="columna">El nombre de la columna con el que vamos a obtener el max</param>
        /// <param name="table">El nombre de la tabla para realizar la consulta</param>
        public int MaxID(string columna, string table) {
            return GetMaxCount(columna: columna, table: table);
        }

        private int GetMaxCount(string columna, string table) {
            try {
                int cont;
                using (var conexion = ConnectionToDatabase()) {
                    using (var countID = ExecuteSelect($"SELECT COUNT({columna}) FROM {table}", conexion)) {
                        countID.Read();
                        cont = int.Parse(countID[0].ToString()) + 1;
                        countID.Close();
                    }
                    conexion.Close();
                }
                return cont;
            } catch (Exception) {
                return 1;
            }
        }
        #endregion
    }
}
