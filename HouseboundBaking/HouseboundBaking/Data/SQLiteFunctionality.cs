using HouseboundBaking.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.Data
{
    class SQLiteFunctionality
    {
        static object locker = new object();

        public SQLiteConnection database;

        public SQLiteFunctionality()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
        }

        public Boolean TableExists(String tableName)
        {
            SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

            //Int32 tableCount = database.Table<tableName>().Count();
            Int32 tableCount = database.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'", ps).Count; // Executes the query from which we can count the results
            if (tableCount == 0)
            {
                return false;
            }
            else if (tableCount == 1)
            {
                return true;
            }
            else
            {
                throw new Exception("More than one table by the name of " + tableName + " exists in the database.", null);
            }
        }

        public int DeleteAllTables_ReturnCountTablesDeleted()
        {
            int NumberOfTablesDeleted = 0;

            SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

            Int32 tableCount = database.Query(map, "SELECT * FROM sqlite_master", ps).Count; // Executes the query from which we can count the results
            //while (tableCount > 0)

            var tables = database.Table<SqliteMaster>().Where(a => a.Type == "table").Select(a => a.Name).ToList();

            //{
            //database.DeleteAll(map);
            //database.Query(map, "DELETE * FROM sqlite_master", ps)[0];
            // var e = database.Query(map, "SELECT * FROM sqlite_master", ps)[0];

            // var TableNameToDelete = database.Query(map, "SELECT [0] FROM sqlite_master", ps).ToString();


            //var tables = database.Query(map, "SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%", ps);
            // var tables = database.Query(map, "SELECT name FROM sqlite_master WHERE type = 'table'", ps);

            // if (database.Table<Order>().Count() > 0)
            if (tables.Contains("OrderDetailsModel"))
            {
                database.Query(map, "DROP TABLE 'OrderDetailsModel'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps); // VACUUM command to clear unused space.
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("Order"))
            {
                database.Query(map, "DROP TABLE 'Order'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps); // VACUUM command to clear unused space.
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("UserModel"))
            {
                database.Query(map, "DROP TABLE 'UserModel'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps); // VACUUM command to clear unused space.
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("User"))
            {
                database.Query(map, "DROP TABLE 'User'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps); // VACUUM command to clear unused space.
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("ConfirmedOrder"))
            {
                database.Query(map, "DROP TABLE 'ConfirmedOrder'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps);
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("OrderModel"))
            {
                database.Query(map, "DROP TABLE 'OrderModel'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps);
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("ProductModel"))
            {
                database.Query(map, "DROP TABLE 'ProductModel'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps);
                NumberOfTablesDeleted++;
            }

            if (tables.Contains("UserModelTable"))
            {
                database.Query(map, "DROP TABLE 'UserModelTable'", ps); // Executes the query from which we can count the results
                database.Query(map, "VACUUM", ps);
                NumberOfTablesDeleted++;
            }


            //   }

            return NumberOfTablesDeleted;
        }

        //public async Task<List<TableName>> GetAllTablesAsync()
        //{
        //    string queryString = $"SELECT name FROM sqlite_master WHERE type = 'table'";
        //    return await database.QueryAsync<TableName>(queryString).ConfigureAwait(false);
        //}

        public Int32 CountNumberOfTablesInDB()
        {
            SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

            Int32 tableCount = database.Query(map, "SELECT * FROM sqlite_master", ps).Count; // Executes the query from which we can count the results
            return tableCount;
        }


        //TODO finish this function
        public void GetColumnNamesOfTable(string tableName)
        {
            SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)



            //var tables = database.Table<SqliteMaster>().Where(a => a.Type == "table" && a.Name == '"' + TableName + '"').Select(a => a.Name).ToList();

            //  var ob = database.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + TableName + "'", ps);
            // var dr = ob.executeReader();

            // Int32 tableCount = database.Query(map, "SELECT * FROM sqlite_master", ps).Count; // Executes the query from which we can count the results
            //return tableCount;


            //   var cmd = new SQLiteCommand("select * from t1", database);
            //  var dr = database.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + TableName + "'", ps);
            //for (var i = 0; i < dr.FieldCount; i++)
            //{
            //    Console.WriteLine(dr.GetName(i));
            //}

            //using (var con = new SQLiteConnection(database.ToString()))
            //{
            //    using (SQLiteCommand cmd = new SQLiteCommand("PRAGMA table_info(" + tableName + ");"))
            //    {
            //        var table = new DataTable();
            //        cmd.Connection = con;
            //        cmd.Connection.Open();

            //        SQLiteDataAdapter adp = null;
            //        try
            //        {
            //            adp = new SQLiteDataAdapter(cmd);
            //            adp.Fill(table);
            //            con.Close();
            //            var res = new List<string>();
            //            for (int i = 0; i < table.Rows.Count; i++)
            //                res.Add(table.Rows[i]["name"].ToString());
            //            return res;
            //        }
            //        catch (Exception ex) { }
            //    }
            //}
            //return new List<string>();
        }

    }

    public class TableName
    {
        public TableName() { }
        public string name { get; set; }
    }
}
