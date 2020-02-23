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
    public class BookDBDao : IBookDao
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void AddBook(Book item)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddBook";
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
                var City = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = item.City,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                var PublishingCompany = new SqlParameter
                {
                    ParameterName = "@PublishingCompany",
                    Value = item.PublishingCompany,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                var ISBN = new SqlParameter
                {
                    ParameterName = "@ISBN",
                    Value = item.isbn,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Input
                };
                var YearOfPublishing = new SqlParameter
                {
                    ParameterName = "@YearOfPublishing",
                    Value = item.YearOfPublishing,
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input
                };

                var authorsId =  from i in item.Authors
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
                command.Parameters.Add(City);
                command.Parameters.Add(PublishingCompany);
                command.Parameters.Add(ISBN);
                command.Parameters.Add(YearOfPublishing);
                command.Parameters.Add(listAuthorsId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Book GetBookById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetBookById";
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
                        isbn = (string)reader["ISBN"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
                return null;
            }
        }

        public IEnumerable<Book> GetBookItems()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetBooks";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull 
                                               ? new List<Author>() 
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    yield return new Book
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson  ,
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        isbn = (string)reader["ISBN"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<IGrouping<string, Book>> GetBooksByPublishingCompany(string publishingCompany)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooksByPublishingCompany2(string publishingCompany)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetBooksByPublishingCompanyStartsWithInputText";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var PublishingCompany = new SqlParameter
                {
                    ParameterName = "@PublishingCompany",
                    Value = publishingCompany,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(PublishingCompany);
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    List<Author> authorjson = (reader["Authors"]) is DBNull
                                               ? new List<Author>()
                                               : JsonConvert.DeserializeObject<List<Author>>((string)(reader["Authors"]));
                    yield return new Book
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        isbn = (string)reader["ISBN"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<Book> GetBookByAuthor(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetBooksByAuthor";
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
                    yield return new Book
                    {
                        Id = (int)(reader["Id"]),
                        Authors = authorjson,
                        City = (string)reader["City"],
                        PublishingCompany = (string)reader["PublishingCompany"],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        isbn = (string)reader["ISBN"],
                        Title = (string)reader["Title"],
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }
    }
}
