using System;
using System.Linq;
using System.Windows.Forms;
using Technoservice.Context;

namespace Technoservice.UI.BasicForms
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            btnEnter.Enabled = !string.IsNullOrWhiteSpace(txtLogin.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            using (var db = new MyDBContext())
            {
                var worker = db.Workers.FirstOrDefault(x => x.Login == txtLogin.Text && x.Password == txtPassword.Text);

                if (worker != null)
                {
                    WorkToUser.Worker = worker;
                    DialogResult = DialogResult.OK;
                    MessageBox.Show("Добро пожаловать!");
                }
                else
                {
                    MessageBox.Show("Такого пользователя нет!");
                }
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            pictureBoxOpen.Visible = true;
            pictureBoxClose.Visible = false;
        }

        private void pictureBoxOpen_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            pictureBoxOpen.Visible = false;
            pictureBoxClose.Visible = true;
        }
    }
}
