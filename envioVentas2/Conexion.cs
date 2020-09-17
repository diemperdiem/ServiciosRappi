using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2
{
    public class Conexion
    {
        public string FRDataContextBO { get; set; }

        public Conexion()
        {
            FRDataContextBO = ConfigurationManager.ConnectionStrings["FRDataContextBO"].ConnectionString;
        }

        public DataTable GetDataWithRawSQL(string query)
        {

            var datatable = new DataTable();
            using (var conn = new System.Data.SqlClient.SqlConnection(FRDataContextBO))
            {
                conn.Open();
                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    
                    var dr = cmd.ExecuteReader();
                    datatable.Load(dr);
                }
            }
            return datatable;
        }
    }
}
