using System;

namespace Garciss.Core.Data.Databases.MockSqlInjection {
    public class SqlExamples {
        public const string SELECT_SQL = @"SELECT * FROM sysobjects";
        public const string UPDATE_SQL = @"UPDATE table_name
                                           SET column1 = value1, column2 = value2
                                           WHERE condition = true";
        public const string DELETE_SQL = @"DELETE FROM table_name WHERE condition";
        public const string INSERT_SQL = @"INSERT INTO table_name
                                            VALUES (value1, value2, value3, ...)";
        public const string CREATE_SQL = @"CREATE TABLE Persons (
                                            PersonID int,
                                            LastName varchar(255),
                                            FirstName varchar(255),
                                            Address varchar(255),
                                            City varchar(255)
                                        )";

        public const string QUERY_CLEAN = "SELECT * FROM sysobjects --";
    }
}

