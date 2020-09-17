using envioVentas2.EnvioVen;
using envioVentas2.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2
{
    public partial class EnvioVentas2 : ServiceBase
    {
        bool blBandera = false;
        public EnvioVentas2()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            stLapso.Start();

        }

        protected override void OnStop()
        {
            stLapso.Stop();
        }

        private void stLapso_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool v=false;
            bool v2 = false;
            bool resultado;
            EventLog.WriteEntry(" Entro a elapsed", EventLogEntryType.Information);

            if (blBandera) return;
            try
            {
                blBandera = true;
                EventLog.WriteEntry(" Entro a elapsed  2 ", EventLogEntryType.Information);

                ServiceClient EnvioVenta = new ServiceClient();
                EventLog.WriteEntry(" Entro a elapsed  EnvioVenta  " + EnvioVenta, EventLogEntryType.Information);
                var res = EnvioVenta.SyncVentas();
                bool correcto = res.Success;

                if (correcto == true)
                {

                  v = res.Result;

                }

                var rapi = new RAPPIProductosRepository();

                rapi.GetDataProducts();

            

                EventLog.WriteEntry(" Entro a elapsed  EnvioVenta  res " + res, EventLogEntryType.Information);


                EventLog.WriteEntry(" Entro a elapsed  EnvioVenta  v " + v, EventLogEntryType.Information);

               // ServiceClient EnvioRetiros = new ServiceClient();
                EventLog.WriteEntry(" Entro a elapsed  EnvioRetiros  " + EnvioVenta, EventLogEntryType.Information);
                var res2 = EnvioVenta.SyncRetiros();

                bool correcto2 = res.Success;

                if (correcto2 == true)
                {

                    v2 = res.Result;

                }

                EventLog.WriteEntry(" Entro a elapsed  EnvioRetiros RESPUESTA   "+v2  , EventLogEntryType.Information);



            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);

            }

            blBandera = false;


        }
    }
}
