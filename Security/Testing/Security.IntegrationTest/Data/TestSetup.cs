using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Security;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace Security.IntegrationTest
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void SetUpDatabase()
        {
            DestroyDatabase();
            CreateDatabase();
        }

        [OneTimeTearDown]
        public void TearDownDatabase()
        {
            DestroyDatabase();
        }

        private void CreateDatabase()
        {
            ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [SanabelSecurityTestDB]
                ON (NAME = 'SanabelSecurityTestDB',
                FILENAME = '{Filename}')");

            var commonMigration = new MigrateDatabaseToLatestVersion<CommonSettings.DAL.CommonSettingDataContext
                , CommonSettings.DAL.Migrations.CommonSettingsDbMigrationsConfiguration>("CommonSettingConnectionString");
            commonMigration.InitializeDatabase(new CommonSettings.DAL.CommonSettingDataContext());

            var migration = new MigrateDatabaseToLatestVersion<Security.DataAccessLayer.SecurityContext
                , Security.DataAccessLayer.Migrations.Configuration>("SecurityConnectionString");
            migration.InitializeDatabase(new DataAccessLayer.SecurityContext());
        }

        private static List<T> ExecuteSqlQuery<T>(
            SqlConnectionStringBuilder connectionStringBuilder,
            string queryText,
            Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }

        private void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('SanabelSecurityTestDB')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [SanabelSecurityTestDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'SanabelSecurityTestDB'");

                fileNames.ForEach(File.Delete);
            }
        }

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };

        private static string Filename => Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "SanabelSecurityDBTest.mdf");




        private static void ExecuteSqlCommand(
            SqlConnectionStringBuilder connectionStringBuilder,
            string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
