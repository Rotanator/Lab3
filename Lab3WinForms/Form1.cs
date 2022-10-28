using GlossaryLibrary;
using System.Data;

namespace Lab3WinForms
{
    public partial class Form1 : Form
    {
        public string? currentFileName = null;
        public WordList? currentWordList = null;
        public int currentWordCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAddWord_Click(object sender, EventArgs e)
        {
            if (currentWordList == null) { NewWordList(); return; }
            FormAddWord form = new FormAddWord(currentWordList);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK && form.AddWordTranslations != null)
            {
                currentWordList.Add(form.AddWordTranslations);
                currentWordList.Save();
                DisplayWordList(currentFileName);
            } else { return; }

        }

        private void buttonRemoveWord_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DisplayWordList(string wordListName)
        {

            Action<string[]> showTranslation = (stringArr) =>
            {
                foreach (string s in stringArr)
                {
                    richTextBoxWordList.Text += string.Format("{0,-20}", s);
                }
                richTextBoxWordList.Text += Environment.NewLine;
            };

            WordList? wordList = WordList.LoadList(wordListName);

            if (wordList == null)
            {
                var result = MessageBox.Show($"Unable to load word list!", "Error", MessageBoxButtons.OK);
                return;
            }

            
            richTextBoxWordList.Clear();
            wordList.List(0, showTranslation);

            currentWordList = wordList;
            currentWordCount = wordList.Count();
            currentFileName = wordListName;

            labelWordCountAmount.Text = currentWordCount.ToString();
            this.Text = wordListName;
        }

        private void NewWordList()
        {
            FormNewWordList form = new FormNewWordList();
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (form.Languages == null || form.Languages.Length < 2)
                {
                    MessageBox.Show($"Incorrectly provided Languages, must provide atleast 2 languages", "Error", MessageBoxButtons.OK);
                    return;
                }
                WordList newWordList = new WordList(form.Title, form.Languages);
                newWordList.Save();
                DisplayWordList(form.Title);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();

            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select file to open";
            

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileNameWithExtension = Path.GetFileName(openFileDialog.FileName);
                currentFileName = fileNameWithExtension.Substring(0, fileNameWithExtension.IndexOf("."));

                currentWordList = WordList.LoadList(currentFileName);

                DisplayWordList(currentFileName);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewWordList();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentWordList == null) { NewWordList(); return; } 
            else
            {
                currentWordList.Save();
                MessageBox.Show($"Word List {currentWordList.Name} has been successfully saved!", "Saved!", MessageBoxButtons.OK);
            }
        }

        private void labelWordCountAmount_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}