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
    public partial class Comment : Form
    {
        private int Id;

        public Comment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with the id of the task.
        /// </summary>
        /// <param name="id"></param>
        public Comment(int id):this()
        {
            this.Id = id;
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add the comment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommentOk_Click(object sender, EventArgs e)
        {
            using (var db = new WorkingTimeEntities())
            {
                Task task = db.Tasks.First(x => x.Id == Id);
                task.Comment = textBox1.Text;
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
