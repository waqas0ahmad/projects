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
    public partial class AddOrder : Form
    {
        DataAccess access = null;
        string pgi = null;
        string item, productId, code, callFrom;
        int quantity, totalprice, price, totalBil = 0;
        public AddOrder()
        {
            access = new DataAccess();
            InitializeComponent();
            populateGroupGridView();
            try
            {
                if (grdvitemsGroup.Rows.Count>0)
                {
                    pgi = grdvitemsGroup.Rows[0].Cells[0].Value.ToString(); 
                }
            }catch(Exception ex)
            {

            }
            populateItemDropdown();
           
        }



        private void btnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvOrder.Rows.Count<=0)
                {
                    MessageBox.Show("Add order to list first");
                    return;
                }
                List<PrintOrder> list = new List<PrintOrder>();
                for (int i = 0; i < grdvOrder.Rows.Count; i++)
                {
                    string query = "insert into [ORDER](TOTAL_BIL,ORDER_DATE,CUSTOMER_ID,SHOP_ID)values(N'" + grdvOrder.Rows[i].Cells[3].Value.ToString() + "',GETDATE(),1,1)";
                    access.insertUpdateDelete(query);
                    var order_id = access.select("select top 1 ORDER_ID from dbo.[ORDER] order by ORDER_ID desc").Rows[0][0].ToString();
                    query = "insert into dbo.[ORDER_DETAIL](ORDER_ID,PRODUCT_ID,PRICE,TOTAL_PRICE,QUANTITY,CREATED_DATE,SHOP_ID)" +
                        "values(N'" + order_id + "',N'" + grdvOrder.Rows[i].Cells[5].Value.ToString() + "',N'" + grdvOrder.Rows[i].Cells[4].Value.ToString() + "'," +
                        "N'" + Convert.ToInt32(grdvOrder.Rows[i].Cells[4].Value.ToString()) * Convert.ToInt32(grdvOrder.Rows[i].Cells[2].Value.ToString()) + "',N'" + grdvOrder.Rows[i].Cells[2].Value.ToString() + "',GETDATE(),1)";
                    totalBil = 0;
                    access.insertUpdateDelete(query);
                    list.Add(new PrintOrder() {
                        amount = Convert.ToInt32(grdvOrder.Rows[i].Cells[4].Value.ToString()) * Convert.ToInt32(grdvOrder.Rows[i].Cells[2].Value.ToString()),
                        product= grdvOrder.Rows[i].Cells[0].Value.ToString(),
                        quantity= Convert.ToInt32(grdvOrder.Rows[i].Cells[2].Value.ToString()),
                        rate= Convert.ToInt32(grdvOrder.Rows[i].Cells[4].Value.ToString())
                    });
                }
                //                grdvOrder.DataSource = new DataTable();
                grdvOrder.Rows.Clear();
                txtQuantity.Text = "1";
                txtCode.Text = "";
                txtTotalBill.Text = "0";
                item = "";
                productId = "";
                code = "";
                callFrom = "";
                quantity = 1;
                totalprice = 0;
                price = 0;
                totalBil = 0;
                MessageBox.Show("Order placed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new PrintForm_(list);
                //loadOrderDetails(DateTime.Now.ToString(), DateTime.Now.AddYears(-10).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grdvOrder.Columns["clmRm"].Index)
            {
                grdvOrder.Rows.RemoveAt(e.RowIndex);
            }
        }
       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            if (itemDropdown.Items.Count > 0)
            {
                itemDropdown.SelectedIndex = 0;
                itemDropdown_SelectedIndexChanged(null, null);
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string q = "select i.Name,i.Id,p.PRODUCT_NAME,p.PRODUCT_PRICE,p.PRODUCT_CODE,p.PRODUCT_ID from PRODUCTS p join Item_Group i on p.PRODUCT_GROUP_ID = i.Id where p.PRODUCT_CODE='" + txtCode.Text + "'";
                    DataTable dt = access.select(q);
                    if (dt.Rows.Count < 1)
                    {
                        MessageBox.Show("Now Such code exists", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    item = dt.Rows[0]["PRODUCT_NAME"].ToString();
                    pgi= dt.Rows[0]["Id"].ToString();
                    populateItemDropdown();
                    code = dt.Rows[0]["PRODUCT_CODE"].ToString();
                    quantity = Convert.ToInt32(txtQuantity.Text);
                    price = Convert.ToInt32(dt.Rows[0]["PRODUCT_PRICE"].ToString());
                    totalprice = quantity * price;
                    productId = dt.Rows[0]["PRODUCT_ID"].ToString();
                    itemDropdown.SelectedIndex = itemDropdown.FindStringExact(item);
                    callFrom = "db";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (callFrom != "db")
                {
                                       
                    item = itemDropdown.Text.ToString();
                    code = txtCode.Text;
                    quantity = Convert.ToInt32(txtQuantity.Text);
                    if (price <=0)
                    {
                        price = Convert.ToInt32(itemDropdown.SelectedValue.ToString());
                    }
                    totalprice = Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(price);
                    productId = itemDropdown.SelectedValue.ToString();
                }
                if (item == "" || code == "" || quantity <= 0)
                {
                    MessageBox.Show("Fill required fields","Validation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                this.grdvOrder.Rows.Add(new string[] {
                    item,
                    code,
                    quantity+"",
                    totalprice+"",
                    productId,
                    pgi,"x" });
                price = -1;
                callFrom = "";
                totalBil += totalprice;
                txtTotalBill.Text = totalBil.ToString();
                //this.grdvOrder.Columns[4].Visible = false;
                txtCode.Text = "";
                txtQuantity.Text = "1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }

        private void itemDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.Text = "1";
                txtCode.Text = "";
                if (itemDropdown.Items.Count<=0)
                {
                    return;
                }
                var param1 = itemDropdown.SelectedValue.ToString();
                var param2 = itemDropdown.Text.ToString();

                string query = "select * from PRODUCTS where " +
                    "PRODUCT_NAME='"+param2.ToString()+"' and " +
                    "PRODUCT_PRICE='"+param1.ToString()+"'";
                txtCode.Text = access.select(query).Rows[0]["PRODUCT_CODE"].ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdvitemsGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var item = e;
            pgi = grdvitemsGroup.Rows[e.RowIndex].Cells[0].Value.ToString();
            populateItemDropdown();
        }
        void populateItemDropdown()
        {
            try
            {
                string query = "select product_name,product_price from products where product_group_id='" + pgi + "'";
                itemDropdown.DataSource = access.select(query);
                itemDropdown.ValueMember = "product_price";
                itemDropdown.DisplayMember = "product_name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }
        void populateGroupGridView()
        {
            try
            {
                string query = "select id,name from item_group where isnull(deleted,0)=0";
                grdvitemsGroup.DataSource = access.select(query);
                grdvitemsGroup.Columns[0].Visible = false;
                grdvitemsGroup.Columns[1].Width = 160;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }
    }
}
