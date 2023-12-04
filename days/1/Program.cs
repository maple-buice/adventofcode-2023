using System.Text.RegularExpressions;
using System.Text.Json;

var numberDict = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase) {
  {"0", 0}, //{"zero", 0},
  {"1", 1}, {"one", 1},
  {"2", 2}, {"two", 2},
  {"3", 3}, {"three", 3},
  {"4", 4}, {"four", 4},
  {"5", 5}, {"five", 5},
  {"6", 6}, {"six", 6},
  {"7", 7}, {"seven", 7},
  {"8", 8}, {"eight", 8},
  {"9", 9}, {"nine", 9},
};

var regexPattern = $"({string.Join("|", numberDict.Keys)})";
var numberRegexLeftRight = new Regex(regexPattern, RegexOptions.Compiled);
var numberRegexRightLeft = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.RightToLeft);

int sum = 0;

foreach (var line in File.ReadLines("input.txt")) {
  sum += int.Parse(
    string.Concat(
      numberDict[numberRegexLeftRight.Match(line).Value],
      numberDict[numberRegexRightLeft.Match(line).Value]));
}

Console.WriteLine(sum);
