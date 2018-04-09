using BusinessSolutions.Common.Core.Events;
using NUnit.Framework;
using Sanabel.Security.Infra;
using Sanabel.Security.Infra.Migrations;
using Sanabel.Volunteers.IntegrationTest.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sanabel.Volunteers.IntegrationTest
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

        [OneTimeSetUp]
        public void IntiateDependancyResolver()
        {

            DomainEvents.Initiate(new DependancyResolver());
        }

        [OneTimeTearDown]
        public void TearDownDatabase()
        {
            DestroyDatabase();
        }

        private void CreateDatabase()
        {
            ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [SanabelVolunteerTestDB]
                ON (NAME = 'SanabelVolunteerTestDB',
                FILENAME = '{Filename}')");

            var commonMigration = new MigrateDatabaseToLatestVersion<CommonSettings.DAL.CommonSettingDataContext
                , CommonSettings.DAL.Migrations.CommonSettingsDbMigrationsConfiguration>("VolunteersConnectionString");
            commonMigration.InitializeDatabase(new CommonSettings.DAL.CommonSettingDataContext());

            var Secuirtymigration = new MigrateDatabaseToLatestVersion<SecurityContext
                , SecurityContextConfiguration>("SecurityConnectionString");
            Secuirtymigration.InitializeDatabase(new SecurityContext());

            var migration = new MigrateDatabaseToLatestVersion<Sanabel.Volunteers.Infra.VolunteersDbCotext
                , Sanabel.Volunteers.Infra.Migrations.Configuration>("VolunteersConnectionString");
            migration.InitializeDatabase(new Sanabel.Volunteers.Infra.VolunteersDbCotext());
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
                WHERE [database_id] = DB_ID('SanabelVolunteerTestDB')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [SanabelVolunteerTestDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'SanabelVolunteerTestDB'");

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
            "SanabelVolunteerTestDB.mdf");

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
