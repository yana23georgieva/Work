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
    public partial class DoneTasks : Form
    {
        private DateTime startDate;
        private DateTime endDate;
        private int UserId;
        private int flag = 0;

        /// <summary>
        /// Empty constructor. Do not use it.
        /// </summary>
        public DoneTasks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for admin.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public DoneTasks(DateTime start, DateTime end):this()
        {
            this.startDate = start;
            this.endDate = end;
        }

        /// <summary>
        /// Constructor when you are not admin.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="id"></param>
        public DoneTasks(DateTime start, DateTime end, int id):this(start, end)
        {
            flag = 1;
            this.UserId = id;
        }

        /// <summary>
        /// Load every done task betwen the given dates and if you are not admin loads ony the 
        /// current user tasks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoneTasks_Load(object sender, EventArgs e)
        {
            using (var db = new WorkingTimeEntities())
            {
                foreach (var item in db.Tasks)
                {
                    if (item.EndTime != null)
                    {
                        if (item.StartTime >= startDate && item.StartTime <= endDate)
                        {
                            if(flag == 1)
                            {
                                if (item.UserId == UserId)
                                {
                                    User user = db.Users.First(x => x.Id == item.UserId);
                                    string name = $"{user.FirstName} {user.FamilyName}";
                                    dataGridView1.Rows.Add(item.Id, item.TaskName, item.StartTime,
                                        item.EndTime, item.TimeForTheTask, item.Comment, name);
                                }
                            }
                            else
                            {
                                User users = db.Users.First(x => x.Id == item.UserId);
                                string name = $"{users.FirstName} {users.FamilyName}";
                                dataGridView1.Rows.Add(item.Id, item.TaskName, item.StartTime,
                                    item.EndTime, item.TimeForTheTask, item.Comment, name);
                            }
                        }
                    }
                }
            }

            flag = 0;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
