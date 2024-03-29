using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using Technoservice.Context;
using Technoservice.Context.Models;
using Technoservice.UI.WorkingForms;

namespace Technoservice.UI.GridForms
{
    public partial class AllApplications : Form
    {
        public AllApplications()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            Init();
            clbWorker.DisplayMember = nameof(Worker.FIO);

            if (WorkToUser.Worker != null)
            {
                var userRoleId = WorkToUser.Worker.RoleId;
                btnAdd.Visible =
                btnEdit.Visible = WorkToUser.CompareRole(1) || WorkToUser.CompareRole(2);
            }
        }

        public void Init()
        {
            using (var db = new MyDBContext())
            {
                var workers = db.Workers.Where(w => w.RoleId == 3).ToList();
                clbWorker.Items.AddRange(workers.ToArray());

                var application = db.Applications.
                    Include(x => x.Equipment).
                    Include(x => x.TypeBroken).
                    Include(x => x.Client).
                    Include(x => x.Status).ToList();
              
                dataGridView1.DataSource = application;

                var statuses = db.Statuss.ToList();
                statuses.Insert(0, new Status { Title = "Все" }); 
                cmbStatus.DataSource = statuses;
                cmbStatus.DataSource = statuses;
                cmbStatus.DisplayMember = "Title"; 
                cmbStatus.ValueMember = "Id";

                toolStripStatusLabel1.Text = $"Кол-во заявок: {application.Count}";
                var completedApplicationsCount = application.Count(a => a.Status.Title == "Выполнено");
                toolStripStatusLabel2.Text = $"Всего выполнено заявок: {completedApplicationsCount}";
            }               
        }
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var formAdd = new AddApplication();
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                using (var db = new MyDBContext())
                {
                    db.Applications.Add(formAdd.Application);
                    db.SaveChanges();
                }
                Init();
            }
        }
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var application = dataGridView1.SelectedRows[0].DataBoundItem as Context.Models.Application; 

            if (application != null)
            {
                using (var db = new MyDBContext())
                {
                    var applicationDB = db.Applications.FirstOrDefault(x => x.Id == application.Id); 
                    var formAdd = new AddApplication(applicationDB); 
                                                          
                    if (formAdd.ShowDialog() == DialogResult.OK)
                    {
                        db.SaveChanges();
                        Init();
                    }
                }
            }
        }

        private void смотретьПолнуюИнформациюToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            var infoApplication = selectedRow.DataBoundItem as Context.Models.Application;

            if (infoApplication != null)
            {
                using (var db = new MyDBContext())
                {
                    var application = db.Applications
                                        .Include(a => a.Status)
                                        .Include(a => a.Client)
                                        .Include(a => a.Equipment)
                                        .Include(a => a.TypeBroken)
                                        .FirstOrDefault(a => a.Id == infoApplication.Id);

                    var report = db.Reports
                                    //.Include(r => r.SparesCounts)
                                    //.Include(sc => sc.SparesType)
                                    .FirstOrDefault(r => r.ApplicationId == application.Id);

                    if (application != null && report != null)
                    {
                        var formInfoApplication = new InfoApplicationcs(application, report);
                        if (formInfoApplication.ShowDialog() == DialogResult.OK)
                        {
                            db.SaveChanges();
                            Init();
                        }
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Заявка еще не имеет отчета!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Stop );            }
        }             

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            //ДЕНИС
        }

        private void cmbStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
             string selectedStatus = cmbStatus.SelectedItem.ToString();

             if (selectedStatus == "Все")
             {
                 using (var db = new MyDBContext())
                 {
                     var applications = db.Applications
                         .Include(a => a.Equipment)
                         .Include(a => a.TypeBroken)
                         .Include(a => a.Client)
                         .Include(a => a.Status)
                         .ToList();

                     dataGridView1.DataSource = applications;
                 }
             }
             else
             {
                 Status status = cmbStatus.SelectedItem as Status;
                 if (status == null)
                     return;

                 using (var db = new MyDBContext())
                 {
                     var filteredApplications = db.Applications
                         .Where(a => a.StatusId == status.Id)
                         .Include(a => a.Equipment)
                         .Include(a => a.TypeBroken)
                         .Include(a => a.Client)
                         .Include(a => a.Status)
                         .ToList();

                     dataGridView1.DataSource = filteredApplications;
                 }
             }           
        }

        private void txtSearch_TextChanged(object sender, System.EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower(); 

            using (var db = new MyDBContext())
            {
                var filteredApplications = db.Applications
                    .Include(a => a.Equipment)
                    .Include(a => a.TypeBroken)
                    .Include(a => a.Client)
                    .Include(a => a.Status)
                    .Where(a =>
                        a.Equipment.Title.ToLower().Contains(searchText) || 
                        a.TypeBroken.Title.ToLower().Contains(searchText) || 
                        a.Client.FullName.ToLower().Contains(searchText) || 
                        a.Description.ToLower().Contains(searchText)) 
                    .ToList();

                dataGridView1.DataSource = filteredApplications;

                toolStripStatusLabel1.Text = $"Кол-во записей: {filteredApplications.Count}";
            }
        }
    }
}
