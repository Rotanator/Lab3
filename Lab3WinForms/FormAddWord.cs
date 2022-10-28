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
    public partial class FormAddWord : Form
    {
        public WordList? currentWordList;
        public string[]? AddWordTranslations { get; private set; }

        private TextBox[]? currentTextBoxArray;
        
        public FormAddWord(WordList wordList)
        {
            currentWordList = wordList;
            InitializeComponent();
        }

        private void FormAddWord_Load(object sender, EventArgs e)
        {   
            if (currentWordList == null)
            {
                MessageBox.Show("Unable to load Word List to add words to!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            TextBox[] textBoxArray = new TextBox[currentWordList.Languages.Length];
            for (int i = 0; i < textBoxArray.Count(); i++)
            {
                textBoxArray[i] = new TextBox();
            }

            int offset = 0;
            foreach(TextBox txt in textBoxArray)
            {
                Controls.Add(txt);
                txt.Location = new Point(75, 25 + (offset*25));
                Label label = new Label();
                Controls.Add(label);
                label.Text = currentWordList.Languages[offset];
                label.Location = new Point(10, 25 + (offset * 25));
                offset++;
            }

            currentTextBoxArray = textBoxArray;
        }

        private void buttonAddTranslations_Click(object sender, EventArgs e)
        {
            if (currentTextBoxArray == null || currentWordList == null) 
            { 
                this.DialogResult = DialogResult.Cancel;
                this.Close(); 
                return; 
            }

            AddWordTranslations = new string[currentWordList.Languages.Length];
            
            for (int i = 0; i < AddWordTranslations.Length; i++)
            {
                AddWordTranslations[i] = currentTextBoxArray[i].Text;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
