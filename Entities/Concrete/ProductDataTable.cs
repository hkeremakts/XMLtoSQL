using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class ProductDataTable:DataTable,IEntity
    {
        public ProductDataTable()
        {
            Columns.Add("Id",typeof(int));
            Columns.Add("code",typeof(int));
            Columns.Add("ws_code");
            Columns.Add("barcode");
            Columns.Add("supplier_code");
            Columns.Add("name");
            Columns.Add("cat1name");
            Columns.Add("cat1code");
            Columns.Add("cat2name");
            Columns.Add("cat2code");
            Columns.Add("cat3name");
            Columns.Add("cat3code");
            Columns.Add("category_path");
            Columns.Add("stock");
            Columns.Add("unit");
            Columns.Add("price_list");
            Columns.Add("price_list_vat_included");
            Columns.Add("price_list_campaign");
            Columns.Add("price_special_vat_included");
            Columns.Add("price_special");
            Columns.Add("price_special_rate");
            Columns.Add("min_order_quantity");
            Columns.Add("price_credit_card");
            Columns.Add("currency");
            Columns.Add("vat");
            Columns.Add("brand");
            Columns.Add("model");
            Columns.Add("desi");
            Columns.Add("width");
            Columns.Add("height");
            Columns.Add("deep");
            Columns.Add("weight");
            Columns.Add("delivery_day");
            Columns.Add("detail");
        }
    }
}
