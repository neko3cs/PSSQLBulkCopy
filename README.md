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

内部的には以下のように扱われます。参考資料として載せておきます。

|SQL Server       |C#             |
|--               |--             |
|BigInt           |long           |
|Binary           |byte[]         |
|Bit              |bool           |
|Char             |string         |
|DateTime         |DateTime       |
|Decimal          |decimal        |
|Float            |double         |
|Image            |byte[]         |
|Int              |int            |
|Money            |decimal        |
|NChar            |string         |
|NText            |string         |
|NVarChar         |string         |
|Real             |float          |
|UniqueIdentifier |Guid           |
|SmallDateTime    |DateTime       |
|SmallInt         |short          |
|SmallMoney       |decimal        |
|Text             |string         |
|Timestamp        |byte[]         |
|TinyInt          |byte           |
|VarBinary        |byte[]         |
|VarChar          |string         |
|Variant          |object         |
|Xml              |XmlElement     |
|Udt              |-              |
|Structured       |-              |
|Date             |DateTime       |
|Time             |TimeSpan       |
|DateTime2        |DateTime       |
|DateTimeOffset   |DateTimeOffset |

## 参考文献

- [SQL Server データ型のマッピング - ADO.NET | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/framework/data/adonet/sql-server-data-type-mappings)
- [SQL と CLR の型マッピング - ADO.NET | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping?source=recommendations)
- [SqlDbType 列挙型 (System.Data) | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/api/system.data.sqldbtype?view=net-8.0)
