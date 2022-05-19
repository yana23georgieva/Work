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
    public partial class AddEditUser : Form
    {
        private int Id;
        private int flag = 0;

        /// <summary>
        /// Constructor for add user.
        /// </summary>
        public AddEditUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for edit user.
        /// </summary>
        /// <param name="id"></param>
        public AddEditUser(int id):this()
        {
            this.Id = id;
            this.flag = 1;
        }

        /// <summary>
        /// Button close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add or edit the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && 
                textBox4.Text != "")
            {
                using (var db = new WorkingTimeEntities())
                {
                    char[] passwordItems = textBox2.Text.ToCharArray();
                    string password = "";

                    for (int i = 0; i < passwordItems.Length; i++)
                    {
                        password += (char)((int)passwordItems[i] + 3);
                    }

                    if (flag == 1)//Edit
                    {
                        User user = db.Users.First(x => x.Id == Id);
                        user.Username = textBox1.Text;
                        user.Password = password;
                        user.FirstName = textBox3.Text;
                        user.FamilyName = textBox4.Text;
                        Possion possionN = db.Possions.
                            First(x => x.PossionName == (string)comboBox1.SelectedItem);
                        user.PossionId = possionN.Id;
                        db.SaveChanges();
                        return;
                    }

                    User username = db.Users.FirstOrDefault(x => x.Username == textBox1.Text);
                    if (username != null)
                    {
                        MessageBox.Show("This username already exist. Choose another.", "Username",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Possion possion = db.Possions.
                            First(x => x.PossionName == (string)comboBox1.SelectedItem);
                    User newUser = new User{
                        Username = textBox1.Text,
                        Password = password,
                        FirstName = textBox3.Text,
                        FamilyName = textBox4.Text,
                        PossionId = possion.Id
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("You have to fill all gaps!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load all data if it is edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditUser_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'workingTimeDataSet3.Possion' table. You can move, or remove it, as needed.
            this.possionTableAdapter.Fill(this.workingTimeDataSet3.Possion);

            if (flag == 1)
            {
                using (var db = new WorkingTimeEntities())
                {
                    User currentUser = db.Users.First(x => x.Id == this.Id);
                    textBox1.Text = currentUser.Username;

                    char[] passwordItems = currentUser.Password.ToCharArray();
                    string password = "";

                    for (int i = 0; i < passwordItems.Length; i++)
                    {
                        password += (char)((int)passwordItems[i] - 3);
                    }

                    Possion possion = db.Possions.
                            First(x => x.Id == currentUser.PossionId);
                    textBox2.Text = password;
                    textBox3.Text = currentUser.FirstName;
                    textBox4.Text = currentUser.FamilyName;
                    comboBox1.SelectedItem = possion.PossionName;
                    this.Text = "Edit User";
                }
            }
        }

        /// <summary>
        /// Open new form for adding possion.=
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            AddEditPossion addEditPossion = new AddEditPossion();
            if (addEditPossion.ShowDialog() == DialogResult.OK)
            {
                this.AddEditUser_Load(sender, e);
            }
        }
    }
}
