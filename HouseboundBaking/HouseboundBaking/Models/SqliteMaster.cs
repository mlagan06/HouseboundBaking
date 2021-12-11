using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Models
{
    [Table("sqlite_master")]
    public class SqliteMaster
    {
        public SqliteMaster()
        {

        }

        [Column("type")]
        public string Type { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("tbl_name")]
        public string TableName { get; set; }

        [Column("rootpage")]
        public string Rootpage { get; set; }

        [Column("Sql")]
        public string sql { get; set; }

        [Column("username")]
        public string Username { get; set; }
    }
}
