using Azure.AI.OpenAI;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ChatGPT_Test
{
    public class Toolbox
    {
        // A method to check if the board is full
        public static bool IsBoardFull(string[,] board)
        {
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == "") return false;
                }
            }
            return true;
        }

        // A method to generate a random rxcy move
        public static string GetRandomRxCyMove(string[,] board)
        {
            Random random = new Random();
            int size = board.GetLength(0);
            List<int> emptyCells = new List<int>();


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == "")
                    {
                        emptyCells.Add(i * size + j);
                    }
                }
            }

            // Check if the list is empty, which means the board is full
            if (emptyCells.Count == 0)
            {
                return null;
            }

            // Choose a random index from the list
            int index = emptyCells[random.Next(0, emptyCells.Count)];

            // Convert the index to row and column numbers
            int row = index / size;
            int col = index % size;

            // Return the move in the format rxcy
            return $"r{row+1}c{col+1}";
        }


        // A method to serialize board to string
        public static string SerializeBoardToString(string[,] board)
        {
            try
            {
                int size = board.GetLength(0);
                string boardString = "[";

                for (int i = 0; i < size; i++)
                {
                    boardString += "[";
                    for (int j = 0; j < size; j++)
                    {
                        boardString += board[i, j];
                        if (j < size - 1)
                        {
                            boardString += ",";
                        }
                    }

                    boardString += "]";

                    if (i < size - 1)
                    {
                        boardString += ",";
                    }
                }

                boardString += "]";

                return boardString;
            }
            catch
            {
                return null;
            }
        }

        public static void UpdateChatCompletionsOptions(ref ChatCompletionsOptions chatCompletionsOptions, string[,] board)
        {
            chatCompletionsOptions.Messages.RemoveAt(chatCompletionsOptions.Messages.Count - 1);
            chatCompletionsOptions.Messages.RemoveAt(chatCompletionsOptions.Messages.Count - 1);
            chatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.User, $"Current board status is: {SerializeBoardToString(board)}"));
            chatCompletionsOptions.Messages.Add(new ChatMessage(ChatRole.User, "Enter your move in the format rxcy, where x and y are the row and column numbers. For example, r1c1 means the top left cell. r3c3 means the bottom right cell. Please make your move."));
        }

        public static bool IsWithinBoardRange(int row, int col, string[,] board)
        {
            int size = board.GetLength(0);
            return (row >= 0 && row < size && col >= 0 && col < size);
        }

        // A method to display a board of type string[,]
        public static void DrawBoard(string[,] board)
        {
            int size = board.GetLength(0);

            Console.WriteLine("\n\t    1   2   3\n");
            for (int i = 0; i < size; i++)
            {
                Console.Write("\t" + (i + 1) + "   " + board[i, 0]);
                for (int j = 1; j < size; j++)
                {
                    Console.Write(" | " + board[i, j]);
                }
                Console.WriteLine();
                if (i < size - 1)
                {
                    Console.WriteLine("\t   ---+---+---");
                }
            }
        }

        // A method to clean a board with empty strings
        public static void CleanBoard(ref string[,] board)
        {
            int size = board.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = "";
                }
            }
        }

        // A method to check if a symbol has won the game
        public static bool CheckBoardWinner(string[,] board, string symbol)
        {
            int size = board.GetLength(0);
            int count;
            for (int i = 0; i < size; i++)
            {
                count = 0;
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == symbol) count++;
                }
                if (count == size) return true;
            }
            for (int j = 0; j < size; j++)
            {
                count = 0;
                for (int i = 0; i < size; i++)
                {
                    if (board[i, j] == symbol) count++;
                }
                if (count == size) return true;
            }
            count = 0;
            for (int i = 0; i < size; i++)
            {
                if (board[i, i] == symbol) count++;
            }
            if (count == size) return true;
            count = 0;
            for (int i = 0; i < size; i++)
            {
                if (board[i, size - 1 - i] == symbol) count++;
            }
            if (count == size) return true;
            return false;
        }

        public static bool AssignPositionFromString(ref int row, ref int col, string rxcy)
        {
            try
            {
                rxcy = new Regex("r[1-3]c[1-3]").Match(rxcy).Value;
                row = int.Parse(rxcy[1].ToString()) - 1; // Subtract 1 to match the array index
                col = int.Parse(rxcy[3].ToString()) - 1; // Subtract 1 to match the array index
                return true;
            }
            catch
            {
                return false;
            }
        }

        // A method to save a single telemetry object to a CSV file
        public static bool SaveTelemetryToCsv(Telemetry telemetry)
        {
            try
            {
                using var writer = new StreamWriter("telemetry.csv", true);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                // Check if the file is empty
                if (writer.BaseStream.Length == 0)
                {
                    csv.WriteHeader<Telemetry>();
                    csv.NextRecord();
                }

                csv.WriteRecord(telemetry);
                csv.NextRecord();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
