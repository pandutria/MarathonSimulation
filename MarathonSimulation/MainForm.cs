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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        DataBaseDataContext db = new DataBaseDataContext();
        public static Event Event;

        private void MainForm_Load(object sender, EventArgs e)
        {
            cbEvent.DataSource = db.Events;
            cbEvent.ValueMember = "EventID";
            cbEvent.DisplayMember = "EventName";
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Event = db.Events.FirstOrDefault(x => x.EventID == Convert.ToInt32(cbEvent.SelectedValue));
            new ParticipantListForm().Show();
            Hide();
        }

        private void btnSimulation_Click(object sender, EventArgs e)
        {
            Event = db.Events.FirstOrDefault(x => x.EventID == Convert.ToInt32(cbEvent.SelectedValue));

            if(Event != null)
            {
                new SimulationForm().Show();
                Hide();
            }

            
        }
    }
}
