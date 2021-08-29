using Garciss.Core.Data.Databases.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.IO;

namespace Core.Data.TestDatabases {
    [TestClass]
    public class TestSQLite {

        public void CrearBBDD_Fake() {
            var baseDatos = new SQLiteDB();
            try {
                //-----------CREAR--------------\\
                baseDatos.CreateDatabase("CREATE TABLE EMPRESA(" +
                    "ID          INT       PRIMARY KEY      NOT NULL," +
                    "NOMBRE      TEXT                       NOT NULL," +
                    "EDAD        INT                        NOT NULL," +
                    "DIRECCION   TEXT                       NOT NULL," +
                    "SALARIO     REAL)"
                    );

                baseDatos.UpdateOrInsert("INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
                    $"VALUES (1, 'Asier', 22, 'alguna', 3000)");
            } catch (Exception e) {
                Console.WriteLine($"------------MESSAGE----------------\n" +
                    $"{e.Message}\n" +
                    $"---------------------STACKTRACE------------------------\n" +
                    $"{e.StackTrace}");
            }
        }

        [TestMethod]
        public void Select() {
            var baseDatos = new SQLiteDB();
            try {
                CrearBBDD_Fake();
                using (var read = baseDatos.Select("SELECT * from EMPRESA")) {
                    Assert.IsTrue(read.Rows.Count > 0 && read.Columns.Count == 5);

                    // Ejemplo de lectura de los datos
                    // Es importante poner el tipo aqui, porque si ponemos var, no funciona,
                    // puesto que lo detecta como Object
                    foreach (DataRow row in read.Rows) {
                        var id = row.Field<int>("ID");
                        var nombre = row.Field<string>("NOMBRE");
                        var edad = row.Field<int>("EDAD");
                        var direccion = row.Field<string>("DIRECCION");
                        var salario = row.Field<double>("SALARIO");
                    }
                    Assert.IsTrue(read.Rows[0].Field<string>("NOMBRE") == "Asier" &&
                        (read.Rows[0].Field<double>("SALARIO") == 3000));
                }
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }

        [TestMethod]
        public void UpdateInsertDelete() {
            var baseDatos = new SQLiteDB();
            try {
                CrearBBDD_Fake();
                var insert = baseDatos.UpdateOrInsert("INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
                    $"VALUES (2, 'prueba', 56, 'insert', 4000)");
                Assert.IsTrue(insert == 1);

                var update = baseDatos.UpdateOrInsert("UPDATE EMPRESA set SALARIO = 4500.00 where ID=1");
                Assert.IsTrue(update == 1);

                var delete = baseDatos.UpdateOrInsert("DELETE FROM EMPRESA WHERE ID=1");
                Assert.IsTrue(delete == 1);
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }

        [TestMethod]
        public void CrearConectar() {
            var baseDatos = new SQLiteDB();
            try {
                CrearBBDD_Fake();
                Assert.IsTrue(baseDatos.MaxID("ID", "EMPRESA") >= 1);
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }
    }
}
