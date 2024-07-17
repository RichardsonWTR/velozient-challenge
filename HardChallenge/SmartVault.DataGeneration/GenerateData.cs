using Dapper;
using Newtonsoft.Json;
using SmartVault.Library;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Xml.Serialization;

namespace SmartVault.DataGeneration
{
    public class GenerateData
    {
        public void Generate(string databaseConnectionString, int numberOfUsers, int numberOfDocumentsPerUser, string fileName) {
            using (var connection = new SQLiteConnection(databaseConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var files = Directory.GetFiles(@"..\..\..\..\BusinessObjectSchema");
                    for (int i = 0; i <= 2; i++)
                    {
                        var serializer = new XmlSerializer(typeof(BusinessObject));
                        var businessObject = serializer.Deserialize(new StreamReader(files[i])) as BusinessObject;
                        connection.Execute(businessObject?.Script);

                    }
                    var documentNumber = 0;
                    for (int i = 0; i < numberOfUsers; i++)
                    {
                        var randomDayIterator = RandomDay().GetEnumerator();
                        randomDayIterator.MoveNext();
                        connection.Execute($"INSERT INTO User (Id, FirstName, LastName, DateOfBirth, AccountId, Username, Password) VALUES('{i}','FName{i}','LName{i}','{randomDayIterator.Current.ToString("yyyy-MM-dd")}','{i}','UserName-{i}','e10adc3949ba59abbe56e057f20f883e')");
                        connection.Execute($"INSERT INTO Account (Id, Name) VALUES('{i}','Account{i}')");

                        for (int d = 0; d < numberOfDocumentsPerUser; d++, documentNumber++)
                        {
                            var documentPath = new FileInfo(fileName).FullName;
                            connection.Execute($"INSERT INTO Document (Id, Name, FilePath, Length, AccountId) VALUES('{documentNumber}','Document{i}-{d}.txt','{documentPath}','{new FileInfo(documentPath).Length}','{i}')");
                        }
                    }

                    var accountData = connection.Query("SELECT COUNT(*) FROM Account;");
                    Console.WriteLine($"AccountCount: {JsonConvert.SerializeObject(accountData)}");
                    var documentData = connection.Query("SELECT COUNT(*) FROM Document;");
                    Console.WriteLine($"DocumentCount: {JsonConvert.SerializeObject(documentData)}");
                    var userData = connection.Query("SELECT COUNT(*) FROM User;");
                    Console.WriteLine($"UserCount: {JsonConvert.SerializeObject(userData)}");

                    transaction.Commit();
                }
            }
            
        }

        static IEnumerable<DateTime> RandomDay()
        {
            DateTime start = new DateTime(1985, 1, 1);
            Random gen = new Random();
            int range = (DateTime.Today - start).Days;
            while (true)
                yield return start.AddDays(gen.Next(range));
        }
    }
}
