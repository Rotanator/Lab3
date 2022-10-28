using GlossaryLibrary;

// Creates Glossary folder on app start if it doesn't already exist

string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
string glossaryFolder = Path.Combine(folder, "Glossary");

Directory.CreateDirectory(glossaryFolder);




WordList exampleWordList = WordList.LoadList("test");



Action<string[]> showTranslation = (stringArr) =>
{
    foreach (var s in stringArr) 
    {
        Console.Write($"{s} ");
    }
    Console.WriteLine();
};

//Word wordToPractice = exampleWordList.GetWordToPractice();
//exampleWordList.List(3, showTranslation);
//exampleWordList.Save();



switch (args.ElementAtOrDefault(0)?.ToLower())
{
    case "-lists":
        ListAllWordListsinGlossary();
        break;
    case "-new":
        CreateNewList();
        break;
    case "-add":
        AddNewWord();
        break;
    case "-remove":
        RemoveWord();
        break;
    case "-words":
        ListAllWords();
        break;
    case "-count":
        CountAllWords();
        break;
    case "-practice":
        PracticeWords();
        break;
    case null:
    default:
        PrintConsoleAppParameters();
        break;
}





void ListAllWordListsinGlossary()
{
    string[] fileNameList = WordList.GetLists();

    foreach (var fileName in fileNameList)
    {
        Console.WriteLine(fileName);
    }
}

void CreateNewList()
{
    if (args.Length < 4)
    {
        PrintConsoleAppInvalidCreateNewList();
        return;
    }

    string createListName = args[1].ToLower();
    string[] createListLanguages = args.Skip(2).ToArray();
    createListLanguages = Array.ConvertAll(createListLanguages, x => x.ToLower());

    WordList newWordList = new WordList(createListName, createListLanguages);
    newWordList.Save();
}

void AddNewWord()
{
    if (args.Length < 2)
    {
        PrintConsoleAppInvalidAmountOfArguments();
        return;
    }

    string wordListName = args[1].ToLower();
    WordList? wordList = WordList.LoadList(wordListName);

    if (wordList == null)
    {
        PrintConsoleAppInvalidListName();
        return;
    }

    Console.WriteLine("Press enter (empty line) to stop adding new words/translations");

    string[] wordListLanguages = wordList.Languages;
    string[] newWordToAdd = new string[wordListLanguages.Length];

    int amountOfWordsAdded = 0;
    bool stop = false;
    while (!stop)
    {
        for (int i = 0; i < wordListLanguages.Length; i++)
        {
            string currentLanguage = wordListLanguages[i];
            Console.Write($"Add new word ({currentLanguage}):");
            string newWordTranslation = Console.ReadLine() ?? string.Empty;

            if (newWordTranslation == null || newWordTranslation == string.Empty)
            {
                stop = true;
                break;
            }

            newWordToAdd[i] = newWordTranslation;
        }
        
        if (stop) { break; }
        wordList.Add(newWordToAdd);
        amountOfWordsAdded++;
    }
    wordList.Save();
    Console.WriteLine($"{amountOfWordsAdded} word(s) have been added to list '{wordListName}'");

}

void RemoveWord()
{
    if (args.Length < 4)
    {
        PrintConsoleAppInvalidAmountOfArguments();
        return;
    }

    string wordListName = args[1].ToLower();
    WordList? wordList = WordList.LoadList(wordListName);

    if (wordList == null)
    {
        PrintConsoleAppInvalidListName();
        return;
    }

    string removeWordFromLanguage = args[2].ToLower();
    string[] wordsToRemove = args.Skip(3).ToArray();
    wordsToRemove = Array.ConvertAll(wordsToRemove, x => x.ToLower());

    int indexOfLanguage = Array.IndexOf(wordList.Languages, removeWordFromLanguage);
    if (indexOfLanguage == -1)
    {
        Console.WriteLine("Language could not be found, please make sure language provided exists within the list");
        return;
    }

    Console.WriteLine($"The following word(s) were removed from list '{wordListName}'");

    int amountOfWordsRemoved = 0;

    foreach (string wordToRemove in wordsToRemove)
    {
        bool wasWordRemoved = wordList.Remove(indexOfLanguage, wordToRemove);
        if (wasWordRemoved) { Console.WriteLine($"- {wordToRemove}"); amountOfWordsRemoved++; }
    }
    if (amountOfWordsRemoved <= 0) { Console.WriteLine("No words removed"); }
    wordList.Save();
}

