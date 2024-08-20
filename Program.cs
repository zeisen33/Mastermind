// See https://aka.ms/new-console-template for more information

Console.WriteLine("================================================================================");
Console.WriteLine("Welcome to Mastermind!\n");

Game.printInstructions();

bool startGame = false;

while (!startGame) {
    string startInput = Console.ReadLine()!;
    if (startInput.Equals("help")) {
        Game.printInstructions();
    } else if (startInput == "exit") {
        break;
    } else {
        startGame = true;
    }
}

while (startGame) {
    Game game = new Game();
    startGame = false;
}

Console.WriteLine("\nThanks for playing Mastermind. Goodbye!");





