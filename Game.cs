class Game {
    private static int numStartingGuesses = 10;
    private int guessesRemaining;
    // private int[] code;

    public Game() {
        this.guessesRemaining = numStartingGuesses;
        playGame();
    }

    public void playGame() {
        Console.WriteLine("===============================================================================");
        Console.WriteLine("Thinking of a Code...");
        Thread.Sleep(1000);
        Console.WriteLine("I have a code");
    }
}