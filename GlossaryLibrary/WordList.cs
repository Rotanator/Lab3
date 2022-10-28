using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryLibrary
{
    public class WordList
    {
        private List<Word> Words { get; }
        public string Name { get; }
        public string[] Languages { get; private set; }

        private static string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string glossaryFolder = Path.Combine(folder, "Glossary");

        public WordList(string name, params string[] languages)
        {
            Name = name;
            Languages = languages;
            Words = new List<Word>();
        }

        public static string[] GetLists()
        {

            //Unprocessed string array of all paths to .dat files in Glossary folder
            string[] filesInGlossary = Directory.GetFiles(glossaryFolder, "*.dat");


            //Removes .dat from all names
            //Replacing all entries in filesInGlossary to only strings of the .dat file names (excluding extension .dat)
            for (int i = 0; i < filesInGlossary.Length; i++)
            {
                string currentFile = filesInGlossary[i];
                string nameOfCurrentFile = Path.GetFileName(currentFile);

                filesInGlossary[i] = nameOfCurrentFile.Substring(0, nameOfCurrentFile.IndexOf("."));
            }

            return filesInGlossary;
        }

        public static WordList? LoadList(string name)
        {
            string currentGlossaryList = Path.Combine(glossaryFolder, $"{name}.dat");

            string firstLine;
            //List<Word> loadedListOfWords = new List<Word>();

            if (File.Exists(currentGlossaryList))
            {
                using (StreamReader reader = new StreamReader(currentGlossaryList))
                {
                    //Reading first line for languages
                    firstLine = reader.ReadLine() ?? String.Empty;

                    if (firstLine == String.Empty)
                    {
                        return null;
                    }
                    string[] languages = firstLine.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    WordList loadedWordList = new WordList(name, languages);

                    while (!reader.EndOfStream)
                    {
                        var currentLine = reader.ReadLine();
                        string[] currentLineToTranslations = currentLine.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        loadedWordList.Add(currentLineToTranslations);
                    }

                    return loadedWordList;
                }
            }
            else { return null; }

        }
        public void Save()
        {
            string fileName = $"{Name}.dat";
            string savedListPath = Path.Combine(glossaryFolder,fileName);

            FileStream fs = File.Create(savedListPath);
            fs.Close();

            using (StreamWriter sw = new StreamWriter(savedListPath))
            {
                sw.WriteLine($"{String.Join(";", Languages)};");
                foreach (Word word in this.Words)
                {
                    sw.WriteLine($"{String.Join(";", word.Translations)};");
                }
            }

        }

        public void Add(params string[] translations)
        {
            if (Words.Count == 0)
            {
                Words.Add(new Word(translations));
            }
            else
            {
                if (Words.First().Translations.Length != translations.Length)
                {
                    throw new ArgumentException("Attempted to add an incorrect amount of translations to an instance of WordList");
                }
                else
                {
                    Words.Add(new Word(translations));
                }
            }
        }

        public bool Remove(int translation, string word)
        {
            for (int i = 0; i < Words.Count; i++)
            {
                Word w = Words[i];
                if (w.Translations[translation] == word)
                {
                    Words.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public int Count()
        {
            return Words.Count;
        }

        public void List(int sortByTranslation, Action<string[]> showTranslation)
        {
            string[] sortedLanguages = Languages;

            int[] indexesKeys = Enumerable.Range(0, sortedLanguages.Length).ToArray();


            //Since they rest of the keys in indexesKeys will be sorted in alphabetical order, we make sure to prepend the
            //language & the index the language corresponds to to the start of the array

            //By doing that we can use Array.Sort with overload that allows us to choose the range of values we wish to sort to,
            //letting us exclude the language we want to show up first from being sorted alphabetically.

            var tmp = indexesKeys[0];
            indexesKeys[0] = indexesKeys[sortByTranslation];
            indexesKeys[sortByTranslation] = tmp;

            var tmplang = sortedLanguages[0];
            sortedLanguages[0] = sortedLanguages[sortByTranslation];
            sortedLanguages[sortByTranslation] = tmplang;

            Array.Sort(sortedLanguages, indexesKeys, 1, sortedLanguages.Length - 1);
            Languages = sortedLanguages;
            showTranslation(sortedLanguages);


            //Now indexesKeys will be sorted the same way that our languagues are, which lets us implement indexesKeys
            //for all the Words and their translations so the right translation of a word will be in the same index
            //as the language it belongs to.

            for (int i = 0; i < Words.Count; i++)
            {
                Word currentWord = Words[i];
                string[] wordTranslations = currentWord.Translations;
                wordTranslations = indexesKeys.Select(index => wordTranslations[index]).ToArray();

                Words[i] = new Word(wordTranslations);
                showTranslation(wordTranslations);

            }
            
        }

        public Word GetWordToPractice()
        {
            Random random = new Random();
            int setFromLanguage;
            int setToLanguage;

            int wordToPracticeIndex = random.Next(0, Words.Count);
            Word wordToPractice = Words[wordToPracticeIndex];

            setFromLanguage = random.Next(0, Languages.Length);

            do
            {
                setToLanguage = random.Next(0, Languages.Length);
            } while (setFromLanguage == setToLanguage);

            Words[wordToPracticeIndex] = new Word(setFromLanguage, setToLanguage, wordToPractice.Translations);
            return Words[wordToPracticeIndex];
        }
    }
}