using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Boolean inGame = true;

            Boolean startGame = false, validInput = false;
            String startGameText = String.Empty;
            //set turn and hands used counts
            int turnCount = 0, rockCount = 0, paperCount = 0, scissorCount = 0, lizardCount = 0, spockCount = 0;

            //set possible hands
            List<Hands> possibleHands = new List<Hands>()
            {
                new Hands() { id = 0, description = "rock", shortDesc = "r", handChosen = HandType.Rock, 
                    beats = new List<HandType> { HandType.Scissors, HandType.Lizard }, 
                    beatsDesc = new Dictionary<HandType, String> { { HandType.Scissors, "crushes" }, { HandType.Lizard, "crushes" } } },
                new Hands() { id = 1, description = "paper", shortDesc = "p", handChosen = HandType.Paper, 
                    beats = new List<HandType> { HandType.Rock, HandType.Spock },
                    beatsDesc = new Dictionary<HandType, String> { { HandType.Rock, "covers" }, { HandType.Spock, "disproves" } }},
                new Hands() { id = 2, description = "scissors", shortDesc = "sc", handChosen = HandType.Scissors, 
                    beats = new List<HandType> { HandType.Paper, HandType.Lizard },
                    beatsDesc = new Dictionary<HandType, String> { { HandType.Paper, "cuts" }, { HandType.Lizard, "decapitates" } }},
                new Hands() { id = 3, description = "lizard", shortDesc = "l", handChosen = HandType.Lizard, 
                    beats = new List<HandType> { HandType.Spock, HandType.Paper },
                    beatsDesc = new Dictionary<HandType, String> { { HandType.Spock, "poisons" }, { HandType.Paper, "eats" } }},
                new Hands() { id = 4, description = "spock", shortDesc = "sp", handChosen = HandType.Spock, 
                    beats = new List<HandType> { HandType.Scissors, HandType.Rock },
                    beatsDesc = new Dictionary<HandType, String> { { HandType.Scissors, "smashes" }, { HandType.Rock, "vaporizes" } }}
            };

            while (!validInput)
            {
                Console.WriteLine("\nDo you wish to play Rock Paper Scissors Lizard Spock? Please type yes/no and press enter. The application will also accept y/n.");
                String playChoice = Console.ReadLine().ToLower();
                //validate entry
                if (playChoice == "yes" || playChoice == "y")
                {
                    validInput = true;
                    startGameText = "\nGreat decision! Please press any key to start playing.";
                    startGame = true;
                }
                else
                {
                    if (playChoice == "no" || playChoice == "n")
                    {
                        validInput = true;
                        startGameText = "\nNo problem! Please press any key to exit";
                        startGame = false;
                    }
                    else
                    {
                        validInput = false;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nInvalid input. Please try again.");
                        Console.ResetColor();
                    }
                }
            }
            
            Console.WriteLine(startGameText);
            Console.ReadKey();
            Console.Clear();

            while (inGame)
            {
                //reset everything for replaying
                Boolean validPlay = false;
                Hands playerHand = new Hands();
                Outcome result = Outcome.draw;
                turnCount = 0; rockCount = 0; paperCount = 0; scissorCount = 0; lizardCount = 0; spockCount = 0;

                //keep going until winner decided
                while (result == Outcome.draw)
                {
                    while (!validPlay)
                    {
                        String playerChoice = String.Empty;
                        if (startGame)
                        {
                            Console.WriteLine("\nPlease type in your chosen hand: rock/paper/scissors/lizard/spock. The application will also accept r/p/sc/l/sp.");
                            playerChoice = Console.ReadLine().ToLower();
                        }
                        else
                        {
                            //exit if not starting game
                            Environment.Exit(0);
                        }

                        //make sure valid hand has been entered
                        playerHand = possibleHands.FirstOrDefault(x => x.description == playerChoice || x.shortDesc == playerChoice);
                        if (playerHand != null)
                        {
                            validPlay = true;
                            turnCount++;
                        }
                        else
                        {
                            validPlay = false;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\nInvalid input. Please try again.");
                            Console.ResetColor();
                        }
                    }

                    //return computer hand
                    Hands computerHand = pickComputerHand(possibleHands);

                    Console.WriteLine(String.Format("\nYou have chosen {0}, and the computer has chosen {1}.", playerHand.description, computerHand.description));

                    String gameText = String.Empty;

                    //update move count
                    switch (playerHand.handChosen)
                    {
                        case HandType.Rock:
                            rockCount++;
                            break;
                        case HandType.Paper:
                            paperCount++;
                            break;
                        case HandType.Scissors:
                            scissorCount++;
                            break;
                        case HandType.Lizard:
                            lizardCount++;
                            break;
                        case HandType.Spock:
                            spockCount++;
                            break;
                    }
                    switch (computerHand.handChosen)
                    {
                        case HandType.Rock:
                            rockCount++;
                            break;
                        case HandType.Paper:
                            paperCount++;
                            break;
                        case HandType.Scissors:
                            scissorCount++;
                            break;
                        case HandType.Lizard:
                            lizardCount++;
                            break;
                        case HandType.Spock:
                            spockCount++;
                            break;
                    }

                    //calculate most used hand
                    Dictionary<String, int> handCountDict = new Dictionary<String, int>{
                        { "rock", rockCount },
                        { "paper", paperCount },
                        { "scissors", scissorCount },
                        { "lizard", lizardCount },
                        { "spock", spockCount }
                    };

                    int topValue = handCountDict.OrderByDescending(x => x.Value).FirstOrDefault().Value;
                    List<String> topHands = handCountDict.Where(x => x.Value == topValue).Select(y => y.Key).ToList();
                    String mostUsedHands = string.Join(" and ", topHands);

                    String mostUsedText = topHands.Count > 1 ? topValue > 1 ? String.Format("The most used hands were {0}. Used {1} times", mostUsedHands, topValue)
                        : String.Format("The most used hands were {0}. Used {1} time", mostUsedHands, topValue)
                            : topValue > 1 ? String.Format("The most used hand was {0}. Used {1} times", mostUsedHands, topValue)
                            : String.Format("The most used hand was {0}. Used {1} time", mostUsedHands, topValue);
                    String turnsText = turnCount > 1 ? String.Format("{0} turns", turnCount) : String.Format("{0} turn", turnCount);
                    
                    String outcomeDesc = String.Empty;

                    if (playerHand == computerHand)
                    {
                        result = Outcome.draw;
                        gameText = "\nThis round ends in a draw. Get ready to go again!";
                        //reset hand validation for next round
                        validPlay = false;
                    }
                    else
                    {
                        if (playerHand.beats.Contains(computerHand.handChosen))
                        {
                            result = Outcome.playerWin;
                            outcomeDesc = String.Format("{0} {1} {2}", playerHand.description, playerHand.beatsDesc.FirstOrDefault(x => x.Key == computerHand.handChosen).Value, computerHand.description);
                            gameText = String.Format("\nCongratulations, you win, {2}!\nThis game took {0} to complete.\n{1}.", turnsText, mostUsedText, outcomeDesc);
                        }
                        else
                        {
                            result = Outcome.computerWin;
                            outcomeDesc = String.Format("{0} {1} {2}", computerHand.description, computerHand.beatsDesc.FirstOrDefault(x => x.Key == playerHand.handChosen).Value, playerHand.description);
                            gameText = String.Format("\nCommiserations, you lose, {2}.\nThis game took {0} to complete.\n{1}", turnsText, mostUsedText, outcomeDesc);
                        }
                    }

                    Console.WriteLine(gameText);
                }

                //Console.WriteLine("\nPress any key to exit");
                //Console.ReadKey();

                Boolean validPlayAgain = false;
                while (!validPlayAgain)
                {
                    Console.WriteLine("\nDo you want to play again? Please type yes/no and press enter. The application will also accept y/n.");
                    string playAgainChoice = Console.ReadLine().ToLower();
                    if (playAgainChoice == "yes" || playAgainChoice == "y")
                    {
                        validPlayAgain = true;
                        Console.WriteLine("\nGreat decision! Please press any key to start again.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        if (playAgainChoice == "no" || playAgainChoice == "n")
                        {
                            validPlayAgain = true;
                            Console.WriteLine("\nNo problem! Please press any key to exit.");
                            Console.ReadKey();
                            inGame = false;
                        }
                        else
                        {
                            validPlayAgain = false;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\nInvalid input. Please try again.");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }

        public static Hands pickComputerHand(IList<Hands> possibleHands)
        {
            Random rnd = new Random();
            int choice = rnd.Next(possibleHands.Count);
            Hands computerHand = possibleHands[choice];

            //hardcode scissors for testing
            //return possibleHands.FirstOrDefault(x => x.handChosen == HandType.Scissors);
            return computerHand;
        }

    }
}
