using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2.Utilidades
{
    public class Querys
    {
        public Dictionary<string, string> QuerysProductos;
        public Querys()                                                                                                                                                                   
        {                                                                                                                                                                                 
            AssignQueryProductos();                                                                                                                                                       
        }                                                                                                                                                                                 
                                                                                                                                                                                          
        private void AssignQueryProductos()                                                                                                                                               
        {                                                                                                                                                                                 
            QuerysProductos = new Dictionary<string, string>();

            #region Query1                                                                                                                                                                              
            QuerysProductos.Add("Query1",  "SELECT i.sucursal store_id                                                                                                                    " +
                                           "     , i.codigo id                                                                                                                           " +
                                           "     , p.codigo_barra gtin                                                                                                                   " +
                                           "     , p.descripcion name                                                                                                                    " +
                                           "     , p.descripcion description                                                                                                             " +
                                           "     , p.marca brand                                                                                                                         " +
                                           "     , p.marca trademark                                                                                                                     " +
                                           "     , p.categoria category_first_level                                                                                                      " +
                                           "     , p.subcategoria category_second_level                                                                                                  " +
                                           "     , i.precio_vta_cenefa price                                                                                                             " +
                                           "                                                                                                                                              " +
                                           "     , (i.precio_vta_cenefa * (1 - (CASE WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S' AND p.otorga_descuento = 'S' " +
                                           "           THEN i.porc_descuento_vta_cenefa WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S'                               " +
                                           "           AND p.otorga_descuento = 'N' THEN 0 ELSE i.porc_descuento_vta_cenefa END) / 100)) discount_price                                      " +
                                           "                                                                                                                                                 " +
	                                       "     , ((CASE WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S' AND p.otorga_descuento = 'S'                               " +
                                           "           THEN i.porc_descuento_vta_cenefa WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S' AND p.otorga_descuento = 'N'  " +
                                           "           THEN 0 ELSE i.porc_descuento_vta_cenefa END)) discount                                                                                 " +
                                           "                                                                                                                                                  " +
                                           "     , i.fecha_inicial_oferta discount_start_at                                                                                                  " +
                                           "     , '' discount_end_at                                                                                                                        " +
                                           "     ,  i.unidades_existencia - i.unidades_comprometidas stock                                                                                   " +
                                           "                                                                                                                                                  " +
                                           " FROM productos p(nolock)                                                                                                                       " +
                                           "    , inventario_perpetuo i(nolock)                                                                                                               " +
                                           "    , sucursales s(NOLOCK)                                                                                                                        " +
                                           " WHERE s.tipo_sucursal = 'T'                                                                                                                      " +
                                           " AND s.status = 'A'                                                                                                                               " +
                                           " AND s.sucursal = 1 " +
                                           " AND s.sucursal = i.sucursal                                                                                                                      " +
                                           " AND p.codigo = i.codigo                                                                                                                          ");
            #endregion

            #region Query2

            QuerysProductos.Add("Query2",   " SELECT i.sucursal store_id                                                                                                                           " +
                                            "      , i.codigo id                                                                                                                                    " +
                                            "      , p.codigo_barra gtin                                                                                                                            " +
                                            "      , p.descripcion name                                                                                                                             " +
                                            "      , p.descripcion description                                                                                                                      " +
                                            "      , p.marca brand                                                                                                                                  " +
                                            "      , p.marca trademark                                                                                                                              " +
                                            "      , p.categoria category_first_level                                                                                                               " +
                                            "      , p.subcategoria category_second_level                                                                                                           " +
                                            "      , i.precio_vta_cenefa price                                                                                                                      " +
                                            "                                                                                                                                                      " +
                                            "      , (i.precio_vta_cenefa * (1 - (CASE WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S' AND p.otorga_descuento = 'S'          " +
                                            "          THEN i.porc_descuento_vta_cenefa WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S'                                     " +
                                            "          AND p.otorga_descuento = 'N' THEN 0 ELSE i.porc_descuento_vta_cenefa END) / 100)) discount_price                                            " +
                                            "                                                                                                                                                      " +
                                            "	  , ((CASE WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S' AND p.otorga_descuento = 'S'                                      " +
                                            "          THEN i.porc_descuento_vta_cenefa WHEN i.respeta_otorga_dscto_cenefa = 'S' AND i.dscto_gral_oferta = 'S' AND p.otorga_descuento = 'N'        " +
                                            "          THEN 0 ELSE i.porc_descuento_vta_cenefa END)) discount                                                                                      " +
                                            "                                                                                                                                                      " +
                                            "     , i.fecha_inicial_oferta discount_start_at                                                                                                       " +
                                            "     , '' discount_end_at                                                                                                                             " +
                                            "     , i.unidades_existencia - i.unidades_comprometidas stock                                                                                         " +
                                            "                                                                                                                                                      " +
                                            " FROM productos p(nolock)                                                                                                                            " +
                                            "    , inventario_perpetuo i(nolock)                                                                                                                    " +
                                            "    , sucursales s(NOLOCK)                                                                                                                             " +
                                            " WHERE s.tipo_sucursal = 'T'                                                                                                                          " +
                                            " AND s.status = 'A'                                                                                                                                   " +
                                            " AND s.sucursal = i.sucursal                                                                                                                          " +
                                            " AND p.codigo = i.codigo                                                                                                                              " +
                                            " AND(p.fecha_modificacion >= convert(varchar(10), convert(datetime, GETDATE(), 120), 120)                                                             " +
                                            " OR p.fecha_alta >= convert(varchar(10), convert(datetime, GETDATE(), 120), 120)                                                                      " +
                                            " OR i.fecha_aceptacion_p_vta >= convert(varchar(10), convert(datetime, GETDATE(), 120), 120)                                                          " +
                                            " OR i.fecha_inicial_oferta >= convert(varchar(10), convert(datetime, GETDATE(), 120), 120)                                                            " +
                                            " OR i.fecha_para_mantenimiento >= convert(varchar(10), convert(datetime, GETDATE(), 120), 120))");

            #endregion


        }
    }
}
