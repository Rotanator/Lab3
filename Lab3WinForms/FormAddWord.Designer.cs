namespace Lab3WinForms
{
    partial class FormAddWord
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
            this.buttonAddTranslations = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAddTranslations
            // 
            this.buttonAddTranslations.Location = new System.Drawing.Point(227, 420);
            this.buttonAddTranslations.Name = "buttonAddTranslations";
            this.buttonAddTranslations.Size = new System.Drawing.Size(135, 23);
            this.buttonAddTranslations.TabIndex = 0;
            this.buttonAddTranslations.Text = "Add Translations";
            this.buttonAddTranslations.UseVisualStyleBackColor = true;
            this.buttonAddTranslations.Click += new System.EventHandler(this.buttonAddTranslations_Click);
            // 
            // FormAddWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 455);
            this.Controls.Add(this.buttonAddTranslations);
            this.Name = "FormAddWord";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormAddWord_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonAddTranslations;
    }
}