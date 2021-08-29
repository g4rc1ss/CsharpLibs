using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Garciss.Core.Data.Databases.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Data.TestDatabases {
    [TestClass]
    public class TestSQLite {
        private readonly string dbName = "Database.db";

        public async Task CrearBBDD_Fake() {
            using var baseDatos = new SQLiteDB(dbName);
            try {
                //-----------CREAR--------------\\
                await baseDatos.NonQueryAsync("CREATE TABLE EMPRESA(" +
                    "ID          INT       PRIMARY KEY      NOT NULL," +
                    "NOMBRE      TEXT                       NOT NULL," +
                    "EDAD        INT                        NOT NULL," +
                    "DIRECCION   TEXT                       NOT NULL," +
                    "SALARIO     REAL)"
                    );

                await baseDatos.NonQueryAsync("INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
                    $"VALUES (1, 'Asier', 22, 'alguna', 3000)");
            } catch (Exception e) {
                Console.WriteLine($"------------MESSAGE----------------\n" +
                    $"{e.Message}\n" +
                    $"---------------------STACKTRACE------------------------\n" +
                    $"{e.StackTrace}");
            }
        }

        [TestMethod]
        public async Task Select() {
            using var baseDatos = new SQLiteDB(dbName);
            try {
                await CrearBBDD_Fake();
                using (var read = await baseDatos.SelectAsync("SELECT * from EMPRESA")) {
                    Assert.IsTrue(read.GetSchemaTable().Rows.Count > 0 && read.GetSchemaTable().Columns.Count == 5);

                    // Ejemplo de lectura de los datos
                    // Es importante poner el tipo aqui, porque si ponemos var, no funciona,
                    // puesto que lo detecta como Object
                    foreach (DataRow row in read.GetSchemaTable().Rows) {
                        var id = row.Field<int>("ID");
                        var nombre = row.Field<string>("NOMBRE");
                        var edad = row.Field<int>("EDAD");
                        var direccion = row.Field<string>("DIRECCION");
                        var salario = row.Field<double>("SALARIO");
                    }
                    Assert.IsTrue(read.GetSchemaTable().Rows[0].Field<string>("NOMBRE") == "Asier" &&
                        (read.GetSchemaTable().Rows[0].Field<double>("SALARIO") == 3000));
                }
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }

        [TestMethod]
        public async Task UpdateInsertDelete() {
            using var baseDatos = new SQLiteDB(dbName);
            try {
                await CrearBBDD_Fake();
                var insert = await baseDatos.NonQueryAsync("INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
                    $"VALUES (2, 'prueba', 56, 'insert', 4000)");
                Assert.IsTrue(insert == 1);

                var update = await baseDatos.NonQueryAsync("UPDATE EMPRESA set SALARIO = 4500.00 where ID=1");
                Assert.IsTrue(update == 1);

                var delete = await baseDatos.NonQueryAsync("DELETE FROM EMPRESA WHERE ID=1");
                Assert.IsTrue(delete == 1);
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }

        [TestMethod]
        public async Task CrearConectar() {
            using var baseDatos = new SQLiteDB(dbName);
            try {
                await CrearBBDD_Fake();
                Assert.IsTrue(await baseDatos.MaxIDAsync("ID", "EMPRESA") >= 1);
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }
    }
}
