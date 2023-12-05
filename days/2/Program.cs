using System.Text.RegularExpressions;
using System.Text.Json;

var gameRegex = new Regex("Game (?<game_id>\\d+): (?<game_string>.+)");
var colorRegex = new Regex("(?<quantity>\\d+)\\s+(?<color>\\w+)");

List<Game> games = new List<Game>();

foreach (var line in File.ReadLines("input.txt"))
{
  var gameRegexMatch = gameRegex.Match(line);
  var game = new Game()
  {
    Id = int.Parse(gameRegexMatch.Groups["game_id"].Value),
    Colors = new Dictionary<string, List<int>>()
  };
  var gameString = gameRegexMatch.Groups["game_string"].Value;

  foreach (var round in gameString.Split(';'))
  {
    foreach (var color in round.Split(','))
    {
      var colorMatch = colorRegex.Match(color);
      var colorName = colorMatch.Groups["color"].Value;
      var colorQuantity = int.Parse(colorMatch.Groups["quantity"].Value);

      if (!game.Colors.ContainsKey(colorName))
      {
        game.Colors.Add(colorName, new List<int>());
      }
      game.Colors[colorName].Add(colorQuantity);
    }
  }

  games.Add(game);
}

var possibleGames = games.Where(x =>
  !x.Colors["red"].Any(y => y > 12)
  && !x.Colors["green"].Any(y => y > 13)
  && !x.Colors["blue"].Any(y => y > 14));

Console.WriteLine($"Part 1: {possibleGames.Sum(x => x.Id)}");

Console.WriteLine($"Part 2: {games.Sum(x =>
  x.Colors.Keys.Aggregate(1, (acc, val) => acc * x.Colors[val].Max())
)}");

class Game
{
  public int Id { get; set; }
  public Dictionary<string, List<int>> Colors { get; set; }
}
