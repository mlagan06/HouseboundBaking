using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HouseboundBaking.Droid.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace HouseboundBaking.Droid.Data
{
    [BroadcastReceiver]
    public class SQLite_Android : HouseboundBaking.Data.ISQLite
    {
        //public override void OnReceive(Context context, Intent intent)
        //{
        //    Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
        //}

        public void SQLite_Andriod() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "TestDB.db3;";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentsPath, sqliteFileName);
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}