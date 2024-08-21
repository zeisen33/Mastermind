public class Game {
    private const int NUM_STARTING_GUESSES = 10;
    private const int CODE_LENGTH = 4;
    private int guessesRemaining;
    private string gameState = "playing";
    private string codeStr = "";

    public Game() {
        this.guessesRemaining = NUM_STARTING_GUESSES;
        PlayGame();
    }

    public void PlayGame() {
        gameState = "playing";
        Console.WriteLine("================================================================================");
        Console.WriteLine("Thinking of a Code...");
        Thread.Sleep(1000);

        Random rnd = new Random();
        for (int i = 0; i < CODE_LENGTH; i++) {
            int digit = rnd.Next(1,7);
            codeStr += digit;
        }

        string guess;

        while (gameState.Equals("playing")) {
            if (guessesRemaining > 0) {
                Console.Write($"\nYou have {guessesRemaining} guesses remaining. Please enter your 4 digit guess: ");

                try {
                    guess = Console.ReadLine()!;
                    if (guess.Equals("exit")) {
                        return;
                    } else if (guess.Equals("help")) {
                        PrintInstructions();
                        continue;
                    } else {
                        string res = CompareGuessToCode(guess, codeStr);
                        if (res.Count(c => c == '+') == CODE_LENGTH) {
                            Console.WriteLine("\nCongratulations! You guessed the code!");
                            gameState = "won";
                            break;
                        } else {
                            Console.WriteLine("\n" + res);
                            guessesRemaining--;
                        }
                    }
                } catch (Exception) {
                    Console.WriteLine("\nPlease make sure to enter four digits 1-6.\n");
                }

            } else {
                gameState = "lost";
                PrintLost();
            }
        }
    }

    public static string CompareGuessToCode(string guess, string codeStr) {
        int countPlus = 0, countMinus = 0;

        // Create dictionary of each digitInCode -> Positions where digit appears in code
        Dictionary<char, List<int>> codeDict = new ();
        for (int i = 0; i < CODE_LENGTH; i++) {
            if (!codeDict.ContainsKey(codeStr[i])) {
                codeDict.Add(codeStr[i], new List<int>());
            }
            List<int> idcs = codeDict[codeStr[i]];
            idcs.Add(i);
        } 

        // Loop over each digit guessed checking for +, and remove position from dictionary if found
        if (guess.Length != CODE_LENGTH) {
            throw new Exception($"{CODE_LENGTH} digits only");
        }
        for (int g = 0; g < CODE_LENGTH ; g++) {
            char guessedDigit = guess[g];
            if (guessedDigit > '6' || guessedDigit < '1') {
                throw new Exception("1-6 only");
            }
            if (codeDict.ContainsKey(guessedDigit)) {
                List<int> positions = codeDict[guessedDigit];
                if (positions.Contains(g)) {
                    countPlus++;
                    positions.Remove(g);
                }
            }
        }

        // Loop over guessed digits checking for -, remove from positions list
        for (int k = 0; k < CODE_LENGTH; k++) {
            char guessedDigit = guess[k];
            if (guessedDigit == codeStr[k]) {
                continue;
            }
            if (codeDict.ContainsKey(guessedDigit)) {
                List<int> positions = codeDict[guessedDigit];
                if (positions.Count > 0) {
                    countMinus++;
                    positions.RemoveAt(0);
                }
            }
        }
       
        string res = "";
        while (countPlus > 0) {
            res += "+ ";
            countPlus--;
        }
        while (countMinus > 0) {
            res += "- ";
            countMinus--;
        }
        return res;
    }


    public void PrintLost() {
        Console.Write($"\nYou are out of guesses! The code was {codeStr}.\n");
    }

    public static void PrintInstructions() {
        Console.WriteLine("\nI will think of a secret code. Your job is to guess the secret code.");
        Console.WriteLine($"My code will be four digits 1 through 6. You will have {NUM_STARTING_GUESSES} tries to guess the code.");
        Console.WriteLine("After each guess, I will print out a '+' for each digit in the correct place,");
        Console.WriteLine("then a '-' for every other digit in the puzzle but not in the correct place,");
        Console.WriteLine("and nothing for digits not in the puzzle.");
        Console.WriteLine("For example, if the code is '1234' and the guess is '4233',");
        Console.WriteLine("I'd print out '+ + -'.");
        Console.WriteLine("When guessing, make sure to type four digits 1-6, then hit Enter.\n");

        Console.WriteLine("Enter 'help' at any time to see these instructions, or 'exit' to quit the game.");
        Console.WriteLine("Press any other key to get started!");
    }
}