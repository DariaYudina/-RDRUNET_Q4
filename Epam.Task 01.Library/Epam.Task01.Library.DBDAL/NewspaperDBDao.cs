using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Epam.Task01.Library.AbstractDAL.INewspaper;
using Epam.Task01.Library.Entity;

namespace Epam.Task01.Library.DBDAL
{
    public class NewspaperDBDao : INewspaperDao
    {
        private readonly string _connectionString;

        public NewspaperDBDao(SqlConnectionConfig sqlConnectionConfig)
        {
            _connectionString = sqlConnectionConfig.ConnectionString;
        }

        public int AddNewspaper(Newspaper newspaper)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "AddNewspaper";
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
                        Value = newspaper.Title,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter city = new SqlParameter
                    {
                        ParameterName = "@City",
                        Value = newspaper.City,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter publishingCompany = new SqlParameter
                    {
                        ParameterName = "@PublishingCompany",
                        Value = newspaper.PublishingCompany,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };
                    SqlParameter issn = new SqlParameter
                    {
                        ParameterName = "@ISSN",
                        Value = newspaper.Issn,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(id);
                    command.Parameters.Add(title);
                    command.Parameters.Add(city);
                    command.Parameters.Add(publishingCompany);
                    command.Parameters.Add(issn);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)id.Value;
                }
            }
            catch (System.Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public Newspaper GetNewspaperById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "GetNewspaperById";
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
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

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
            catch (System.Exception e)
            {
                throw new AppLayerException(e.Message) { AppLayer = "Dal" };
            }
        }

        public IEnumerable<Newspaper> GetNewspapers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "GetNewspapers";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        var Id = (int)(reader["Id"]);
                        var Title = (string)reader["Title"];
                        var City = (string)reader["City"];
                        var PublishingCompany = (string)reader["PublishingCompany"];
                        var Issn = (string)reader["ISSN"];
                    }
                    catch (Exception e)
                    {
                        throw new AppLayerException(e.Message) { AppLayer = "Dal" };
                    }
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
