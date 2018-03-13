using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeMakcer.Database
{
    public class Database : IDbConnection 
    {
        private string _address = "172.20.101.237";
        private string _port = "1521";
        private string _serviceName = "xe";
        private string _userID = "sbg";
        private string _password = "1";

        private string _connectionString = "";
   
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
            MessageBox.Show("aaa");
        }

        public void Dispose()
        {
            Close();
        }

        public void DbConnection()
        {
            _connectionString = string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id = {3}; Password = {4}", _address, _port, _serviceName, _userID, _password);
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
                MessageBox.Show("오류가 발생하여 트랜잭션이 취소 되었습니다");
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
