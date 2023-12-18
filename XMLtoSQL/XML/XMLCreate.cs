using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLtoSQL.Constants;

namespace XMLtoSQL.XML
{
    public class XMLCreate
    {
        public XMLAdd _xmlAdd = new XMLAdd();
        public IResult CreateXmlDataTable(string path, ref ProductDataTable productDt,ref int productId, ref SubproductDataTable subproductDt,ref int subproductId, ref ImageDataTable imageDt,ref int imageId)
        {
            var result = Rules.Run(
                CheckIfPathIsNull(path),
                CheckIfPathIsValid(path));
            if (!result.Success)
            {
                return new ErrorDataResult<DataTable>(result.Message);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            try
            {
                productDt.TableName = "Product";

                subproductDt.TableName = "Subproduct";

                imageDt.TableName = "Image";

                XmlNode products = doc.DocumentElement;
                object[] productInnerTextArray = new object[productDt.Columns.Count];
                foreach (XmlNode product in products.ChildNodes)
                {
                    productInnerTextArray[0] = productId;
                    var allProductRows = product.ChildNodes.Cast<XmlNode>().ToList();
                    foreach (var productRow in allProductRows)
                    {
                        if (productRow.Name == "subproducts")
                        {
                            _xmlAdd.AddSubproductRows(ref subproductDt, productRow, ref subproductId, ref imageDt, ref imageId, productId);
                        }
                        else if (productRow.Name == "images")
                        {
                            _xmlAdd.AddImageRows(ref imageDt, productRow, ref imageId, productId);
                        }
                        else
                        {
                            int productIndex = productDt.Columns.IndexOf(productRow.Name);
                            productInnerTextArray[productIndex] = productRow.InnerText;
                        }
                    }
                    productDt.Rows.Add(productInnerTextArray);
                    productId++;
                    Array.Clear(productInnerTextArray);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            int a = 0;
            return new SuccessResult();
        }

        private IResult CheckIfPathIsNull(string path)
        {
            if (path == null)
            {
                return new ErrorResult(Messages.XMLDoesNotExist);
            }

            return new SuccessResult();
        }

        private IResult CheckIfFileIsXML(string path)
        {
            if (path.EndsWith(".xml") == false)
            {
                return new ErrorResult(Messages.FileIsNotXML);
            }
            return new SuccessResult();
        }
        private IResult CheckIfPathIsValid(string path)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                return new SuccessResult();
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.URLIsNotValid);
            }
        }
    }
}
