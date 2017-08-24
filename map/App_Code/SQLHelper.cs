//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
 
//
//
// 文件名：SQLHelper.cs
// 文件功能描述：对 SqlClient 类的再次封装
//
//
// 修改描述：添加注释，添加四个重载方法
// public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameter) 
// public static object ExecuteScalar(string commandText, params SqlParameter[] parameter) 
// public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameter) 
// public static DataTable ExecuteTable( string commandText, params SqlParameter[] parameter) 
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;

namespace Utility 
{

	/// <summary>
	/// SqlHelper 类是对 SqlClient 类的再次封装
	/// </summary>
	public abstract class SQLHelper 
	{
		/// <summary>
		///  默认的数据库连接字符串
		/// </summary>
        //public static string CONN_STRING = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        public static string CONN_STRING = "server=.;database=school;user id=sa;password=123456";
		// 用来缓存参数的哈希表
		private static Hashtable c_ParmCache = Hashtable.Synchronized(new Hashtable());


		#region 构造sqlCommand
		/// <summary>
		/// 构造sqlCommand
		/// </summary>
		/// <param name="sqlCommand">System.Data.SqlClient.SqlCommand</param>
		/// <param name="commandText">SQL语句</param>
		/// <param name="sqlConnection">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="sqlTransaction">要在 SQL Server 数据库中处理的 Transact-SQL 事务</param>
		/// <param name="sqlParameter">SQL语句参数</param>
		private static void PrepareCommand(SqlCommand sqlCommand, string commandText, SqlConnection sqlConnection, CommandType commandType, SqlTransaction sqlTransaction, SqlParameter[] sqlParameter) 
		{
			if (sqlConnection.State != ConnectionState.Open) 
			{
				sqlConnection.Open();
			}
			sqlCommand.Connection = sqlConnection;
			sqlCommand.CommandText = commandText;
			if (sqlTransaction != null) 
			{
				sqlCommand.Transaction = sqlTransaction;
			}
			sqlCommand.CommandType = commandType;
			if (sqlParameter != null) 
			{
				foreach (SqlParameter iParm in sqlParameter)
					sqlCommand.Parameters.Add(iParm);
			}
		}
		#endregion

		#region 缓存SQL语句参数/取得缓存的SQL语句参数
		/// <summary>
		/// 缓存SQL语句参数
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="parameters"></param>
		public static void CacheParameters(string CacheKey, params SqlParameter[] parameters) 
		{
			c_ParmCache[CacheKey] = parameters;
		}


		/// <summary>
		/// 取得缓存的SQL语句参数
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
		public static SqlParameter[] GetCachedParameters(string CacheKey) 
		{
			SqlParameter[] CachedParms = (SqlParameter[])c_ParmCache[CacheKey];
			
			if (CachedParms == null)
				return null;
			
			SqlParameter[] iClonedParms = new SqlParameter[CachedParms.Length];

			for (int i = 0, j = CachedParms.Length; i < j; i++) 
			{
				iClonedParms[i] = (SqlParameter)((ICloneable)CachedParms[i]).Clone();
			}

			return iClonedParms;
		}
		#endregion

		#region 由简化的参数构造一个sqlParameter
		/// <summary>
		/// 由简化的参数构造一个sqlParameter
		/// </summary>
		/// <param name="parameterName">参数名称</param>
		/// <param name="sqlDbType">参数类型</param>
		/// <param name="value">参数值</param>
		/// <returns>创建好的sqlParameter</returns>
		public static SqlParameter PrepareParameter(string parameterName, SqlDbType sqlDbType, object value) 
		{
			SqlParameter sqlParameter = new SqlParameter(parameterName, value);
			sqlParameter.SqlDbType = sqlDbType;
			return sqlParameter;
		}

		/// <summary>
		/// 由简化的参数构造一个sqlParameter
		/// </summary>
		/// <param name="parameterName">参数名称</param>
		/// <param name="sqlDbType">参数类型</param>
		/// <param name="value">参数值</param>
		/// <param name="size">参数最大字节数</param>
		/// <returns>创建好的sqlParameter</returns>
		public static SqlParameter PrepareParameter(string parameterName, SqlDbType sqlDbType, object value, int size) 
		{
			SqlParameter sqlParameter = new SqlParameter(parameterName, value);
			sqlParameter.SqlDbType = sqlDbType;
			sqlParameter.Size = size;
			return sqlParameter;
		}
		#endregion

