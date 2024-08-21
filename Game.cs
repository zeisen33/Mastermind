public class Game {
    private const int numStartingGuesses = 10;
    private int guessesRemaining;
    private string gameState = "playing";
    private string codeStr = "";

    public Game() {
        this.guessesRemaining = numStartingGuesses;
        PlayGame();
    }

    public void PlayGame() {
        gameState = "playing";
        Console.WriteLine("================================================================================");
        Console.WriteLine("Thinking of a Code...");
        Thread.Sleep(1000);

        Random rnd = new Random();
        for (int i = 0; i < 4; i++) {
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
                        if (res.Equals("+ + + + ")) {
                            Console.WriteLine("\nCongratulations! You guessed the code!");
                            gameState = "won";
                            break;
                        } else {
                            Console.WriteLine("\n" + res);
                            guessesRemaining--;
                        }
                    }
                } catch (Exception e) {
                    Console.WriteLine("\nPlease make sure to enter four digits 1-6.\n");
                    // Console.WriteLine(e.ToString());
                }

            } else {
                gameState = "lost";
                PrintLost();
            }
        }
    }

    public static string CompareGuessToCode(string guess, string codeStr) {
        // Console.WriteLine(guess);
        // Console.WriteLine("Comparing");
        int countPlus = 0, countMinus = 0;

        // Create dictionary of each digitInCode -> Positions where digit appears in code
        Dictionary<char, List<int>> codeDict = new ();
        for (int i = 0; i < 4; i++) {
            if (!codeDict.ContainsKey(codeStr[i])) {
                codeDict.Add(codeStr[i], new List<int>());
            }
            List<int> idcs = codeDict[codeStr[i]];
            idcs.Add(i);
            codeDict[codeStr[i]] = idcs;
        } 

        // foreach(char key in codeDict.Keys) {
        //     string keyPositions = "";
        //     List<int> positions = codeDict[key];
        //     foreach (int idx in positions) {
        //         keyPositions += idx + " ";
        //     }
        //     Console.WriteLine($"{key} -> {keyPositions}");
        // }

        // Loop over each digit guessed checking for +, and remove position from dictionary if found
        if (guess.Length != 4) {
            throw new Exception("4 digits only");
        }
        for (int g = 0; g < 4 ; g++) {
            char guessedDigit = guess[g];
            if (guessedDigit > '6' || guessedDigit < 1) {
                throw new Exception("1-6 only");
            }
            // Console.WriteLine($"Digit Guessed: {guessedDigit}");
            if (codeDict.ContainsKey(guessedDigit)) {
                List<int> positions = codeDict[guessedDigit];
                if (positions.Contains(g)) {
                    countPlus++;
                    positions.Remove(g);
                    // guess = guess.Substring(0, g) + guess.Substring(g + 1);
                }
            }
        }

        // foreach(char key in codeDict.Keys) {
        //     string keyPositions = "";
        //     List<int> positions = codeDict[key];
        //     foreach (int idx in positions) {
        //         keyPositions += idx + " ";
        //     }
        //     Console.WriteLine($"{key} -> {keyPositions}");
        // }

        // Loop over guessed digits checking for -, remove from positions list
        for (int k = 0; k < 4; k++) {
            char guessedDigit = guess[k];
            if (codeDict.ContainsKey(guessedDigit)) {
                List<int> positions = codeDict[guessedDigit];
                if (positions.Count > 0) {
                    countMinus++;
                    positions.RemoveAt(0);
                }
            }
        }
       
        string res = "";
        // Console.WriteLine($"countPlus: {countPlus}");
        // Console.WriteLine($"countMinus: {countMinus}");
        while (countPlus > 0) {
            res += "+ ";
            countPlus--;
        }
        while (countMinus > 0) {
            res += "- ";
            countMinus--;
        }
        // Console.WriteLine(res);
        return res;
    }


    public void PrintLost() {
        Console.Write($"\nYou are out of guesses! The code was {codeStr}.\n");
        // printCode();
    }

    public void PrintCode() {
        for (int i = 0; i < 4; i++) {
            Console.Write($"{codeStr[i]} ");
        }
    }
    public static void PrintInstructions() {
        Console.WriteLine("\nI will think of a secret code. Your job is to guess the secret code.");
        Console.WriteLine("My code will be four digits 1 through 6. You will have 10 tries to guess the code.");
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