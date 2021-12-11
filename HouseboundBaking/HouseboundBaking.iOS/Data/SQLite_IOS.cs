using Foundation;
using HouseboundBaking.Data;
using HouseboundBaking.iOS.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]

namespace HouseboundBaking.iOS.Data
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Testdb.db3;";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}