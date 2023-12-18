using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Security.Cryptography;
using System.Data;
using Core.Utilities.Results;
using XMLtoSQL.Constants;
using Core.Utilities.Business;
using static System.Net.WebRequestMethods;
using System.Security.Policy;
using System.Drawing;
using Entities.Concrete;
using XMLtoSQL.SQL;
using XMLtoSQL.XML;
using static System.Net.Mime.MediaTypeNames;

namespace XMLtoSQL
{
    public partial class MainForm : Form
    {
        public string XMLPath;
        public XMLCreate _xmlCreate = new XMLCreate();
        public SQLCreate _sqlCreate=new SQLCreate();
        public SQLDelete _sqlDelete=new SQLDelete();
        public SQLAdd _sqlAdd=new SQLAdd();

        public int productId = 0;
        public int subproductId = 0;
        public int imageId = 0;
        public MainForm()
        {
            InitializeComponent();

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK)
                XMLPath = OFD.FileName;
        }
        private void btnURL_Click(object sender, EventArgs e)
        {
            XMLPath = textBoxURL.Text;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ProductDataTable productDt = new ProductDataTable();
            SubproductDataTable subproductDt = new SubproductDataTable();
            ImageDataTable imageDt = new ImageDataTable();
            var XMLResult= _xmlCreate.CreateXmlDataTable(XMLPath,ref productDt,ref productId,ref subproductDt,ref subproductId,ref imageDt,ref imageId);
            if (!XMLResult.Success)
            {
                MessageBox.Show(XMLResult.Message);
                return;
            }

            string conStr = ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
            SqlConnection connection = new SqlConnection(conStr);
            connection.Open();

            string productQuery = _sqlCreate.CreateTableQuery(productDt);
            string subproductQuery = _sqlCreate.CreateTableQuery(subproductDt);
            string imageQuery = _sqlCreate.CreateTableQuery(imageDt);

            _sqlDelete.DeleteIfTableExists(imageDt, connection);
            _sqlDelete.DeleteIfTableExists(subproductDt, connection);
            _sqlDelete.DeleteIfTableExists(productDt,connection);

            var cmd = new SqlCommand(productQuery, connection);
            int check = cmd.ExecuteNonQuery();
            if (check != 0)
            {
                _sqlAdd.SQLAddColums(productDt,connection);
            }
            cmd = new SqlCommand(subproductQuery, connection);
            check = cmd.ExecuteNonQuery();
            if (check != 0)
            {
                _sqlAdd.SQLAddColums(subproductDt, connection);
            }
            cmd = new SqlCommand(imageQuery, connection);
            check = cmd.ExecuteNonQuery();
            if (check != 0)
            {
                _sqlAdd.SQLAddColums(imageDt, connection);
            }
            MessageBox.Show("Database Created Successfully");
            connection.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDataTable productDt = new ProductDataTable();
            SubproductDataTable subproductDt = new SubproductDataTable();
            ImageDataTable imageDt = new ImageDataTable();
            var XMLResult = _xmlCreate.CreateXmlDataTable(XMLPath, ref productDt,ref productId, ref subproductDt,ref subproductId, ref imageDt,ref imageId);
            if (!XMLResult.Success)
            {
                MessageBox.Show(XMLResult.Message);
                return;
            }
            string conStr = ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
            SqlConnection connection = new SqlConnection(conStr);
            connection.Open();

            _sqlAdd.SQLAddColums(productDt,connection);
            _sqlAdd.SQLAddColums(subproductDt,connection);
            _sqlAdd.SQLAddColums(imageDt,connection);

            MessageBox.Show("Database Updated Successfully");
            connection.Close();
        }

    }
}