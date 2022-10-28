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
    public partial class FormPracticeWords : Form
    {
        private WordList currentWordList;
        private Word currentWord;
        private string[] wordListLanguages;

        int totalTranslationAttempts = 0;
        int correctTranslations = 0;

        string currentFromLanguage = String.Empty;
        string currentToLanguage = String.Empty;
        string currentTranslationFromLanguage = String.Empty;
        string currentTranslationToLanguage = String.Empty;
        public FormPracticeWords(WordList wordList)
        {   
            currentWordList = wordList;
            currentWord = currentWordList.GetWordToPractice();
            wordListLanguages = currentWordList.Languages;
            InitializeComponent();
        }

        private void PracticeNewWord()
        {
            currentWord = currentWordList.GetWordToPractice();

            currentFromLanguage = wordListLanguages[currentWord.FromLanguage];
            currentToLanguage = wordListLanguages[currentWord.ToLanguage];

            currentTranslationFromLanguage = currentWord.Translations[currentWord.FromLanguage];
            currentTranslationToLanguage = currentWord.Translations[currentWord.ToLanguage];

            labelTranslateQuery.Text = $"Translate the {currentFromLanguage} word '{currentTranslationFromLanguage}' to {currentToLanguage}.";
            labelTranslationTally.Text = $"{correctTranslations}/{totalTranslationAttempts}";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string userInputTranslation = textBox1.Text;

                if (userInputTranslation == currentTranslationToLanguage)
                {
                    correctTranslations++;
                }
                totalTranslationAttempts++;
                PracticeNewWord();
                textBox1.Clear();
            }
                
        }

        private void FormPracticeWords_Load(object sender, EventArgs e)
        {
            PracticeNewWord();
        }
    }
}
