using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace enclara_Assessment
{
    class Program
    {
        public static void Main()
        {
            String input = "";
            int numPalindromeWords;
            int x = 0;
            int count;

            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Input a paragraph");
                Console.WriteLine("2. Input a character");
                Console.WriteLine("3. Exit ");
                Console.Write("User input: ");
                string userChoice = Console.ReadLine();
                if (userChoice.Length > 1)
                {
                    Console.WriteLine("Please enter one number.");
                }
                else if (!char.IsDigit(Convert.ToChar(userChoice)))
                {
                    Console.WriteLine("Please Enter a number.");
                }
                else
                {
                    x = Convert.ToInt32(userChoice);

                    if (x == 1)
                    {
                        Console.WriteLine("Please Enter your paragraph: ");
                        Console.Write("User input: ");
                        input = Console.ReadLine();
                        //Number of palindrome words
                        numPalindromeWords = GetNumOfPalindromeWords(input);
                        Console.WriteLine("Number of palindrome words: {0}", numPalindromeWords);

                        //Number of palindrome sentences
                        string[] sentences = CreateInputSentences(input);
                        count = GetNumOfPalindromeSentences(sentences);
                        Console.WriteLine("Number of palindrome sentences: " + count);

                        //List of Unique words:
                        int numOfUniqueWords = GetCountUniqeWords(input);



                    }
                    else if (x == 2)
                    {
                        if (!String.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Please enter a letter to find all the words containing that letter.");
                            Console.Write("User input: ");
                            List<string> words = new List<string>();
                            string t = Console.ReadLine();
                            if (t.Length > 1)
                            {
                                Console.WriteLine("Please input a character!");
                            }
                            else
                            {
                                //gets words that contain the letter entered by the user
                                words = GetWordscontainingChar(Convert.ToChar(t), input);
                                if (words.Count > 0)
                                {
                                    Console.WriteLine("Words containing \'" + t + "\': ");
                                    foreach (var word in words)
                                    {
                                        Console.WriteLine(word);
                                    }
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("No words found containing the letter \'" + t + "\'! ");
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Please select 1 to enter a paragraph since there is no paragraph to search");
                        }
                    }
                    else if (x == 3)
                    {
                        Console.WriteLine("Thank you. Goodbye!");
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid option!");
                    }
                    count = 0;
                }


            }
            while (x != 3);
        }
        public static List<string> GetEachWords(string input)
        {
            List<string> words = new List<string>();
            input = input + " ";

            string word = "";
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];

                //Getting all the words
                if (ch != ' ' && ch != '.' && ch != ',' && ch != ':' && ch != ';')
                {
                    word += ch;
                }
                else
                {
                    words.Add(word);
                    word = "";
                }
            }
            return words;


        }

        //method to check for palindrome
        public static bool CheckPalindrome(string word)
        {
            int num = word.Length;
            word = word.ToLower();
            for (int i = 0; i < num; i++, num--)
            {
                if (word[i] != word[num - 1])
                {
                    return false;
                }
            }
            return true;
        }

        // Method to count palindrome words  
        public static int GetNumOfPalindromeWords(string input)
        {
            int count = 0;
            List<string> words = new List<string>();
            words = GetEachWords(input);
            //Checks to see if the word is a palindrome
            foreach (var word in words)
            {
                if (!String.IsNullOrEmpty(word) && CheckPalindrome(word))
                {
                    count++;
                }
            }
            return count;
        }
        //method to separate each sentences from paragraph.
        public static string[] CreateInputSentences(string input)
        {
            char[] endings = { '.', '!', '?' };

            input = input.ToLower();
            string[] sentences = input.Split(endings);
            return sentences;
        }
        //Retruns the number of palindrome sentences 
        public static int GetNumOfPalindromeSentences(string[] sentences)
        {
            string str;
            int count = 0;
            foreach (var sentence in sentences)
            {
                str = Regex.Replace(sentence, @"[^\w\d\s]", "");
                str = str.Replace(" ", "");
                if (!String.IsNullOrEmpty(str) && CheckPalindrome(str))
                {
                    count++;
                }
            }
            return count;
        }

        //returns the words that have the letter entered by the user within it.
        public static List<string> GetWordscontainingChar(char c, string input)
        {
            List<string> x = new List<string>();
            List<string> words = new List<string>();

            x = GetEachWords(input);
            foreach (var word in x)
            {
                if (word.Contains(char.ToString(c)))
                {
                    words.Add(word);
                }
            }
            return words;

        }

        //counts each unique words
        public static int GetCountUniqeWords(string input)
        {
            IEnumerable<string> words = GetEachWords(input);
            IEnumerable<string> uniqueWords = words.GroupBy(w => w).Where(g => g.Count() == 1).Select(g => g.Key);
            return uniqueWords.Count();
        }
    }
}
/*
 * When the program is first started a menu pops up. The user can can select option 1, 2 or 3. 
 * option 1 lets user input a paragraph, option 2 lets user input a character and option 3 lets user exit the program.
 * If a user selects option 1, user is asked with to input a paragraph. once user inputs a paragraph the program finds the palindrome words, palindrome sentences and unique words that appear in the sentence.
 * According to the dictionary defination of unique is "being the only one of its kind; unlike anything else". So, This program finds the words that only appear once in the paragraph.
 * if a user selects option 2 the program checks to see if a paragraph exist. If not then user is asked to select option 1 to enter a paragraph. If the user has already inputted paragraph then the program finds all the words that contain the letter inputted by the user.
 * User has to input 3 to exit program otherwise it will keep asking the user to input a valid option.

    */
