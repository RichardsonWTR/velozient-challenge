
namespace FileUtils.Tests
{
    public class CheckIfFileHasTextTests
    {
        [Theory]
        [InlineData("some")]
        [InlineData("content")]
        [InlineData("me")]
        [InlineData("me con")]
        public void HasText_ReturnsTrueIfHasText(string textToSearch)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            var result = new CheckIfFileHasText(fileName).HasText(textToSearch);

            Assert.True(result);
        }

        [Theory]
        [InlineData("another")]
        [InlineData("--content")]
        public void HasText_ReturnsFalseIfDoesNotHaveText(string textToSearch)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "some content");

            var result = new CheckIfFileHasText(fileName).HasText(textToSearch);

            Assert.False(result);
        }


        [Theory(Skip = "To be implemented")]
        [InlineData("some content")]
        [InlineData("some")]
        public void HasText_ReturnsTrueIfHasTextSplittedIntoMultipleLines(string textToSearch)
        {
            string fileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            File.WriteAllText(fileName, "so\nme\ncon\ntent");

            var result = new CheckIfFileHasText(fileName).HasText(textToSearch);

            Assert.True(result);
        }
    }
}
