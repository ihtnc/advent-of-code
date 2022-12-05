using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day2;

public class Part1Description : Description
{
    public override string Text =>
@"Given a list of command pairs, calculate the total player score based on the following rules:
1. First command is for the opponent and can be: A (Rock), B (Paper), C (Scissors).
2. Second command is for the player and can be: X (Rock), Y (Paper), Z (Scissors).
3. Each command has a particular value: Rock=1, Paper=2, Scissors=3.
4. Each outcome has a particular value: Player win=6, Draw=3, Player lose=0.
5. Score = sum of the command and outcome values.
6. Total score = Sum of all the player scores.";

    public override string Example =>
@"Given: [A Y B X C Z]
Output: 15";

    public override string Explanation =>
@"There are 3 command pairs in the list:
|------|-------|-----------------------------|---------------------------|
|      |       |             Move            |           Score           |
|------|-------|-----------------------------|---------------------------|
| Pair | Value | Opponent | Player | Outcome | Command | Outcome | Total |
|------|-------|----------|--------|---------|---------|---------|-------|
|  1   |  A Y  |     A    |   Y    |   Win   |    2    |    6    |   8   |
|  2   |  B X  |     B    |   X    |  Lose   |    1    |    0    |   1   |
|  3   |  C Z  |     C    |   Z    |  Draw   |    3    |    3    |   6   |
|------|-------|----------|--------|---------|---------|---------|-------|

Pair 1: Rock vs Paper results in a player win. Paper=2, Win=6, Score=8.
Pair 2: Paper vs Rock results in a player loss. Rock=1, Loss=0, Score=1.
Pair 3: Scissors vs Scissors results in a draw. Scissors=3, Draw=3, Score=6.
Total scores for player is 15 (8+1+6).";
}