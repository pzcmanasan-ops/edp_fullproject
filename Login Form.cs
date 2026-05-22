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
                MessageBox.Show("Please enter username!", "Validation");
                tbUsername.Focus();
            }
            else if (tbPassword.Text == "")
            {
                MessageBox.Show("Please enter password!", "Validation");
                tbPassword.Focus();
            }
            else
            {
                DataTable dt = db.ExecuteReturnQuery("select * from tblLoginCredentials where user_username = @uname and user_password = @pword;",
                new MySqlParameter("@uname", tbUsername.Text),
                new MySqlParameter("@pword", tbPassword.Text));
                if (dt.Rows.Count == 1)
                {

                    if (Convert.ToInt32(dt.Rows[0]["is_active"]) == 1)
                    {
                        frmHome frm = new frmHome();
                        frm.Owner = this;
                        this.Hide();
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Your account has been deactivated!", "Account Deactivated");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
                }

            }
        }
    }
}
