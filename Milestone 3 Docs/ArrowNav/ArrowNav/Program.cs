using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowNav
{
    class Program
    {
        static void Main(string[] args)
        {
            //github change test
            string connectionString = "Data Source = LAPTOP - KI9GTVUJ\SQLEXPRESS01; Initial Catalog = ArrowNav; Integrated Security = True"; //user id= idhere; password= passwordhere";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = conn.CreateCommand();

            /*
            command.CommandText = "Delete from tablename WHERE parameter";//deletes entire row
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            */

            /*EXAMPLE OF DOING AND UPDATE
            command.CommandText = "Update tablename SET columnname = 'values' WHERE parameter";
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            */

            /*EXAMPLE OF DOING AN ADD STATEMENT
            command.CommandText = "Insert into tablename (column1, column2, etc.) values (corresponding values for listed columnnames)";
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            */

            /*EXAMPLE OF DOING A SELECT STATEMENT
            command.CommandText = "Select columnname from tablename where parameter";
            try
            {
                conn.Open();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["text"].ToString());
            }
            Console.ReadLine();
            */


            /*CODE FROM VONG'S VIDEO, USE FOR POTENTIAL REFERENCE
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = "Select * FROM Account";
                using (var command = new SqlCommand(sql, conn))
                {
                    command.ExecuteReader();
                }

                
                using (var adapter = new SqlDataAdapter())
                {

                }
                
            }
            */
        }
    }
}
