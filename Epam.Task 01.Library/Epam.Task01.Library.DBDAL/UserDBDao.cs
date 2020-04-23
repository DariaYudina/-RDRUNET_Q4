using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.DBDAL
{
    public class UserDBDao : IUserDao
    {
        private readonly string _connectionString;

        public UserDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public int AddUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "AddUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter login = new SqlParameter
                    {
                        ParameterName = "@Login",
                        Value = user.Login,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter password = new SqlParameter
                    {
                        ParameterName = "@Password",
                        Value = user.Password,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(login);
                    command.Parameters.Add(password);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)id.Value;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public bool ChangeUserRoles(int userId, List<int> rolesId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "ChangeUserRoles";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@userId",
                        Value = userId,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter listrolesId = new SqlParameter
                    {
                        ParameterName = "@listRolesId",
                        Value = JsonConvert.SerializeObject(rolesId.Select(i => new { Id = i })),
                        IsNullable = true,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(listrolesId);
                    connection.Open();
                    var res = command.ExecuteNonQuery();
                    return res > 0;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "GetRoles";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var role = (string)reader["RoleName"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Role
                    {
                        Id = (int)(reader["Id"]),
                        RoleName = (string)reader["RoleName"]
                    };
                }
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "GetUsers";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter idparam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(idparam);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Role> rolejson = (reader["Roles"]) is DBNull
                        ? new List<Role>()
                        : JsonConvert.DeserializeObject<List<Role>>((string)(reader["Roles"]));

                    return new User
                    {
                        Id = (int)(reader["Id"]),
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        Roles = rolejson
                    };
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "GetUsers";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    List<Role> rolejson = (reader["Roles"]) is DBNull
                      ? new List<Role>()
                      : JsonConvert.DeserializeObject<List<Role>>((string)(reader["Roles"]));
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Login = (string)reader["Login"];
                        var Password = (string)reader["Password"];
                        var Roles = rolejson;
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new User
                    {
                        Id = (int)(reader["Id"]),
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        Roles = rolejson
                    };
                }
            }
        }
    }
}
