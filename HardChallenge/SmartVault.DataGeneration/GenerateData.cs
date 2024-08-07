﻿using Dapper;
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
        public void Generate(string databaseConnectionString, int numberOfUsers, int numberOfDocumentsPerUser, string fileName, bool generateMultipleTextFilesFromOriginal = true)
        {
            using (var connection = new SQLiteConnection(databaseConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    CreateEntityTables(connection);

                    var userInsertCommand = connection.CreateCommand();
                    userInsertCommand.CommandText = @"
                        INSERT INTO User (Id, FirstName, LastName, DateOfBirth, AccountId, Username, Password, CreatedAt)
                        VALUES
                            (
                                @id,
                                @fName,
                                @lName,
                                @dateOfBirth,
                                @accountId,
                                @username,
                                'e10adc3949ba59abbe56e057f20f883e',
                                @createdAt
                            )";

                    var idParam = AddParam("id", userInsertCommand);
                    var firstNameParam = AddParam("fName", userInsertCommand);
                    var lastParam = AddParam("lName", userInsertCommand);
                    var dateOfBirthParam = AddParam("dateOfBirth", userInsertCommand);
                    var accountFKParam = AddParam("accountId", userInsertCommand);
                    var usernameParam = AddParam("username", userInsertCommand);
                    var userCreatedAtParam = AddParam("createdAt", userInsertCommand);


                    var accountInsertCommand = connection.CreateCommand();
                    accountInsertCommand.CommandText = @"INSERT INTO Account (Id, Name, CreatedAt) VALUES(@id, @name, @createdAt)";
                    var accountIdParam = AddParam("id", accountInsertCommand);
                    var accountNameParam = AddParam("name", accountInsertCommand);
                    var accountCreatedAtParam = AddParam("createdAt", accountInsertCommand);

                    var documentInsertCommand = connection.CreateCommand();
                    documentInsertCommand.CommandText = @"
                        INSERT INTO Document (Id, Name, FilePath, Length, AccountId, CreatedAt) VALUES
                        (
                            @documentNumber,
                            @documentName,
                            @documentPath,
                            @documentSize,
                            @documentAccountId,
                            @createdAt
                        )";


                    var documentNumberParam = AddParam("documentNumber", documentInsertCommand);
                    var documentNameParam = AddParam("documentName", documentInsertCommand);
                    var documentPathParam = AddParam("documentPath", documentInsertCommand);
                    var documentSizeParam = AddParam("documentSize", documentInsertCommand);
                    var documentAccountFKParam = AddParam("documentAccountId", documentInsertCommand);
                    var documentCreatedAtParam = AddParam("createdAt", documentInsertCommand);

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
                        userCreatedAtParam.Value = randomDayIterator.Current.ToString("yyyy-MM-dd");

                        userInsertCommand.ExecuteNonQuery();

                        accountIdParam.Value = i;
                        accountNameParam.Value = $"Account{i}";
                        accountCreatedAtParam.Value = randomDayIterator.Current.ToString("yyyy-MM-dd");

                        accountInsertCommand.ExecuteNonQuery();

                        for (int d = 0; d < numberOfDocumentsPerUser; d++, documentNumber++)
                        {

                            var documentPath = new FileInfo(fileName).FullName;
                            if (generateMultipleTextFilesFromOriginal)
                            {
                                var fi = new FileInfo(fileName);
                                documentPath = Path.Combine(fi.Directory.FullName, $"Document{i}-{d}.txt");
                                fi.CopyTo(documentPath, true);
                            }

                            documentNumberParam.Value = documentNumber;
                            documentNameParam.Value = $"Document{i}-{d}.txt";
                            documentPathParam.Value = documentPath;
                            documentSizeParam.Value = new FileInfo(documentPath).Length;
                            documentAccountFKParam.Value = i;
                            documentCreatedAtParam.Value = randomDayIterator.Current.ToString("yyyy-MM-dd");
                            documentInsertCommand.ExecuteNonQuery();
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
            for (int i = 0; i < files.Length; i++)
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
