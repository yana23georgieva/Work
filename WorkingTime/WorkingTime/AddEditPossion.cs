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
    public partial class AddEditPossion : Form
    {
        int flag = 0;
        string possionName;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public AddEditPossion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with the possionName for edit.
        /// </summary>
        /// <param name="possion"></param>
        public AddEditPossion(string possion) : this()
        {
            this.possionName = possion;
            flag = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new WorkingTimeEntities())
            {
                if (textBox1.Text != "")
                {
                    Possion possion = db.Possions.FirstOrDefault(x => x.PossionName == textBox1.Text);

                    if (possion != null)
                    {
                        MessageBox.Show("The possion exist.", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (flag == 1)
                    {
                        Possion currentPossion = db.Possions.
                            First(x => x.PossionName == possionName);
                        currentPossion.PossionName = textBox1.Text;
                        db.SaveChanges();
                        return;
                    }

                    Possion newPossion = new Possion
                    {
                        PossionName = textBox1.Text,
                    };
                    db.Possions.Add(newPossion);
                    db.SaveChanges();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have to add possion!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// If it is edit load the data in textBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditPossion_Load(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                textBox1.Text = possionName;
                this.Text = "Edit possion";
            }
        }

        /// <summary>
        /// Cancel - close the current form.s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
