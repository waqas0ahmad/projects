using System;
using System.Data;
using System.Windows.Forms;

namespace SalesDesktop
{
    public partial class DailyReport : Form
    {
        DataAccess access = null;        
        public DailyReport()
        {
            access = new DataAccess();
            InitializeComponent();
            loadOrderDetails( DateTime.Now.AddYears(-10).ToString(), DateTime.Now.ToString());
        }

        private void loadOrderDetails(string d1, string d2)
        {
            try
            {
                dataGridView1.DataSource = new DataTable();
                if (dataGridView1.Columns["dataGridViewDeleteButton"] != null)
                {
                    dataGridView1.Columns.Remove("dataGridViewDeleteButton");
                }
                string q = "select p.PRODUCT_NAME,od.QUANTITY,od.PRICE,od.TOTAL_PRICE, od.CREATED_DATE as ORDER_DATE from ORDER_DETAIL od inner join PRODUCTS p on p.PRODUCT_ID=od.PRODUCT_ID where ISNULL(od.deleted,0)=0 and ISNULL(p.DELETED,0)=0 and od.CREATED_DATE >='" + Convert.ToDateTime(d1).ToShortDateString() + "'and od.CREATED_DATE<='" + Convert.ToDateTime(d2).ToShortDateString() + "'";
                dataGridView1.DataSource = access.select(q);
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
                MessageBox.Show(ex.Message,"Exception",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            if (e.ColumnIndex==dataGridView1.Columns["dataGridViewDeleteButton"].Index)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
