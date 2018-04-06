using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Oracle.ManagedDataAccess.Types;

namespace CoffeeMaker_Server.Database
{
    public partial class Database : IDbConnection
    {
        private string _address = "172.20.101.237";
        private string _port = "1521";
        private string _serviceName = "xe";
        private string _userID = "sbg";
        private string _password = "1";

        private string _connectionString = "";
        internal static object _lock = new object();

        private OracleConnection _connection;

        public Database()
        {
            DbConnection();
        }

        public string ConnectionString
        {
            get { return _connectionString; }

            set { value = _connectionString; }
        }

        public int ConnectionTimeout
        {
            get { return _connection.ConnectionTimeout; }
        }

        string IDbConnection.Database
        {
            get { return _connection.Database; }
        }

        public ConnectionState State
        {
            get { return _connection.State; }
        }

        public IDbTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _connection.BeginTransaction(il);
        }

        public void Close()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection.Close();
                OracleConnection.ClearPool(_connection);
            }
        }

        public void ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        public IDbCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Dispose()
        {
            Close();
        }

        public void DbConnection()
        {
            _connectionString = string.Format($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_address})(PORT={_port}))(CONNECT_DATA=(SERVICE_NAME={_serviceName})));User Id = {_userID}; Password = {_password}");
            _connection = new OracleConnection(_connectionString);

            Open();
        }

        //조회 메서드 (SELECT 값 반환)ExecuteQuery
        public DataTable ExecuteQuery(OracleConnection conn, string _query)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = conn;
            command.CommandText = _query;

            DataTable data = new DataTable();
            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = command;

            adapter.Fill(data);

            return data;
        }

        public int ExcutePakage(string pakage, List<OracleParameter> parameters, CommandType commandType, int arrayBindCount)
        {
            int result = 0;
            int outResult = 0;
            OracleTransaction transaction = _connection.BeginTransaction();
            using (OracleCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = pakage;
                cmd.CommandType = commandType;
                if (parameters != null && parameters.Count > 0)
                {
                    if (arrayBindCount >= 0)
                    {
                       // cmd.ArrayBindCount = arrayBindCount;

                        foreach (var param in parameters)
                        {
                            OracleParameter parameter = new OracleParameter(param.ParameterName, param.OracleDbType);
                            parameter.Direction = param.Direction;
                            parameter.Value = param.Value;
                           
                            if (param.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
                            {
                                parameter.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                                

                                if (param.Size == 0)
                                {
                                    parameter.Size = 1;

                                    if (param.OracleDbType == OracleDbType.Varchar2)
                                    {
                                        parameter.Value = new OracleString[1] { OracleString.Null };
                                    }
                                    else
                                    {
                                        parameter.Value = new OracleDecimal[1] { OracleDecimal.Null };
                                    }
                                }
                                else if(param.Size ==1)
                                {
                                    parameter.Size = param.Size + 1;

                                    if (param.OracleDbType == OracleDbType.Varchar2)
                                    {
                                        var list = new List<string>((string[])param.Value);
                                        list.Insert(0, "");

                                        parameter.Value = list.ToArray();
                                    }
                                    else
                                    {
                                        var list = new List<int>((int[])param.Value);
                                        list.Insert(0, 0);

                                        parameter.Value = list.ToArray();
                                    }
                                }
                                else
                                {
                                    if (param.OracleDbType == OracleDbType.Varchar2)
                                    {
                                        var list = new List<string>((string[])param.Value);
                                        parameter.Value = list.ToArray();
                                    }
                                    else
                                    {
                                        var list = new List<int>((int[])param.Value);
                                        parameter.Value = list.ToArray();
                                    }
                                }

                            }
                            else if (param.Direction == ParameterDirection.Output)
                            {
                                if (param.DbType == DbType.Int32)
                                {
                                    param.DbType = DbType.Int32;
                                    param.Size = 256;
                                    parameter.Size = param.Size;
                                }
                                param.DbType = DbType.AnsiStringFixedLength;
                                param.Size = 256;
                                parameter.Size = param.Size;
                            }
                            cmd.Parameters.Add(parameter);
                        }
                    }
                }
                result = cmd.ExecuteNonQuery();
                if (parameters != null)
                { 
                    foreach (var param in parameters)
                    {
                        if (param.OracleDbType == OracleDbType.Int32)
                        {
                            outResult = int.Parse(param.Value.ToString());
                        }
                    }
                }

                result = outResult;
                transaction.Commit();
                }
            
            return result;
        }

        //반환값 없는 쿼리 실행 메서드(INSERT, UPDATE, DELETE 등)
        public void ExecuteNonQuery(OracleConnection conn, string _query)
        {
            OracleCommand command = new OracleCommand(_query);
            command.Connection = conn;
            try
            {
                command.ExecuteNonQuery();
                command.Transaction = DbTransaction(conn);
                command.Transaction.Commit();
            }
            catch
            {
                command.Transaction.Rollback();
            }

        }

        //트렌잭션 관리 메서드 (커밋, 롤백 등)
        public OracleTransaction DbTransaction(OracleConnection conn)
        {
            OracleTransaction transaction = null;
            transaction = conn.BeginTransaction();

            return transaction;
        }
    }
}
