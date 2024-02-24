# PSSQLBulkCopy

SQLBulkCopyをラップした、PowerShellコマンドレットです。

## 使用方法

サポートされたフォーマットにしたがって用意したCSVをコマンドレットに渡します。

コマンドレットのパラメータは `help` コマンドを使用して確認してください。

サポートするCSVのフォーマットは以下の通りになります。

```csv
Column1Name,Column2Name,Column3Name...
Column1Type,Column2Type,Column3Type...
1stRowValueOfColumn1,1stRowValueOfColumn2,1stRowValueOfColumn3...
2ndRowValueOfColumn1,2ndRowValueOfColumn2,2ndRowValueOfColumn3...
...
```

- 1行目: カラム名
- 2行目: SQL Server型名
- 3行目以降: 値

## 型一覧表

以下はSQL Serverデータ型とC#データ型のマッピングになります。

内部的には以下の型ように扱われます。参考資料として載せておきます。

|SQL Server       |C#             |
|--               |--             |
|bigint           |long           |
|binary           |byte[]         |
|bit              |bool           |
|char             |string         |
|datetime         |DateTime       |
|decimal          |decimal        |
|float            |double         |
|image            |byte[]         |
|int              |int            |
|money            |decimal        |
|nchar            |string         |
|ntext            |string         |
|nvarchar         |string         |
|real             |float          |
|uniqueidentifier |Guid           |
|smalldatetime    |DateTime       |
|smallint         |short          |
|smallmoney       |decimal        |
|text             |string         |
|timestamp        |byte[]         |
|tinyint          |byte           |
|varbinary        |byte[]         |
|varchar          |string         |
|variant          |object         |
|xml              |XmlElement     |
|udt              |-              |
|structured       |-              |
|date             |DateTime       |
|time             |TimeSpan       |
|datetime2        |DateTime       |
|datetimeoffset   |DateTimeOffset |

- udtとstructuredはSQL Serverの特殊な型のため、未対応です
- byte[], Guid, object, XmlElement, DateTimeOffsetに関しては動作未確認です
  - TODO: いずれテストする

## 参考文献

- [SQL Server データ型のマッピング - ADO.NET | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/framework/data/adonet/sql-server-data-type-mappings)
- [SQL と CLR の型マッピング - ADO.NET | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping?source=recommendations)
- [SqlDbType 列挙型 (System.Data) | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/api/system.data.sqldbtype?view=net-8.0)
