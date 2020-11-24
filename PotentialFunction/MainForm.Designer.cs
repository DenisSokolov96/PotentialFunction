namespace PotentialFunction
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.обучитьНаФайлеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.распознатьНаФайлеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обучитьНаФайлеToolStripMenuItem,
            this.распознатьНаФайлеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(449, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // обучитьНаФайлеToolStripMenuItem
            // 
            this.обучитьНаФайлеToolStripMenuItem.Name = "обучитьНаФайлеToolStripMenuItem";
            this.обучитьНаФайлеToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.обучитьНаФайлеToolStripMenuItem.Text = "Обучить на файле...";
            this.обучитьНаФайлеToolStripMenuItem.Click += new System.EventHandler(this.обучитьНаФайлеToolStripMenuItem_Click);
            // 
            // распознатьНаФайлеToolStripMenuItem
            // 
            this.распознатьНаФайлеToolStripMenuItem.Name = "распознатьНаФайлеToolStripMenuItem";
            this.распознатьНаФайлеToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.распознатьНаФайлеToolStripMenuItem.Text = "Распознать на файле...";
            this.распознатьНаФайлеToolStripMenuItem.Click += new System.EventHandler(this.распознатьНаФайлеToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 49);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(426, 389);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Инфо:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Классификатор по методу потенциальных функций";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem обучитьНаФайлеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem распознатьНаФайлеToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
    }
}

