using System;
namespace Mastermind;

public partial class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("================================================================================");
        Console.WriteLine("Welcome to Mastermind!\n");

        Game.PrintInstructions();

        bool startGame = false;

        while (!startGame) {
            string startInput = Console.ReadLine()!;
            if (startInput.Equals("help")) {
                Game.PrintInstructions();
            } else if (startInput == "exit") {
                break;
            } else {
                startGame = true;
            }
        }

        while (startGame) {
            Game game = new Game();
            startGame = false;
            
            while (true) {
                Console.WriteLine("\nWould you like to play again? [y/n]");
                string playAgain = Console.ReadLine()!;

                if (playAgain.Equals("y")) {
                    startGame = true;
                    break;
                } else if (playAgain.Equals("n") || playAgain.Equals("exit")) {
                    break;
                } else if (playAgain.Equals("help")) {
                    Game.PrintInstructions();
                } else {
                    Console.WriteLine("Please enter 'y' or 'n':");
                }
            }
        }

        Console.WriteLine("\nThanks for playing Mastermind. Goodbye!");
    }
}

