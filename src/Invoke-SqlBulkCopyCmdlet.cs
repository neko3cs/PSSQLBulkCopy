using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace PSSQLBulkCopy
{
  /// <summary>
  /// .NETのSQLBulkCopyを実行します。
  /// テーブル名とCSVファイルパスを指定することで任意のテーブルにCSVの値を登録することが出来ます。
  /// </summary>
  [Cmdlet(VerbsLifecycle.Invoke, "SqlBulkCopy")]
  [OutputType(typeof(void))]
  public class InvokeSqlBulkCopyCmdlet : PSCmdlet
  {
    [Parameter(
        Mandatory = true,
        Position = 0,
        HelpMessage = "SQL Serverデータベースエンジンのインスタンス名を指定します。" +
                      "デフォルトの値はコマンドレットが実行される端末のコンピューター名になります。" +
                      "名前付きインスタンスの場合は 'ComputerName\\InstanceName' のように指定します。")]
    public string ServerInstance { get; set; } = Environment.MachineName;
    [Parameter(
        Mandatory = true,
        Position = 1,
        HelpMessage = "データベース名を指定します。" +
                      "このコマンドレットはServerInstanceパラメーターで指定されたインスタンスのこのデータベースに接続します。")]
    public string Database { get; set; }
    [Parameter(
        Mandatory = true,
        Position = 2,
        HelpMessage = "データベースエンジンのインスタンスにSQL Server認証接続をおこなうためのログインIDを指定します。")]
    public string Username { get; set; }
    [Parameter(
        Mandatory = true,
        Position = 3,
        HelpMessage = "Usernameパラメーターで指定したSQL Server認証ログインIDのパスワードを指定します。" +
                      "パスワードは大文字と小文字を区別します。")]
    public string Password { get; set; }
    [Parameter(
        Mandatory = true,
        Position = 4,
        HelpMessage = "バルクコピー先のテーブル名を指定します。")]
    public string TableName { get; set; }
    [Parameter(
        Mandatory = true,
        Position = 5,
        HelpMessage = "バルクコピーで登録したいデータが記述されたCSVファイルを指定します。" +
                      "CSVファイルの文字エンコードはUTF-8とします。")]
    public string CsvFilePath { get; set; }
    [Parameter(
        Position = 6,
        HelpMessage = "このコマンドレットがデータベースエンジンのインスタンスに正常に接続できない場合にタイムアウトする秒数を指定します。" +
                      "タイムアウト値は 0 ~ 65534 の整数値でなければなりません。" +
                      "0 を指定すると接続試行はタイムアウトしません。")]
    public int ConnectionTimeout { get; set; } = 15;

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
        ConnectTimeout = ConnectionTimeout

      }.ConnectionString;

      _csvLines = File.ReadAllLines(CsvFilePath, Encoding.UTF8);
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