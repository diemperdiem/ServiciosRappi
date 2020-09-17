using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2.DTO
{
    public class RequestProductosRAPPI
    {
        public string job_id { get; set; }  //"job_id": "6d9beb1b-8d91-4d18-9424-6e2a94383842",
        public string status { get; set; }    //"status": "created",
        public string created_at { get; set; }    //"created_at": "2019-06-27T19:23:18.393653186Z"
    }
}
