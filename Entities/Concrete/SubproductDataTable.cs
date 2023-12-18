using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class SubproductDataTable:DataTable,IEntity
    {
        public SubproductDataTable()
        {
            Columns.Add("Id", typeof(int));
            //table.Columns[0].AutoIncrement = true;
            Columns.Add("VaryantGroupID");
            Columns.Add("code", typeof(int));
            Columns.Add("ws_code");
            Columns.Add("type1");
            Columns.Add("type2");
            Columns.Add("barcode");
            Columns.Add("stock");
            Columns.Add("desi");
            Columns.Add("price_list");
            Columns.Add("price_list_discount");
            Columns.Add("price_special");
            Columns.Add("supplier_code");
            Columns.Add("ProductId", typeof(int));
        }
    }
}
