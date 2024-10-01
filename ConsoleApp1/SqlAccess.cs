using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global;
using System.Xml.Serialization;
using System.Runtime.Intrinsics.Arm;

namespace SQL
{
    public class SqlAccess
    {
        private string sqlServer = Globals.sqlServer_G;
        private string sqlDatabase = Globals.sqlDatabase_G;
        private string connectionString;
        private string procedureName;
        private SqlConnection con;
        private SqlCommand cmd;

        private string getConnectString() {
        return  connectionString = $"Data Source={sqlServer};Initial Catalog={sqlDatabase};Integrated Security=True;";

        }

        public SqlConnection connectToDataBase( ) {
            connectionString = getConnectString();
            return con = new SqlConnection(connectionString);
        }

        public SqlCommand getCmd(string procedureName)
        {
             return cmd = new SqlCommand(procedureName, con);
        }

        public void addSqlParam(string parameterName, SqlDbType SqlDbType, object value, SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = SqlDbType,
                Value = value
            });
        }

       

    }

}
