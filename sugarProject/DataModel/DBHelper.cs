using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using sugarProject.DataModel;
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
        public List<T> RetrieveList<T>(string SQLStr, string table)
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
            return TableToList<T>(dt);
        }

        private List<T> TableToList<T>(DataTable table)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(row[prop.Name].ToString(), propType));
                }
                list.Add(obj);
            }
            return list;
        }

        public int Insert(User user, string table)
        // The Method recieve a user objects and insert it to the Database as new row. 
        // if the user is already taken the method will return -1.
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM {table} WHERE eMail = '{user.eMail}'";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת סכימת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            if (ds.Tables[table].Rows.Count > 0)
            {
                return -1;
            }

            // בניית השורה להוספה
            DataRow dr = ds.Tables[table].NewRow();
            dr["eMail"] = user.eMail;
            dr["pass"] = user.pass;
            dr["fName"] = user.fName;
            dr["lName"] = user.lName;
            dr["prefix"] = user.prefix;
            dr["uName"] = user.uName;
            dr["phone"] = user.phone;
            dr["gender"] = user.gender;
            dr["yearBorn"] = user.yearBorn;
            dr["eatsSugarRegularly"] = user.eatsSugarRegularly;

            ds.Tables[table].Rows.Add(dr);

            // עדכון הדאטה סט בבסיס הנתונים
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int numRowsAffected = adapter.Update(ds, table);
            return numRowsAffected;
        }

        public int Insert2(User user, string table)
        // The Method recieve a user objects and insert it to the Database as new row. 
        // if the user is already taken the method will return -1.
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            string SQLStr = $"INSERT INTO users VALUES (";
            SQLStr += $"'{user.eMail}', ";
            SQLStr += $"'{user.pass}', ";
            SQLStr += $"'{user.fName}', ";
            SQLStr += $"'{user.lName}', ";
            SQLStr += $" {user.prefix}, ";
            SQLStr += $"'{user.phone}', ";
            SQLStr += $"'{user.gender}', ";
            SQLStr += $"'{user.eatsSugarRegularly}', ";
            SQLStr += $"'{user.eatsWhiteBread}', ";
            SQLStr += $"'{user.doesSports}', ";
            SQLStr += $"'{user.yearBorn}', ";
            SQLStr += $"'{user.uName}', ";

            SqlCommand cmd = new SqlCommand(SQLStr, con);

            int numRowsAffected = cmd.ExecuteNonQuery();

            return numRowsAffected;
        }

        public int ExecuteNonQuery(string SQL)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            // בניית פקודת SQL
            SqlCommand cmd = new SqlCommand(SQL, con);

            // ביצוע השאילתא
            int numRowsEffected = cmd.ExecuteNonQuery();

            con.Close();
            //con.Dispose();

            // return the number of rows affected
            return numRowsEffected;
        }

        public int Delete(int id, string table)
        {
            if (id == 0) // < 0??
            {
                return -1;
            }
            string SQL = $"DELETE FROM {table} WHERE ID = {id}";
            int numRowsEffected = ExecuteNonQuery(SQL);
            return numRowsEffected;
        }
        
        public int Update(User user, string table)
        {
            string SQL = $"UPDATE {table} " +
                $"SET Email ='{user.eMail}', " +
                $"Password = '{user.pass}', " +
                $"FirstName = '{user.fName}', " +
                $"LastName = '{user.lName}', " +
                $"PrefixID = {user.prefix}, " +
                $"Phone = '{user.phone}', " +
                $"Gender = '{user.gender}', " +
                $"Car1 = '{user.eatsSugarRegularly}', " +
                $"Car2 = '{user.eatsWhiteBread}', " +
                $"Car3 = '{user.doesSports}', " +
                $"YearOfBirth = '{user.yearBorn}', " +
                //$"Birthday = '{user.Birthday:MM-dd-yyyy HH:mm:ss}' " +
                $"WHERE UserId = {user.Id} ";
            // TBD add the other fields !!!
            int numRowsEffected = ExecuteNonQuery(SQL);
            return numRowsEffected;
        }

        public int UpdateAdmin(int userId, bool isAdmin)
        {
            string sql = string.Empty;
            int numRowsEffected = 0;
            // we know for sure that the Admin Status changed so the insert will succed

            sql= $"SELECT * FROM AdminTbl WHERE UserID = {userId}";
            bool found = Find(sql);

            sql = string.Empty;

            if (found && !isAdmin) // need to remove from admin list
                sql = $"DELETE FROM AdminTbl WHERE UserID = {userId}";
            else if (!found && isAdmin) // need to add to Admin list
                sql = $"INSERT INTO AdminTbl VALUES ({userId})";

            numRowsEffected = 0;
            if (!string.IsNullOrEmpty(sql))
                numRowsEffected = ExecuteNonQuery(sql);

            return numRowsEffected;
        }

        public int Update_disconnected(User user, string table)
        {
            // The Method recieve a user objects and update its fields it to the Database . 
            // The method return the number of rows affected (1) if it succeded.
            // if the id of the user is not in the databse it will return -1

            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM {table} WHERE Id = {user.Id}";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת סכימת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            if (ds.Tables[table].Rows.Count != 1)
            {
                return -1;
            }

            // קבלת מצביע לשורה בטבלה
            DataRow dr = ds.Tables[table].Rows[0]; //Get the only row available

            // עדכון השורה
            dr["Email"] = user.eMail;
            dr["Password"] = user.pass;
            dr["FirstName"] = user.fName;
            dr["LastName"] = user.lName;
            dr["PrefixID"] = user.prefix;
            dr["Phone"] = user.phone;
            dr["Gender"] = user.gender;
            //dr["Birthday"] = user.Birthday.ToString();
            dr["YearOfBirth"] = user.yearBorn;

            //dr["isAdmin"] = user.isAdmin;

            // עדכון הדאטה סט בבסיס הנתונים
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int numRowsAffected = adapter.Update(ds, table);
            return numRowsAffected;
        }

        public int Delete_disconnected(int id, string table)
        {
            // The Method recieve an Id and delete it from the user table.
            // The method return the number of rows affected (1) if it succeded.
            // if the id of the user is not in the databse it will return -1

            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            // בניית פקודת SQL
            string SQLStr = $"SELECT * FROM {table} WHERE Id = {id}";
            SqlCommand cmd = new SqlCommand(SQLStr, con);

            // בניית DataSet
            DataSet ds = new DataSet();

            // טעינת סכימת הנתונים
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds, table);

            if (ds.Tables[table].Rows.Count == 0)
            {
                return -1;
            }

            // מחיקת השורה
            ds.Tables[table].Rows[0].Delete();

            // עדכון הדאטה סט בבסיס הנתונים
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int n = adapter.Update(ds, table);
            return n;
        }

        // This is via the OPEN connection method with DataReader
        // even if the param to ExecuteReader says close the coinnection
        // so this is a "bastard" setup
        public bool Find(string sql)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader data = com.ExecuteReader(CommandBehavior.CloseConnection);

            bool found = Convert.ToBoolean(data.Read());

            con.Close();
            con.Dispose();

            return found;
        }

        public bool FindDisconnected(string sql)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            SqlCommand com = new SqlCommand(sql, con);
            DataTable dataTable = new DataTable();
            SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, con);
            tableAdapter.Fill(dataTable);

            bool found = dataTable.Rows.Count == 1; // or > 1 if we want FindAll
            con.Close();
            con.Dispose();

            return found;
        }

        // Query must be a count() query
        public int Count(string sql)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            SqlCommand com = new SqlCommand(sql, con);
            int numRowsAffected = com.ExecuteNonQuery();

            //con.Close();
            con.Dispose();

            return numRowsAffected;
        }

        // not used for Login bc this does not return the User DB record, so we won't have his/her name to display
        public bool Login(string email, string password)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            string sql = $"SELECT COUNT(*) FROM users WHERE eMail = '{email}' and pass = '{password}'";
            SqlCommand com = new SqlCommand(sql, con);
            int numRowsAffected = (int) com.ExecuteScalar();

            //con.Close();
            con.Dispose();

            return numRowsAffected == 1; // found exactly one user
        }

        // Parametrized Login Query
        public DataTable LoginParam(params string[] parameters)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();


            DataTable dataTable = new DataTable();

            //string constr = System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
            //SqlConnection con = new SqlConnection(constr);

            // use sql with parameters
            string safeSQL = $"SELECT * FROM UsersTbl WHERE Email=@Email and [Password]=@Password";

            //string safeSQL = $"SELECT COUNT(uName) FROM {DBHelper.DB_USER_TABLE} WHERE uName=@uName AND password=@password";

            SqlCommand cmd = new SqlCommand(safeSQL, con);

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@eMail", parameters[0]);
            param[1] = new SqlParameter("@pass", parameters[1]);
            cmd.Parameters.Add(param[0]);
            cmd.Parameters.Add(param[1]);


            SqlDataAdapter tableAdapter = new SqlDataAdapter();
            tableAdapter.SelectCommand = cmd;
            tableAdapter.Fill(dataTable);

            con.Close();
            con.Dispose();
            return dataTable;
        }

        // Stored Procedure
        // https://social.technet.microsoft.com/wiki/contents/articles/53361.sql-server-stored-procedures-for-c-windows-forms.aspx
        public DataTable LoginStoredProcedure(params string[] parameters)
        {
            DataTable dataTable = new DataTable();
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            using (con)
            {
                con.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.Login";

                    command.Parameters.AddWithValue("@eMail", parameters[0]);
                    command.Parameters.AddWithValue("@pass", parameters[1]);

                    // 3 types of EXECUTE: command ExeceuteNonQuery, ExecuteReader, ExecuteScalar
                    // or user DataAdapter to return a table for SELECT
                    SqlDataAdapter tableAdapter = new SqlDataAdapter(command);

                    tableAdapter.Fill(dataTable);
                }
            }
            return dataTable;
        }


        // Advanced
        public void RenameTable(string dbFileName, string oldName, string newName)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);

            con.Open();
            string sql = $"EXEC sp_rename @objname = '{oldName}', @newname = '{newName}', @objtype = 'OBJECT';";
            SqlCommand com = new SqlCommand(sql, con);
            int result = com.ExecuteNonQuery(); // returns -1, but WORKS!!!
        }

        //https://stackoverflow.com/questions/18298433/how-can-i-show-the-table-structure-in-sql-server-query
        public DataTable GetSchema(string dbFileName, string tableName)
        {
            DataTable dataTable = new DataTable();

            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            //string sql = $"EXEC sp_columns '{tableName}'";
            string sql = $"SELECT COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{tableName}'";
            //string sql = $"SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('{tableName}')";
            // SYNTAX ERROR IN SQL: string sql = $"SHOW COLUMNS FROM '{dbFileName}.{tableName}';";

            SqlCommand com = new SqlCommand(sql, con);
            var reader = com.ExecuteReader(); // returns -1, but WORKS!!!

            // I think this gets the schema of the resulting table that is stored in reader
            //var schema = reader.GetSchemaTable();
            //foreach (DataRow row in schema.Rows)
            //{
            //Debug.WriteLine(row["ColumnName"] + " - " + row["DataTypeName"]);
            //}

            string names = "";
            while (reader.Read())
            {
                names += $"{reader["column_name"]}, ";
            }

            return dataTable;
        }


        public object GetScalar(string SQL)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            // ביצוע השאילתא
            con.Open();

            // בניית פקודת SQL
            SqlCommand cmd = new SqlCommand(SQL, con);
            object scalar = cmd.ExecuteScalar();
            con.Close();

            return scalar;
        }

        public SqlDataReader GetDataReader(string SQL)
        {
            // התחברות למסד הנתונים
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            // בניית פקודת SQL
            SqlCommand cmd = new SqlCommand(SQL, con);

            // Command behavior insure closing the reader will close the connection 
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
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