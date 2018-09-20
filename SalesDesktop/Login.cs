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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                DataAccess access = new DataAccess();
                string query = "select * from web_user where username='"+txtUsername.Text+"'";
                DataTable dt = access.select(query);
                if (dt.Rows.Count>0)
                {
                    if (dt.Rows[0]["password"].ToString()==txtPassword.Text)
                    {
                        MDI mDI = new MDI();
                        this.Hide();
                        mDI.Show();
                        //Home home = new Home();
                        //this.Hide();
                        //home.Show();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password!", "Error",  MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect username!", "Error",  MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private bool validate()
        {
            if (txtUsername.Text==null||txtUsername.Text=="")
            {
                MessageBox.Show("Enter username", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            if (txtPassword.Text==null||txtPassword.Text=="")
            {
                MessageBox.Show("Enter password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue>='a'&&e.KeyValue<='z')
                ||
                (e.KeyValue>='A' && e.KeyValue <= 'Z')||
                e.KeyCode==Keys.Back
                )
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                char.IsNumber(e.KeyChar)
                ||
                char.IsLetter(e.KeyChar)
                ||
                e.KeyChar==8
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
