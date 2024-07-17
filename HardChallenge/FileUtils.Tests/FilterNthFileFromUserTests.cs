using SmartVault.DataGeneration;

namespace FileUtils.Tests
{
    public class FilterNthFileFromUserTests
    {
        private string TestDatabaseFile;
        private string ConnectionString;

        public FilterNthFileFromUserTests()
        {
            TestDatabaseFile = Path.Combine(Path.GetTempPath(), $"unit-test-database-{Guid.NewGuid().ToString()}.sqlite");
            ConnectionString = $"Data Source={TestDatabaseFile};";
            if (File.Exists(TestDatabaseFile))
                File.Delete(TestDatabaseFile);
        }

        [Theory]
        [InlineData(3, new[] { 2, 5, 8 })]
        [InlineData(2, new [] {1, 3, 5, 7, 9})]
        public void Filter_ReturnsCorreclyTheNthDocuments(int getFilesMultiplesOf, int[] expectedOutput)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            new GenerateData().Generate(ConnectionString, 1, 11, fileName);

            var userId = 0;
            List<int> recordIds = new FilterNthFileFromUser(userId, getFilesMultiplesOf).FilterDocumentRowIds(ConnectionString);

            Assert.Equal(expectedOutput, recordIds.ToArray());
        }
    }
}