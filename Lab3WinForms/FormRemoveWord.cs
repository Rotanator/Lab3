using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlossaryLibrary;

namespace Lab3WinForms
{
    public partial class FormRemoveWord : Form
    {
        private string[] Languages;
        public string WordToRemove { get; private set; }
        public int RemoveFromLanguage { get; private set; }
        public FormRemoveWord(string[] languages)
        {   
            Languages = languages;
            WordToRemove = String.Empty;
            RemoveFromLanguage = -1;
            InitializeComponent();
        }

        private void FormRemoveWord_Load(object sender, EventArgs e)
        {
            foreach (string s in Languages)
            {
                comboBoxLanguages.Items.Add(s);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            WordToRemove = textBoxRemoveWord.Text;
            RemoveFromLanguage = comboBoxLanguages.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
