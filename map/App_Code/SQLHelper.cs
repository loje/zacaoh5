//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
 
//
//
// �ļ�����SQLHelper.cs
// �ļ������������� SqlClient ����ٴη�װ
//
//
// �޸����������ע�ͣ�����ĸ����ط���
// public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameter) 
// public static object ExecuteScalar(string commandText, params SqlParameter[] parameter) 
// public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameter) 
// public static DataTable ExecuteTable( string commandText, params SqlParameter[] parameter) 
//
// �޸ı�ʶ��
// �޸�������
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
	/// SqlHelper ���Ƕ� SqlClient ����ٴη�װ
	/// </summary>
	public abstract class SQLHelper 
	{
		/// <summary>
		///  Ĭ�ϵ����ݿ������ַ���
		/// </summary>
        //public static string CONN_STRING = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        public static string CONN_STRING = "server=.;database=school;user id=sa;password=123456";
		// ������������Ĺ�ϣ��
		private static Hashtable c_ParmCache = Hashtable.Synchronized(new Hashtable());


		#region ����sqlCommand
		/// <summary>
		/// ����sqlCommand
		/// </summary>
		/// <param name="sqlCommand">System.Data.SqlClient.SqlCommand</param>
		/// <param name="commandText">SQL���</param>
		/// <param name="sqlConnection">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="sqlTransaction">Ҫ�� SQL Server ���ݿ��д���� Transact-SQL ����</param>
		/// <param name="sqlParameter">SQL������</param>
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

		#region ����SQL������/ȡ�û����SQL������
		/// <summary>
		/// ����SQL������
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="parameters"></param>
		public static void CacheParameters(string CacheKey, params SqlParameter[] parameters) 
		{
			c_ParmCache[CacheKey] = parameters;
		}


		/// <summary>
		/// ȡ�û����SQL������
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

		#region �ɼ򻯵Ĳ�������һ��sqlParameter
		/// <summary>
		/// �ɼ򻯵Ĳ�������һ��sqlParameter
		/// </summary>
		/// <param name="parameterName">��������</param>
		/// <param name="sqlDbType">��������</param>
		/// <param name="value">����ֵ</param>
		/// <returns>�����õ�sqlParameter</returns>
		public static SqlParameter PrepareParameter(string parameterName, SqlDbType sqlDbType, object value) 
		{
			SqlParameter sqlParameter = new SqlParameter(parameterName, value);
			sqlParameter.SqlDbType = sqlDbType;
			return sqlParameter;
		}

		/// <summary>
		/// �ɼ򻯵Ĳ�������һ��sqlParameter
		/// </summary>
		/// <param name="parameterName">��������</param>
		/// <param name="sqlDbType">��������</param>
		/// <param name="value">����ֵ</param>
		/// <param name="size">��������ֽ���</param>
		/// <returns>�����õ�sqlParameter</returns>
		public static SqlParameter PrepareParameter(string parameterName, SqlDbType sqlDbType, object value, int size) 
		{
			SqlParameter sqlParameter = new SqlParameter(parameterName, value);
			sqlParameter.SqlDbType = sqlDbType;
			sqlParameter.Size = size;
			return sqlParameter;
		}
		#endregion

		#region �� ExecuteNonQuery �������ߴ����ط�װ��ִ�� SQL ��䣬������Ӱ���������
		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string commandText) 
		{
			return ExecuteNonQuery(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteNonQuery(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString) 
		{
			return ExecuteNonQuery(commandText, connectionString, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, CommandType commandType) 
		{
			return ExecuteNonQuery(commandText, connectionString, commandType, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, params SqlParameter[] parameter) 
		{
			return ExecuteNonQuery(commandText, connectionString, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��Ӱ�������</returns>
		public static int ExecuteNonQuery(string commandText, string connectionString, CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteNonQuery(commandText, connectionString, commandType, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="sqlTransaction">Ҫ�� SQL Server ���ݿ��д���� Transact-SQL ����</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��Ӱ�������</returns>
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

		#region �� ExecuteScalar �������ߴ����ط�װ��ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С���
		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
		public static object ExecuteScalar(string commandText) 
		{
			return ExecuteScalar(commandText,  CONN_STRING, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
		public static object ExecuteScalar(string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteScalar(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
		public static object ExecuteScalar(string commandText, string connectionString) 
		{
			return ExecuteScalar(commandText, connectionString, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
		public static object ExecuteScalar(string commandText, string connectionString, CommandType commandType) 
		{
			return ExecuteScalar(commandText, connectionString, commandType, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
		public static object ExecuteScalar(string commandText, string connectionString, params SqlParameter[] parameter) 
		{
			return ExecuteScalar(commandText, connectionString, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
		public static object ExecuteScalar(string commandText, string connectionString, CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteScalar(commandText, connectionString, commandType, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�У������������л��С�
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="sqlTransaction">Ҫ�� SQL Server ���ݿ��д���� Transact-SQL ����</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
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
		
		#region �� ExecuteReader �������ߴ����ط�װ��ִ�в�ѯ������һ�� SqlDataReader��
		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText) 
		{
			return ExecuteReader(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteReader(commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString) 
		{
			return ExecuteReader(commandText, connectionString, CommandType.Text, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, CommandType commandType) 
		{
			return ExecuteReader(commandText, connectionString, commandType, (SqlTransaction)null, null);
		}

		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, params SqlParameter[] parameter) 
		{
			return ExecuteReader(commandText, connectionString, CommandType.Text, (SqlTransaction)null, parameter);
		}

		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>SqlDataReader</returns>
		public static SqlDataReader ExecuteReader(string commandText, string connectionString, CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteReader(commandText, connectionString, commandType, (SqlTransaction)null, parameter);
		}
		
		/// <summary>
		/// ������ִ�в�ѯ������һ�� SqlDataReader
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="sqlTransaction">Ҫ�� SQL Server ���ݿ��д���� Transact-SQL ����</param>
		/// <param name="parameter">SQL������</param>
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

		#region �� ExecuteTable������������ط�װ��ʹ�� SqlDataAdapter ִ�в�ѯ������ DataSet �ĵ�һ�� DataTable��
		/// <summary>
		/// ʹ�� SqlDataAdapter ִ�в�ѯ������ DataSet �ĵ�һ�� DataTable
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText )
		{
			return ExecuteTable( commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, null); 
		}

		/// <summary>
		/// ʹ�� SqlDataAdapter ִ�в�ѯ������ DataSet �ĵ�һ�� DataTable
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText, params SqlParameter[] parameter) 
		{
			return ExecuteTable( commandText, CONN_STRING, CommandType.Text, (SqlTransaction)null, parameter); 
		}

		/// <summary>
		/// ʹ�� SqlDataAdapter ִ�в�ѯ������ DataSet �ĵ�һ�� DataTable
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText, string connectionString )
		{
			return ExecuteTable( commandText, connectionString, CommandType.Text, (SqlTransaction)null, null); 
		}

		/// <summary>
		/// ʹ�� SqlDataAdapter ִ�в�ѯ������ DataSet �ĵ�һ�� DataTable
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="parameter">SQL������</param>
		/// <returns>System.Data.DataTable</returns>
		public static DataTable ExecuteTable( string commandText, string connectionString ,CommandType commandType, params SqlParameter[] parameter) 
		{
			return ExecuteTable( commandText, connectionString, commandType, (SqlTransaction)null, parameter); 
		}

		/// <summary>
		/// ʹ�� SqlDataAdapter ִ�в�ѯ������ DataSet �ĵ�һ�� DataTable
		/// </summary>
		/// <param name="commandText">SQL���</param>
		/// <param name="connectionString">���ݿ������ַ���</param>
		/// <param name="commandType">SQL�������ԣ�Ĭ��ֵΪCommandType.Text</param>
		/// <param name="sqlTransaction">Ҫ�� SQL Server ���ݿ��д���� Transact-SQL ����</param>
		/// <param name="parameter">SQL������</param>
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
	
		#region ���ݿ��ҳͨ�ô洢����
		/// <summary>
		/// ���ݿ��ҳͨ�ô洢����
		/// </summary>
		/// <param name="tblName">����</param>
		/// <param name="fldName">�������ؼ��ֶΣ�</param>
		/// <param name="pageSize">ÿҳ��¼��</param>
		/// <param name="pageIndex">Ҫ��ȡ��ҳ��</param>
		/// <param name="orderType">������, 0 - ����, 1 - ����</param>
		/// <param name="strWhere">��ѯ���� (ע��: ��Ҫ�� where)</param>
		/// <param name="output">�ܼ�¼��</param>
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

        #region SqlServer2005ͨ�÷�ҳ����
        /// <summary>
        /// SqlServer2005ͨ�÷�ҳ����
        /// </summary>
        /// <param name="p_TblName">����</param>
        /// <param name="p_PageSize">ÿҳ��¼��</param>
        /// <param name="p_PageIndex">��ǰҳ��</param>
        /// <param name="p_OrderType">����ʽ��0-���� 1-����</param>
        /// <param name="p_OrderColumnName">����������</param>
        /// <param name="p_Condition">��ѯ����</param>
        /// <param name="p_RecordCount">�ܼ�¼��</param>
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
