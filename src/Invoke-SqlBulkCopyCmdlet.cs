using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Management.Automation;

namespace PSSQLBulkCopy
{
    [Cmdlet(VerbsLifecycle.Invoke, "SqlBulkCopy")]
    [OutputType(typeof(int))]
    public class InvokeSqlBulkCopyCmdlet : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0
        )]
        public string ServerInstance { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 1
        )]
        public string Database { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 2
        )]
        public string Username { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 3
        )]
        public string Password { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 4
        )]
        public string TableName { get; set; }
        [Parameter(
            Mandatory = true,
            Position = 5
        )]
        public string CsvFilePath { get; set; }
        [Parameter(
            Position = 6
        )]
        public int? ConnectionTimeout { get; set; }

        private string _connectionString;
        private IEnumerable<string> _csvLines;

        protected override void BeginProcessing()
        {
            _connectionString = new SqlConnectionStringBuilder
            {
                DataSource = ServerInstance,
                InitialCatalog = Database,
                UserID = Username,
                Password = Password,
                ConnectTimeout = ConnectionTimeout ?? 15

            }.ConnectionString;

            _csvLines = File.ReadAllLines(CsvFilePath);
        }

        protected override void ProcessRecord()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = TableName;
                        bulkCopy.WriteToServer(_csvLines.ToDataTableFromCsvLines());
                    }
                    transaction.Commit();
                }
            }
        }

        protected override void EndProcessing()
        {
        }
    }
}