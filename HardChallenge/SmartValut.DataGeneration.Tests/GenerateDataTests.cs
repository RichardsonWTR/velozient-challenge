using SmartVault.DataGeneration;
using Dapper;

using System.Data.SQLite;

namespace SmartValut.DataGeneration.Tests
{
    public class GenerateDataTests
    {
        private string TestDatabaseFile;
        private string ConnectionString;

        public GenerateDataTests()
        {
            TestDatabaseFile = Path.Combine(Path.GetTempPath(), $"unit-test-database-{Guid.NewGuid().ToString()}.sqlite");
            ConnectionString = $"Data Source={TestDatabaseFile};";
            if (File.Exists(TestDatabaseFile))
                File.Delete(TestDatabaseFile);
        }

        // TODO: re-implement the proper cleanup of the temporary database file

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Generate_CreatesTheCorrectNumberOfUsers(int quantityOfUsers)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

           new GenerateData().Generate(ConnectionString, quantityOfUsers, 1, fileName);

           using (var connection = new SQLiteConnection(ConnectionString))
           {
                var qtyOfUsers = Convert.ToInt32(connection.ExecuteScalar("SELECT COUNT(*) FROM User;"));
                Assert.Equal(quantityOfUsers, qtyOfUsers);
           }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Generate_CreatesTheCorrectNumberOfAccounts(int quantityOfAccounts)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            new GenerateData().Generate(ConnectionString, quantityOfAccounts, 1, fileName);

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var qtyOfAccounts = Convert.ToInt32(connection.ExecuteScalar("SELECT COUNT(*) FROM Account;"));
                Assert.Equal(quantityOfAccounts, qtyOfAccounts);
            }
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 5)]
        public void Generate_CreatesTheCorrectNumberOfDocuments(int quantityOfAccounts, int quantityOfDocuments)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            new GenerateData().Generate(ConnectionString, quantityOfAccounts, quantityOfDocuments, fileName);

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var qtyOfDocuments = Convert.ToInt32(connection.ExecuteScalar("SELECT COUNT(*) FROM Document;"));
                Assert.Equal(quantityOfAccounts * quantityOfDocuments, qtyOfDocuments);
            }
        }

        [Fact]
        public void Generate_UserHasTheCreatedOnAttribute()
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            new GenerateData().Generate(ConnectionString, 1, 1, fileName);

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var createdAtValue = connection.ExecuteScalar("SELECT CreatedAt FROM User limit 1;");
                Assert.NotNull(createdAtValue);
            }
        }

        [Fact]
        public void Generate_AccountHasTheCreatedOnAttribute()
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            new GenerateData().Generate(ConnectionString, 1, 1, fileName);

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var createdAtValue = connection.ExecuteScalar("SELECT CreatedAt FROM Account limit 1;");
                Assert.NotNull(createdAtValue);
            }
        }
    }
}