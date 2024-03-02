using Azure.AI.OpenAI;

namespace ChatGPT_Test
{
    class Program
    {
        // IMPORTANT: To make this working, insert your OpenAI API key into openAIApiKey variable.
        private static string openAIApiKey = "yourOpenAIApiKeyHere";
        private static OpenAIClient openAIClient = new OpenAIClient(openAIApiKey, new OpenAIClientOptions());
        private static ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
        {
            DeploymentName = "gpt-3.5-turbo",
            Messages =
                {
                    new ChatMessage(ChatRole.System, "You are a helpful assistant. You will talk minimalistic and can play TicTacToe."),
                    new ChatMessage(ChatRole.User, "Can you play TicTacToe?"),
                    new ChatMessage(ChatRole.Assistant, "Yes. I know how to play TicTacToe."),
                    new ChatMessage(ChatRole.User, "You will play as X and I will play as O."),
                    new ChatMessage(ChatRole.User, "Current board status is: [[,,],[,,],[,,]]"),
                    new ChatMessage(ChatRole.User, "Enter your move in the format rxcy, where x and y are the row and column numbers. For example, r1c1 means the top left cell. r3c3 means the bottom right cell. Please make your move.")
                }
        };

        private static bool playing = true;
        private static string[,] board = new string[3, 3];
        private static string playerSymbol = "O";
        private static string aiSymbol = "X";
        private static bool aiAutoplay = false;
        private static bool randomAutoplay = false;
        private static bool aiFirst = false;
        private static int row = -1;
        private static int col = -1;
        private static string rxcy = "";
        private static string consoleInput = "";
        private static int aiRandomsUsed = 0;
        private static int movesMade = 0;
        private static int totalAttempts = 0;
        private static string? winningSymbol = "";
        private static int tokenTimeouts = 0;
        private static int roundsPlayed = 0;
        private static bool fullyAutomatic = false;
        private static int maxRounds = 20;
        private static int sleepTimeBetweenQueries = 20000;
        private static int maxAttempts = 2;


        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Fully automatic testing? [y|n]");
            fullyAutomatic = (Console.ReadLine() == "y" ?  true : false);

            while(roundsPlayed < maxRounds) {
                Toolbox.CleanBoard(ref board);

                Console.WriteLine("Welcome to the TicTacToe game with the OpenAI chat bot!");
                Console.WriteLine("Enter your move in the format rxcy, where x and y are the row and column numbers. For example, r1c1 means the top left cell. To restart the game, enter restart.");
                Console.WriteLine("Do you want to play as [h]uman, [r]andom or [a]utoplay AI? [h|r|a]");
                
                if(fullyAutomatic)
                    consoleInput = (roundsPlayed % 2 == 0 ? "r" : "a");
                else
                    consoleInput = Console.ReadLine();

                switch(consoleInput)
                {
                    case "h":
                        Console.WriteLine("Do you want to go first or second? [1|2]");
                        consoleInput = Console.ReadLine();
                        aiFirst = consoleInput == "1" ? false : true;
                        break;
                    case "r":
                        aiFirst = false;
                        randomAutoplay = true;
                        break;
                    case "a":
                        aiFirst = false;
                        aiAutoplay = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid game type.");
                        continue;
                }

                Console.WriteLine("This is the updated board:");
                Toolbox.DrawBoard(board);

                if (aiFirst)
                {
                    await MakeAIMove();
                    movesMade++;
                }

                // Start the game loop
                while (playing)
                {
                    Console.WriteLine("\nYour turn.");
                    if(aiAutoplay)
                        consoleInput = await GetAIMove(openAIClient, chatCompletionsOptions, board, aiSymbol);
                    else if (randomAutoplay)
                        consoleInput = Toolbox.GetRandomRxCyMove(board);
                    else
                        consoleInput = Console.ReadLine();

                    if (!Toolbox.AssignPositionFromString(ref row, ref col, consoleInput))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid move.");
                        continue;
                    }

                    // Check if the user input is within the board range
                    if (!Toolbox.IsWithinBoardRange(row, col, board))
                    {
                        Console.WriteLine("Invalid input. Please enter a move within the board range.");
                        continue;
                    }

                    // Check if the user input is an empty cell
                    if (board[row, col] != "")
                    {
                        Console.WriteLine("Invalid input. Please enter a move on an empty cell.");
                        continue;
                    }

                    // Mark the user move on the board
                    board[row, col] = playerSymbol;
                    Toolbox.UpdateChatCompletionsOptions(ref chatCompletionsOptions, board);
                    movesMade++;

                    // Print the board to the console
                    Console.WriteLine($"You marked {consoleInput} as {playerSymbol}");
                    Console.WriteLine("This is the updated board");
                    Toolbox.DrawBoard(board);

                    // Check if the user has won the game
                    if (Toolbox.CheckBoardWinner(board, playerSymbol))
                    {
                        Console.WriteLine("Congratulations! You have won the game!");
                        playing = false;
                        winningSymbol = playerSymbol;
                        continue;
                    }

                    // Check if the board is full
                    if (Toolbox.IsBoardFull(board))
                    {
                        Console.WriteLine("The game is a draw. No one has won.");
                        playing = false;
                        winningSymbol = "-";
                        continue;
                    }

                    await MakeAIMove();
                    movesMade++;

                    // Check if the AI has won the game
                    if (Toolbox.CheckBoardWinner(board, aiSymbol))
                    {
                        Console.WriteLine("Sorry, you have lost the game. Better luck next time.");
                        playing = false;
                        winningSymbol = aiSymbol;
                        continue;
                    }

                    // Check if the board is full
                    if (Toolbox.IsBoardFull(board))
                    {
                        Console.WriteLine("The game is a draw. No one has won.");
                        playing = false;
                        winningSymbol = "-";
                        continue;
                    }
                }
                roundsPlayed++;

