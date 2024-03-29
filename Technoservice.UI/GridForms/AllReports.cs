using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Technoservice.Context;
using Technoservice.Context.Models;
using Technoservice.UI.WorkingForms;

namespace Technoservice.UI.GridForms
{
    public partial class AllReports : Form
    {
        public Report Report { get; set; }
        public AllReports()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            Initilaze();
        }
        public void Initilaze()
        {
            using (var db = new MyDBContext())
            {
                var reports = db.Reports.ToList();
                dataGridView1.DataSource = reports;

                toolStripStatusLabel1.Text = $"Кол-во записей: {reports.Count}";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAdd = new AddReport();
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                using (var db = new MyDBContext())
                {
                    db.Reports.Add(formAdd.Report);
                    db.SaveChanges();
                }
                Initilaze();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var report = dataGridView1.SelectedRows[0].DataBoundItem as Report;

            if (report != null)
            {
                using (var db = new MyDBContext())
                {
                    var reportDB = db.Reports.FirstOrDefault(x => x.Id == report.Id);
                    var formAdd = new AddReport(reportDB);

                    if (formAdd.ShowDialog() == DialogResult.OK)
                    {
                        db.SaveChanges();
                        Initilaze();
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var report = dataGridView1.SelectedRows[0].DataBoundItem as Report;

            if (report != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить этот отчет?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var db = new MyDBContext())
                    {
                        var reportDB = db.Reports.FirstOrDefault(x => x.Id == report.Id);

                        db.Reports.Remove(reportDB);
                        db.SaveChanges();
                    }
                    Initilaze();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            using (var db = new MyDBContext())
            {
                var search = db.Reports
                    .Where(a =>
                        a.ApplicationId.ToString().Contains(searchText) ||
                        a.Reason.ToLower().Contains(searchText)).ToList();

                dataGridView1.DataSource = search;

                toolStripStatusLabel1.Text = $"Кол-во записей: {search.Count}";
            }
        }
    }
}
