namespace Lab3WinForms
{
    partial class FormPracticeWords
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
            this.labelTranslateQuery = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelTranslationTally = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTranslateQuery
            // 
            this.labelTranslateQuery.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelTranslateQuery.Location = new System.Drawing.Point(-3, 51);
            this.labelTranslateQuery.Name = "labelTranslateQuery";
            this.labelTranslateQuery.Size = new System.Drawing.Size(555, 125);
            this.labelTranslateQuery.TabIndex = 0;
            this.labelTranslateQuery.Text = "Translate";
            this.labelTranslateQuery.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(150, 96);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // labelTranslationTally
            // 
            this.labelTranslationTally.AutoSize = true;
            this.labelTranslationTally.Location = new System.Drawing.Point(265, 132);
            this.labelTranslationTally.Name = "labelTranslationTally";
            this.labelTranslationTally.Size = new System.Drawing.Size(24, 15);
            this.labelTranslationTally.TabIndex = 2;
            this.labelTranslationTally.Text = "0/0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "End Practice";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPracticeWords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 247);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelTranslationTally);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelTranslateQuery);
            this.Name = "FormPracticeWords";
            this.Text = "FormPracticeWords";
            this.Load += new System.EventHandler(this.FormPracticeWords_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelTranslateQuery;
        private TextBox textBox1;
        private Label labelTranslationTally;
        private Button button1;
    }
}