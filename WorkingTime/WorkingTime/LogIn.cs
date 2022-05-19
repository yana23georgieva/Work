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
    public partial class LogIn : Form
    {
        /// <summary>
        /// Empty defaut constructor. It loads all elements.
        /// </summary>
        public LogIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// If you confirm close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to quit?", "Quit",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Button Ok. Check if it exists this user to start the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                using (var db = new WorkingTimeEntities())
                {
                    char[] passwordItems = textBox2.Text.ToCharArray();
                    string password = "";

                    for (int i = 0; i < passwordItems.Length; i++)
                    {
                        password += (char)((int)passwordItems[i] + 3);
                    }

                    User currentUser = db.Users.Where(x => x.Username == textBox1.Text).
                        FirstOrDefault(x => x.Password == password);

                    if (currentUser == null)
                    {
                        MessageBox.Show("The password or username are incorrect.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Main main = new Main(currentUser.Id);
                        main.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have to enter username and password!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
