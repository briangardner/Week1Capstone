using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Capstone
{
    class Program
    {
        const string VOWELS = "aeiou";
        const string NUMBERS = "0123456789";
        const string SYMBOLS = "@#$%^&*()<>{}\\|[]-=_+`~";
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to the Pig Latin Translator!");
            do
            {
                var input = GetEnglishPhrase();
                StringBuilder translatedText = new StringBuilder();
                foreach (var word in input.Split(' '))
                {
                    if (string.IsNullOrWhiteSpace(word))
                    {
                        continue;
                    }
                    translatedText.Append($"{GetWord(word)} ");
                }
                Console.WriteLine($"{translatedText}");
                if (!ShouldContinue())
                {
                    break;
                }
            } while (true);
        }

        private static string GetWord(string word)
        {
            var punctuation = ",.?!";
            if (punctuation.IndexOf(word[word.Length - 1]) >= 0)
            {
                //recursively remove punctuation, then reappend it back.
                return GetWord(word.Substring(0, word.Length - 1)) + word[word.Length-1];
            }
            if (ContainsNumbersOrSymbols(word))
            {
                return word;
            }
            if (VOWELS.IndexOf(word[0]) >= 0)
            {
                return word + "way";
            }
            return GetPigLatinWord(word);
        }

        private static bool ContainsNumbersOrSymbols(string input)
        {
            foreach (var character in input)
            {
                //if numbers or symbols 
                if (NUMBERS.IndexOf(character) >=0 || SYMBOLS.IndexOf(character) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetPigLatinWord(string word)
        {
            var indexToMove = GetIndexOfFirstVowel(word);
            if (indexToMove < 0)
            {
                indexToMove = 1;
            }
            var leftPart = word.Substring(0, indexToMove );
            var rightPart = word.Substring(indexToMove);
            return rightPart + leftPart + "ay";
        }

        private static int GetIndexOfFirstVowel(string input)
        {
            const string vowels = "aeiou";
            for (var i = 0; i < input.Length; ++i)
            {
                if (vowels.IndexOf(char.ToLower(input[i])) >= 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool ShouldContinue()
        {
            do
            {
                Console.WriteLine("Translate another line? y/n: ");
                var input = char.ToLower( Console.ReadKey().KeyChar);
                if (input == 'y' || input == 'n')
                {
                    return input == 'y';
                }
                Console.WriteLine("\nThat is not a valid option.\n");
            } while (true);
        }

        private static string GetEnglishPhrase()
        {
            do
            {
                Console.Write("\nEnter a line to be translated: ");
                var input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Please enter text.\n");
                continue;

            } while (true);

        }
    }
}
