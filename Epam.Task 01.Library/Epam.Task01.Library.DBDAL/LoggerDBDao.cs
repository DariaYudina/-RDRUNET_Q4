using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.DBDAL
{
    public class LoggerDBDao : ILoggerDao
    {
        private readonly string _connectionString;

        public LoggerDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public void LogError(LogDetail exception)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "AddAppLog";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter Id = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                SqlParameter message = new SqlParameter
                {
                    ParameterName = "@Message",
                    Value = exception.Message,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter appLayer = new SqlParameter
                {
                    ParameterName = "@AppLayer",
                    Value = exception.AppLayer,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter methodName = new SqlParameter
                {
                    ParameterName = "@MethodName",
                    Value = exception.MethodName,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter className = new SqlParameter
                {
                    ParameterName = "@ClassName",
                    Value = exception.ClassName,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter controllerName = new SqlParameter
                {
                    ParameterName = "@ControllerName",
                    Value = exception.ControllerName,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter actionName = new SqlParameter
                {
                    ParameterName = "@ActionName",
                    Value = exception.ActionName,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter login = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = exception.Login,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter date = new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = exception.Date,
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input
                };

                SqlParameter stackTrace = new SqlParameter
                {
                    ParameterName = "@StackTrace",
                    Value = exception.StackTrace,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                command.Parameters.Add(message);
                command.Parameters.Add(methodName);
                command.Parameters.Add(className);
                command.Parameters.Add(appLayer);
                command.Parameters.Add(controllerName);
                command.Parameters.Add(actionName);
                command.Parameters.Add(login);
                command.Parameters.Add(date);
                command.Parameters.Add(stackTrace);
                command.Parameters.Add(Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
