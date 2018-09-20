using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesDesktop
{
    public partial class OrderDetail : Form
    {
        DataAccess access = null;
        public OrderDetail(string orderId)
        {
            access = new DataAccess();
            InitializeComponent();
            populateGrid(orderId);
        }
        void populateGrid( string   id)
        {
            try
            {
                string query = "select p.PRODUCT_NAME,p.PRODUCT_PRICE,o.QUANTITY,o.TOTAL_PRICE " +
                    "from ORDER_DETAIL o inner join PRODUCTS p on 1=1  " +
                    "where ISNULL(p.deleted,0)=0 and ISNULL(p.deleted,0)=0 and o.ORDER_ID='"+id+"'";
                DataTable dt = access.select(query);
                dt.Columns["PRODUCT_NAME"].ColumnName = "Product Name";
                dt.Columns["PRODUCT_PRICE"].ColumnName = "Product Price";
                dt.Columns["QUANTITY"].ColumnName = "Quantity";
                dt.Columns["TOTAL_PRICE"].ColumnName = "Total Price";
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }
    }
}
