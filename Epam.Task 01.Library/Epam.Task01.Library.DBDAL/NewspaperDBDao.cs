using Epam.Task01.Library.AbstractDAL;
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
    public class NewspaperDBDao : INewspaperDao
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public int AddNewspaper(Newspaper newspaper)
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

                var Title = new SqlParameter
                {
                    ParameterName = "@Title",
                    Value = newspaper.Title,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                var City = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = newspaper.Title,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                var PublishingCompany = new SqlParameter
                {
                    ParameterName = "@PublishingCompany",
                    Value = newspaper.Title,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                var ISSN = new SqlParameter
                {
                    ParameterName = "@ISSN",
                    Value = newspaper.Title,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var YearOfPublishing = new SqlParameter
                {
                    ParameterName = "@YearOfPublishing",
                    Value = newspaper.Title,
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input
                };

                command.Parameters.Add(Id);
                command.Parameters.Add(Title);
                command.Parameters.Add(City);
                command.Parameters.Add(PublishingCompany);
                command.Parameters.Add(ISSN);
                command.Parameters.Add(YearOfPublishing);
                connection.Open();
                command.ExecuteNonQuery();

                return (int)Id.Value;
            }
        }

        public Newspaper GetNewspaperItemById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetNewspaperById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var Id = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(Id);
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                while (reader.Read())
                {
                    return new Newspaper
                    {
                        Id = (int)(reader["Id"]),
                        Title = (string)reader["Title"],
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        Issn = (string)reader["ISSN"],
                    };
                }
                return null;
            }
        }

        public IEnumerable<Newspaper> GetNewspaperItems()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetNewspapers";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                while (reader.Read())
                {
                    yield return new Newspaper
                    {
                        Id = (int)(reader["Id"]),
                        Title = (string)reader["Title"],
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        Issn = (string)reader["ISSN"],
                    };
                }
            }
        }
    }
}
