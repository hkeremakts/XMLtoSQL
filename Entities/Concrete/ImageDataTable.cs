using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class ImageDataTable:DataTable,IEntity
    {
        public ImageDataTable()
        {
            Columns.Add("Id", typeof(int));
            //table.Columns[0].AutoIncrement=true;
            Columns.Add("img_item");
            Columns.Add("ProductId", typeof(int));
            Columns.Add("SubproductId", typeof(int));
        }
    }
}
