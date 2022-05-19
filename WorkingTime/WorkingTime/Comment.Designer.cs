namespace WorkingTime
{
    partial class Comment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CommentOk = new System.Windows.Forms.Button();
            this.CommentCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comment:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(111, 11);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(316, 122);
            this.textBox1.TabIndex = 1;
            // 
            // CommentOk
            // 
            this.CommentOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CommentOk.Location = new System.Drawing.Point(136, 160);
            this.CommentOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CommentOk.Name = "CommentOk";
            this.CommentOk.Size = new System.Drawing.Size(88, 30);
            this.CommentOk.TabIndex = 2;
            this.CommentOk.Text = "Ok";
            this.CommentOk.UseVisualStyleBackColor = true;
            this.CommentOk.Click += new System.EventHandler(this.CommentOk_Click);
            // 
            // CommentCancel
            // 
            this.CommentCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CommentCancel.Location = new System.Drawing.Point(318, 160);
            this.CommentCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CommentCancel.Name = "CommentCancel";
            this.CommentCancel.Size = new System.Drawing.Size(88, 30);
            this.CommentCancel.TabIndex = 3;
            this.CommentCancel.Text = "Cancel";
            this.CommentCancel.UseVisualStyleBackColor = true;
            this.CommentCancel.Click += new System.EventHandler(this.CommentCancel_Click);
            // 
            // Comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(444, 210);
            this.Controls.Add(this.CommentCancel);
            this.Controls.Add(this.CommentOk);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Comment";
            this.Text = "Comment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button CommentOk;
        private System.Windows.Forms.Button CommentCancel;
    }
}