using Epam.Task01.Library.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Task01.Library.DBDAL
{
    public class AuthorDBDao
    {
        private static readonly string ConnectionString;
        static AuthorDBDao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        public void AddAuthor(Author author)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddNewspaper";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var Id = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                var Firstname = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    Value = author.FirstName,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var Lastname = new SqlParameter
                {
                    ParameterName = "@LastName",
                    Value = author.LastName,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                command.Parameters.Add(Id);
                command.Parameters.Add(Firstname);
                command.Parameters.Add(Lastname);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<Author> GetAuthors()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAuthors";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                while (reader.Read())
                {
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
