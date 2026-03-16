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
            Text = $"Race Result [{MainForm.Event.EventName}]";
            listBox1.Items.Clear();

            chart1.Series.Clear();

            var series = new Series("s1");
            series.ChartType = SeriesChartType.Bar;

            var num = 1;

            foreach (var i in SimulationForm.participant.OrderByDescending(x => x.Speed))
            {
                var meter = SimulationForm.events.DistanceKm * 1000;
                var speed = i.Speed;

                var time = Convert.ToInt32(meter / speed);
                var timeSpan = TimeSpan.FromSeconds(Convert.ToInt32(meter / speed));

                listBox1.Items.Add($"{num++}. {i.Name} - Time: {timeSpan.Hours} hour, {timeSpan.Minutes} min, {timeSpan.Seconds} sec");

                series.LegendText = "";
                series.Points.AddXY(i.Name, time);             
            }   

            chart1.Series.Add(series);
            chart1.Series["s1"].Color = Color.AliceBlue;
            chart1.Series["s1"].Points[0].Color = Color.Gold;
            chart1.Series["s1"].Points[1].Color = Color.Silver;
            chart1.Series["s1"].Points[2].Color = Color.Brown;

            num = 1;
            foreach (var dp in chart1.Series["s1"].Points)
            {
                var ta = new TextAnnotation();
                ta.Text = $"{num++}th Position";
                ta.Alignment = ContentAlignment.MiddleRight;
                ta.AnchorDataPoint = dp;

                chart1.Annotations.Add(ta);
            }
        }
    }
}