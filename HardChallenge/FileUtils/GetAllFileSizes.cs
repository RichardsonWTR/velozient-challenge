using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils
{
    public class GetAllFileSizes
    {
        public Dictionary<string, long> GetAllFileSizesFromDatabase(string databaseConnectionString)
        {
            Dictionary<string, long> sizes = new Dictionary<string, long>(){ };
            var paths = GetDistinctFilePaths(databaseConnectionString);

            foreach (var filePath in paths)
            {
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists) {
                    Console.WriteLine("skipping file\n");
                    continue;
                }
                sizes.Add(fi.Name, fi.Length);
            }

            return sizes;
        }


        private List<string> GetDistinctFilePaths(string databaseConnectionString)
        {
            List<string> paths;
            using (var connection = new SQLiteConnection(databaseConnectionString))
            {
                var command = $"select distinct(FilePath) from Document;";

                paths = connection.Query<string>(command).ToList();
            }

            return paths;
        }
    }
}
