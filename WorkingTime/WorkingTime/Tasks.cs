using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingTime
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the form with the information from the base.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tasks_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'workingTimeDataSet.Task' table. You can move, or remove it, as needed.
            this.taskTableAdapter.Fill(this.workingTimeDataSet.Task);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Add new task. And load the form again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTask_Click(object sender, EventArgs e)
        {
            AddTask add = new AddTask();
            if (add.ShowDialog() == DialogResult.OK)
            {
                this.Tasks_Load(sender, e);
            }
        }

        /// <summary>
        /// Button edit task. And load again the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTask_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            int taskId = int.Parse(dataGridView1[0, rowIndex].Value.ToString());
            AddTask add = new AddTask(taskId);
            if (add.ShowDialog() == DialogResult.OK)
            {
                this.Tasks_Load(sender, e);
            }
        }

        /// <summary>
        /// Delete the task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTask_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this" +
                " task?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var db = new WorkingTimeEntities())
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    int taskId = int.Parse(dataGridView1[0, rowIndex].Value.ToString());
                    Task task = db.Tasks.First(x => x.Id == taskId);
                    db.Tasks.Remove(task);
                    db.SaveChanges();
                    this.Tasks_Load(sender, e);
                }
            }
        }
    }
}
