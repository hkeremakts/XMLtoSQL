using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLtoSQL.XML
{
    public class XMLAdd
    {
        public void AddImageRows(ref ImageDataTable imageDt, XmlNode row, ref int imageId, int foreignId)
        {
            var images = row.ChildNodes.Cast<XmlNode>().ToList();
            object[] imageInnerTextArray = new object[imageDt.Columns.Count];
            foreach (var image in images)
            {
                imageInnerTextArray[0] = imageId;
                var allImageRows = image.ChildNodes.Cast<XmlNode>().ToList();

                ////use when image has 2 or more child nodes
                //foreach (var imageRow in allImageRows)
                //{
                //    int imageIndex = imageDt.Columns.IndexOf(imageRow.Name);
                //    imageInnerTextArray[imageIndex] = imageRow.InnerText;
                //}  

                int imageIndex = imageDt.Columns.IndexOf(allImageRows[0].ParentNode.Name);
                string fName = row.ParentNode.Name;
                int fkIndex = imageDt.Columns.IndexOf(char.ToUpper(fName[0]) + fName.Substring(1) + "Id");
                imageInnerTextArray[fkIndex] = foreignId;
                imageInnerTextArray[imageIndex] = allImageRows[0].InnerText;
                imageDt.Rows.Add(imageInnerTextArray);
                imageId++;
                Array.Clear(imageInnerTextArray);
            }
        }

        public void AddSubproductRows(ref SubproductDataTable subproductDt, XmlNode productRow, ref int subproductId, ref ImageDataTable imageDt, ref int imageId, int productId)
        {
            var subproducts = productRow.ChildNodes.Cast<XmlNode>().ToList();
            object[] subproductInnerTextArray = new object[subproductDt.Columns.Count];
            foreach (var subproduct in subproducts)
            {
                subproductInnerTextArray[0] = subproductId;
                var allSubproductRows = subproduct.ChildNodes.Cast<XmlNode>().ToList();
                foreach (var subproductRow in allSubproductRows)
                {
                    if (subproductRow.Name == "images")
                    {
                        AddImageRows(ref imageDt, subproductRow, ref imageId, subproductId);
                    }
                    else
                    {
                        int subproductIndex = subproductDt.Columns.IndexOf(subproductRow.Name);
                        string fName = productRow.ParentNode.Name;
                        int fkIndex = subproductDt.Columns.IndexOf(char.ToUpper(fName[0]) + fName.Substring(1) + "Id");
                        subproductInnerTextArray[fkIndex] = productId;
                        subproductInnerTextArray[subproductIndex] = subproductRow.InnerText;
                    }
                }

                subproductDt.Rows.Add(subproductInnerTextArray);
                subproductId++;
                Array.Clear(subproductInnerTextArray);
            }
        }
    }
}
