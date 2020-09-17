using envioVentas2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var sdfs = new RAPPIProductosRepository();

            sdfs.GetDataProducts();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new EnvioVentas2()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
