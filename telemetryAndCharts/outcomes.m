clear all;
close all;

AiRandomsUsed = [1, 0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 3, 1, 0, 2, 0, 1, 0, 0, 0, 3, 5, 0, 1, 1, 0, 2, 2, 0, 0, 1, 0, 1, 3, 0, 0, 1, 0, 0, 1, 0, 3, 0, 0, 0, 3, 1, 1, 0, 0, 1, 0, 0, 0, 2, 0, 1, 1, 0, 0, 2, 0, 3, 0, 2, 1, 0, 3, 0, 0, 1, 1, 1, 2, 1, 0, 0, 0, 0, 1, 1, 2, 0, 2, 0, 1, 1, 0, 3, 0, 0, 1, 1, 1, 2, 0, 0, 3, 0, 0, 0, 0, 0, 2, 1, 3, 0, 1, 0, 3, 0, 1, 0, 4, 1, 2, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 3, 0, 2, 0, 1, 1, 3, 7, 4, 0, 1, 0, 0, 0, 2, 0];
PlayerSymbol = ["O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O"];
AiFirst = [false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false];
MovesMade = [9, 5, 5, 5, 5, 9, 9, 9, 6, 5, 6, 8, 6, 8, 9, 5, 9, 5, 7, 6, 9, 9, 7, 8, 8, 8, 8, 7, 5, 8, 7, 7, 7, 9, 8, 8, 9, 8, 6, 9, 7, 9, 5, 5, 9, 8, 9, 7, 9, 6, 7, 8, 9, 6, 9, 5, 9, 8, 7, 8, 8, 5, 9, 9, 9, 9, 8, 7, 7, 5, 9, 7, 8, 9, 6, 7, 7, 5, 8, 5, 6, 9, 6, 9, 5, 9, 5, 5, 9, 7, 7, 8, 8, 8, 9, 8, 9, 9, 5, 7, 6, 7, 9, 9, 7, 9, 7, 6, 9, 9, 9, 9, 7, 9, 8, 9, 8, 8, 9, 9, 9, 7, 7, 8, 7, 9, 7, 9, 7, 7, 9, 9, 8, 8, 7, 9, 6, 7, 9, 9, 7, 7, 9, 8, 8, 9];
TotalAttempts = [3, 0, 1, 0, 0, 1, 4, 2, 4, 0, 0, 6, 2, 1, 4, 1, 3, 1, 1, 1, 6, 11, 0, 2, 2, 1, 4, 4, 0, 0, 2, 0, 2, 8, 0, 1, 3, 0, 0, 2, 0, 6, 0, 0, 1, 6, 5, 2, 1, 0, 3, 0, 1, 2, 4, 0, 2, 4, 1, 0, 4, 0, 6, 0, 4, 2, 2, 6, 0, 0, 2, 2, 3, 4, 4, 2, 1, 1, 1, 2, 2, 6, 2, 5, 0, 3, 2, 0, 6, 1, 1, 2, 3, 3, 4, 2, 1, 7, 0, 0, 1, 0, 0, 5, 2, 7, 0, 2, 0, 6, 0, 3, 0, 9, 2, 4, 0, 1, 4, 0, 2, 1, 2, 3, 0, 2, 2, 0, 2, 1, 6, 0, 4, 0, 3, 3, 6, 14, 8, 0, 2, 0, 0, 2, 4, 0];
WinningSymbol = ["-", "O", "O", "O", "O", "-", "-", "-", "O", "O", "X", "X", "X", "X", "-", "O", "O", "O", "O", "O", "O", "-", "O", "X", "X", "X", "X", "O", "O", "X", "O", "O", "O", "O", "X", "X", "-", "X", "X", "-", "O", "O", "O", "O", "-", "X", "O", "O", "O", "X", "O", "X", "O", "X", "O", "O", "O", "X", "O", "X", "X", "O", "-", "O", "-", "O", "X", "O", "O", "O", "-", "O", "X", "-", "X", "O", "O", "O", "X", "O", "X", "-", "X", "-", "O", "-", "O", "O", "O", "O", "O", "X", "X", "X", "O", "X", "-", "O", "O", "O", "X", "O", "-", "O", "O", "-", "O", "X", "O", "O", "O", "O", "O", "O", "X", "-", "X", "X", "-", "O", "-", "O", "O", "X", "O", "O", "O", "-", "O", "O", "O", "O", "X", "X", "O", "O", "X", "O", "O", "O", "O", "O", "-", "X", "X", "O"];
AiAutoplay = [false, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, true, false, true, true, false, false, true, false, false, false, true, true, false, true, false, false, true, true, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, false, true, false, true, false, true, false, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, false, true, false, false, true, false, false, false, true, false];
RandomAutoplay = [false, false, false, false, false, false, false, false, false, true, false, false, false, true, true, true, true, false, false, false, false, false, true, true, true, true, true, true, true, true, false, true, false, false, true, true, false, true, true, true, false, false, true, false, true, true, false, false, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, true, false, true, false, true, false, true, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, true, false, true, true, false, true, true, true, false, true];
TokenTimeouts = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 7, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 14, 8, 0, 0, 0, 0, 0, 0, 0];

