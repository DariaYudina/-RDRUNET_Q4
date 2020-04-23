using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.DBDAL
{
    public class AuthorDBDao : IAuthorDao
    {
        private readonly string _connectionString;

        public AuthorDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public int AddAuthor(Author author)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "AddAuthor";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter firstname = new SqlParameter
                    {
                        ParameterName = "@FirstName",
                        Value = author.FirstName,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter lastname = new SqlParameter
                    {
                        ParameterName = "@LastName",
                        Value = author.LastName,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter type = new SqlParameter
                    {
                        ParameterName = "@Type",
                        Value = author.GetType().Name,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };
                    command.Parameters.Add(id);
                    command.Parameters.Add(firstname);
                    command.Parameters.Add(lastname);
                    command.Parameters.Add(type);
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

        public Author GetAuthorById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = "GetAuthorById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter aid = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(aid);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                    while (reader.Read())
                    {
                        return new Author
                        {
                            Id = (int)(reader["Id"]),
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"]
                        };
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };

            }
        }

        public IEnumerable<Author> GetAuthors()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "GetAuthors";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var FirstName = (string)reader["FirstName"];
                        var LastName = (string)reader["LastName"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Author
                    {
                        Id = (int)(reader["Id"]),
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"]
                    };
                }
            }
        }
    }
}
