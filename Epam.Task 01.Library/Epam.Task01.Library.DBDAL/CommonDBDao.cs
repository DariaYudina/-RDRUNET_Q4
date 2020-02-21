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
    public class CommonDBDao : ICommonDao
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void AddAbstractLibraryItem(AbstractLibraryItem item)
        {
            throw new NotImplementedException();
        }

        //public bool DeleteIssueItemById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public bool DeleteLibraryItemById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AbstractLibraryItem> GetAllAbstractLibraryItems()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetAllLibraryItems";
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

                reader.NextResult();
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

                reader.NextResult();
               
                while (reader.Read())
                {
                    //var issue = (reader["Newspaper"]) is DBNull
                    //                          ? new Issue()
                    //                          : JsonConvert.DeserializeObject<Issue>((string)(reader["Newspaper"]));
                    yield return new Newspaper
                    {
                        Id = (int)(reader["Id"]),
                        Issue = new Issue(),
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        CountOfPublishing = (int)(reader["CountOfPublishing"]),
                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<AbstractLibraryItem> GetLibraryItemsByTitle(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetLibraryItemsByTitle";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var Title = new SqlParameter
                {
                    ParameterName = "@Title",
                    Value = name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };
                command.Parameters.Add(Title);
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

                reader.NextResult();
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

                reader.NextResult();

                while (reader.Read())
                {
                    //var issue = (reader["Newspaper"]) is DBNull
                    //                          ? new Issue()
                    //                          : JsonConvert.DeserializeObject<Issue>((string)(reader["Newspaper"]));
                    yield return new Newspaper
                    {
                        Id = (int)(reader["Id"]),
                        Issue = new Issue(),
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        CountOfPublishing = (int)(reader["CountOfPublishing"]),
                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }

            }
        }

        public IEnumerable<IGrouping<int, AbstractLibraryItem>> GetLibraryItemsByYearOfPublishing()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AbstractLibraryItem> GetTwoTypesByAuthor<T, G>()
            where T : AbstractLibraryItem
            where G : AbstractLibraryItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetTypeByAuthor<T>() where T : AbstractLibraryItem
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AbstractLibraryItem> SortByYear()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AbstractLibraryItem> SortByYearDesc()
        {
            throw new NotImplementedException();
        }
    }
}
