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
using Technoservice.Context.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Technoservice.UI.GridForms
{
    public partial class InfoApplicationcs : Form
    {
        public Context.Models.Application Application { get; set; }
        public Report Report { get; set; }
        public InfoApplicationcs(Context.Models.Application application, Report report)
        {
            InitializeComponent();
            this.Application = application;
            this.Report = report;
            Init();
        }
        private void Init()
        {
            Context.Models.Application infoApplication = Report.Application;

            lblNumber.Text = infoApplication.Id.ToString();
            lblDate.Text = infoApplication.CreatedDate.ToString();
            lblStatus.Text = infoApplication.Status.Title;
            lblClient.Text = infoApplication.Client.FullName;
            lblEquipment.Text = infoApplication.Equipment.Title;
            lblTypeBroke.Text = infoApplication.TypeBroken.Title;
            lblDescription.Text = infoApplication.Description;
            lblReason.Text = Report.Reason;
            lblTime.Text = Report.CompilationTime.ToString();
            lblPrice.Text = Report.Price.ToString();

            lblPriority.Text = infoApplication.Priority;

            if (Report.SparesCounts != null && Report.SparesCounts.Count > 0)
            {
                SparesCount sparesCount = Report.SparesCounts.First();
                lblCount.Text = sparesCount.Count.ToString();

                if (sparesCount.SparesType != null)
                {
                    lblSpares.Text = sparesCount.SparesType.Title;
                }
            }
        }

        private void InfoApplicationcs_Load(object sender, EventArgs e)
        {

        }
    }
}
