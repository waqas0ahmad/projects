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
    public partial class AddProduct : Form
    {
        DataAccess access = null;
        public AddProduct()
        {
            access = new DataAccess();
            InitializeComponent();
            populateGroupDropdown();
        }
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGroup.Text==null|| txtGroup.Text=="")
                {
                    MessageBox.Show("Enter group name please.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string query = "insert into item_group ([name])values(N'" + txtGroup.Text + "')";
                if (access.insertUpdateDelete(query) > 0)
                {
                    MessageBox.Show("Inserted Successfully", "Success", MessageBoxButtons.OK);
                    populateGroupDropdown();                    
                }
                else
                {
                    MessageBox.Show("Error adding product group", "Error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName.Text==""|| txtProductName.Text==null)
                {
                    MessageBox.Show("Enter product name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (txtPrice.Text == "" || txtPrice.Text == null)
                {
                    MessageBox.Show("Enter product price", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (txtPcode.Text == "" || txtPcode.Text == null)
                {
                    MessageBox.Show("Enter product code", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                string query = "insert into products(product_name,PRODUCT_CODE,product_group_id,product_price,shop_id) " +
                    "values(N'" + txtProductName.Text + "',N'" + txtPcode.Text + "' ,N'" + groupDropdown.SelectedValue + "', N'" + txtPrice.Text + "', '1')";
                if (access.insertUpdateDelete(query) > 0)
                {
                    MessageBox.Show("Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error adding product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }
        void populateGroupDropdown()
        {
            try
            {
                string query = "select id,[name] from item_group where isnull(deleted,0)=0";
                groupDropdown.DataSource = access.select(query);
                groupDropdown.ValueMember = "id";
                groupDropdown.DisplayMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }
    }
}
