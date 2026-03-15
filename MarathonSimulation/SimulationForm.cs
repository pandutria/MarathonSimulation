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
    public partial class SimulationForm : Form
    {
        DataBaseDataContext db = new DataBaseDataContext();
        public SimulationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
            Hide();
        }

        public static Event events;
        public static List<Participant> participant;

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            Text = $"Simulation [{MainForm.Event.EventName}]";
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;
            

            events = db.Events.FirstOrDefault(x => x.EventID == MainForm.Event.EventID);

            if (events != null)
            {
                participant = db.Participants.Where(x => x.EventID == events.EventID).ToList();

                foreach(var i in participant)
                {
                    flowLayoutPanel1.Controls.Add(new UcParticipant(i, events, timer1));
                }
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            var allDone = true;
            foreach (Control ctl in flowLayoutPanel1.Controls)
            {
                if (ctl is UcParticipant uc)
                {
                    if (uc.isDone == false)
                    {
                        allDone = false;
                        break;
                    }
                }
            }

            if (allDone)
            {
                timer1.Stop();
                new RaceRangkingForm().Show();
                Hide();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
