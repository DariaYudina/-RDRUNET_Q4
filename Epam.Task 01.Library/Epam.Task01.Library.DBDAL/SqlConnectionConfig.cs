namespace Epam.Task01.Library.DBDAL
{
    public class SqlConnectionConfig
    {
        public SqlConnectionConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
