using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Technoservice.Context;
using Technoservice.Context.Models;

namespace Technoservice.UI.WorkingForms
{
    public partial class AddWorker : Form
    {
        public Worker Worker { get; set; }
        public AddWorker()
        {
            InitializeComponent();
            Worker = new Worker();
            Init();
        }
       /* public AddWorker(Worker worker) : this()
        {
            Worker = worker;

            txtFIO.Text = worker.FIO;
            cmbRole.SelectedItem = cmbRole.Items.Cast<Role>().FirstOrDefault(x => x.Id == Worker.RoleId);
            txtLogin.Text = worker.Login;
            txtPassword.Text = worker.Password;
        }*/
        private void Init()
        {
            using (var db = new MyDBContext())
            {
                cmbRole.Items.Clear();
                cmbRole.Items.Add("Выбрать роль");
                cmbRole.Items.AddRange(db.Roles.AsNoTracking().ToArray());
                cmbRole.SelectedIndex = 0;
            }
        }       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbRole.SelectedItem != null)
            {
                using (var db = new MyDBContext())
                {
                    Worker.FIO = txtFIO.Text;
                    Worker.RoleId = ((Role)cmbRole.SelectedItem).Id;
                    Worker.Login = txtLogin.Text;
                    Worker.Password = txtPassword.Text;

                    db.Workers.Add(Worker);
                    db.SaveChanges();
                }
                DialogResult = DialogResult.OK;
            }
            else
            {              
                MessageBox.Show("Пожалуйста, выберите роль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
