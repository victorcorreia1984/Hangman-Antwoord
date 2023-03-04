using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string wordToGuess = await GetWordToGuessAsync();
            List<char> guessedLetters = new List<char>();

            Console.WriteLine("Welcome to Hangman!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Word to guess:");
                Console.WriteLine(GetMaskedWord(wordToGuess, guessedLetters));

                Console.Write("Guess a letter: ");
                char letter = Console.ReadLine()[0];
                if (guessedLetters.Contains(letter))
                {
                    Console.WriteLine("You've already guessed that letter.");
                    continue;
                }

                guessedLetters.Add(letter);

                if (wordToGuess.Contains(letter))
                {
                    Console.WriteLine("Correct!");
                    if (HasWon(wordToGuess, guessedLetters))
                    {
                        Console.WriteLine("You win!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                    if (HasLost(guessedLetters))
                    {
                        Console.WriteLine("You lose. The word was: " + wordToGuess);
                        break;
                    }
                }
            }
        }

        static async Task<string> GetWordToGuessAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://otv-hangman.azurewebsites.net/api/GetWord");
                string responceString = await response.Content.ReadAsStringAsync();

                JObject obj = JObject.Parse(responceString);
                string word = obj.GetValue("Value").ToString();

                return word.Trim();
            }
        }

        static string GetMaskedWord(string word, List<char> guessedLetters)
        {
            string maskedWord = "";
            foreach (char c in word)
            {
                maskedWord += (guessedLetters.Contains(c) ? c : '_') + " ";
            }
            return maskedWord.Trim();
        }

        static bool HasWon(string word, List<char> guessedLetters)
        {
            foreach (char c in word)
            {
                if (!guessedLetters.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        static bool HasLost(List<char> guessedLetters)
        {
            return guessedLetters.Count >= 6;
        }
    }
}