#{
AiRandomsUsed = [1, 0, 0]; # Values can be: positive integer
PlayerSymbol = ["O", "O", "O"]; # Values can be: "O" or "X"
AiFirst = [false, false, false]; # Values can be: boolean
MovesMade = [9, 5, 5]; # Values can be: positive integer
TotalAttempts = [3, 0, 1]; # Values can be: positive integer
WinningSymbol = ["-", "O", "O"]; # Values can be: "O" or "X" or "-"
AiAutoplay = [false, false, true]; # Values can be: boolean
RandomAutoplay = [true, false, false]; # Values can be: boolean
TokenTimeouts = [0, 0, 0]; # Values can be: positive integer
#}


# Initialize an empty matrix to store the outcomes
outcome = [];

# Loop through the three scenarios
for i = 1:3
  # Set the filter condition based on the scenario
  if i == 1
    # Human vs chatGPT
    condition = AiAutoplay == false & RandomAutoplay == false & AiRandomsUsed <= 1 & TokenTimeouts == 0;
  elseif i == 2
    # Random vs chatGPT
    condition = AiAutoplay == false & RandomAutoplay == true & AiRandomsUsed <= 1 & TokenTimeouts == 0;
  else
    # chatGPT vs chatGPT
    condition = AiAutoplay == true & RandomAutoplay == false & AiRandomsUsed <= 1 & TokenTimeouts == 0;
  endif

  # Filter the data according to the condition
  filtered_data = [AiRandomsUsed; PlayerSymbol; AiFirst; MovesMade; TotalAttempts; WinningSymbol; AiAutoplay; RandomAutoplay; TokenTimeouts];
  filtered_data = filtered_data(:, condition);

  # Count the number of wins, draws, and loses
  wins = sum(filtered_data(6, :) == "O");
  draws = sum(filtered_data(6, :) == "-");
  loses = sum(filtered_data(6, :) == "X");
  total = sum([wins, draws, loses]);
  total
  # Append the counts to the outcome matrix
  outcome = [outcome; [total, wins, draws, loses]];
endfor

font_size = 24;
offset = 0.1;
maximum_y_value = 80;
y_offset = -6;

# Plot the bar chart
bar(outcome); # plot the bar chart for the i-th scenario
set(title("Wyniki gier dla poszczególnych trybów (AiRandomsUsed <= 1 & TokenTimeouts == 0)"), "fontsize", font_size);
set(legend("Suma gier", "Wygrana", "Remis", "Przegrana"), "fontsize", font_size);
set(ylabel("Ilość gier"), "fontsize", font_size);
axis([0, 4, 0, maximum_y_value]);
set(gca, "xticklabel", get(gca, "xtick"), "fontsize", font_size);
set(gca, "yticklabel", get(gca, "ytick"), "fontsize", font_size);
set(gca, "ytick", yticks, "fontsize", font_size);

% Set the text labels
set(text(1 - 0.19 - offset, y_offset, "Człowiek vs. model"), "fontsize", font_size);
set(text(2 - 0.27 - offset, y_offset, "Losowanie pozycji vs. model"), "fontsize", font_size);
set(text(3 - 0.15 - offset, y_offset, "Model vs. model"), "fontsize", font_size);
