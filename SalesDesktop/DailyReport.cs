using System;
using System.Data;
using System.Windows.Forms;

namespace SalesDesktop
{
    public partial class DailyReport : Form
    {
        string date1 = "", date2 = "";
        DataAccess access = null;
        public DailyReport()
        {
            access = new DataAccess();
            InitializeComponent();
            loadOrderDetails(DateTime.Now.AddYears(-10).ToString(), DateTime.Now.ToString());
        }

        private void loadOrderDetails(string d1, string d2)
        {
            date1 = d1;
            date2 = d2;
            try
            {
                dataGridView1.DataSource = new DataTable();
                if (dataGridView1.Columns["dataGridViewDeleteButton"] != null)
                {
                    dataGridView1.Columns.Remove("dataGridViewDeleteButton");
                }
                string q = "select od.ORDER_ID,od.ORDER_DETAIL_ID, p.PRODUCT_NAME,od.QUANTITY,od.PRICE,od.TOTAL_PRICE, od.CREATED_DATE as ORDER_DATE from ORDER_DETAIL od inner join PRODUCTS p on 1=1 where ISNULL(od.deleted,0)=0 and ISNULL(p.DELETED,0)=0 and od.CREATED_DATE >='" + Convert.ToDateTime(d1).ToShortDateString() + "'and od.CREATED_DATE<='" + Convert.ToDateTime(d2).ToShortDateString() + "'";
                DataTable dt = access.select(q);
                dt.Columns["PRODUCT_NAME"].ColumnName = "Product Name";
                dt.Columns["QUANTITY"].ColumnName = "Quantity";
                dt.Columns["PRICE"].ColumnName = "Price";
                dt.Columns["TOTAL_PRICE"].ColumnName = "Total bill";
                dt.Columns["ORDER_DATE"].ColumnName = "Created Date";
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["ORDER_ID"].Visible = false;
                dataGridView1.Columns["ORDER_DETAIL_ID"].Visible = false;
                dataGridView1.Columns.Add(new DataGridViewButtonColumn()
                {
                    Name = "dataGridViewDeleteButton",
                    HeaderText = "delete",
                    Text = "x",
                    ToolTipText = "DELETE",
                    UseColumnTextForButtonValue = true,

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DailyReport_Load(object sender, EventArgs e)
        {
            loadOrderDetails(DateTime.Now.AddYears(-10).ToString(), DateTime.Now.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string d = dateTimePicker1.Text.ToString(), d3 = dateTimePicker2.Text.ToString();
            loadOrderDetails(d, d3);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["dataGridViewDeleteButton"].Index)
                {
                    var id = dataGridView1.Rows[e.RowIndex].Cells["ORDER_ID"].Value;
                    if (access.insertUpdateDelete("update [ORDER] set DELETED=1 where ORDER_ID='"+id+"'") >0)
                    {
                        if (access.insertUpdateDelete("update ORDER_DETAIL set DELETED=1 where ORDER_ID='"+id+"'") > 0)
                        {
                            MessageBox.Show("Deleted succesfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadOrderDetails(date1,date2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                new OrderDetail(dataGridView1.Rows[e.RowIndex].Cells["ORDER_ID"].Value.ToString()).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);                
            }
            
        }
    }
}
