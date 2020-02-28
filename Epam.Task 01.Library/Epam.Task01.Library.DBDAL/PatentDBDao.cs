using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;
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
    public class PatentDBDao : IPatentDao
    {
        private static readonly string ConnectionString;
        static PatentDBDao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        public int AddPatent(Patent item)
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
                    Value = item.Title,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var PagesCount = new SqlParameter
                {
                    ParameterName = "@PagesCount",
                    Value = item.PagesCount,
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input
                };

                var Commentary = new SqlParameter
                {
                    ParameterName = "@Commentary",
                    Value = item.Commentary,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var LibraryType = new SqlParameter
                {
                    ParameterName = "@LibraryType",
                    Value = item.GetType().Name,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var Country = new SqlParameter
                {
                    ParameterName = "@Country",
                    Value = item.Country,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var RegistrationNumber = new SqlParameter
                {
                    ParameterName = "@RegistrationNumber",
                    Value = item.RegistrationNumber,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };

                var ApplicationDate = new SqlParameter
                {
                    ParameterName = "@RegistrationNumber",
                    Value = item.ApplicationDate,
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input
                };

                var PublicationDate = new SqlParameter
                {
                    ParameterName = "@RegistrationNumber",
                    Value = item.PublicationDate,
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input
                };

                var authorsId = from i in item.Authors
                                select new { i.Id };
                string json = JsonConvert.SerializeObject(authorsId, Formatting.Indented);
                var listAuthorsId = new SqlParameter
                {
                    ParameterName = "@listAuthorsId",
                    Value = json,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                command.Parameters.Add(Id);
                command.Parameters.Add(Title);
                command.Parameters.Add(PagesCount);
                command.Parameters.Add(Commentary);
                command.Parameters.Add(LibraryType);
                command.Parameters.Add(Country);
                command.Parameters.Add(RegistrationNumber);
                command.Parameters.Add(ApplicationDate);
                command.Parameters.Add(PublicationDate);
                command.Parameters.Add(listAuthorsId);
                connection.Open();
                command.ExecuteNonQuery();
                return (int)Id.Value;
            }
        }

        public IEnumerable<Patent> GetPatentItems()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetPatents";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    yield return new Patent
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        Country = (string)reader["Country"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        ApplicationDate = (DateTime)(reader["ApplicationDate"]),
                        PublicationDate = (DateTime)reader["PublicationDate"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }
        public IEnumerable<Patent> GetPatentsByAuthorId(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetPatentsByAuthor";
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

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    yield return new Patent
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        Country = (string)reader["Country"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        ApplicationDate = (DateTime)(reader["ApplicationDate"]),
                        PublicationDate = (DateTime)reader["PublicationDate"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

    }
}