                if(Toolbox.SaveTelemetryToCsv(new Telemetry(aiRandomsUsed, playerSymbol, aiFirst, movesMade, totalAttempts, winningSymbol, aiAutoplay, randomAutoplay, tokenTimeouts)))
                    Console.WriteLine("Telemetry has successfully been saved.");
                else
                    Console.WriteLine("Telemetry has not been saved, file access issue.");

                ResetGame();
            }
            Console.WriteLine("The End.");
        }

        static void ResetGame()
        {
            playing = true;
            playerSymbol = "O";
            aiSymbol = "X";
            aiAutoplay = false;
            randomAutoplay = false;
            aiFirst = false;
            row = -1;
            col = -1;
            rxcy = "";
            consoleInput = "";
            aiRandomsUsed = 0;
            movesMade = 0;
            totalAttempts = 0;
            winningSymbol = "";
            tokenTimeouts = 0;
            Toolbox.CleanBoard(ref board);
            Toolbox.UpdateChatCompletionsOptions(ref chatCompletionsOptions, board);
            Console.WriteLine("The game has been restarted.");
        }

        static async Task MakeAIMove()
        {
            Console.WriteLine("It's the AI's turn. Please wait...");
            rxcy = await GetAIMove(openAIClient, chatCompletionsOptions, board, aiSymbol);
            Toolbox.AssignPositionFromString(ref row, ref col, rxcy);

            board[row, col] = aiSymbol;
            Toolbox.UpdateChatCompletionsOptions(ref chatCompletionsOptions, board);

            Console.WriteLine($"The AI marked: \"{rxcy}\" as {aiSymbol}");
            Console.WriteLine("This is the updated board");
            Toolbox.DrawBoard(board);
        }

        // A method to get the AI move using the OpenAI chat bot, will return rxcy
        static async Task<string> GetAIMove(OpenAIClient client, ChatCompletionsOptions options, string[,] board, string aiSymbol)
        {
            Thread.Sleep(sleepTimeBetweenQueries);
            int row = -1;
            int col = -1;
            var content = "";
            var attempts = 0;

            while (true)
            {
                if (attempts >= maxAttempts)
                {
                    aiRandomsUsed++;
                    Console.WriteLine($"The OpenAI chat bot failed to return a valid move after {attempts} attempts.");
                    Console.WriteLine("Generating a random move for the AI...");
                    content = Toolbox.GetRandomRxCyMove(board);
                    break;
                }

                try
                {
                    var response = await client.GetChatCompletionsAsync(options);
                    var choice = response.Value.Choices[0];
                    content = choice.Message.Content;

                    if (!Toolbox.AssignPositionFromString(ref row, ref col, content))
                    {
                        Console.WriteLine("Warning: The OpenAI chat bot returned invalid move: " + content);
                        attempts++;
                        continue;
                    }

                    if (!Toolbox.IsWithinBoardRange(row, col, board))
                    {
                        Console.WriteLine("Warning: The OpenAI chat bot returned an out of range move: " + content);
                        attempts++;
                        continue;
                    }

                    if (board[row, col] != "")
                    {
                        Console.WriteLine("Warning: The OpenAI chat bot returned an occupied cell move: " + content);
                        attempts++;
                        continue;
                    }

                    break;
                }
                catch
                {
                    Console.WriteLine("Error: An exception occurred when parsing the AI move, probably token timeout.");
                    attempts++;
                    tokenTimeouts++;
                }
            }
            totalAttempts += attempts;
            return content;
        }
    }
}