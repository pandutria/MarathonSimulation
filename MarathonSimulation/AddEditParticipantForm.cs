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
    public partial class AddEditParticipantForm : Form
    {
        int id;
        DataBaseDataContext db = new DataBaseDataContext();
        public AddEditParticipantForm(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void AddEditParticipantForm_Load(object sender, EventArgs e)
        {
            if (id != -1)
            {
                Text = "Edit";
                var query = db.Participants.FirstOrDefault(x => x.ParticipantID == id);

                if (query != null)
                {
                    tbName.Text = query.Name;
                    tbAge.Text = query.Age.ToString();
                    tbSpeed.Text = query.Speed.ToString();
                }
            }
            else
            {
                Text = "Add";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new ParticipantListForm().Show();
            Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text == string.Empty || tbAge.Text == string.Empty || tbSpeed.Text == string.Empty)
            {
                MessageBox.Show("All field must be filled");
                return;
            }

            if (!tbAge.Text.All(x => char.IsDigit(x)))
            {
                MessageBox.Show("Age must be a number, cannot be filled with decimal numbers.");
                return;
            }

            if (tbAge.Text.Contains("."))
            {
                MessageBox.Show("Age must be a number, cannot be filled with decimal numbers.");
                return;
            }

            if (!tbSpeed.Text.ToString().Replace(".", "").All(x => char.IsDigit(x)))
            {
                MessageBox.Show("Speed must be a decimal number with maximum two zeros after the comma.");
                return;
            }

            if (tbSpeed.Text.Contains("."))
            {
                var speed = tbSpeed.Text.Split('.');

                if (speed[1].Length > 2)
                {
                    MessageBox.Show("Speed must be a decimal number with maximum two zeros after the comma.");
                    return;
                }
            }

            if (id != -1)
            {
                var queryUpdate = db.Participants.FirstOrDefault(x => x.ParticipantID == id);

                if (queryUpdate != null)
                {
                    queryUpdate.Name = tbName.Text;
                    queryUpdate.Age = Convert.ToInt32(tbAge.Text);
                    queryUpdate.Speed = Convert.ToDecimal(tbSpeed.Text);

                    db.SubmitChanges();
                    MessageBox.Show("Update data success");
                    new ParticipantListForm().Show();
                    Hide();
                    return;
                }
            }

            var query = new Participant();
            query.ParticipantID = id;
            query.EventID = MainForm.Event.EventID;
            query.Name = tbName.Text;
            query.Age = Convert.ToInt32(tbAge.Text);
            query.Speed = Convert.ToDecimal(tbSpeed.Text);

            db.Participants.InsertOnSubmit(query);
            db.SubmitChanges();
            MessageBox.Show("Add data success");
            new ParticipantListForm().Show();
            Hide();
        }
    }
}
