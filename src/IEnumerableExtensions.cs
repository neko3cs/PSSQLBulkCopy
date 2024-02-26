using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PSSQLBulkCopy
{
  public static class IEnumerableExtensions
  {
    public static DataTable ToDataTableFromCsvLines(this IEnumerable<string> items)
    {
      if (items.Count() < 3)
        throw new InvalidOperationException("カラム名行、型名行、値行からなる3行以上のCSVに対応しています。");

      var columnNames = items.First().Split(',');
      var types = items.Skip(1).First().Split(',');

      if (columnNames.Length != types.Length)
        throw new InvalidOperationException("カラム名行と型名行の項目数が異なっています。");

      var table = new DataTable();
      foreach (var i in Enumerable.Range(0, columnNames.Length))
      {
        table.Columns.Add(new DataColumn
        {
          ColumnName = columnNames[i],
          DataType = TypeMapper.GetType(types[i].GetNullableUnderlyingTypeString()),
          AllowDBNull = types[i].IsNullable()
        });
      }
      foreach (var item in items.Skip(2))
      {
        var columns = item.Split(',');
        if (columnNames.Length != columns.Length)
          throw new InvalidOperationException($"カラム名行と値行の項目数が異なっています。: {item}");

        var row = table.NewRow();
        foreach (var i in Enumerable.Range(0, columnNames.Length))
        {
          object value;
          if (columns[i].Trim().ToUpper() is "DBNULL")
          {
            value = DBNull.Value;
          }
          else if (types[i].GetNullableUnderlyingTypeString() is "bit")
          {
            value = Convert.ToBoolean(Convert.ToInt16(columns[i]));
          }
          else
          {
            value = Convert.ChangeType(columns[i], TypeMapper.GetType(types[i].GetNullableUnderlyingTypeString()));
          }

          row[columnNames[i]] = value ?? DBNull.Value;
        }
        table.Rows.Add(row);
      }

      return table;
    }

    private static bool IsNullable(this string self) =>
      self.Trim().EndsWith("?");
    private static string GetNullableUnderlyingTypeString(this string self) =>
      self.IsNullable() ? self.Trim().Replace("?", "") : self.Trim();
  }
}