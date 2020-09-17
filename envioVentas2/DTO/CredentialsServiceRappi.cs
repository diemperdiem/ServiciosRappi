using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2.DTO
{
    public class CredentialsServiceRappi
    {
        public CredentialsServiceRappi()
        {
            User = "XX";
            pass = "YY";
        }
        public string User { get; set; }
        public string pass { get; set; }
    }
}
