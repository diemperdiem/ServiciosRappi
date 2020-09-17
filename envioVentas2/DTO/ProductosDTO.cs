using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace envioVentas2.DTO
{
    public class ProductosDTO
    {
        public Int16 store_id { get; set; }
        public int id { get; set; }
        public string gtin { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Int16 brand { get; set; }
        public Int16 trademark { get; set; }
        public Int16 category_first_level { get; set; }
        public Int16 category_second_level { get; set; }
        public decimal price { get; set; }
        public decimal discount_price { get; set; }
        public decimal discount { get; set; }
        public DateTime discount_start_at { get; set; }
        public string discount_end_at { get; set; }
        public int stock { get; set; }
    }
}
