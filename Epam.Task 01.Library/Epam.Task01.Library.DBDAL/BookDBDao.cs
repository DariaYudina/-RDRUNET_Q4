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
    public class BookDBDao : IBookDao
    {
        private readonly string _connectionString;

        public BookDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public int AddBook(Book item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "AddBook";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
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
                        IsNullable = true,
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
                    SqlParameter city = new SqlParameter
                    {
                        ParameterName = "@City",
                        Value = item.City,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };
                    SqlParameter publishingCompany = new SqlParameter
                    {
                        ParameterName = "@PublishingCompany",
                        Value = item.PublishingCompany,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };
                    SqlParameter isbn = new SqlParameter
                    {
                        ParameterName = "@ISBN",
                        Value = item.Isbn,
                        IsNullable = true,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };
                    SqlParameter yearOfPublishing = new SqlParameter
                    {
                        ParameterName = "@YearOfPublishing",
                        Value = item.YearOfPublishing,
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    var authorsId = from i in item.Authors
                                    select new { i.Id };
                    string json = JsonConvert.SerializeObject(authorsId);

                    SqlParameter listAuthorsId = new SqlParameter
                    {
                        ParameterName = "@listAuthorsId",
                        Value = json,
                        IsNullable = true,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(title);
                    command.Parameters.Add(pagesCount);
                    command.Parameters.Add(commentary);
                    command.Parameters.Add(libraryType);
                    command.Parameters.Add(city);
                    command.Parameters.Add(publishingCompany);
                    command.Parameters.Add(isbn);
                    command.Parameters.Add(yearOfPublishing);
                    command.Parameters.Add(listAuthorsId);
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

        public int EditBook(Book item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "UpdateBook";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
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
                        IsNullable = true,
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

                    SqlParameter city = new SqlParameter
                    {
                        ParameterName = "@City",
                        Value = item.City,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter publishingCompany = new SqlParameter
                    {
                        ParameterName = "@PublishingCompany",
                        Value = item.PublishingCompany,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter isbn = new SqlParameter
                    {
                        ParameterName = "@ISBN",
                        Value = item.Isbn,
                        IsNullable = true,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };
                    SqlParameter yearOfPublishing = new SqlParameter
                    {
                        ParameterName = "@YearOfPublishing",
                        Value = item.YearOfPublishing,
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    var authorsId = from i in item.Authors
                                    select new { i.Id };
                    string json = JsonConvert.SerializeObject(authorsId);

                    SqlParameter listAuthorsId = new SqlParameter
                    {
                        ParameterName = "@AuthorsId",
                        Value = json,
                        IsNullable = true,
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Input
                    };

                    SqlParameter cntUpdateRow = new SqlParameter
                    {
                        ParameterName = "@CntUpdateRow",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(title);
                    command.Parameters.Add(pagesCount);
                    command.Parameters.Add(commentary);
                    command.Parameters.Add(libraryType);
                    command.Parameters.Add(city);
                    command.Parameters.Add(publishingCompany);
                    command.Parameters.Add(isbn);
                    command.Parameters.Add(yearOfPublishing);
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

        public Book GetBookById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "GetBookById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter _id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };
                    command.Parameters.Add(_id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                    while (reader.Read())
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
                            Isbn = (string)reader["ISBN"],
                            Title = (string)reader["Title"],
                            PagesCount = (int)(reader["PagesCount"]),
                            Commentary = (string)reader["Commentary"]
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

        public IEnumerable<Book> GetBooks()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetBooks";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                        Commentary = reader["Commentary"] is DBNull ? null : (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<Book> GetBooksByPublishingCompany(string publishingCompany)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetBooksByPublishingCompanyStartsWithInputText";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter publishing_Company = new SqlParameter
                {
                    ParameterName = "@PublishingCompany",
                    Value = publishingCompany,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(publishing_Company);
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
                        Commentary = reader["Commentary"] is DBNull ? null : (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetBooksByAuthor";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = author.Id,
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
                        Commentary = reader["Commentary"] is DBNull ? null : (string)reader["Commentary"]
                    };
                }
            }
        }

        public int SoftDeleteBook(int idBook)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SoftDeleteBook";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = idBook,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter cntdeleterow = new SqlParameter
                    {
                        ParameterName = "@CntDeleteRow",
                        Value = 0,
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(cntdeleterow);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)cntdeleterow.Value;
                }
            }
            catch (Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }
    }
}
