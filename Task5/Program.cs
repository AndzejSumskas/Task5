using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Andzej\DataBaseTest.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();

            ReadAllPersons(con);
            
            con.Close();
        }

        public static void ReadAllPersons(SqlConnection con)
        {
            SqlCommand command;
            SqlDataReader dataReader;
            string sql = "";

            sql = "Select * from PERSONS";
            command = new SqlCommand(sql, con);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetValue(0)} {dataReader.GetValue(1)} {dataReader.GetValue(2)}");
            }
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }
    }
}
