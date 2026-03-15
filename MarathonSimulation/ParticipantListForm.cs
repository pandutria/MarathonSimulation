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
            showData();
        }

        void showData()
        {
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

        int id = -1;

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                id = (int) dgvData.Rows[e.RowIndex].Cells["ParticipantID"].Value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            id = -1;
            new AddEditParticipantForm(id).Show();
            Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (id == -1)
            {
                MessageBox.Show("Select data first!");
                return;
            }
            new AddEditParticipantForm(id).Show();
            Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id == -1)
            {
                MessageBox.Show("Select data first!");
                return;
            }

            var query = db.Participants.FirstOrDefault(x => x.ParticipantID == id);

            if (query != null)
            {
                db.Participants.DeleteOnSubmit(query);
                db.SubmitChanges();
                MessageBox.Show("Delete data success");
                showData();
            }
        }
    }
}
