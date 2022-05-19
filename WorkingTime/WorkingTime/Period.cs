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
    public partial class Period : Form
    {
        private int UserId;
        private int flag = 0;

        public Period()
        {
            InitializeComponent();
        }

        public Period(int id):this()
        {
            this.UserId = id;
            this.flag = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            if (flag == 1)
            {
                DoneTasks doneTasks1 = new DoneTasks(startDate, endDate, UserId);
                doneTasks1.ShowDialog();
            }
            else
            {
                DoneTasks doneTasks = new DoneTasks(startDate, endDate);
                doneTasks.ShowDialog();
            }
            flag = 0;
        }
    }
}
