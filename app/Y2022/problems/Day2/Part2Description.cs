using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day2;

public class Part2Description : Description
{
    public override string Text =>
@"Given a list of command pairs, calculate the total player score based on the following rules:
1. First command is for the opponent and can be: A (Rock), B (Paper), C (Scissors).
2. Second command is the expected outcome for the player and can be: X (Lose), Y (Draw), Z (Win).
3. Calculate the player command based on the opponent command and the expected result.
4. Player command can be: A (Rock), B (Paper), C (Scissors).
5. Each command has a particular value: Rock=1, Paper=2, Scissors=3.
6. Each outcome has a particular value: Player win=6, Draw=3, Player lose=0.
7. Score = sum of the command and outcome values.
8. Total score = Sum of all the player scores.";

    public override string Example =>
@"Given: [A Y B X C Z]
Output: 12";

    public override string Explanation =>
@"There are 3 command pairs in the list:
|------|-------|-----------------------------|---------------------------|
|      |       |             Move            |           Score           |
|------|-------|-----------------------------|---------------------------|
| Pair | Value | Opponent | Player | Outcome | Command | Outcome | Total |
|------|-------|----------|--------|---------|---------|---------|-------|
|  1   |  A Y  |     A    |  Rock  |    Y    |    1    |    3    |   4   |
|  2   |  B X  |     B    |  Rock  |    X    |    1    |    0    |   1   |
|  3   |  C Z  |     C    |  Rock  |    Z    |    1    |    6    |   7   |
|------|-------|----------|--------|---------|---------|---------|-------|

Pair 1: If the outcome is to be a Draw against Rock, player command should be Rock. Rock=1, Draw=3, Score=4.
Pair 2: If the outcome is to be a player loss against Paper, player command should be Rock. Rock=1, Loss=0, Score=1.
Pair 3: If the outcome is to be a player win against Scissors, player command should be Rock. Rock=1, Win=6, Score=7.
Total scores for player is 12 (4+1+7).";
}