using System;
using System.Linq;
using System.Windows.Forms;
using Technoservice.Context;
using Technoservice.Context.Models;

namespace Technoservice.UI.WorkingForms
{
    public partial class AddApplication : Form
    {
        public Context.Models.Application Application { get; set; }
        public AddApplication()
        {
            InitializeComponent();
            Application = new Context.Models.Application();
            Init();
        }

        public AddApplication(Context.Models.Application application) : this()
        {
            Application = application;

            cmbEquipment.SelectedItem = cmbEquipment.Items.Cast<Equipment>().FirstOrDefault(x => x.Id == Application.EquipmentId);
            cmbBroke.SelectedItem = cmbBroke.Items.Cast<TypeBroken>().FirstOrDefault(x => x.Id == Application.TypeBrokenId);
            txtDescription.Text = application.Description;
            cmbClient.SelectedItem = cmbClient.Items.Cast<Client>().FirstOrDefault(x => x.Id == Application.ClientId);
            cmbStatus.SelectedItem = cmbStatus.Items.Cast<Status>().FirstOrDefault(x => x.Id == Application.StatusId);
            int index = cmbPriority.Items.IndexOf(application.Priority);
            cmbPriority.SelectedIndex = index;
        }

        private void Init()
        {
            using (var db = new MyDBContext())
            {
                cmbEquipment.Items.Clear();
                cmbEquipment.Items.Add("Выбрать оборудование");
                cmbEquipment.Items.AddRange(db.Equipments.AsNoTracking().ToArray());
                cmbEquipment.SelectedIndex = 0;

                cmbBroke.Items.Clear();
                cmbBroke.Items.Add("Выбрать тип неисправности");
                cmbBroke.Items.AddRange(db.TypeBrokens.AsNoTracking().ToArray());
                cmbBroke.SelectedIndex = 0;

                cmbClient.Items.Clear();
                cmbClient.Items.Add("Выбрать клиента");
                cmbClient.Items.AddRange(db.Clients.AsNoTracking().ToArray());
                cmbClient.SelectedIndex = 0;

                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("Выбрать статус");
                cmbStatus.Items.AddRange(db.Statuss.AsNoTracking().ToArray());
                cmbStatus.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Application.CreatedDate = DateTime.Today;
            Application.EquipmentId = ((Equipment)cmbEquipment.SelectedItem).Id;
            Application.TypeBrokenId = ((TypeBroken)cmbBroke.SelectedItem).Id;
            Application.Description = txtDescription.Text;
            Application.ClientId = ((Client)cmbClient.SelectedItem).Id;
            Application.Status = (Status)cmbStatus.SelectedItem;
            Application.Priority = cmbPriority.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            var formAdd = new AddClient();
            if (formAdd.ShowDialog() == DialogResult.OK)
            {
                using (var db = new MyDBContext())
                {
                    db.Clients.Add(formAdd.Client);
                    db.SaveChanges();
                }
                Init();
            }
        }

        private void AddApplication_Load(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