		#region 对 ExecuteNonQuery 方法的七次重载封装（执行 SQL 语句，返回受影响的行数）
		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText) 
		{
			return ExecuteNonQuery(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteNonQuery(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString) 
		{
			return ExecuteNonQuery(commandText, connectionString, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, CommandType commandType) 
		{
			return ExecuteNonQuery(commandText, connectionString, commandType, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, params SqlParameter[] parameter) 
		{
			return ExecuteNonQuery(commandText, connectionString, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteNonQuery(commandText, connectionString, commandType, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="sqlTransaction">要在 SQL Server 数据库中处理的 Transact-SQL 事务</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>受影响的行数</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, CommandType commandType, SqlTransaction sqlTransaction, params SqlParameter[] parameter) 
		{
			SqlCommand iCommand = new SqlCommand();
			SqlConnection iConn = new SqlConnection(connectionString);
			try 
			{
				PrepareCommand(iCommand, commandText, iConn, commandType, sqlTransaction, parameter);
				int iValue = iCommand.ExecuteNonQuery();
				iCommand.Parameters.Clear();
				iConn.Close();
				iConn.Dispose();
				return iValue;
			}
			catch 
			{
				iConn.Close();
				throw;
			}
		}
		#endregion

		#region 对 ExecuteScalar 方法的七次重载封装（执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。）
		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText) 
		{
			return ExecuteScalar(commandText,  CONN_STRING, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteScalar(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText, string connectionString) 
		{
			return ExecuteScalar(commandText, connectionString, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText, string connectionString, CommandType commandType) 
		{
			return ExecuteScalar(commandText, connectionString, commandType, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText, string connectionString, params SqlParameter[] parameter) 
		{
			return ExecuteScalar(commandText, connectionString, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText, string connectionString, CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteScalar(commandText, connectionString, commandType, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行查询，并返回查询所返回的结果集中第一行的第一列，忽略其他的列或行。
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="sqlTransaction">要在 SQL Server 数据库中处理的 Transact-SQL 事务</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>查询所返回的结果集中第一行的第一列</returns>
		public static object ExecuteScalar(string commandText, string connectionString, CommandType commandType, SqlTransaction sqlTransaction, params SqlParameter[] parameter) 
		{
			SqlCommand iCommand = new SqlCommand();

			using (SqlConnection iConn = new SqlConnection(connectionString)) 
			{
				PrepareCommand(iCommand, commandText, iConn, commandType, sqlTransaction, parameter);
				object iValue = iCommand.ExecuteScalar();
				iCommand.Parameters.Clear();
				iConn.Close();
				iConn.Dispose();
				return iValue;
			}
		}
		#endregion
		
		#region 对 ExecuteReader 方法的七次重载封装（执行查询，返回一个 SqlDataReader）
		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText) 
		{
			return ExecuteReader(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteReader(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString) 
		{
			return ExecuteReader(commandText, connectionString, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, CommandType commandType) 
		{
			return ExecuteReader(commandText, connectionString, commandType, (SqlTransaction)null, null);
		}

		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, params SqlParameter[] parameter) 
		{
			return ExecuteReader(commandText, connectionString, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteReader(commandText, connectionString, commandType, (SqlTransaction)null, parameter);
		}
		
		/// <summary>
		/// 对连接执行查询并返回一个 SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="sqlTransaction">要在 SQL Server 数据库中处理的 Transact-SQL 事务</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, CommandType commandType, SqlTransaction sqlTransaction, params SqlParameter[] parameter) 
		{
			SqlCommand iCommand = new SqlCommand();
			SqlConnection iConn = new SqlConnection(connectionString);
			try 
			{
				PrepareCommand(iCommand, commandText, iConn, commandType, sqlTransaction, parameter);
				SqlDataReader iReader = iCommand.ExecuteReader(CommandBehavior.CloseConnection);
				iCommand.Parameters.Clear();
				return iReader;
			}
			catch 
			{
				iConn.Close();
				throw;
			}
		}
		#endregion

		#region 对 ExecuteTable方法的五次重载封装（使用 SqlDataAdapter 执行查询并返回 DataSet 的第一个 DataTable）
		/// <summary>
		/// 使用 SqlDataAdapter 执行查询并返回 DataSet 的第一个 DataTable
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText )
		{
			return ExecuteTable( commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, null); 
		}

		/// <summary>
		/// 使用 SqlDataAdapter 执行查询并返回 DataSet 的第一个 DataTable
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteTable( commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter); 
		}

		/// <summary>
		/// 使用 SqlDataAdapter 执行查询并返回 DataSet 的第一个 DataTable
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText, string connectionString )
		{
			return ExecuteTable( commandText, connectionString, CommandType.Text, (SqlTransaction)null, null); 
		}

		/// <summary>
		/// 使用 SqlDataAdapter 执行查询并返回 DataSet 的第一个 DataTable
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText, string connectionString ,CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteTable( commandText, connectionString, commandType, (SqlTransaction)null, parameter); 
		}

		/// <summary>
		/// 使用 SqlDataAdapter 执行查询并返回 DataSet 的第一个 DataTable
		/// </summary>
		/// <param name="commandText">SQL语句</param>
		/// <param name="connectionString">数据库连接字符串</param>
		/// <param name="commandType">SQL语句的属性，默认值为CommandType.Text</param>
		/// <param name="sqlTransaction">要在 SQL Server 数据库中处理的 Transact-SQL 事务</param>
		/// <param name="parameter">SQL语句参数</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable(string commandText, string connectionString, CommandType commandType, SqlTransaction sqlTransaction, params SqlParameter[] parameter) 
		{
			SqlCommand iCommand = new SqlCommand();
			SqlConnection iConn = new SqlConnection(connectionString);			
			try
			{
				PrepareCommand(iCommand, commandText, iConn, commandType, sqlTransaction, parameter);
				using (SqlDataAdapter iDa = new SqlDataAdapter(iCommand))
				{
					DataSet ds = new DataSet();
					iDa.Fill(ds);
					iCommand.Parameters.Clear();
						
					iConn.Close();
					iCommand.Dispose();
					iConn.Dispose();

					return ds.Tables[0];
				}			
			}
			catch
			{
				iConn.Close();
				throw;
			}
		}
		#endregion
	
		#region 数据库分页通用存储过程
		/// <summary>
		/// 数据库分页通用存储过程
		/// </summary>
		/// <param name="tblName">表名</param>
		/// <param name="fldName">主键（关键字段）</param>
		/// <param name="pageSize">每页记录数</param>
		/// <param name="pageIndex">要获取的页码</param>
		/// <param name="orderType">序类型, 0 - 升序, 1 - 降序</param>
		/// <param name="strWhere">查询条件 (注意: 不要加 where)</param>
		/// <param name="output">总记录数</param>
		/// <returns></returns>
		public static DataTable GetRecordFromPage(string tblName,string fldName,int pageSize,int pageIndex,int orderType,string strWhere,ref int output)
		{
			SqlConnection conn = new SqlConnection(SQLHelper.CONN_STRING);
			SqlCommand cmd = new SqlCommand("GetRecordFromPage",conn);

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@tblName",SqlDbType.VarChar,255));
			cmd.Parameters.Add(new SqlParameter("@fldName",SqlDbType.VarChar,255));
			cmd.Parameters.Add(new SqlParameter("@PageSize",SqlDbType.Int,4));
			cmd.Parameters.Add(new SqlParameter("@PageIndex",SqlDbType.Int,4));
			cmd.Parameters.Add(new SqlParameter("@OrderType",SqlDbType.Bit));
			cmd.Parameters.Add(new SqlParameter("@strWhere",SqlDbType.VarChar,2000));
			cmd.Parameters.Add(new SqlParameter("@rowTotal",SqlDbType.Int));
			
			cmd.Parameters[0].Value = tblName;
			cmd.Parameters[1].Value = fldName;
			cmd.Parameters[2].Value = pageSize;
			cmd.Parameters[3].Value = pageIndex;
			cmd.Parameters[4].Value = orderType;
			cmd.Parameters[5].Value = strWhere;
			cmd.Parameters[6].Direction = ParameterDirection.Output;

			try
			{
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				
				DataSet ds = new DataSet();
				da.Fill(ds);
				
				if (cmd.Parameters[6].Value!=DBNull.Value  && cmd.Parameters[6].Value.ToString()!=string.Empty )
					output = Convert.ToInt32(cmd.Parameters[6].Value);
				
				da.Dispose();
				conn.Close();
				conn.Dispose();
				return ds.Tables[0];
			}
			catch(Exception strEx)
			{
				conn.Close();
				return null;
			}
			finally
			{
				cmd.Dispose();
				conn.Dispose();				
			}
        }
        #endregion

        #region SqlServer2005通用分页方法
        /// <summary>
        /// SqlServer2005通用分页方法
        /// </summary>
        /// <param name="p_TblName">表名</param>
        /// <param name="p_PageSize">每页记录数</param>
        /// <param name="p_PageIndex">当前页数</param>
        /// <param name="p_OrderType">排序方式：0-正序 1-倒序</param>
        /// <param name="p_OrderColumnName">排序列名称</param>
        /// <param name="p_Condition">查询条件</param>
        /// <param name="p_RecordCount">总记录数</param>
        /// <returns></returns>
        public static DataSet GetRecordFromPageBySQL(string ClassID, string Field, string p_TblName, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string p_Condition, ref int p_RecordCount)
        {
            string _SQL = "Select Count(*) From " + p_TblName + " Where 1 = 1 " + p_Condition;
            p_RecordCount = int.Parse(ExecuteScalar(_SQL).ToString());
            StringBuilder _SB = new StringBuilder();
            _SB.Append("SELECT * ");
            _SB.Append("FROM ");
            _SB.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY " + ClassID + (p_OrderType == 1 ? " DESC" : " ASC") + ") ");
            _SB.Append(" AS OrderRank," + Field + " FROM " + p_TblName + " Where 1 = 1 " + p_Condition + ") ");
            _SB.Append(" AS Rank ");
            _SB.Append(" WHERE OrderRank BETWEEN " + ((p_PageIndex - 1) * p_PageSize + 1) + " AND " + p_PageIndex * p_PageSize + " ");
            _SB.Append(" ORDER BY " + p_OrderColumnName + " " + (p_OrderType == 1 ? "DESC" : "ASC") + " ");



            SqlConnection iConn = new SqlConnection(CONN_STRING);
            SqlCommand iCommand = iConn.CreateCommand();

            DataSet ds = new DataSet();
            try
            {
                iCommand.CommandText = _SB.ToString();
                iConn.Open();
                using (SqlDataAdapter iDa = new SqlDataAdapter(iCommand))
                {
                    iDa.Fill(ds);

                    return ds;
                }
            }
            catch(Exception _Ex)
            {
                throw _Ex;
            }
            finally
            {
                iConn.Close();
                iCommand.Dispose();
                iConn.Dispose();
                ds.Dispose();
            }
        }

        public static DataSet GetRecordFromPageBySQL2(string ClassID, string Field, string p_TblName, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string p_Condition)
        {
            StringBuilder _SB = new StringBuilder();
            _SB.Append("SELECT * ");
            _SB.Append("FROM ");
            _SB.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY " + ClassID + (p_OrderType == 1 ? " DESC" : " ASC") + ") ");
            _SB.Append(" AS OrderRank," + Field + " FROM " + p_TblName + " Where 1 = 1 " + p_Condition + ") ");
            _SB.Append(" AS Rank ");
            _SB.Append(" WHERE OrderRank BETWEEN " + ((p_PageIndex - 1) * p_PageSize + 1) + " AND " + p_PageIndex * p_PageSize + " ");
            _SB.Append(" ORDER BY " + p_OrderColumnName + " " + (p_OrderType == 1 ? "DESC" : "ASC") + " ");



            SqlConnection iConn = new SqlConnection(CONN_STRING);
            SqlCommand iCommand = iConn.CreateCommand();

            DataSet ds = new DataSet();
            try
            {
                iCommand.CommandText = _SB.ToString();
                iConn.Open();
                using (SqlDataAdapter iDa = new SqlDataAdapter(iCommand))
                {
                    iDa.Fill(ds);

                    return ds;
                }
            }
            catch (Exception _Ex)
            {
                throw _Ex;
            }
            finally
            {
                iConn.Close();
                iCommand.Dispose();
                iConn.Dispose();
                ds.Dispose();
            }
        }
        
        public static DataSet GetRecordFromPageBySQL3(string ClassID, string Field, string p_TblName, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string p_Condition, ref int p_RecordCount, SqlParameter[] sqlParameter)
        {
            string _SQL = "Select Count(*) From " + p_TblName + " Where 1 = 1 " + p_Condition;
            p_RecordCount = int.Parse(ExecuteScalar(_SQL,sqlParameter).ToString());
            StringBuilder _SB = new StringBuilder();
            _SB.Append("SELECT * ");
            _SB.Append("FROM ");
            _SB.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY " + ClassID + (p_OrderType == 1 ? " DESC" : " ASC") + ") ");
            _SB.Append(" AS OrderRank," + Field + " FROM " + p_TblName + " Where 1 = 1 " + p_Condition + ") ");
            _SB.Append(" AS Rank ");
            _SB.Append(" WHERE OrderRank BETWEEN " + ((p_PageIndex - 1) * p_PageSize + 1) + " AND " + p_PageIndex * p_PageSize + " ");
            _SB.Append(" ORDER BY " + p_OrderColumnName + " " + (p_OrderType == 1 ? "DESC" : "ASC") + " ");



            SqlConnection iConn = new SqlConnection(CONN_STRING);
            SqlCommand iCommand = iConn.CreateCommand();
            if (sqlParameter != null)
            {
                foreach (SqlParameter iParm in sqlParameter)
                    iCommand.Parameters.Add(iParm);
            }
            DataSet ds = new DataSet();
            try
            {
                iCommand.CommandText = _SB.ToString();
                iConn.Open();
                using (SqlDataAdapter iDa = new SqlDataAdapter(iCommand))
                {
                    iDa.Fill(ds);

                    return ds;
                }
            }
            catch (Exception _Ex)
            {
                throw _Ex;
            }
            finally
            {
                iConn.Close();
                iCommand.Dispose();
                iConn.Dispose();
                ds.Dispose();
            }
        }

        public static DataSet GetRecordFromPageBySQL4(string ClassID, string Field, string p_TblName, int p_PageSize, int p_PageIndex, int p_OrderType, string p_OrderColumnName, string p_Condition, SqlParameter[] sqlParameter)
        {
            StringBuilder _SB = new StringBuilder();
            _SB.Append("SELECT * ");
            _SB.Append("FROM ");
            _SB.Append(" (SELECT ROW_NUMBER() OVER (ORDER BY " + ClassID + (p_OrderType == 1 ? " DESC" : " ASC") + ") ");
            _SB.Append(" AS OrderRank," + Field + " FROM " + p_TblName + " Where 1 = 1 " + p_Condition + ") ");
            _SB.Append(" AS Rank ");
            _SB.Append(" WHERE OrderRank BETWEEN " + ((p_PageIndex - 1) * p_PageSize + 1) + " AND " + p_PageIndex * p_PageSize + " ");
            _SB.Append(" ORDER BY " + p_OrderColumnName + " " + (p_OrderType == 1 ? "DESC" : "ASC") + " ");



            SqlConnection iConn = new SqlConnection(CONN_STRING);
            SqlCommand iCommand = iConn.CreateCommand();
            if (sqlParameter != null)
            {
                foreach (SqlParameter iParm in sqlParameter)
                    iCommand.Parameters.Add(iParm);
            }
            DataSet ds = new DataSet();
            try
            {
                iCommand.CommandText = _SB.ToString();
                iConn.Open();
                using (SqlDataAdapter iDa = new SqlDataAdapter(iCommand))
                {
                    iDa.Fill(ds);

                    return ds;
                }
            }
            catch (Exception _Ex)
            {
                throw _Ex;
            }
            finally
            {
                iConn.Close();
                iCommand.Dispose();
                iConn.Dispose();
                ds.Dispose();
            }
        }

        #endregion
    }
}
