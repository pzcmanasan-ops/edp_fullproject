using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginDB
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        MyDatabase db = new MyDatabase();

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (db.TestConnection() == true)
            {
                MessageBox.Show("Connected to Database.");
            }
            else
            {
                MessageBox.Show("Database connection failed!");
            }
        }

        string[,] userCredentials =
        {
            { "admin", "cashier" },
            { "admin", "password" },
            { "Prince Zaki Manasan", "Aeron Alingasa" },
            { "Admin Department", "Staff Department" },
        };

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == "")
            {
                MessageBox.Show("Please enter username.", "Username Required!");
                tbUsername.Focus();
            }
            else if (tbPassword.Text == "")
            {
                MessageBox.Show("Please enter password.", "Password Required!");
                tbPassword.Focus();
            }
            else
            {
                string query = "SELECT * FROM tbllogincredentials WHERE user_username = @username AND user_password = @password;";
                DataTable dt = db.ExecuteReturnQuery(query,
                    new MySqlParameter("@username", tbUsername.Text),
                    new MySqlParameter("@password", tbPassword.Text));

                if (dt.Rows.Count == 1)
                {
                    frmHome frm = new frmHome();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }
            }
        }
    }
}
