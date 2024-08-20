// See https://aka.ms/new-console-template for more information

Console.WriteLine("===============================================================================");
Console.WriteLine("Welcome to Mastermind!\n");

printInstructions();

bool startGame = false;

while (!startGame) {
    string startInput = Console.ReadLine()!;
    if (startInput.Equals("help")) {
        printInstructions();
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

Console.WriteLine("Thanks for playing Mastermind. Goodbye!");




static void printInstructions() {
    Console.WriteLine("\nI will think of a 4 digit secret code. Your job is to guess the secret code.");
    Console.WriteLine("You will have 10 tries to guess the code.");
    Console.WriteLine("After each guess, I will print out a '+' for each digit in the correct place,");
    Console.WriteLine("then a '-' for every other digit in the puzzle but not in the correct place,");
    Console.WriteLine("and nothing for digits not in the puzzle.");
    Console.WriteLine("For example, if the code is '1234' and the guess is '4233',");
    Console.WriteLine("I'd print out '+ + -'.\n");

    Console.WriteLine("Enter 'help' at any time to see these instructions, or 'exit' to quit the game.");
    Console.WriteLine("Press any other key to get started!");
}

