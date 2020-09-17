using AutoMapper;
using envioVentas2.DTO;
using envioVentas2.Utilidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2.Repository
{
    public class RAPPIProductosRepository
    {
        Conexion _DBCon;
        IMapper _mapper;
        Querys _Query;
        HttpClient _httpContext;

        public RAPPIProductosRepository()
        {
            _DBCon = new Conexion();
            _Query = new Querys();
            _httpContext = new HttpClient();

            SetMappers();
        }

        public bool GetDataProducts()
        {
            var Productos = _DBCon.GetDataWithRawSQL(_Query.QuerysProductos["Query1"]);
            var datosXML = _mapper.Map<List<DataRow>, List<ProductosDTO>>(Productos.Rows.Cast<DataRow>().ToList());
            return Task.Run(() => ServiceRappiAsync(datosXML)).Result;
        }

        public bool GetDataProductsToDate()
        {
            var Productos = _DBCon.GetDataWithRawSQL(_Query.QuerysProductos["Query2"]);
            var datosXML = _mapper.Map<List<DataRow>, List<ProductosDTO>>(Productos.Rows.Cast<DataRow>().ToList());
            return Task.Run(() => ServiceRappiAsync(datosXML)).Result;
        }

        private async Task<bool> ServiceRappiAsync(List<ProductosDTO> Productos) 
        {
            RequestProductosRAPPI Respuesta = new RequestProductosRAPPI();

            var Token = Task.Run(() => GetToken());

            _httpContext.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Api_Key", Token.Result);

            string serialized = JsonConvert.SerializeObject(Productos);
            StringContent stringContent = new StringContent(serialized);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await _httpContext.PostAsync("https://services.grability.rappi.com/api/cpgs-integration/datasets", stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    Respuesta = JsonConvert.DeserializeObject<RequestProductosRAPPI>(apiResponse); //checar que se hace con respuesta

                    if (Respuesta.job_id.FirstOrDefault().ToString() != "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        private async Task<string> GetToken()
        {
            CredentialsServiceRappi Credenciales = new CredentialsServiceRappi();
          
            string serialized = JsonConvert.SerializeObject(Credenciales);
            StringContent stringContent = new StringContent(serialized);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await _httpContext.PostAsync("https://dev.workspaces.api.sad.jelp.io/dev/v2/order", stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    string Token = JsonConvert.DeserializeObject<string>(apiResponse);
                    if (Token != null)
                    {
                        return Token;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        private void SetMappers()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataRow, ProductosDTO>()
                   .ForMember(m => m.store_id, dto => dto.MapFrom(s =>              DBUtils.GetValueOrDefault<Int16>(s, "store_id", new Int16())))         
                   .ForMember(m => m.id, dto => dto.MapFrom(s =>                    DBUtils.GetValueOrDefault<int>(s, "id", 0)))
                   .ForMember(m => m.gtin, dto => dto.MapFrom(s =>                  DBUtils.GetValueOrDefault<string>(s, "gtin", string.Empty)))
                   .ForMember(m => m.name, dto => dto.MapFrom(s =>                  DBUtils.GetValueOrDefault<string>(s, "name", string.Empty)))
                   .ForMember(m => m.description, dto => dto.MapFrom(s =>           DBUtils.GetValueOrDefault<string>(s, "description", string.Empty)))
                   .ForMember(m => m.brand, dto => dto.MapFrom(s =>                 DBUtils.GetValueOrDefault<Int16>(s, "brand", new Int16())))
                   .ForMember(m => m.trademark, dto => dto.MapFrom(s =>             DBUtils.GetValueOrDefault<Int16>(s, "trademark", new Int16())))
                   .ForMember(m => m.category_first_level, dto => dto.MapFrom(s =>  DBUtils.GetValueOrDefault<Int16>(s, "category_first_level", new Int16())))
                   .ForMember(m => m.category_second_level, dto => dto.MapFrom(s => DBUtils.GetValueOrDefault<Int16>(s, "category_second_level", new Int16())))
                   .ForMember(m => m.price, dto => dto.MapFrom(s =>                 DBUtils.GetValueOrDefault<decimal>(s, "price", decimal.Zero)))
                   .ForMember(m => m.discount_price, dto => dto.MapFrom(s =>        DBUtils.GetValueOrDefault<decimal>(s, "discount_price", decimal.Zero)))
                   .ForMember(m => m.discount, dto => dto.MapFrom(s =>              DBUtils.GetValueOrDefault<decimal>(s, "discount", decimal.Zero)))
                   .ForMember(m => m.discount_start_at, dto => dto.MapFrom(s =>     DBUtils.GetValueOrDefault<DateTime>(s, "discount_start_at", new DateTime())))
                   .ForMember(m => m.discount_end_at, dto => dto.MapFrom(s =>       DBUtils.GetValueOrDefault<string>(s, "discount_end_at", string.Empty)))
                   .ForMember(m => m.stock, dto => dto.MapFrom(s =>                 DBUtils.GetValueOrDefault<int>(s, "stock", 0)));

            }).CreateMapper();
        }
    }
}
