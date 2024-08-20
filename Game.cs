class Game {
    private static int numStartingGuesses = 10;
    private int guessesRemaining;
    private string gameState = "playing";
    private int[] code;
    private string codeStr = "";

    public Game() {
        this.guessesRemaining = numStartingGuesses;
        playGame();
    }

    public void playGame() {
        gameState = "playing";
        Console.WriteLine("================================================================================");
        Console.WriteLine("Thinking of a Code...");
        Thread.Sleep(1000);

        Random rnd = new Random();
        code = new int[4];
        for (int i = 0; i < 4; i++) {
            int digit = rnd.Next(1,7);
            code[i] = digit;
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
                        printInstructions();
                        continue;
                    } else {
                        string res = compareGuessToCode(guess, codeStr);
                        if (res.Equals("+ + + + ")) {
                            Console.WriteLine("\nCongratulations! You guessed the code!");
                            gameState.Equals("won");
                            break;
                        } else {
                            Console.WriteLine("\n" + res);
                            guessesRemaining--;
                        }
                    }
                } catch (Exception e) {
                    Console.WriteLine("\nPlease make sure to enter only four digits 1-6.\n");
                    Console.WriteLine(e.ToString());
                }

            } else {
                gameState = "lost";
                printLost();
            }
        }
    }

    public string compareGuessToCode(string guess, string codeStr) {
        Console.WriteLine(guess);
        // Console.WriteLine("Comparing");
        int countPlus = 0, countMinus = 0;

        // Create dictionary of each digitInCode -> Positions where digit appears in code
        Dictionary<int, List<int>> codeDict = new Dictionary<int, List<int>>();
        for (int i = 0; i < 4; i++) {
            if (!codeDict.ContainsKey(code[i])) {
                codeDict.Add(code[i], new List<int>());
            }
            List<int> idcs = codeDict[code[i]];
            idcs.Add(i);
            codeDict[code[i]] = idcs;
        } 

        foreach(int key in codeDict.Keys) {
            string keyPositions = "";
            List<int> positions = codeDict[key];
            foreach (int idx in positions) {
                keyPositions += idx + " ";
            }
            Console.WriteLine($"{key} -> {keyPositions}");
        }

        // Loop over each digit guessed
        for (int g = 0; g < 4 ; g++) {
            int guessedDigit = guess[g] - '0';
            Console.WriteLine($"Digit Guessed: {guessedDigit}");
            if (codeDict.ContainsKey(guessedDigit)) {
                List<int> positions = codeDict[guessedDigit];
                if (positions.Contains(g)) {
                    countPlus++;
                    positions.Remove(g);
                    codeDict[guessedDigit] = positions;
                    // guess = guess.Substring(0, g) + guess.Substring(g + 1);
                }
            }
        }

        foreach(int key in codeDict.Keys) {
            string keyPositions = "";
            List<int> positions = codeDict[key];
            foreach (int idx in positions) {
                keyPositions += idx + " ";
            }
            Console.WriteLine($"{key} -> {keyPositions}");
        }


        for (int k = 0; k < 4; k++) {
            int guessedDigit = guess[k] - '0';
            if (codeDict.ContainsKey(guessedDigit)) {
                List<int> positions = codeDict[guessedDigit];
                if (positions.Count > 0) {
                    countMinus++;
                    positions.RemoveAt(0);
                    codeDict[guessedDigit] = positions;
                }
            }
        }
       
        string res = "";
        Console.WriteLine($"countPlus: {countPlus}");
        Console.WriteLine($"countMinus: {countMinus}");
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


    public void printLost() {
        Console.Write($"You are out of guesses! The code was {codeStr}.");
        // printCode();
    }

    public void printCode() {
        for (int i = 0; i < 4; i++) {
            Console.Write($"{code[i]} ");
        }
    }
    public static void printInstructions() {
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