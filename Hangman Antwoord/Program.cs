namespace Hangman
{
    class Program
    {
        static string correctWord;
        static char[] letters;
        static Player player; 
        static List<string> words;

        static void Main(string[] args)
        {
           //top level exception handling
            try 
            { 
                StartGame();
                PlayGame();
                EndGame();
            }
            catch 
            {
                WriteLine("Oops, something went wrong");
            }

        }

        private static void StartGame()
        {
           
            //Vervang alle arrays met lists
            List<string> words; 
            //try
            //{
            //   // words = new List<string> { File.ReadAllLines("https://otv-hangman.azurewebsites.net/api/GetWord").ToList() };
            //}
            //catch 
            //{ 
                //Catch vir iets wat jy nie verwag nie
                // vervang alle arays met lists
                words = new List<string> { "dog", "tree", "cat" };
            //}
            
            Random random = new Random();
            correctWord = words[random.Next(0,words.Count)];

            letters = new char[correctWord.Length];
            //letters = new List<CorrectWord>();
            
            for (int i = 0; i < correctWord.Length; i++)
                letters[i] = '-';
    
            AskForUsersName();
        }

        static void AskForUsersName()
        {
            WriteLine("Enter your name:");
            string input = ReadLine();
 

            if (input.Length >= 2)
                player = new Player(input);
            else
            {
                // The user entered an invalid name
                AskForUsersName();
            }
        }

        private static void PlayGame()
        {
            do
            {
                Clear();
                DisplayMaskedWord();
                char guessedLetter = AskForLetter();
                CheckLetter(guessedLetter);
            } while (correctWord != new string(letters));

            Clear();
        }

        private static void CheckLetter(char guessedLetter)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (guessedLetter == correctWord[i])
                {
                    letters[i] = guessedLetter;
                    player.Score++;
                }
            }
        }

        static void DisplayMaskedWord()
        {
            foreach (char c in letters)
                Write(c);

            WriteLine();
        }

        static char AskForLetter()
        {
            string input;
            do
            {
                WriteLine("Enter a letter:");
                input = ReadLine();
            } while (input.Length != 1);

            var letter = input[0];

            if (!player.GuessedLetters.Contains(letter))
                player.GuessedLetters.Add(letter);

            return letter;
        }

        private static void EndGame()
        {
            WriteLine("Congrats!");
            WriteLine($"Thanks for playing {player.Name}. The Correct word was {correctWord}");
            WriteLine($"Guesses:{player.GuessedLetters.Count} Score:{player.Score}");

           

        }
    }
}
