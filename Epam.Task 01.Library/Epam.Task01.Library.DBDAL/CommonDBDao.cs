using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;

namespace Epam.Task01.Library.DBDAL
{

    public class CommonDBDao : ICommonDao
    {
        private readonly string _connectionString;

        public CommonDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public bool DeleteLibraryItemById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "DeleteLibraryItem";
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
                    var res = command.ExecuteNonQuery() > 0;
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItems()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetLibraryItems";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string type = (string)(reader["LibraryType"]);
                    switch (type)
                    {
                        case "Book":

                        {
                            List<Author> authorjson = (reader["Authors"]) is DBNull
                            ? new List<Author>()
                            : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                            try
                            {
                                var Id = (int)(reader["Id"]);
                                var Authors = authorjson;
                                var City = (string)reader["City"];
                                var PublishingCompany = (string)reader["PublishingCompany"];
                                var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                var Isbn = reader["ISBN"] is DBNull ? "" : (string)reader["ISBN"];
                                var Title = (string)reader["Title"];
                                var PagesCount = (int)(reader["PagesCount"]);
                                var Commentary = reader["Commentary"] is DBNull ? "" : (string)reader["Commentary"];
                            }
                            catch (Exception e)
                            {
                                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                            }
                            yield return new Book
                            {
                                Id = (int)(reader["Id"]),
                                Authors = authorjson,
                                City = (string)reader["City"],
                                PublishingCompany = (string)reader["PublishingCompany"],
                                YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"],
                                Title = (string)reader["Title"],
                                PagesCount = (int)(reader["PagesCount"]),
                                Commentary = reader["Commentary"] is DBNull ? "" : (string)reader["Commentary"]
                            };
                            break;
                        }

                    case "Issue":
                        {
                            List<Newspaper> issue = (reader["Newspaper"]) is DBNull
                            ? new List<Newspaper>()
                            : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));
                                int id;

                            try
                            {
                                id = (int)(reader["Id"]);
                                var Newspaper = issue[0];
                                var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                var CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"];
                                var DateOfPublishing = (DateTime)(reader["DateOfPublishing"]);
                                var PagesCount = (int)(reader["PagesCount"]);
                                var Commentary = (string)reader["Commentary"];
                            }
                            catch (Exception e)
                            {
                                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                            }
                            yield return new Issue
                            {
                                Id = id,
                                Newspaper = issue[0],
                                YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"],
                                DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                                PagesCount = (int)(reader["PagesCount"]),
                                Commentary = (string)reader["Commentary"],
                            };
                            break;
                        }

