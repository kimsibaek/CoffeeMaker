using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CoffeeMaker_Server.Database
{
    class DbQuery : Database
    {
        private readonly List<OracleParameter> _parameters = new List<OracleParameter>();
        private int _arrayBindCount = 0;
        private void CreateParams(DataTable dataTable)
        {
            //Input parameters
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                OracleParameter param = new OracleParameter(dataTable.Columns[i].ColumnName, OracleDbType.Varchar2, ParameterDirection.Input);
                param.Value = ObjectToString(dataTable,i).ToArray();
                param.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                param.Size =dataTable.Columns.Count;
                //_arrayBindCount = dataTable.Rows.Count;
                _parameters.Add(param);
            }
            //Output Parameters
            _parameters.Add(new OracleParameter("OUT_MESSAGE", OracleDbType.Varchar2, ParameterDirection.Output));
            _parameters.Add(new OracleParameter("OUT_ERRORCNT", OracleDbType.Int32, ParameterDirection.Output));
        }
        private List<string> ObjectToString(DataTable dataTable, int index)
        {
            List<string> strArayList = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                strArayList.Add(row.ItemArray[index].ToString());
            }

            return strArayList;
        }

        public int DML_InsertOrderHistory(DataTable dtOrderHistory)
        {
            int result = 0;
            string package = "PKG_CM_ORDER.CM_ORDERHISTORY_INSERT";
            _parameters.Clear();
            _arrayBindCount = 0;

            CreateParams(dtOrderHistory);
            result = ExcutePakage(package, _parameters, CommandType.StoredProcedure, _arrayBindCount);

            return result;
        }

        public int DML_InsertAccount(DataTable dtAccount)
        {
            int result = 0;
            string package = "PKG_CM_ORDER.CM_ACCOUNT_INSERT";
            _parameters.Clear();
            _arrayBindCount = 0;
            CreateParams(dtAccount);

            result=ExcutePakage(package, _parameters, CommandType.StoredProcedure, _arrayBindCount);

            return result;
        }
    }
}
