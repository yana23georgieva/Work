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
    public partial class AddTask : Form
    {
        private int Id;
        private int flag = 0;

        /// <summary>
        /// Constructor for add.
        /// </summary>
        public AddTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for edit.
        /// </summary>
        /// <param name="id"></param>
        public AddTask(int id) : this()
        {
            this.Id = id;
            flag = 1;
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Button add or edit if the task does not exist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new WorkingTimeEntities())
            {
                if (textBox1.Text != "")
                {
                    Task task = db.Tasks.FirstOrDefault(x => x.TaskName == textBox1.Text);

                    if (task != null)
                    {
                        MessageBox.Show("The task exist.", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (flag == 1)
                    {
                        Task currentTask = db.Tasks.First(x => x.Id == this.Id);
                        currentTask.TaskName = textBox1.Text;
                        db.SaveChanges();
                        return;
                    }

                    Task newTask = new Task
                    {
                        TaskName = textBox1.Text,
                        StartTime = null,
                        EndTime = null,
                        TimeForTheTask = null,
                        Comment = ""
                    };
                    db.Tasks.Add(newTask);
                    db.SaveChanges();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have to add task name", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Load the name when is for edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTask_Load_1(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                using (var db = new WorkingTimeEntities())
                {
                    Task currentTask = db.Tasks.First(x => x.Id == this.Id);
                    textBox1.Text = currentTask.TaskName;
                    this.Text = "Edit task";
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
