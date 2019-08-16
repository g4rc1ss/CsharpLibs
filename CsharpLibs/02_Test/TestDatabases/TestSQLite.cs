using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Databases.SQLite;
using System.IO;

namespace TestDatabases {
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
                    "DIRECCION   INT                        NOT NULL," +
                    "SALARIO     REAL)"
                    );
                using(var connect = baseDatos.Conexion())
                    baseDatos.UpdateOrInsert("INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
                        $"VALUES (1, 'Asier', 22, 'alguna', 3000)", connect);
            } catch(Exception e) {
                Console.WriteLine($"------------MESSAGE----------------\n" +
                    $"{e.Message}\n" +
                    $"---------------------STACKTRACE------------------------\n" +
                    $"{e.StackTrace}");
            }
        }


        [TestMethod]
        public void Conectar() {
            var baseDatos = new SQLiteDB();
            try {
                using (var connect = baseDatos.Conexion())
                    Assert.IsTrue(baseDatos.IsCreateDatabase() && connect != null);

            } finally {
                File.Delete(baseDatos.DBName);
            }
        }

        [TestMethod]
        public void Select() {
            var baseDatos = new SQLiteDB();
            try {
                CrearBBDD_Fake();
                using(var connect = baseDatos.Conexion())
                    using (var read = baseDatos.Select("SELECT * from EMPRESA", connect)) {
                        Assert.IsTrue(read.HasRows && read.FieldCount == 5);
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

                using (var connect = baseDatos.Conexion()) {
                    var insert = baseDatos.UpdateOrInsert("INSERT INTO EMPRESA (ID, NOMBRE, EDAD, DIRECCION, SALARIO) " +
                        $"VALUES (2, 'prueba', 56, 'insert', 4000)", connect);
                    Assert.IsTrue(insert == 1);

                    var update = baseDatos.UpdateOrInsert("UPDATE EMPRESA set SALARIO = 4500.00 where ID=1", connect);
                    Assert.IsTrue(update == 1);

                    var delete = baseDatos.UpdateOrInsert("DELETE FROM EMPRESA WHERE ID=1", connect);
                    Assert.IsTrue(delete == 1);
                }
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }

        [TestMethod]
        public void CrearConectar() {
            var baseDatos = new SQLiteDB();
            try {
                CrearBBDD_Fake();

                Assert.IsTrue(baseDatos.maxID("ID", "EMPRESA") >= 1);
            } finally {
                File.Delete(baseDatos.DBName);
            }
        }
    }
}
