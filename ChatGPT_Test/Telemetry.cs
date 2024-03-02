namespace ChatGPT_Test
{
    public class Telemetry
    {
        public Telemetry(int aiRandomsUsed, string playerSymbol, bool aiFirst, int movesMade, int totalAttempts, string? winningSymbol, bool aiAutoplay, bool randomAutoplay, int tokenTimeouts)
        {
            AiRandomsUsed = aiRandomsUsed;
            PlayerSymbol = playerSymbol;
            AiFirst = aiFirst;
            MovesMade = movesMade;
            TotalAttempts = totalAttempts;
            WinningSymbol = winningSymbol;
            AiAutoplay = aiAutoplay;
            RandomAutoplay = randomAutoplay;
            TokenTimeouts = tokenTimeouts;
        }

        public int AiRandomsUsed { get; set; }
        public string PlayerSymbol {  get; set; }
        public bool AiFirst { get; set; }
        public int MovesMade { get; set; }
        public int TotalAttempts { get; set; }
        public string? WinningSymbol { get; set; }
        public bool AiAutoplay { get; set; }
        public bool RandomAutoplay { get; set; }
        public int TokenTimeouts { get; set; }
    }
}