void ListAllWords()
{
    if (args.Length < 2)
    {
        PrintConsoleAppInvalidAmountOfArguments();
        return;
    }
    string wordListName = args[1].ToLower();
    WordList? wordList = WordList.LoadList(wordListName);

    if (wordList == null)
    {
        PrintConsoleAppInvalidListName();
        return;
    }

    string? sortByLanguage = args.ElementAtOrDefault(2)?.ToLower();
    int sortByTranslation = 0;

    if (!String.IsNullOrEmpty(sortByLanguage))
    {
        sortByTranslation = Array.IndexOf(wordList.Languages, sortByLanguage);
        if (sortByTranslation == -1)
        {
            Console.WriteLine("Could not find language, proceeding without sortByLanguage");
            sortByTranslation = 0;
        }
    }

    wordList.List(sortByTranslation, showTranslation);
}

void CountAllWords()
{
    if (args.Length < 2)
    {
        PrintConsoleAppInvalidAmountOfArguments();
        return;
    }

    string wordListName = args[1].ToLower();
    WordList? wordList = WordList.LoadList(wordListName);

    if (wordList == null)
    {
        PrintConsoleAppInvalidListName();
        return;
    }

    Console.WriteLine($"There are {wordList.Count()} word(s) in {wordListName}");
}

void PracticeWords()
{
    if (args.Length < 2)
    {
        PrintConsoleAppInvalidAmountOfArguments();
        return;
    }

    string wordListName = args[1].ToLower();
    WordList? wordList = WordList.LoadList(wordListName);
    

    if (wordList == null)
    {
        PrintConsoleAppInvalidListName();
        return;
    }

    string[] wordListLanguages = wordList.Languages;

    int totalTranslationAttempts = 0;
    int correctTranslations = 0;

    bool stop = false;
    while (!stop)
    {
        Word currentWord = wordList.GetWordToPractice();

        string currentFromLanguage = wordListLanguages[currentWord.FromLanguage];
        string currentToLanguage = wordListLanguages[currentWord.ToLanguage];

        string currentTranslationFromLanguage = currentWord.Translations[currentWord.FromLanguage];
        string currentTranslationToLanguage = currentWord.Translations[currentWord.ToLanguage];



        Console.WriteLine($"Translate the {currentFromLanguage} word '{currentTranslationFromLanguage}' to {currentToLanguage}.");
        string userInputTranslation = Console.ReadLine() ?? String.Empty;

        if (String.IsNullOrEmpty(userInputTranslation) || userInputTranslation.Trim().Length == 0)
        {
            Console.WriteLine("exiting");
            Console.WriteLine($"{correctTranslations}/{totalTranslationAttempts} correct guesses");
            return;
        }


        if (userInputTranslation == currentTranslationToLanguage)
        {
            Console.WriteLine("correct");
            correctTranslations++;
        } else
        {
            Console.WriteLine("incorrect");
        }
        totalTranslationAttempts++;
        Console.WriteLine($"{correctTranslations}/{totalTranslationAttempts} correct guesses");
    }
}








void PrintConsoleAppInvalidListName()
{
    Console.WriteLine("Unable to load a list with provided list name.");
}

void PrintConsoleAppInvalidAmountOfArguments()
{
    Console.WriteLine("Not enough arguments to perform requested command.");
}

void PrintConsoleAppInvalidCreateNewList()
{
    Console.WriteLine("Not enough arguments to create a new list.");
    Console.WriteLine("This command requires atleast <list name> and 2 languages to create a new word list.");
    Console.WriteLine("-new <list name> <language 1> <language 2> .. <langauge n> \n");
    Console.WriteLine("Example:");
    Console.WriteLine("-new examplelist english swedish");
}

void PrintConsoleAppParameters()
{
    Console.WriteLine("Use any of the following parameters:  \n\n" +
        "-lists  \n" +
        "-new <list name> <language 1> <language 2> .. <langauge n> \n" +
        "-add <list name> \n" +
        "-remove <list name> <language> <word 1> <word 2> .. <word n>  \n" +
        "-words <listname> <sortByLanguage> \n" +
        "-count <listname> \n" +
        "practice <listname>");
}









//string appName = AppDomain.CurrentDomain.FriendlyName;
//Console.WriteLine(appName);
