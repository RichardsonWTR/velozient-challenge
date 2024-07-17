using FileUtils;
using Microsoft.Extensions.Configuration;
using SmartVault.DataGeneration;
using SmartVault.Program.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace SmartVault.Program
{
    partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                return;
            }

            // FIXME: build database path dynamically by getting the config from the data generation project, then remove it from the cli args.
            string databasePath = args[1];
            WriteEveryThirdFileToFile(args[0], databasePath);
            GetAllFileSizes(databasePath);
        }

        private static void GetAllFileSizes(string databasePath)
        {
            var configuration = new GetConfig().config;
            var databaseConnectionString = string.Format(configuration?["ConnectionStrings:DefaultConnection"] ?? "", databasePath);

            var fileNamesAndSizes = new GetAllFileSizes().GetAllFileSizesFromDatabase(databaseConnectionString);

            foreach (var file in fileNamesAndSizes) {
                Console.WriteLine($"{file.Key} has the size of {file.Value}");
            }
        }


        private static void WriteEveryThirdFileToFile(string accountId, string databasePath, string outputFilePath = "output-file.txt")
        {
            var configuration = new GetConfig().config;

            var databaseConnectionString = string.Format(configuration?["ConnectionStrings:DefaultConnection"] ?? "", databasePath);

            List<string> recordFilePaths = new FilterNthFileFromUser(int.Parse(accountId), 3).FilterDocumentRowPaths(databaseConnectionString);

            foreach (var recordFilePath in recordFilePaths) {
                if (!(new CheckIfFileHasText(recordFilePath).HasText("Smith Property")))
                    continue;

                string content = File.ReadAllText(recordFilePath);
                File.AppendAllText(outputFilePath, content);
            }
        }
    }
}