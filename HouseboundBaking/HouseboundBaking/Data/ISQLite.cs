using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
