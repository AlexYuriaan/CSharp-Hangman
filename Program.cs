namespace Hangman
{
    class Hangman
    {
        public static void Main(string[] args)
        {
            Random random = new Random();
            Console.Title = "Hangman";
            int cursorX = 0;
            int cursorY = 0;
            bool win = false;
            bool lose = false;
            string[] fireworks =

            {
                    @"                               *    *                                 ",
                    @"   *         '       *       .  *   '     .           * *             ",
                    @"                                                               '      ",
                    @"       *                *'          *          *        '             ",
                    @"   .           *               |               /                      ",
                    @"               '.         |    |      '       |   '     *             ",
                    @"                 \*        \   \             /                        ",
                    @"       '          \     '* |    |  *        |*                *  *    ",
                    @"            *      `.       \   |     *     /    *      '             ",
                    @"  .                  \      |   \          /               *          ",
                    @"     *'  *     '      \      \   '.       |                           ",
                    @"        -._            `                  /         *                 ",
                    @"  ' '      ``._   *                           '          .      '     ",
                    @"   *           *\*          * .   .      *                            ",
                    @"*  '        *    `-._                       .         _..:='        *",
                    @"             .  '      *       *    *   .       _.:--'               ",
                    @"          *           .     .     *         .-'         *            ",
                    @"   .               '             . '   *           *         .       ",
                    @"  *       ___.-=--..-._     *                '               '       ",
                    @"                                  *       *                          ",
                    @"                *        _.'  .'       `.        '  *             *  ",
                    @"     *              *_.-'   .'            `.               *         ",
                    @"                   .'                       `._             *  '     ",
                    @"   '       '                        .       .  `.     .              ",
                    @"       .                      *                  `                   ",
                    @"               *        '             '                          .   ",
                    @"     .                          *        .           *  *            ",
                    @"             *        .                                    '         ",


            };

            string[] wordArray =
            {
                "shirt",
                "piano",
                "user",
                "county",
                "moment",
                "hair",
                "tennis",
                "law",
                "thanks",
                "heart",
                "debt",
                "drawer",
                "church",
                "power",
                "pizza",
                "family",
                "hall",
                "method",
                "nature",
                "city",
            };
            char guess = 'a';
            string displayWord = "";
            string word = "";
            List<string> guesses = new List<string>();
            string[] shortWords = File.ReadAllLines(@"shortWords.txt");
            string[] mediumWords = File.ReadAllLines(@"mediumWords.txt");
            string[] longWords = File.ReadAllLines(@"longWords.txt");
            int incorrectGuesses = 0;
            bool done = false;
            int difficulty = 0; // 0 - Easy, 1 - Normal, 2 - Hard, 3 - Insane
            int level = 1;
            string playAgain = "";
            bool cheatMode = false;
            while (true)
            {
                cheatMode = false;
                done = false;
                incorrectGuesses = 0;
                level = 1;
                word = "";
                displayWord = "";
                 
                Console.WriteLine("Welcome to Hangman! Please Select Your Difficulty: ");
                Console.WriteLine("0 - Easy (7 Lives) | 1 - Normal (5 lives) | 2 - Hard (3 lives) | 3 - Insane | 4 - Cheat Mode");
                bool inputFlag = true;
                while (inputFlag)
                {
                    if (!int.TryParse(Console.ReadLine(), out difficulty) || difficulty > 4 || difficulty < 0)
                    {
                        Console.WriteLine("Invalid Input, Please enter a number between 0 and 4.");
                    }
                    else
                    {
                        if (difficulty == 4)
                        {
                            cheatMode = true;
                            difficulty -= 2;
                        }
                        inputFlag = false;
                    }
                }







                // TODO: make the code read the txt files

                word = generateWord(level, shortWords, mediumWords, longWords, displayWord).Split(",")[0];
                displayWord = generateDisplayWord(word);
                DrawHangman(incorrectGuesses, difficulty, displayWord);



                Console.WriteLine($"You are on level {level} of 3!");
                while (!done)
                {

                    Console.WriteLine("Please enter your guess!");

                    guess = userGuessLetter(displayWord);
                    if (!CheckGuess(word, guess))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Clear();
                        incorrectGuesses++;

                        Console.Beep(400, 500);

                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                    }
                    Console.Clear();
                    displayWord = EditDisplayText(word, displayWord, guess);
                    DrawHangman(incorrectGuesses, difficulty, displayWord);
                    if (cheatMode)
                    {
                        Console.WriteLine($"CHEAT MODE: {word}");
                    }
                    win = DidUserWin(displayWord);
                    lose = DidUserLose(incorrectGuesses, difficulty);

                    if (win)
                    {
                        if (level == 3)
                        {
                            done = true;
                            for (int i = 0; i < 5; i++)
                            {
                                Thread.Sleep(1000);
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Clear();
                                for (int t = 0; t < fireworks.Length; t++)
                                {
                                    Console.WriteLine(fireworks[t]);
                                }
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.Clear();
                                for (int t = 0; t < fireworks.Length; t++)
                                {
                                    Console.WriteLine(fireworks[t]);
                                }
                                


                            }
                            inputFlag = true;
                            Console.WriteLine("Play Again? - y/n");
                            while (inputFlag)
                            {
                                playAgain = Console.ReadLine();
                                if (playAgain == "y")
                                {
                                    inputFlag = false;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                }
                                else if (playAgain == "n")
                                {
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.WriteLine("Please enter either y or n.");
                                }
                            }




                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You got the word!");
                            level++;
                            Console.WriteLine($"You are on level {level} of 3!");
                            word = generateWord(level, shortWords, mediumWords, longWords, displayWord).Split(",")[0];
                            displayWord = generateDisplayWord(word);
                            DrawHangman(incorrectGuesses, difficulty, displayWord);
                            incorrectGuesses = 0;
                        }




                    }
                    else if (lose)
                    {
                        inputFlag = true;
                        Console.WriteLine("<|-------------------------------|>");
                        Console.WriteLine($"You Lose! - The word was {word}");
                        Console.WriteLine("<|-------------------------------|>");
                        done = true;
                        Console.WriteLine("Play Again? - y/n");
                        while (inputFlag)
                        {
                            playAgain = Console.ReadLine();
                            if (playAgain == "y")
                            {
                                inputFlag = false;
                            }
                            else if (playAgain == "n")
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.WriteLine("Please enter either y or n.");
                            }
                        }
                        
                    }

                }
            



            }


        }




        static void DrawHangman(int incorrectGuess, int difficulty,string displayString)
        {
            int stage = 0;
            bool done = false;
            string[] stage1 =
            {
                "  +---+  ",
                "  |   |  ",
                "      |  ",
                "      |  ",
                "      |  ",
                "      |  ",
                "========="
            };
            string[] stage2 =
            {
                "  +---+  ",
                "  |   |  ",
                "  O   |  ",
                "      |  ",
                "      |  ",
                "      |  ",
                "========="

            };
            string[] stage3 =
            {
                "  +---+  ",
                "  |   |  ",
                "  O   |  ",
                "  |   |  ",
                "      |  ",
                "      |  ",
                "========="
            };
            string[] stage4 =
            {
                "  +---+  ",
                "  |   |  ",
                "  O   |  ",
                " /|   |  ",
                "      |  ",
                "      |  ",
                "=========",
            };
            string[] stage5 =
            {

                @"  +---+  ",
                @"  |   |  ",
                @"  O   |  ",
                @" /|\  |  ",
                @"      |  ",
                @"      |  ",
                @"=========",
            };
            string[] stage6 = 
            {
                @"  +---+  ",
                @"  |   |  ",
                @"  O   |  ",
                @" /|\  |  ",
                @" /    |  ",
                @"      |  ",
                @"=========",

            };
            string[] stage7 = 
            {
                @"  +---+  ",
                @"  |   |  ",
                @"  O   |  ",
                @" /|\  |  ",
                @" / \  |  ",
                @"      |  ",
                @"========="
            };
            string[] stage8 =
            {
                @"  +---+  ",
                @"      |  ",
                @"      |  ",
                @" \O/  |  ",
                @"  |   |  ",
                @" / \  |  ",
                @"========="
            };
            List<string[]> stages = new List<string[]> {stage1,stage2,stage3,stage4,stage5,stage6,stage7,stage8};
            switch (difficulty)
            {
                case 0:
                    stage = incorrectGuess;
                    break;
                case 1:
                    stage = incorrectGuess + 2;
                    break;
                case 2:
                    stage = incorrectGuess + 4;
                    break;
                case 3:
                    stage = incorrectGuess + 6;
                    break;
                default:
                    break;
            }

            (int cursorX,int cursorY) = Console.GetCursorPosition();

            for (int i = 0; i < stages[stage].Length -1; i++)
            {
                Console.SetCursorPosition(cursorX + 30,cursorY + i);
                Console.WriteLine(stages[stage][i]);
            }
            Console.SetCursorPosition(cursorX + 30, cursorY + stages[stage].Length);
            Console.WriteLine(displayString);
        }


        static bool CheckGuess(string secretWord, char guess)
        {
            if (secretWord.Contains(guess))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static string EditDisplayText(string secretWord, string displayWord, char guess)
        {
            char[] displayWordArray;
            if (secretWord.Contains(guess))
            {
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWord[i] == guess)
                    {
                        displayWordArray = displayWord.ToCharArray();
                        displayWordArray[i] = guess;
                        string temp = new string(displayWordArray);
                        displayWord = temp;
                    }
                }
            }
            return displayWord;
        }

        static char userGuessLetter(string displayWord)
        {
            char guess = 'a';
            bool flag = true;
            while (flag)
            {
                if (!char.TryParse(Console.ReadLine(), out guess) || displayWord.Contains(Char.ToLower(guess)) || !Char.IsLetter(guess))
                {
                    Console.WriteLine("Invalid Input! Please ensure you enter 1 letter!");
                }
                else
                {
                    return Char.ToLower(guess);
                }
            }

            return guess;
        }

        static bool DidUserWin(string displayWord)
        {
            if (!displayWord.Contains("_"))
            {

                Console.WriteLine("Congratulations You Win!!!");
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool DidUserLose(int incorrectGuesses, int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    if (incorrectGuesses >= 7) { return true;}
                    break;
                case 1:
                    if (incorrectGuesses >=5) { return true; }
                    break;
                case 2: 
                    if (incorrectGuesses >= 3) { return true; }
                    break;
                case 3:
                    if (incorrectGuesses > 0) { return true; }
                    break;
                default:
                    break;
            }
            return false;
        }

        static string generateWord(int level, string[] shortWords, string[] mediumWords, string[] longWords, string displayWord)
        {
            Random random = new Random();
            string word = "";
            switch (level)
            {
                case 1:
                    word = shortWords[random.Next(0, shortWords.Length - 1)];
                    break;
                case 2:
                    word = mediumWords[random.Next(0, mediumWords.Length - 1)];
                    break;
                case 3:
                    word = longWords[random.Next(0, longWords.Length - 1)];
                    break;
            }
            displayWord.Remove(0);
            for (int i = 0; i < word.Length; i++)
            {
  
                displayWord = (displayWord + "_");
            }
            return ($"{word}");
        }
        static string generateDisplayWord(string word)
        {
            string displayWord  = "";
            for (int i = 0; i < word.Length; i++)
            {

                displayWord = (displayWord + "_");
            }
            return displayWord;
        }
    }
}