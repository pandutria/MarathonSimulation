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
    public partial class UcParticipant : UserControl
    {
        Participant participant;
        Event events;
        Timer timer;
        public bool isDone = false;
        Random random;
        public UcParticipant(Participant participant, Event events, Timer timer, Random random)
        {
            InitializeComponent();
            this.participant = participant;
            this.events = events;
            this.timer = timer;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.random = random;
        }

        int time;
        int distance;

        private void UcParticipant_Load(object sender, EventArgs e)
        {
            lblName.Text = participant.Name;
            lblName.ForeColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            var meter = events.DistanceKm * 1000;
            var speed = participant.Speed;

            time = Convert.ToInt32(meter / speed);
            distance = Convert.ToInt32(speed * time);

            pb.Minimum = 0;
            pb.Maximum = distance;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pb.Increment(Convert.ToInt32(distance / time) * 100);

            if (pb.Value == pb.Maximum)
            {
                isDone = true;
            }

            lblName.Left += Convert.ToInt32((distance / time));

            if (lblName.Left == lblName.Width)
            {
                lblName.Left += 0;
            }
        }
    }
}