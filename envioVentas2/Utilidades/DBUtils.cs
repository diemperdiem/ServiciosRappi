using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2.Utilidades
{
    public class DBUtils
    {
        public static T GetValueOrDefault<T>(System.Data.IDataReader dataReader, string columnName, T defaultValue = default(T))
        {
            if (dataReader != null && !string.IsNullOrWhiteSpace(columnName))
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (dataReader[columnName] == DBNull.Value)
                            return defaultValue;

                        return (T)dataReader[columnName];
                    }
                }
            }

            return defaultValue;
        }

        public static T GetValueOrDefault<T>(System.Data.DataRow dataReader, string columnName, T defaultValue = default(T))
        {
            if (dataReader != null && !string.IsNullOrWhiteSpace(columnName))
            {
                var currentValue = dataReader[columnName];
                //if(currentValue == DBNull)

                if (currentValue != null)
                {
                    var ctype = currentValue.GetType();

                    if (ctype.Name != "DBNull")
                        return (T)dataReader[columnName];
                }
            }
            return defaultValue;
        }

        public static string ValidateNullEmpty(string newVal, string currentVal)
        {
            return string.IsNullOrEmpty(newVal) ? currentVal : newVal;
        }
    }
}
