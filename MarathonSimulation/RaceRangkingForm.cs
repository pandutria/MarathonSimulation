using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace MarathonSimulation
{
    public partial class RaceRangkingForm : Form
    {
        public RaceRangkingForm()
        {
            InitializeComponent();
        }

        private void RaceRangkingForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            var num = 1;

            foreach (var i in SimulationForm.participant)
            {
                var meter = SimulationForm.events.DistanceKm * 1000;
                var speed = i.Speed;

                var time = TimeSpan.FromSeconds(Convert.ToInt32(meter / speed));
                
                listBox1.Items.Add($"{num++}. {i.Name} - Time: {time.Hours} hour, {time.Minutes} min, {time.Seconds} sec");
            }

            chart1.Series.Clear();

            var series = new Series("");
            series.ChartType = SeriesChartType.Bar;

            foreach (var i in SimulationForm.events.Participants.OrderByDescending(x => x.Speed))
            {
                var meter = SimulationForm.events.DistanceKm * 1000;
                var speed = i.Speed;

                var time = Convert.ToInt32(meter / speed);
                series.Points.AddXY(i.Name, time);
            }

            chart1.Series.Add(series);
        }
    }
}
