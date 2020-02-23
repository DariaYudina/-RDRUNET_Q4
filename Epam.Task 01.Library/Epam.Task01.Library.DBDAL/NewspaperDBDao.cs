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
    public class NewspaperDBDao : INewspaperDao
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void AddNewspaper(Issue item)
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
                var NewspaperId = new SqlParameter
                {
                    ParameterName = "@Newspaper_Id",
                    Value = item.Newspaper.Id,
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input
                };
                var CountOfPublishing = new SqlParameter
                {
                    ParameterName = "@CountOfPublishing",
                    Value = item.Newspaper.Id,
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input
                };
                var DateOfPublishing = new SqlParameter
                {
                    ParameterName = "@DateOfPublishing",
                    Value = item.DateOfPublishing,
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input
                };
                var YearOfPublishing = new SqlParameter
                {
                    ParameterName = "@YearOfPublishing",
                    Value = item.YearOfPublishing,
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input
                };

                command.Parameters.Add(Id);
                command.Parameters.Add(Title);
                command.Parameters.Add(PagesCount);
                command.Parameters.Add(Commentary);
                command.Parameters.Add(LibraryType);
                command.Parameters.Add(NewspaperId);
                command.Parameters.Add(CountOfPublishing);
                command.Parameters.Add(DateOfPublishing);
                command.Parameters.Add(YearOfPublishing);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Issue> GetNewspaperItems()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "GetIssues";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var issue = (reader["Newspaper"]) is DBNull
                                              ? new List<Newspaper>()
                                              : JsonConvert.DeserializeObject<List<Newspaper>>((string)(reader["Newspaper"]));
                    yield return new Issue
                    {
                        Id = (int)(reader["Id"]),
                        Newspaper = issue[0],
                        YearOfPublishing = (int)(reader["YearOfPublishing"]),
                        CountOfPublishing = (int)(reader["CountOfPublishing"]),
                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }
    }
}
