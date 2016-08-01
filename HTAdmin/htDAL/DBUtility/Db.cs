using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace htDAL.DBUtility
{
    [Serializable()]
    public class Db : IDisposable
    {
        protected SqlConnection dbConn = null;
        protected SqlCommand dbCmd = null;
        protected string dbConnString = string.Empty;

        protected int? commandTimeOut;

        public ConnectionState State
        {
            get
            {
                try
                {
                    ConnectionState state;
                    if (dbConn != null)
                        state = this.dbConn.State;
                    else
                        state = ConnectionState.Broken;

                    return state;
                }
                catch
                {
                    return ConnectionState.Broken;
                }
            }
        }
        public Db()
        { //Class constructor.
            Initialize(null);
        }

        public Db(string connString)
        {
            if (string.IsNullOrEmpty(connString))
            {
                throw new ArgumentException("A connection string must be provided.");
            }

            Initialize(connString);
        }


        #region Public Static Methods
        // ********************************************************************************

        /// <summary>
        /// Get field value as string
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        /// <param name="defaultValue">Default value.</param>
        public static string ToString(object fieldValue, string defaultValue)
        {
            // Return string.
            try
            {
                if (fieldValue == null || fieldValue == DBNull.Value)
                {
                    return defaultValue;
                }
                return fieldValue.ToString();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as string
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        public static string ToString(object fieldValue)
        {
            return Db.ToString(fieldValue, string.Empty);
        }

        /// <summary>
        /// Get field value as string
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static string ToString(DataRow dataRow, string fieldName, string defaultValue)
        {
            try
            {
                return Db.ToString(dataRow[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as string
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static string ToString(DataRow dataRow, string fieldName)
        {
            return Db.ToString(dataRow[fieldName], string.Empty);
        }

        /// <summary>
        /// Get field value as string
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static string ToString(SqlDataReader dataReader, string fieldName, string defaultValue)
        {
            try
            {
                return Db.ToString(dataReader[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as string
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static string ToString(SqlDataReader dataReader, string fieldName)
        {
            return Db.ToString(dataReader[fieldName], string.Empty);
        }

        /// <summary>
        /// Get field value as int (Int32)
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        /// <param name="defaultValue">Default value.</param>
        public static int ToInt(object fieldValue, int defaultValue)
        {
            // Return string.
            try
            {
                if (fieldValue == null || fieldValue == DBNull.Value)
                {
                    return defaultValue;
                }
                return Convert.ToInt32(fieldValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as int (Int32)
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        public static int ToInt(object fieldValue)
        {
            return Db.ToInt(fieldValue, 0);
        }

        /// <summary>
        /// Get field value as int (Int32)
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static int ToInt(DataRow dataRow, string fieldName, int defaultValue)
        {
            try
            {
                return Db.ToInt(dataRow[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as int (Int32)
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static int ToInt(DataRow dataRow, string fieldName)
        {
            return Db.ToInt(dataRow, fieldName, 0);
        }

        /// <summary>
        /// Get field value as int (Int32)
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static int ToInt(SqlDataReader dataReader, string fieldName, int defaultValue)
        {
            try
            {
                return Db.ToInt(dataReader[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as int (Int32)
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static int ToInt(SqlDataReader dataReader, string fieldName)
        {
            return Db.ToInt(dataReader, fieldName, 0);
        }

        /// <summary>
        /// Get field value as boolean
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        /// <param name="defaultValue">Default value.</param>
        public static bool ToBool(object fieldValue, bool defaultValue)
        {
            // Return string.
            try
            {
                if (fieldValue == null || fieldValue == DBNull.Value)
                {
                    return defaultValue;
                }
                return Convert.ToBoolean(fieldValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as boolean
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        public static bool ToBool(object fieldValue)
        {
            return Db.ToBool(fieldValue, false);
        }

        /// <summary>
        /// Get field value as boolean
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static bool ToBool(DataRow dataRow, string fieldName, bool defaultValue)
        {
            try
            {
                return Db.ToBool(dataRow[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as boolean
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static bool ToBool(DataRow dataRow, string fieldName)
        {
            return Db.ToBool(dataRow, fieldName, false);
        }

        /// <summary>
        /// Get field value as boolean
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static bool ToBool(SqlDataReader dataReader, string fieldName, bool defaultValue)
        {
            try
            {
                return Db.ToBool(dataReader[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as boolean
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static bool ToBool(SqlDataReader dataReader, string fieldName)
        {
            return Db.ToBool(dataReader, fieldName, false);
        }

        /// <summary>
        /// Get field value as DateTime
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        /// <param name="defaultValue">Default value.</param>
        public static DateTime ToDate(object fieldValue, DateTime defaultValue)
        {
            // Return string.
            try
            {
                if (fieldValue == null || fieldValue == DBNull.Value)
                {
                    return defaultValue;
                }
                return Convert.ToDateTime(fieldValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as DateTime
        /// </summary>
        /// <param name="fieldValue">Field item from any reader object (ex: row["FieldName"])</param>
        public static DateTime ToDate(object fieldValue)
        {
            return Db.ToDate(fieldValue, DateTime.Now);
        }

        /// <summary>
        /// Get field value as DateTime
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static DateTime ToDate(DataRow dataRow, string fieldName, DateTime defaultValue)
        {
            try
            {
                return Db.ToDate(dataRow[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as DateTime
        /// </summary>
        /// <param name="dataRow">DataRow object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static DateTime ToDate(DataRow dataRow, string fieldName)
        {
            return Db.ToDate(dataRow, fieldName, DateTime.Now);
        }

        /// <summary>
        /// Get field value as DateTime
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="defaultValue">Default value.</param>
        public static DateTime ToDate(SqlDataReader dataReader, string fieldName, DateTime defaultValue)
        {
            try
            {
                return Db.ToDate(dataReader[fieldName], defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Get field value as DateTime
        /// </summary>
        /// <param name="dataReader">SqlDataReader object containing field values</param>
        /// <param name="fieldName">Field name</param>
        public static DateTime ToDate(SqlDataReader dataReader, string fieldName)
        {
            return Db.ToDate(dataReader, fieldName, DateTime.Now);
        }

        /// <summary>
        /// Encode data values for SQL processing.
        /// </summary>
        /// <param name="dataValue">Unencoded data value</param>
        public static string EncodeSql(string dataValue)
        {
            return dataValue.Replace(@"'", @"''");
        }

        /// <summary>
        /// Encode data values for SQL processing.
        /// </summary>
        /// <param name="dataValue">Input data value</param>
        public static string RemoveBadWhitespace(string dataValue)
        {
            return Regex.Replace(dataValue, @"[\t\n\r\b\v]", string.Empty);
        }

        // ********************************************************************************
        #endregion  // Public Static Methods

        #region Public Instance Methods
        // ********************************************************************************

        /// <summary>
        /// Get/set database connection string.
        /// </summary>
        public string ConnectionString
        {
            set
            {
                this.dbConnString = value;
            }
            get
            {
                return this.dbConnString;
            }
        }

        /// <summary>
        /// Connect to database automatically based on configuration settings.
        /// </summary>
        public void Connect()
        {
            if (this.dbConnString.Length < 1) throw new Exception("No database connection string is set.");
            dbConn = new SqlConnection(this.dbConnString);
            dbConn.Open();
            dbCmd = new SqlCommand();
            dbCmd.Connection = dbConn;
        }

        /// <summary>
        /// Connect to database automatically based on configuration settings.
        /// </summary>
        public void Connect(string connStr)
        {
            this.ConnectionString = connStr;
            this.Connect();
        }

        /// <summary>
        /// Free any resources associated with this instance.
        /// </summary>
        public void Dispose()
        {
            if (dbCmd != null)
            {
                try
                {
                    dbCmd.Connection.Close();
                    dbCmd.Dispose();
                    dbCmd = null;
                }
                catch { }
            }
            if (dbConn != null)
            {
                try
                {
                    dbConn.Close();
                    dbConn.Dispose();
                    dbConn = null;
                }
                catch { }
                //try
                //{
                //    //dbConn.Dispose();
                //    //dbConn = null;
                //}
                //catch { }
            }
        }

        public void Dispose(bool isDatabaseOwner)
        {
            if (isDatabaseOwner)
            {
                Dispose();
            }
        }

        //public SqlDataReader QueryReader(string sqlStmt)
        //{
        //    if(dbConn == null) this.Connect();

        //    dbCmd.CommandText = sqlStmt;
        //    return dbCmd.ExecuteReader();
        //}

        /// <summary>
        /// Executes procedure after setting up parameters from the procedureParams array.
        /// </summary>
        /// <param name="procedure">The name of the stored procedure to execute.</param>
        /// <param name="procedureParams">The parameters to be passed to the stored procedure being executed.</param>
        /// <returns>A reader populated with the result set from executing the stored procedure.</returns>
        public SqlDataReader QueryReader(string procedure, SqlParameter[] procedureParams)
        {
            if (dbConn == null) this.Connect();

            SqlCommand command = new SqlCommand();
            command.Connection = dbConn;
            command.CommandText = procedure;
            command.CommandType = CommandType.StoredProcedure;
            if (procedureParams != null) { command.Parameters.AddRange(procedureParams); }
            return command.ExecuteReader();
        }

        ///// <summary>
        ///// Executes procedure after setting up parameters from the procedureParams array.
        ///// <param name="Parameters">Repeat sequence of [SQL Paramter Name, Value]</param>
        ///// </summary>
        //public SqlDataReader QueryReader(string procedure, params object[] Parameters)
        //{
        //    SqlCommand cmd = PrepareCommand(procedure, Parameters);
        //    return cmd.ExecuteReader();
        //}

        ///// <summary>
        ///// Query DataTable
        ///// </summary>
        //public DataTable QueryDataTable(string sqlStmt)
        //{
        //    if(dbConn == null) this.Connect();

        //    DataTable oDataTable = null;
        //    SqlDataAdapter oSda = null;
        //    try
        //    {
        //        dbCmd.CommandText = sqlStmt;
        //        oSda = new SqlDataAdapter(dbCmd);
        //        oDataTable = new DataTable();
        //        oSda.Fill(oDataTable);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            if(oSda != null) oSda.Dispose();
        //        }
        //        catch {}
        //    }

        //    return oDataTable;
        //}

        /// <summary>
        /// Query DataTable
        /// </summary>
        public DataTable QueryDataTable(string procedure, SqlParameter[] procedureParams)
        {
            if (dbConn == null) this.Connect();
            SqlCommand command = new SqlCommand();
            command.CommandText = procedure;
            command.CommandType = CommandType.StoredProcedure;
            if (this.commandTimeOut != null) { command.CommandTimeout = this.commandTimeOut.Value; }
            command.Connection = dbConn;
            if (procedureParams != null) { command.Parameters.AddRange(procedureParams); }

            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        ///// <summary>
        ///// Query DataSet
        ///// </summary>
        //public DataSet QueryDataSet(string sqlStmt)
        //{
        //    if(dbConn == null) this.Connect();

        //    DataSet oDataSet = null;
        //    SqlDataAdapter oSda = null;
        //    try
        //    {
        //        dbCmd.CommandText = sqlStmt;
        //        oSda = new SqlDataAdapter(dbCmd);
        //        oDataSet = new DataSet();
        //        oSda.Fill(oDataSet);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            if(oSda != null) oSda.Dispose();
        //        }
        //        catch {}
        //    }

        //    return oDataSet;
        //}

        /// <summary>
        /// Not using the stored procedure just for Big Extract service.
        /// Later this has to be moved to the SP if Possible.
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <returns>DataSet</returns>
        public DataSet QueryDataSet(string query)
        {
            if (dbConn == null) this.Connect();
            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            command.Connection = dbConn;
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// Query DataSet
        /// </summary>
        public DataSet QueryDataSet(string procedure, SqlParameter[] procedureParams)
        {
            if (dbConn == null) this.Connect();
            SqlCommand command = new SqlCommand();
            command.CommandText = procedure;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = dbConn;
            if (procedureParams != null) { command.Parameters.AddRange(procedureParams); }

            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        ///// <summary>
        ///// Query DataSet
        ///// </summary>
        //public DataSet QueryDataSet(string procedure, params object[] Parameters)
        //{
        //    SqlCommand command = PrepareCommand(procedure, Parameters);

        //    DataSet dataSet = new DataSet();
        //    SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //    dataAdapter.SelectCommand = command;
        //    dataAdapter.Fill(dataSet);
        //    return dataSet;
        //}

        ///// <summary>
        ///// Run a non-query SQL statement - does not return records.
        ///// </summary>
        //public void RunNonQuery(string sqlStmt)
        //{
        //    if (dbConn == null) this.Connect();
        //    dbCmd.CommandText = sqlStmt;
        //    dbCmd.ExecuteNonQuery();
        //}

        ///// <summary>
        ///// Run a non-query SQL statement - does not return records.
        ///// <param name="Parameters">Repeat sequence of [SQL Paramter Name, Value]</param>
        ///// </summary>
        //public void RunNonQuery(string SQLProcName, params object[] Parameters)
        //{
        //    SqlCommand cmd = PrepareCommand(SQLProcName, Parameters);
        //    cmd.ExecuteNonQuery();
        //}

        ///// <summary>
        ///// Sets up a command with the parameters from a params array
        ///// </summary>
        //private SqlCommand PrepareCommand(string SQLProcName, params object[] Parameters)
        //{
        //    if (dbConn == null) this.Connect();
        //    dbCmd.CommandType = CommandType.StoredProcedure;
        //    dbCmd.CommandText = SQLProcName;

        //    for (int i=0; i<Parameters.Length/2; i++)
        //    {
        //        dbCmd.Parameters.Add(new SqlParameter());
        //        dbCmd.Parameters[i].ParameterName = Parameters[i*2].ToString();
        //        object param = Parameters[(i*2) + 1];
        //        dbCmd.Parameters[i].Value = param == null ? DBNull.Value : param ;
        //    }
        //    return dbCmd;
        //}

        /// <summary>
        /// Executes procedure after setting up parameters from the procedureParams array.
        /// </summary>
        /// <param name="procedure">The name of the stored procedure to execute.</param>
        /// <param name="procedureParams">The parameters to be passed to the stored procedure being executed.</param>
        public void RunNonQuery(string procedure, SqlParameter[] procedureParams)
        {
            if (dbConn == null) this.Connect();
            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    command.Connection = dbConn;
                    command.CommandText = procedure;
                    command.CommandType = CommandType.StoredProcedure;
                    if (procedureParams != null) { command.Parameters.AddRange(procedureParams); }
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Parameters.Clear();
                }
            }
        }

        /// <summary>
        /// Run a SQL statement that returns a single value in the first column from the first record (row[0][0]).
        /// <param name="Parameters">Repeat sequence of [SQL Paramter Name, Value]</param>
        /// </summary>
        public object QuerySingleValue(string procedure, SqlParameter[] procedureParams)
        {
            if (dbConn == null) this.Connect();
            SqlCommand command = new SqlCommand();
            command.Connection = dbConn;
            command.CommandText = procedure;
            command.CommandType = CommandType.StoredProcedure;
            if (procedureParams != null) { command.Parameters.AddRange(procedureParams); }
            return command.ExecuteScalar();
        }

        /// <summary>
        /// Run a SQL statement that returns a single value in the first column from the first record (row[0][0]).
        /// </summary>
        public object QuerySingleValue(string sqlStmt)
        {
            if (dbConn == null) this.Connect();
            dbCmd.CommandText = sqlStmt;
            return dbCmd.ExecuteScalar();
        }

        /// <summary>
        /// Get stored procedure parameter list from database.
        /// </summary>
        /// <returns></returns>
        public SqlParameterCollection GetSprocParams(string sprocString)
        {
            try
            {
                if (sprocString == null || sprocString.Length < 1) return null;
                if (dbConn == null) this.Connect();
                dbCmd.CommandText = sprocString;
                dbCmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(dbCmd);
                return dbCmd.Parameters;
            }
            catch
            {
                return null;  // Don't blow up on error, just return null.
            }
        }

        // ********************************************************************************
        #endregion  // Public Instance Methods

        /// <summary>
        /// Initialize this DB access object.
        /// </summary>
        private void Initialize(string connString)
        {
            if (string.IsNullOrEmpty(connString))
            {
                throw new ArgumentException("A connection string must be provided.");
            }
            this.dbConnString = connString;


            //if (!string.IsNullOrEmpty(ConfigUtil.GetConfigString("CommandTimeOut")))
            //{
            //    this.commandTimeOut = Convert.ToInt32(ConfigUtil.GetConfigString("CommandTimeOut"));
            //}
        }

    }
}
