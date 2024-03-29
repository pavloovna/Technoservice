using System;
using System.Windows.Forms;
using Technoservice.Context.Models;
using Technoservice.UI.GridForms;
using Technoservice.UI.WorkingForms;

namespace Technoservice.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            lblName.Text = $"Добро пожаловать, {WorkToUser.Worker.FIO}";

            if (WorkToUser.Worker != null)
            {
                var userRoleId = WorkToUser.Worker.RoleId;
                создатьЗаявкуToolStripMenuItem.Visible = 
                добавитьРаботникаToolStripMenuItem.Visible = WorkToUser.CompareRole(1) || WorkToUser.CompareRole(2);
            }
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allClients = new AllClients();
            allClients.Show();
        }

        private void заявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allApplications = new AllApplications();
            allApplications.Show();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allReports = new AllReports();
            allReports.Show();
        }

        private void добавитьРаботникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var AddWorker = new AddWorker();
            AddWorker.Show();
        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addApplication = new AddApplication();
            addApplication.Show();
        }

    }
}
