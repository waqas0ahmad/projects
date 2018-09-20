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
    public partial class MDI : Form
    {
        public MDI()
        {
            InitializeComponent();
            this.FormClosing += MDI_FormCLose;
            lblOrder_Click(null, null);
        }
        private void MDI_FormCLose(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?","Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
            {
                e.Cancel=true;
            }
            else
            {
                Application.ExitThread();
            }
        }
        private void lblOrder_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["DailyReport"] != null)
            {

                Application.OpenForms["DailyReport"].WindowState = FormWindowState.Minimized;
            }
            if (Application.OpenForms["AddProduct"] != null)
            {
                Application.OpenForms["AddProduct"].WindowState = FormWindowState.Minimized;
            }
            if (Application.OpenForms["AddOrder"] == null)
            {
                Form addOrder = new AddOrder();
                addOrder.MdiParent = this;
                addOrder.Show();
            }
            else
            {
                Form addOrder = Application.OpenForms["AddOrder"];
                addOrder.Focus();
                addOrder.WindowState = FormWindowState.Normal;
                addOrder.TopMost=true;
                addOrder.BringToFront();
            }
            
        }

        private void logoutPicture_Click(object sender, EventArgs e)
        {
            Form login = Application.OpenForms["Login"] == null ?  new Login(): Application.OpenForms["Login"] ;
            login.Show();
            this.Hide();
        }

        private void lblAddProduct_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["DailyReport"] != null)
            {
                
                Application.OpenForms["DailyReport"].WindowState = FormWindowState.Minimized;
            }
            if (Application.OpenForms["AddOrder"] != null)
            {
                Application.OpenForms["AddOrder"].WindowState = FormWindowState.Minimized;
            }
            if (Application.OpenForms["AddProduct"] == null)
            {
                Form addProduct = new AddProduct();
                addProduct.MdiParent = this;
                addProduct.Show();
            }
            else
            {
                Form addProduct = Application.OpenForms["AddProduct"];
                addProduct.Focus();
                addProduct.TopMost = true;
                addProduct.WindowState = FormWindowState.Normal;
                addProduct.BringToFront();
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AddProduct"] != null)
            {
                Application.OpenForms["AddProduct"].WindowState = FormWindowState.Minimized;
            }
            if (Application.OpenForms["AddOrder"] != null)
            {
                Application.OpenForms["AddOrder"].WindowState = FormWindowState.Minimized;
            }
            if (Application.OpenForms["DailyReport"] == null)
            {
                Form dailyReport = new DailyReport();
                dailyReport.MdiParent = this;
                dailyReport.Show();
            }
            else
            {
                Form dailyReport = Application.OpenForms["DailyReport"];
                dailyReport.Focus();
                dailyReport.TopMost = true;
                dailyReport.WindowState = FormWindowState.Normal;
                dailyReport.BringToFront();
            }
        }
    }
}
