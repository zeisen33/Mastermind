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
                    string res = compareGuessToCode(guess);
                } catch (Exception) {

                }

            } else {
                gameState = "lost";
                printLost();
            }
        }
    }

    public string compareGuessToCode(string guess) {
        return "";
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
}