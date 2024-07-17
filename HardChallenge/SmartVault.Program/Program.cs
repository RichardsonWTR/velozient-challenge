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

            WriteEveryThirdFileToFile(args[0], args[1]);
            GetAllFileSizes();
        }

        private static void GetAllFileSizes()
        {
            // TODO: Implement functionality
        }


        // FIXME: build database path dynamically, remove it from the cli args.
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