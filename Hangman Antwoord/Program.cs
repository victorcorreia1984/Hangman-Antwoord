using System;

namespace Hangman
{
    class Program
    {
        static string correctWord = "hangman";
        static string name;
        static int numberOfGuesses;

        static void Main(string[] args)
        {
            StartGame();
            PlayGame();
            EndGame();
        }

        private static void StartGame()
        {
            Console.WriteLine("Starting the game...");
            AskForUsersName();
        }

        static void AskForUsersName()
        {
            Console.WriteLine("Enter your name:");
            string input = Console.ReadLine();

            if (input.Length >= 2)
                name = input;
            else
            {
                // The user entered an invalid name
                AskForUsersName();
            }
        }

        private static void PlayGame()
        {
            DisplayMaskedWord();
            AskForLetter();
        }

        static void DisplayMaskedWord()
        {
            foreach (char c in correctWord)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }

        static void AskForLetter()
        {
            string input;
            do
            {
                Console.WriteLine("Enter a letter:");
                input = Console.ReadLine();
            } while (input.Length != 1);

            numberOfGuesses++;
        }

        private static void EndGame()
        {
            Console.WriteLine("Game over...");
            Console.WriteLine($"Thanks for playing {name}");
            Console.WriteLine($"Guesses:{numberOfGuesses}");
        }
    }
}
