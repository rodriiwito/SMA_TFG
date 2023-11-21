using Dapper;
using System.Data;

namespace BootesConsulta.Database;

public static class DataBaseHelper
{
    public static DataTable EnumToDataTable<T>(IEnumerable<T> Enums) where T : Enum
    {
        DataTable dt = new();
        dt.SetTypeName("dbo.IntCollectionType");
        dt.Columns.Add("ID", typeof(int));

        if (Enums is not null)
        {
            foreach (T Enum in Enums)
            {
                dt.Rows.Add(Enum);
            }
        }

        return dt;
    }
}
