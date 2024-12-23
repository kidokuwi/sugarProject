using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using sugarProject.Model;
using sugarProject.Pages;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

// ADO NET
// https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/

// Disconnected vs Connected
// 
// https://www.oreilly.com/library/view/adonet-in-a/0596003617/ch01s02.html
// https://dotnettutorials.net/lesson/connected-and-disconnected-architecture-in-ado-net/
// In ADO.NET, there are two primary data access models: connected and disconnected.
//Connected Model
//Continuous Connection: In the connected model, the application maintains a continuous connection to the data source. This is typically used for operations that require real-time data access and updates.
//DataReader: The DataReader object is used in this model. It provides a fast, forward-only, read-only cursor over the data retrieved from the database.
//Example: Suitable for scenarios where you need to read data quickly and efficiently, such as displaying data in a web application.

//Disconnected Model
//Intermittent Connection: The disconnected model involves connecting to the data source only when necessary, such as to retrieve or update data, and then disconnecting. This reduces the load on the database server.
//DataSet and DataAdapter: The DataSet and DataAdapter objects are used in this model. The DataSet can hold multiple tables of data in memory, and the DataAdapter is used to fill the DataSet and update the data source.
//Example: Ideal for scenarios where you need to work with data offline or perform batch updates, such as in desktop applications.
//For more detailed information, you can refer to the official Microsoft documentation on establishing connections in ADO.NET1.

namespace ClassicCarsRazor.DataModel
{
    public class DBHelper
    {
        private string conString;

        public DBHelper()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            conString = configuration.GetConnectionString(Utils.CONFIG_DB_FILE);
        }

        public DataTable RetrieveTable(string SQLStr, string table)
        // Gets A table from the data base acording to the SELECT Command in SQLStr;
        // Returns DataTable with the Table.
        {

            // connect to DataBase
            SqlConnection con = new SqlConnection(conString);

            // Build SQL Query
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // Build DataAdapter
            SqlDataAdapter ad = new SqlDataAdapter(cmd);


            //// Build DataSet to store the data
            //DataSet ds = new DataSet();
            //// Get Data form DataBase into the DataSet
            //ad.Fill(ds, table); // ad.Fill(ds);
            //return ds.Tables[table];

            // !!!!!!!  ALTERNATIVELY  the adapter returns just the table, not a DataSet (many tables)
            // Or just get just one table - not a DataSet (which includes many tables)
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
    }
}


// Advanced
// using DAPPER
//    using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    string selectSql = "SELECT * FROM users WHERE id = @id";
//    IEnumerable<User> users = connection.Query<User>(selectSql, new { id = 1 });
//    foreach (User user in users)
//    {
//        Console.WriteLine("ID: {0}, Name: {1} {2}", user.Id, user.FirstName, user.LastName);
//    }
//}


// REAL TIME UPDATE
//https://www.codeproject.com/Tips/5256345/Real-Time-HTML-Page-Content-Update-with-Blazor-and