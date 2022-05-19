using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingTime
{
    public partial class Main : Form
    {
        /// <summary>
        /// Private variables for the userID, task name and stopwatch for the time.
        /// </summary>
        private int UserId;
        static Stopwatch sw = new Stopwatch();
        private string taskName;

        /// <summary>
        /// Empty default constructor. It loads every element.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with user Id.
        /// </summary>
        /// <param name="id"></param>
        public Main(int id):this()
        {
            this.UserId = id;
        }

        /// <summary>
        /// The things we want to see when we load the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'workingTimeDataSet4.Possion' table. You can move, or remove it, as needed.
            this.possionTableAdapter.Fill(this.workingTimeDataSet4.Possion);
            // TODO: This line of code loads data into the 'workingTimeDataSet2.User' table. You can move, or remove it, as needed.
            this.userTableAdapter1.Fill(this.workingTimeDataSet2.User);
            // TODO: This line of code loads data into the 'workingTimeDataSet.Task' table. You can move, or remove it, as needed.
            this.taskTableAdapter.Fill(this.workingTimeDataSet.Task);
            using (var db = new WorkingTimeEntities())
            {
                User user = db.Users.First(x => x.Id == UserId);
                string logedUser = $"Name: {user.FirstName} {user.FamilyName}";
                label1.Text = logedUser;
                label5.Text = logedUser;

                foreach (var item in db.Tasks)
                {
                    if (item.StartTime == null && !taskComboBox.Items.Contains(item.TaskName))
                    {
                        taskComboBox.Items.Add(item.TaskName);
                    }
                }

                Possion possion = db.Possions.
                            First(x => x.Id == user.PossionId);
                if (possion.PossionName != "admin")
                {
                    tabControl1.TabPages.Remove(tabPage3);
                    tabControl1.TabPages.Remove(tabPage4);
                    comboBox2.Visible = false;
                }
                else
                {
                    if (!comboBox2.Items.Contains("all"))
                    {
                        comboBox2.Items.Add("all");
                    }
                    foreach (var item in db.Tasks)
                    {
                        User currentUser = db.Users.FirstOrDefault(x => x.Id == item.UserId);

                        if (item.StartTime != null)
                        {
                            string name = currentUser.FirstName + " " + currentUser.FamilyName;
                            if (!comboBox2.Items.Contains(name))
                            {
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                    comboBox2.SelectedIndex = 0;
                    label6.Text = logedUser;
                    label7.Text = logedUser;
                }

                Pause.Enabled = false;
                Continue.Enabled = false;
                Finish.Enabled = false;
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Button to turn back.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log out",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                LogIn logIn = new LogIn();
                logIn.Show();
            }
        }

        /// <summary>
        /// Button for start the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, EventArgs e)
        {
            if (taskComboBox.Text == "")
            {
                MessageBox.Show("You have to choose task first!", "Task", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            using (var db = new WorkingTimeEntities())
            {
                taskName = taskComboBox.SelectedItem.ToString();
                Task task = db.Tasks.First(x => x.TaskName == taskName);
                task.StartTime = DateTime.Now;
                db.SaveChanges();
                Start.Enabled = false;
                Pause.Enabled = true;
                Finish.Enabled = true;
                Back.Enabled = false;
                taskComboBox.Enabled = false;

                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                tabControl1.TabPages.Remove(tabPage4);
                tabControl1.TabPages.Remove(tabPage5);

                Quit.Enabled = false;
                sw.Start();
            }
        }

        /// <summary>
        /// Load the form with all users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Users_Click(object sender, EventArgs e)
        {
            Users user = new Users();
            user.ShowDialog();
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pause_Click(object sender, EventArgs e)
        {
            Pause.Enabled = false;
            Continue.Enabled = true;
            sw.Stop();
            using (var db = new WorkingTimeEntities())
            {
                User user = db.Users.First(x => x.Id == this.UserId);
                if (user.PossionId == 1)
                {
                    tabControl1.TabPages.Add(tabPage2);
                    tabControl1.TabPages.Add(tabPage3);
                    tabControl1.TabPages.Add(tabPage4);
                    tabControl1.TabPages.Add(tabPage5);
                }
            }
        }

        /// <summary>
        /// Continue with the timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Continue_Click(object sender, EventArgs e)
        {
            Pause.Enabled = true;
            Continue.Enabled = false;
            sw.Start();
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage4);
        }

        /// <summary>
        /// Finish with the task. Save the time when finished and calculate the time for the task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Finish_Click(object sender, EventArgs e)
        {
            using (var db = new WorkingTimeEntities())
            {
                sw.Stop();
                Start.Enabled = true;
                Back.Enabled = true;
                Pause.Enabled = false;
                Continue.Enabled = false;
                Finish.Enabled = false;
                taskComboBox.Enabled = true;

                tabControl1.TabPages.Add(tabPage2);
                User user = db.Users.First(x => x.Id == this.UserId);
                if (user.PossionId == 1)
                {
                    tabControl1.TabPages.Add(tabPage3);
                    tabControl1.TabPages.Add(tabPage4);
                    tabControl1.TabPages.Add(tabPage5);
                }

                Quit.Enabled = true;
                taskComboBox.Items.RemoveAt(taskComboBox.SelectedIndex);
                Task task = db.Tasks.First(x => x.TaskName == taskName);
                task.EndTime = DateTime.Now;
                TimeSpan ts = sw.Elapsed;
                DateTime day = DateTime.Now;
                DateTime workingTime = new DateTime(day.Year, day.Month, day.Day, ts.Hours, 
                    ts.Minutes, ts.Seconds);
                task.TimeForTheTask = workingTime;
                task.UserId = this.UserId;
                db.SaveChanges();

                Comment comment = new Comment(task.Id);
                comment.ShowDialog();
            }
            this.Main_Load(sender, e);
        }

        /// <summary>
        /// When you click on the combobox load all data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Main_Load(sender, e);
        }

        /// <summary>
        /// Quit button. If you choose yes, close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Quit", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// When the form is closing, close the hole application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Open the form for adding user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            if (addEditUser.ShowDialog() == DialogResult.OK)
            {
                this.Main_Load(sender, e);
            }
        }

        /// <summary>
        /// Get the user id and open the form for editting user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUser_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            int userId = int.Parse(dataGridView1[0, rowIndex].Value.ToString());
            AddEditUser addEditUser = new AddEditUser(userId);
            if (addEditUser.ShowDialog() == DialogResult.OK)
            {
                this.Main_Load(sender, e);
            }
        }

        /// <summary>
        /// Delete user from db and update the table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this" +
                " user?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var db = new WorkingTimeEntities())
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    if (dataGridView1[0, rowIndex].Value == null)
                    {
                        MessageBox.Show("You have to choose correct line!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int userId = int.Parse(dataGridView1[0, rowIndex].Value.ToString());                    
                    User user = db.Users.First(x => x.Id == userId);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    this.Main_Load(sender, e);
                }
            }
        }

        /// <summary>
        /// Open new form for adding task and update the table if it was click on yes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTask_Click(object sender, EventArgs e)
        {
            AddTask add = new AddTask();
            if (add.ShowDialog() == DialogResult.OK)
            {
                this.Main_Load(sender, e);
            }
        }

        /// <summary>
        /// Get task id and open the form for editting task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTask_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView2.SelectedCells[0].RowIndex;
            int taskId = int.Parse(dataGridView2[0, rowIndex].Value.ToString());
            AddTask add = new AddTask(taskId);
            if (add.ShowDialog() == DialogResult.OK)
            {
                this.Main_Load(sender, e);
            }
        }

        /// <summary>
        /// Remove the task from db and load the table again.
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
                    int rowIndex = dataGridView2.SelectedCells[0].RowIndex;
                    if (dataGridView2[0, rowIndex].Value == null)
                    {
                        MessageBox.Show("You have to choose correct line!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int taskId = int.Parse(dataGridView2[0, rowIndex].Value.ToString());
                    Task task = db.Tasks.First(x => x.Id == taskId);
                    db.Tasks.Remove(task);
                    db.SaveChanges();
                    this.Main_Load(sender, e);
                }
            }
        }

        /// <summary>
        /// Search tasks between the dates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Main_Load(sender, e);
            string[] firstHour = maskedTextBox1.Text.Split(':');
            string[] secondHour = maskedTextBox2.Text.Split(':');
            DateTime dateTime1 = new DateTime(dateTimePicker1.Value.Year,
                dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, int.Parse(firstHour[0]),
                int.Parse(firstHour[1]), 0);
            DateTime dateTime2 = new DateTime(dateTimePicker2.Value.Year,
                dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, int.Parse(secondHour[0]),
                int.Parse(secondHour[1]), 0);

            if (dateTime1 > dateTime2)
            {
                MessageBox.Show("The first date has to be smaller than the next!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new WorkingTimeEntities())
            {
                EmptyDataGridView3();
                User currentUser = db.Users.First(x => x.Id == this.UserId);
                Possion currentUPossion = db.Possions.First(x => x.Id == currentUser.PossionId);
                foreach (var item in db.Tasks)
                {
                    if (item.EndTime != null)
                    {
                        if (item.StartTime >= dateTime1 && item.StartTime <= dateTime2)
                        {
                            if (currentUPossion.PossionName != "admin" && 
                                item.UserId != currentUser.Id)
                            {
                                continue;
                            }
                            User users = db.Users.First(x => x.Id == item.UserId);
                            string name = $"{users.FirstName} {users.FamilyName}";
                            dataGridView3.Rows.Add(item.Id, item.TaskName, item.StartTime,
                                item.EndTime, item.TimeForTheTask, item.Comment, name);
                        }
                    }
                }

                Possion possion = db.Possions.First(x => x.Id == currentUser.PossionId);
                if (possion.PossionName == "admin")
                {
                    comboBox2.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Empty every line in dataGridView3.
        /// </summary>
        private void EmptyDataGridView3()
        {
            while (dataGridView3.RowCount != 1)
            {
                dataGridView3.Rows.RemoveAt(0);
            }
        }

        /// <summary>
        /// Choose another element in comboBox2.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "all")
            {
                this.button2_Click(sender, e);
            }
            else
            {
                EmptyDataGridView3();
                using (var db = new WorkingTimeEntities())
                {
                    foreach (var item in db.Tasks)
                    {
                        User currentUser = db.Users.FirstOrDefault(x => x.Id == item.UserId);

                        if (item.StartTime != null)
                        {
                            if (item.StartTime >= dateTimePicker1.Value &&
                            item.StartTime <= dateTimePicker2.Value)
                            {
                                string name = currentUser.FirstName + " " + currentUser.FamilyName;
                                if (name == comboBox2.SelectedItem.ToString())
                                {
                                    dataGridView3.Rows.Add(item.Id, item.TaskName, item.StartTime,
                                    item.EndTime, item.TimeForTheTask, item.Comment, name);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Refresh the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Main_Load(sender, e);
        }

        /// <summary>
        /// Refresh the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Main_Load(sender, e);
        }

        /// <summary>
        /// Add new possion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            AddEditPossion addEditPossion = new AddEditPossion();
            if (addEditPossion.ShowDialog() == DialogResult.OK)
            {
                this.Main_Load(sender, e);
            }
        }

        /// <summary>
        /// Edit the possions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            using (var db = new WorkingTimeEntities())
            {
                int rowIndex = dataGridView4.SelectedCells[0].RowIndex;
                string possion = dataGridView4[1, rowIndex].Value.ToString();
                AddEditPossion addEditPossion = new AddEditPossion(possion);
                if (addEditPossion.ShowDialog() == DialogResult.OK)
                {
                    this.Main_Load(sender, e);
                }
            }
        }

        /// <summary>
        /// Refresh the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Main_Load(sender, e);
        }

        /// <summary>
        /// Delete possion and refresh tables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this" +
                " possion?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var db = new WorkingTimeEntities())
                {
                    int rowIndex = dataGridView4.SelectedCells[0].RowIndex;
                    if (dataGridView4[0, rowIndex].Value == null)
                    {
                        MessageBox.Show("You have to choose correct line!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int possionId = int.Parse(dataGridView4[0, rowIndex].Value.ToString());
                    Possion possion = db.Possions.First(x => x.Id == possionId);
                    db.Possions.Remove(possion);
                    db.SaveChanges();
                    this.Main_Load(sender, e);
                }
            }
        }
    }
}
