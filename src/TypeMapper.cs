using System;
using System.Collections.Generic;
using System.Xml;

namespace PSSQLBulkCopy
{
  public static class TypeMapper
  {
    private static readonly Dictionary<string, Type> _typeMap = new Dictionary<string, Type>()
    {
      { "bigint"           , typeof(long) },
      { "binary"           , typeof(byte[]) },
      { "bit"              , typeof(bool) },
      { "char"             , typeof(string) },
      { "datetime"         , typeof(DateTime) },
      { "decimal"          , typeof(decimal) },
      { "float"            , typeof(double) },
      { "image"            , typeof(byte[]) },
      { "int"              , typeof(int) },
      { "money"            , typeof(decimal) },
      { "nchar"            , typeof(string) },
      { "ntext"            , typeof(string) },
      { "nvarchar"         , typeof(string) },
      { "real"             , typeof(float) },
      { "uniqueidentifier" , typeof(Guid) },
      { "smalldatetime"    , typeof(DateTime) },
      { "smallint"         , typeof(short) },
      { "smallmoney"       , typeof(decimal) },
      { "text"             , typeof(string) },
      { "timestamp"        , typeof(byte[]) },
      { "tinyint"          , typeof(byte) },
      { "varbinary"        , typeof(byte[]) },
      { "varchar"          , typeof(string) },
      { "variant"          , typeof(object) },
      { "xml"              , typeof(XmlElement) },
      { "date"             , typeof(DateTime) },
      { "time"             , typeof(TimeSpan) },
      { "datetime2"        , typeof(DateTime) },
      { "datetimeoffset"   , typeof(DateTimeOffset) },
    };

    public static Type GetType(string typeName) => _typeMap[typeName.Trim()];
  }
}