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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'workingTimeDataSet1.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.workingTimeDataSet1.User);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            AddEditUser addEditUser = new AddEditUser();
            if (addEditUser.ShowDialog() == DialogResult.OK)
            {
                this.Users_Load(sender, e);
            }
        }

        private void EditUser_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            int userId = int.Parse(dataGridView1[0, rowIndex].Value.ToString());
            AddEditUser addEditUser = new AddEditUser(userId);
            if (addEditUser.ShowDialog() == DialogResult.OK)
            {
                this.Users_Load(sender, e);
            }
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this" +
                " user?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var db = new WorkingTimeEntities())
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    int userId = int.Parse(dataGridView1[0, rowIndex].Value.ToString());
                    User user = db.Users.First(x => x.Id == userId);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    this.Users_Load(sender, e);
                }
            }
        }
    }
}
