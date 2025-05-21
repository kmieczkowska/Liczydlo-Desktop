namespace liczydlo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            openCatalogButton = new Button();
            openNewFileButton = new Button();
            excel1 = new TextBox();
            excel2 = new TextBox();
            pathLabel = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            pictureBox1 = new PictureBox();
            toolTip1 = new ToolTip(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Location = new Point(6, 28);
            button1.Name = "button1";
            button1.Size = new Size(97, 23);
            button1.TabIndex = 0;
            button1.Text = "Wybierz plik 1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.Location = new Point(6, 71);
            button2.Name = "button2";
            button2.Size = new Size(97, 23);
            button2.TabIndex = 2;
            button2.Text = "Wybierz plik 2";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.White;
            button3.Location = new Point(16, 42);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "Scal";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // openCatalogButton
            // 
            openCatalogButton.BackColor = Color.White;
            openCatalogButton.Location = new Point(183, 80);
            openCatalogButton.Name = "openCatalogButton";
            openCatalogButton.Size = new Size(92, 39);
            openCatalogButton.TabIndex = 7;
            openCatalogButton.Text = "Otwórz folder";
            openCatalogButton.UseVisualStyleBackColor = false;
            openCatalogButton.Click += openCatalogButton_Click;
            // 
            // openNewFileButton
            // 
            openNewFileButton.BackColor = Color.White;
            openNewFileButton.Location = new Point(293, 80);
            openNewFileButton.Name = "openNewFileButton";
            openNewFileButton.Size = new Size(79, 39);
            openNewFileButton.TabIndex = 8;
            openNewFileButton.Text = "Otwórz plik";
            openNewFileButton.UseVisualStyleBackColor = false;
            openNewFileButton.Click += openNewFile_Click;
            // 
            // excel1
            // 
            excel1.BackColor = Color.White;
            excel1.Location = new Point(123, 28);
            excel1.Name = "excel1";
            excel1.ReadOnly = true;
            excel1.Size = new Size(379, 23);
            excel1.TabIndex = 9;
            // 
            // excel2
            // 
            excel2.BackColor = Color.White;
            excel2.Location = new Point(123, 71);
            excel2.Multiline = true;
            excel2.Name = "excel2";
            excel2.ReadOnly = true;
            excel2.ScrollBars = ScrollBars.Horizontal;
            excel2.Size = new Size(379, 23);
            excel2.TabIndex = 10;
            // 
            // pathLabel
            // 
            pathLabel.BackColor = Color.White;
            pathLabel.Location = new Point(123, 42);
            pathLabel.Name = "pathLabel";
            pathLabel.ReadOnly = true;
            pathLabel.Size = new Size(379, 23);
            pathLabel.TabIndex = 11;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(230, 157, 184);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(excel2);
            groupBox1.Controls.Add(excel1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(525, 116);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Pliki do scalenia";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(230, 157, 184);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(pathLabel);
            groupBox2.Controls.Add(openCatalogButton);
            groupBox2.Controls.Add(openNewFileButton);
            groupBox2.Location = new Point(12, 143);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(525, 137);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Plik wynikowy";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(550, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(304, 271);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 231, 231);
            ClientSize = new Size(866, 291);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Dziuniowe liczydło";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button openCatalogButton;
        private Button openNewFileButton;
        private TextBox excel1;
        private TextBox excel2;
        private TextBox pathLabel;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private PictureBox pictureBox1;
        private ToolTip toolTip1;
    }
}
