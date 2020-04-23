using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;

namespace Epam.Task01.Library.DBDAL
{
    public class PatentDBDao : IPatentDao
    {
        private readonly string _connectionString;

        public PatentDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public int AddPatent(Patent item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "AddPatent";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter title = new SqlParameter
                    {
                        ParameterName = "@Title",
                        Value = item.Title,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter pagesCount = new SqlParameter
                    {
                        ParameterName = "@PagesCount",
                        Value = item.PagesCount,
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter commentary = new SqlParameter
                    {
                        ParameterName = "@Commentary",
                        Value = item.Commentary,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter libraryType = new SqlParameter
                    {
                        ParameterName = "@LibraryType",
                        Value = item.GetType().Name,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter country = new SqlParameter
                    {
                        ParameterName = "@Country",
                        Value = item.Country,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter registrationNumber = new SqlParameter
                    {
                        ParameterName = "@RegistrationNumber",
                        Value = item.RegistrationNumber,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter applicationDate = new SqlParameter
                    {
                        ParameterName = "@ApplicationDate",
                        Value = item.ApplicationDate,
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter publicationDate = new SqlParameter
                    {
                        ParameterName = "@PublicationDate",
                        Value = item.PublicationDate,
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    var authorsId = from i in item.Authors
                                    select new { i.Id };
                    string json = JsonConvert.SerializeObject(authorsId, Formatting.Indented);
                    SqlParameter listAuthorsId = new SqlParameter
                    {
                        ParameterName = "@listAuthorsId",
                        Value = json,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(title);
                    command.Parameters.Add(pagesCount);
                    command.Parameters.Add(commentary);
                    command.Parameters.Add(libraryType);
                    command.Parameters.Add(country);
                    command.Parameters.Add(registrationNumber);
                    command.Parameters.Add(applicationDate);
                    command.Parameters.Add(publicationDate);
                    command.Parameters.Add(listAuthorsId);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)idParam.Value;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public int EditPatent(Patent item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "UpdatePatent";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = item.Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter title = new SqlParameter
                    {
                        ParameterName = "@Title",
                        Value = item.Title,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter pagesCount = new SqlParameter
                    {
                        ParameterName = "@PagesCount",
                        Value = item.PagesCount,
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter commentary = new SqlParameter
                    {
                        ParameterName = "@Commentary",
                        Value = item.Commentary,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter libraryType = new SqlParameter
                    {
                        ParameterName = "@LibraryType",
                        Value = item.GetType().Name,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter country = new SqlParameter
                    {
                        ParameterName = "@Country",
                        Value = item.Country,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter registrationNumber = new SqlParameter
                    {
                        ParameterName = "@RegistrationNumber",
                        Value = item.RegistrationNumber,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter applicationDate = new SqlParameter
                    {
                        ParameterName = "@ApplicationDate",
                        Value = item.ApplicationDate,
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter publicationDate = new SqlParameter
                    {
                        ParameterName = "@PublicationDate",
                        Value = item.PublicationDate,
                        SqlDbType = System.Data.SqlDbType.DateTime,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    var authorsId = from i in item.Authors
                                    select new { i.Id };
                    string json = JsonConvert.SerializeObject(authorsId, Formatting.Indented);
                    SqlParameter listAuthorsId = new SqlParameter
                    {
                        ParameterName = "@AuthorsId",
                        Value = json,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter cntUpdateRow = new SqlParameter
                    {
                        ParameterName = "@CntUpdateRow",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(title);
                    command.Parameters.Add(pagesCount);
                    command.Parameters.Add(commentary);
                    command.Parameters.Add(libraryType);
                    command.Parameters.Add(country);
                    command.Parameters.Add(registrationNumber);
                    command.Parameters.Add(applicationDate);
                    command.Parameters.Add(publicationDate);
                    command.Parameters.Add(listAuthorsId);
                    command.Parameters.Add(cntUpdateRow);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)cntUpdateRow.Value;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public IEnumerable<Patent> GetPatents()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetPatents";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    DateTime? applicationDate = reader["ApplicationDate"] is DBNull
                    ? null
                    : (DateTime?)reader["ApplicationDate"];
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Authors = authorjson;
                        var Country = (string)reader["Country"];
                        var RegistrationNumber = (string)reader["RegistrationNumber"];
                        var ApplicationDate = applicationDate;
                        var PublicationDate = (DateTime)reader["PublicationDate"];
                        var Title = (string)reader["Title"];
                        var PagesCount = (int)(reader["PagesCount"]);
                        var Commentary = (string)reader["Commentary"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Patent
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        Country = (string)reader["Country"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        ApplicationDate = applicationDate,
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetPatentsByAuthor";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(idParam);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    DateTime? applicationDate = reader["ApplicationDate"] is DBNull
                                               ? null
                                               : (DateTime?)reader["ApplicationDate"];
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Authors = authorjson;
                        var Country = (string)reader["Country"];
                        var RegistrationNumber = (string)reader["RegistrationNumber"];
                        var ApplicationDate = applicationDate;
                        var PublicationDate = (DateTime)reader["PublicationDate"];
                        var Title = (string)reader["Title"];
                        var PagesCount = (int)(reader["PagesCount"]);
                        var Commentary = (string)reader["Commentary"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Patent
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        Country = (string)reader["Country"],
                        RegistrationNumber = (string)reader["RegistrationNumber"],
                        ApplicationDate = applicationDate,
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