                    case "Patent":
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
                                ApplicationDate = (reader["ApplicationDate"]) is DBNull
                                                    ? null
                                                    : (DateTime?)(reader["ApplicationDate"]),
                                PublicationDate = (DateTime)reader["PublicationDate"],
                                Title = (string)reader["Title"],
                                PagesCount = (int)(reader["PagesCount"]),
                                Commentary = (string)reader["Commentary"],
                                YearOfPublishing = (int)reader["YearOfPublishing"]
                            };
                            break;
                        }
                    }
                }
            }
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetLibraryItemsByTitle";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter title = new SqlParameter
                {
                    ParameterName = "@Title",
                    Value = name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(title);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Authors = authorjson;
                        var City = (string)reader["City"];
                        var PublishingCompany = (string)reader["PublishingCompany"];
                        var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                        var Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"];
                        var Title = (string)reader["Title"];
                        var PagesCount = (int)(reader["PagesCount"]);
                        var Commentary = (string)reader["Commentary"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Book
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
                reader.NextResult();
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

                reader.NextResult();
                while (reader.Read())
                {
                    List<Newspaper> issue = (reader["Newspaper"]) is DBNull
                                              ? new List<Newspaper>()
                                              : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Newspaper = issue[0];
                        var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                        var CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"];
                        var DateOfPublishing = (DateTime)(reader["DateOfPublishing"]);
                        var PagesCount = (int)(reader["PagesCount"]);
                        var Commentary = (string)reader["Commentary"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Issue
                    {
                        Id = (int)(reader["Id"]),
                        Newspaper = issue[0],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"],
                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByYearOfPublishing()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetSortedLibraryItemsByYear";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string type = (string)(reader["LibraryType"]);

                    switch (type)
                    {
                        case "Book":

                            {
                                List<Author> authorjson = (reader["Authors"]) is DBNull
                                ? new List<Author>()
                                : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Authors = authorjson;
                                    var City = (string)reader["City"];
                                    var PublishingCompany = (string)reader["PublishingCompany"];
                                    var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                    var Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"];
                                    var Title = (string)reader["Title"];
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                }
                                catch (Exception e)
                                {
                                    throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                                }
                                yield return new Book
                                {
                                    Id = (int)(reader["Id"]),
                                    Authors = authorjson,
                                    City = (string)reader["City"],
                                    PublishingCompany = (string)reader["PublishingCompany"],
                                    YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                    Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"],
                                    Title = (string)reader["Title"],
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"]
                                };
                                break;
                            }

                        case "Issue":
                            {
                                List<Newspaper> issue = (reader["Newspaper"]) is DBNull
                                ? new List<Newspaper>()
                                : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Newspaper = issue[0];
                                    var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                    var CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"];
                                    var DateOfPublishing = (DateTime)(reader["DateOfPublishing"]);
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                }
                                catch (Exception e)
                                {
                                    throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                                }
                                yield return new Issue
                                {
                                    Id = (int)(reader["Id"]),
                                    Newspaper = issue[0],
                                    YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                    CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"],
                                    DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"],
                                };

                                break;
                            }

                        case "Patent":
                            {
                                List<Author> authorjson = (reader["Authors"]) is DBNull
                                                            ? new List<Author>()
                                                            : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Authors = authorjson;
                                    var Country = (string)reader["Country"];
                                    var RegistrationNumber = (string)reader["RegistrationNumber"];
                                    var ApplicationDate = (DateTime)(reader["ApplicationDate"]);
                                    var PublicationDate = (DateTime)reader["PublicationDate"];
                                    var Title = (string)reader["Title"];
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                    var YearOfPublishing = (int)reader["YearOfPublishing"];
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
                                    ApplicationDate = (DateTime)(reader["ApplicationDate"]),
                                    PublicationDate = (DateTime)reader["PublicationDate"],
                                    Title = (string)reader["Title"],
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"],
                                    YearOfPublishing = (int)reader["YearOfPublishing"]
                                };

                                break;
                            }

                        default:
                            break;
                    }
                }
            }
        }

        public AbstractLibraryItem GetLibraryItemById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "GetLibraryItemById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter Id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(Id);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string type = (string)(reader["LibraryType"]);
                        switch (type)
                        {
                            case "Book":

                                {
                                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                    ? new List<Author>()
                                    : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));

                                    return new Book
                                    {
                                        Id = (int)(reader["Id"]),
                                        Authors = authorjson,
                                        City = (string)reader["City"],
                                        PublishingCompany = (string)reader["PublishingCompany"],
                                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                        Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"],
                                        Title = (string)reader["Title"],
                                        PagesCount = (int)(reader["PagesCount"]),
                                        Commentary = (string)reader["Commentary"]
                                    };
                                }

                            case "Issue":
                                {
                                    List<Newspaper> issue = (reader["Newspaper"]) is DBNull
                                    ? new List<Newspaper>()
                                    : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));

                                    return new Issue
                                    {
                                        Id = (int)(reader["Id"]),
                                        Newspaper = issue[0],
                                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                        CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"],
                                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                                        PagesCount = (int)(reader["PagesCount"]),
                                        Commentary = (string)reader["Commentary"],
                                    };
                                }

                            case "Patent":
                                {
                                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                                                ? new List<Author>()
                                                                : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                    return new Patent
                                    {
                                        Id = (int)(reader["Id"]),
                                        Authors = authorjson,
                                        Country = (string)reader["Country"],
                                        RegistrationNumber = (string)reader["RegistrationNumber"],
                                        ApplicationDate = (reader["ApplicationDate"]) is DBNull
                                                            ? null
                                                            : (DateTime?)(reader["ApplicationDate"]),
                                        PublicationDate = (DateTime)reader["PublicationDate"],
                                        Title = (string)reader["Title"],
                                        PagesCount = (int)(reader["PagesCount"]),
                                        Commentary = (string)reader["Commentary"],
                                        YearOfPublishing = (int)reader["YearOfPublishing"]
                                    };
                                }

                            default:
                                return null;
                        }
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public IEnumerable<AbstractLibraryItem> GetBookAndPatentByAuthorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetBooksByAuthor";
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
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Authors = authorjson;
                        var City = (string)reader["City"];
                        var PublishingCompany = (string)reader["PublishingCompany"];
                        var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                        var Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"];
                        var Title = (string)reader["Title"];
                        var PagesCount = (int)(reader["PagesCount"]);
                        var Commentary = (string)reader["Commentary"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
                    yield return new Book
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }

                reader.NextResult();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Authors = authorjson;
                        var Country = (string)reader["Country"];
                        var RegistrationNumber = (string)reader["RegistrationNumber"];
                        var ApplicationDate = (DateTime)(reader["ApplicationDate"]);
                        var PublicationDate = (DateTime)reader["PublicationDate"];
                        var Title = (string)reader["Title"];
                        var PagesCount = (int)(reader["PagesCount"]);
                        var Commentary = (string)reader["Commentary"];
                        var YearOfPublishing = (int)reader["YearOfPublishing"];
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
                        ApplicationDate = (DateTime)(reader["ApplicationDate"]),
                        PublicationDate = (DateTime)reader["PublicationDate"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<AbstractLibraryItem> SortByYear()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetSortedLibraryItemsByYear";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string type = (string)(reader["LibraryType"]);

                    switch (type)
                    {
                        case "Book":
                            {
                                List<Author> authorjson = (reader["Authors"]) is DBNull
                                ? new List<Author>()
                                : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Authors = authorjson;
                                    var City = (string)reader["City"];
                                    var PublishingCompany = (string)reader["PublishingCompany"];
                                    var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                    var Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"];
                                    var Title = (string)reader["Title"];
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                }
                                catch (Exception e)
                                {
                                    throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                                }
                                yield return new Book
                                {
                                    Id = (int)(reader["Id"]),
                                    Authors = authorjson,
                                    City = (string)reader["City"],
                                    PublishingCompany = (string)reader["PublishingCompany"],
                                    YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                    Isbn =  reader["ISBN"] is DBNull ? null : (string)reader["ISBN"],
                                    Title = (string)reader["Title"],
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"]
                                };

                                break;
                            }

                        case "Issue":
                            {
                                List<Newspaper> issue = (reader["Newspaper"]) is DBNull
                                ? new List<Newspaper>()
                                : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Newspaper = issue[0];
                                    var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                    var CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"];
                                    var DateOfPublishing = (DateTime)(reader["DateOfPublishing"]);
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                }
                                catch (Exception e)
                                {
                                    throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                                }
                                yield return new Issue
                                {
                                    Id = (int)(reader["Id"]),
                                    Newspaper = issue[0],
                                    YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                    CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"],
                                    DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"],
                                };

                                break;
                            }

                        case "Patent":
                            {
                                List<Author> authorjson = (reader["Authors"]) is DBNull
                                                            ? new List<Author>()
                                                            : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Authors = authorjson;
                                    var Country = (string)reader["Country"];
                                    var RegistrationNumber = (string)reader["RegistrationNumber"];
                                    var ApplicationDate = (DateTime)(reader["ApplicationDate"]);
                                    var PublicationDate = (DateTime)reader["PublicationDate"];
                                    var Title = (string)reader["Title"];
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                    var YearOfPublishing = (int)reader["YearOfPublishing"];
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
                                    ApplicationDate = (DateTime)(reader["ApplicationDate"]),
                                    PublicationDate = (DateTime)reader["PublicationDate"],
                                    Title = (string)reader["Title"],
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"],
                                    YearOfPublishing = (int)reader["YearOfPublishing"]
                                };

                                break;
                            }

                        default:
                            break;
                    }
                }
            }
        }

        public IEnumerable<AbstractLibraryItem> SortByYearDesc()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetSortedLibraryItemsByYearDesc";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string type = (string)(reader["LibraryType"]);

                    switch (type)
                    {
                        case "Book":
                            {
                                List<Author> authorjson = (reader["Authors"]) is DBNull
                                ? new List<Author>()
                                : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Authors = authorjson;
                                    var City = (string)reader["City"];
                                    var PublishingCompany = (string)reader["PublishingCompany"];
                                    var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                    var Isbn = reader["ISBN"] is DBNull ? null : (string)reader["ISBN"];
                                    var Title = (string)reader["Title"];
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                }
                                catch (Exception e)
                                {
                                    throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                                }
                                yield return new Book
                                {
                                    Id = (int)(reader["Id"]),
                                    Authors = authorjson,
                                    City = (string)reader["City"],
                                    PublishingCompany = (string)reader["PublishingCompany"],
                                    YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                    Isbn = (string)reader["ISBN"],
                                    Title = (string)reader["Title"],
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"]
                                };

                                break;
                            }

                        case "Issue":
                            {
                                List<Newspaper> issue = (reader["Newspaper"]) is DBNull
                                ? new List<Newspaper>()
                                : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Newspaper = issue[0];
                                    var YearOfPublishing = (int)(reader["YearOfPublishing"]);
                                    var CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"];
                                    var DateOfPublishing = (DateTime)(reader["DateOfPublishing"]);
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                }
                                catch (Exception e)
                                {
                                    throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                                }
                                yield return new Issue
                                {
                                    Id = (int)(reader["Id"]),
                                    Newspaper = issue[0],
                                    YearOfPublishing = (int)(reader["YearOfPublishing"]),
                                    CountOfPublishing = reader["CountOfPublishing"] is DBNull ? null : (int?)reader["CountOfPublishing"],
                                    DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"],
                                };

                                break;
                            }

                        case "Patent":
                            {
                                List<Author> authorjson = (reader["Authors"]) is DBNull
                                                            ? new List<Author>()
                                                            : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                                try
                                {
                                    var Id = (int)(reader["Id"]);
                                    var Authors = authorjson;
                                    var Country = (string)reader["Country"];
                                    var RegistrationNumber = (string)reader["RegistrationNumber"];
                                    var ApplicationDate = (DateTime)(reader["ApplicationDate"]);
                                    var PublicationDate = (DateTime)reader["PublicationDate"];
                                    var Title = (string)reader["Title"];
                                    var PagesCount = (int)(reader["PagesCount"]);
                                    var Commentary = (string)reader["Commentary"];
                                    var YearOfPublishing = (int)reader["YearOfPublishing"];
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
                                    ApplicationDate = (DateTime)(reader["ApplicationDate"]),
                                    PublicationDate = (DateTime)reader["PublicationDate"],
                                    Title = (string)reader["Title"],
                                    PagesCount = (int)(reader["PagesCount"]),
                                    Commentary = (string)reader["Commentary"],
                                    YearOfPublishing = (int)reader["YearOfPublishing"]
                                };

                                break;
                            }

                        default:
                            break;
                    }
                }
            }
        }
    }
}
