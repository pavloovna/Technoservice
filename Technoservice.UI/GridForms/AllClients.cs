using System;
using System.Linq;
using System.Windows.Forms;
using Technoservice.Context;
using Technoservice.Context.Models;
using Technoservice.UI.WorkingForms;

namespace Technoservice.UI.GridForms
{
    public partial class AllClients : Form
    {
        public Client Client { get; set; }
        public AllClients()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false; 
            Initilaze();
            if (WorkToUser.Worker != null)
            {
                var userRoleId = WorkToUser.Worker.RoleId;
                btnAdd.Visible =
                btnEdit.Visible =
                btnRemove.Visible = WorkToUser.CompareRole(1) || WorkToUser.CompareRole(2);
            }
        }
        public void Initilaze()
        {
            using (var db = new MyDBContext())
            {
                var clients = db.Clients.ToList();
                dataGridView1.DataSource = clients;

                toolStripStatusLabel1.Text = $"Кол-во записей: {clients.Count}";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var formAdd = new AddClient(); 
            if (formAdd.ShowDialog() == DialogResult.OK) 
            {
                using (var db = new MyDBContext())
                {
                    db.Clients.Add(formAdd.Client); 
                    db.SaveChanges();
                }
                Initilaze();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var client = dataGridView1.SelectedRows[0].DataBoundItem as Client; 

            if (client != null)
            {
                using (var db = new MyDBContext())
                {
                    var clientDB = db.Clients.FirstOrDefault(x => x.Id == client.Id);
                    var formAdd = new AddClient(clientDB); 

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
            var client = dataGridView1.SelectedRows[0].DataBoundItem as Client;

            if (client != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить этого клиента?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var db = new MyDBContext())
                    {
                        var clientDB = db.Clients.FirstOrDefault(x => x.Id == client.Id);

                        db.Clients.Remove(clientDB);
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
                var search = db.Clients.Where(a =>
                        a.FullName.ToLower().Contains(searchText) ||
                        a.Phone.ToLower().Contains(searchText) ||
                        a.Email.ToLower().Contains(searchText)).ToList();

                dataGridView1.DataSource = search;

                toolStripStatusLabel1.Text = $"Кол-во записей: {search.Count}";
            }
        }
    }
}
