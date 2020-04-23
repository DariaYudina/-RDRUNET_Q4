using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task01.Library.AbstractDAL;
using Epam.Task01.Library.Entity;
using Newtonsoft.Json;

namespace Epam.Task01.Library.DBDAL
{
    public class IssueDBDao : IIssueDao
    {
        private readonly string _connectionString;

        public IssueDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public int AddIssue(Issue issue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "AddIssue";
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
                        Value = issue.Newspaper.Title,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter pagesCount = new SqlParameter
                    {
                        ParameterName = "@PagesCount",
                        Value = issue.PagesCount,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter commentary = new SqlParameter
                    {
                        ParameterName = "@Commentary",
                        Value = issue.Commentary,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter libraryType = new SqlParameter
                    {
                        ParameterName = "@LibraryType",
                        Value = issue.GetType().Name,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter newspaperId = new SqlParameter
                    {
                        ParameterName = "@Newspaper_Id",
                        Value = issue.Newspaper.Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter countOfPublishing = new SqlParameter
                    {
                        ParameterName = "@CountOfPublishing",
                        Value = issue.CountOfPublishing,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter dateOfPublishing = new SqlParameter
                    {
                        ParameterName = "@DateOfPublishing",
                        Value = issue.DateOfPublishing,
                        SqlDbType = SqlDbType.DateTime,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(title);
                    command.Parameters.Add(pagesCount);
                    command.Parameters.Add(commentary);
                    command.Parameters.Add(libraryType);
                    command.Parameters.Add(newspaperId);
                    command.Parameters.Add(countOfPublishing);
                    command.Parameters.Add(dateOfPublishing);
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

        public int EditIssue(Issue issue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "UpdateIssue";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter id = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = issue.Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter title = new SqlParameter
                    {
                        ParameterName = "@Title",
                        Value = issue.Newspaper.Title,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter pagesCount = new SqlParameter
                    {
                        ParameterName = "@PagesCount",
                        Value = issue.PagesCount,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter commentary = new SqlParameter
                    {
                        ParameterName = "@Commentary",
                        Value = issue.Commentary,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter libraryType = new SqlParameter
                    {
                        ParameterName = "@LibraryType",
                        Value = issue.GetType().Name,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter newspaperId = new SqlParameter
                    {
                        ParameterName = "@Newspaper_Id",
                        Value = issue.Newspaper.Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter countOfPublishing = new SqlParameter
                    {
                        ParameterName = "@CountOfPublishing",
                        Value = issue.CountOfPublishing,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter dateOfPublishing = new SqlParameter
                    {
                        ParameterName = "@DateOfPublishing",
                        Value = issue.DateOfPublishing,
                        SqlDbType = SqlDbType.DateTime,
                        Direction = ParameterDirection.Input
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
                    command.Parameters.Add(newspaperId);
                    command.Parameters.Add(countOfPublishing);
                    command.Parameters.Add(dateOfPublishing);
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

        public IEnumerable<Issue> GetIssues()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetIssues";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
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
                        var CountOfPublishing = (int?)(reader["CountOfPublishing"]);
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
                        CountOfPublishing = (int?)(reader["CountOfPublishing"]),
                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }

        public IEnumerable<Issue> GetIssuesByNewspaperId(int newspaperId, int currentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "GetIssuesByNewspaperId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter nid = new SqlParameter
                {
                    ParameterName = "@newspaperId",
                    Value = newspaperId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                SqlParameter cid = new SqlParameter
                {
                    ParameterName = "@currentIssueId",
                    Value = currentId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(nid);
                command.Parameters.Add(cid);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
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
                        var CountOfPublishing = (int?)(reader["CountOfPublishing"]);
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
                        CountOfPublishing = (int?)(reader["CountOfPublishing"]),
                        DateOfPublishing = (DateTime)(reader["DateOfPublishing"]),
                        PagesCount = (int)(reader["PagesCount"]),
                        Commentary = (string)reader["Commentary"]
                    };
                }
            }
        }
    }
}
