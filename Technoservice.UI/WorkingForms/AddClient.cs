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

namespace Technoservice.UI.WorkingForms
{
    public partial class AddClient : Form
    {
        public Client Client { get; set; }
        public AddClient()
        {
            InitializeComponent();
            Client = new Client();          
        }
        public AddClient(Client client) : this()
        {
            Client = client;

            txtFIO.Text = client.FullName;
            txtPhone.Text = client.Phone;
            txtMail.Text = client.Email;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Client.FullName = txtFIO.Text;
            Client.Phone = txtPhone.Text;
            Client.Email = txtMail.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
