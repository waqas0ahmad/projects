using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDesktop
{
    class DataAccess
    {
        string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\SalesDesktop\SalesDesktop\Database.mdf;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter sda = null;
        SqlCommand cmd = null;
        public DataAccess()
        {
            con = new SqlConnection(conStr);
        }
        private void OpenConnection()
        {
            if (con.State==ConnectionState.Closed)
            {
                con.Open();                
            }
        }
        private void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public DataTable select(string query)
        {
            try
            {
                OpenConnection();
                sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
        public int insertUpdateDelete( string query)
        {
            try
            {
                OpenConnection();
                cmd = new SqlCommand(query,con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                return -1;
            }
        }
    }
}
