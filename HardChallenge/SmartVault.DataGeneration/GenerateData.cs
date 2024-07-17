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
        public void Generate(string databaseConnectionString, int numberOfUsers, int numberOfDocumentsPerUser, string fileName)
        {
            using (var connection = new SQLiteConnection(databaseConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    CreateEntityTables(connection);

                    var userInsertCommand = connection.CreateCommand();
                    userInsertCommand.CommandText = @"
                        INSERT INTO User (Id, FirstName, LastName, DateOfBirth, AccountId, Username, Password)
                        VALUES
                            (
                                @id,
                                @fName,
                                @lName,
                                @dateOfBirth,
                                @accountId,
                                @username,
                                'e10adc3949ba59abbe56e057f20f883e'
                            )";

                    var idParam = AddParam("id", userInsertCommand);
                    var firstNameParam = AddParam("fName", userInsertCommand);
                    var lastParam = AddParam("lName", userInsertCommand);
                    var dateOfBirthParam = AddParam("dateOfBirth", userInsertCommand);
                    var accountFKParam = AddParam("accountId", userInsertCommand);
                    var usernameParam = AddParam("username", userInsertCommand);


                    var accountInsertCommand = connection.CreateCommand();
                    accountInsertCommand.CommandText = @"INSERT INTO Account (Id, Name) VALUES(@id, @name)";
                    var accountIdParam = AddParam("id", accountInsertCommand);
                    var accountNameParam = AddParam("name", accountInsertCommand);

                    var documentNumber = 0;
                    for (int i = 0; i < numberOfUsers; i++)
                    {
                        var randomDayIterator = RandomDay().GetEnumerator();
                        randomDayIterator.MoveNext();

                        idParam.Value = i;
                        firstNameParam.Value = $"FName{i}";
                        lastParam.Value = $"LName{i}";
                        dateOfBirthParam.Value = randomDayIterator.Current.ToString("yyyy-MM-dd");
                        accountFKParam.Value = i;
                        usernameParam.Value = $"UserName-{i}";

                        userInsertCommand.ExecuteNonQuery();

                        accountIdParam.Value = i;
                        accountNameParam.Value = $"Account{i}";

                        accountInsertCommand.ExecuteNonQuery();

                        for (int d = 0; d < numberOfDocumentsPerUser; d++, documentNumber++)
                        {
                            var documentPath = new FileInfo(fileName).FullName;
                            connection.Execute($"INSERT INTO Document (Id, Name, FilePath, Length, AccountId) VALUES('{documentNumber}','Document{i}-{d}.txt','{documentPath}','{new FileInfo(documentPath).Length}','{i}')");
                        }
                    }

                    PrintItems(connection);

                    transaction.Commit();
                }
            }
        }

        private SQLiteParameter AddParam(string parameterName, SQLiteCommand userInsertCommand)
        {
            var param = userInsertCommand.CreateParameter();
            param.ParameterName = parameterName;
            userInsertCommand.Parameters.Add(param);
            return param;
        }

        private void CreateEntityTables(SQLiteConnection connection)
        {
            var files = Directory.GetFiles(@"..\..\..\..\BusinessObjectSchema");
            for (int i = 0; i <= 2; i++)
            {
                var serializer = new XmlSerializer(typeof(BusinessObject));
                var businessObject = serializer.Deserialize(new StreamReader(files[i])) as BusinessObject;
                connection.Execute(businessObject?.Script);
            }
        }

        private void PrintItems(SQLiteConnection connection)
        {
            var accountData = connection.Query("SELECT COUNT(*) FROM Account;");
            Console.WriteLine($"AccountCount: {JsonConvert.SerializeObject(accountData)}");
            var documentData = connection.Query("SELECT COUNT(*) FROM Document;");
            Console.WriteLine($"DocumentCount: {JsonConvert.SerializeObject(documentData)}");
            var userData = connection.Query("SELECT COUNT(*) FROM User;");
            Console.WriteLine($"UserCount: {JsonConvert.SerializeObject(userData)}");
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
