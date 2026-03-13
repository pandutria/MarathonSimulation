using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarathonSimulation
{
    public partial class ParticipantListForm : Form
    {
        DataBaseDataContext db = new DataBaseDataContext();
        public ParticipantListForm()
        {
            InitializeComponent();
        }

        private void ParticipantListForm_Load(object sender, EventArgs e)
        {
            Text = $"Participants [{MainForm.Event.EventName}]";
            dgvData.Columns.Clear();

            var query = db.Participants.Where(x => x.EventID == MainForm.Event.EventID)
                .Select(x => new
                {
                    x.ParticipantID,
                    x.EventID,
                    x.Name,
                    x.Age,
                    x.Speed
                }).ToList();

            dgvData.DataSource = query;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
            Hide();
        }
    }
}
