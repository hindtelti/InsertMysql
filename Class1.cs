using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Activities;
using System.ComponentModel;
using System.Data;

namespace ConnectionTO_DB
{


    public class Insert: CodeActivity
    {
        [Category("Database")]
        [RequiredArgument]
        public InArgument<MySqlConnection> Database_Connection { get; set; }
        [Category("Table")]
        [RequiredArgument]
        public InArgument<String> Table { get; set; }
        [Category("Table")]
        [RequiredArgument]
        public InArgument<DataTable> DT { get; set; }
        [Category("Table")]
        [RequiredArgument]
        public InArgument<String> Column { get; set; }
        [Category("Table")]
        [RequiredArgument]
        public InArgument<String> Value { get; set; }

        [Category("Output")]
        public OutArgument<bool> Result { get; set; }


        protected override void Execute(CodeActivityContext context)
        {

            var table = DT.Get(context);
            var connection = Database_Connection.Get(context);
            var t = Table.Get(context);
            

            connection.Open();
            
            MySqlCommand com = connection.CreateCommand();
            for(int i =0;i< table.Rows.Count; i++)
            {
                com.CommandText = "INSERT INTO " + t + " VALUES(@First_Name, @Last_Name)";
                com.Parameters.AddWithValue("@First_Name", table.Rows[i]["First_Name"]);
                com.Parameters.AddWithValue("@Last_Name", table.Rows[i]["Last_Name"]);
                com.ExecuteNonQuery();
            }
           
            connection.Close();


        }


    }
}
