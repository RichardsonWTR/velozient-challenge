using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils
{
    public class FilterNthFileFromUser
    {
        private readonly int _userId;
        private readonly int _nthQueriedDocument;

        public FilterNthFileFromUser(int userId, int nthQueriedDocument = 3)
        {
            _userId = userId;
            _nthQueriedDocument = nthQueriedDocument;
        }

        public List<int> FilterDocumentRowIds(string databaseConnectionString)
        {
            List<int> ids;
            using (var connection = new SQLiteConnection(databaseConnectionString))
            {
                var command = $"select id from (select *, row_number() over(partition by AccountId order by id) number_of_inserted_document_of_user from Document) where number_of_inserted_document_of_user % {_nthQueriedDocument} = 0 and AccountId = {_userId}";

                ids = connection.Query<int>(command).ToList();
            }

            return ids;
        }

        public List<string> FilterDocumentRowPaths(string databaseConnectionString)
        {
            List<string> paths;
            using (var connection = new SQLiteConnection(databaseConnectionString))
            {
                var command = $"select FilePath from (select *, row_number() over(partition by AccountId order by id) number_of_inserted_document_of_user from Document) where number_of_inserted_document_of_user % {_nthQueriedDocument} = 0 and AccountId = {_userId}";

                paths = connection.Query<string>(command).ToList();
            }

            return paths;
        }
    }
}
