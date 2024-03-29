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
    public partial class AddReport : Form
    {
        public Report Report { get; set; }
        public AddReport()
        {
            InitializeComponent();
            Report = new Report();
            Init();
        }
        public AddReport(Report report) : this()
        {
            Report = report;

            cmbApplication.Visible = false;
            lblNum.Visible = true;

            lblNum.Text = report.ApplicationId.ToString();
            txtReason.Text = report.Reason;
            txtTime.Text = report.CompilationTime.ToString();
            txtPrice.Text = report.Price.ToString(); 
        }
        private void Init()
        {
            using (var db = new MyDBContext())
            {
                lblNum.Visible = false;
                cmbApplication.Visible = true;

                cmbApplication.Items.Clear();
                cmbApplication.Items.Add("Выбрать номер заявки");
                cmbApplication.Items.AddRange(db.Applications.AsNoTracking().ToArray());
                cmbApplication.SelectedIndex = 0;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            var selectedApplication = cmbApplication.SelectedItem as Context.Models.Application;

            // Проверяем, что выбранная заявка не null
            if (selectedApplication != null)
            {
                // Присваиваем Id выбранной заявки объекту Report
                Report.ApplicationId = selectedApplication.Id;
                Report.Reason = txtReason.Text;
                Report.CompilationTime = TimeSpan.Parse(txtTime.Text);
                Report.Price = decimal.Parse(txtPrice.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void AddReport_Load(object sender, EventArgs e)
        {

        }
    }
}
