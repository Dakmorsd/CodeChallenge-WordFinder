using BenchmarkDotNet.Running;
using Code.Challenge.Word.Finder;

//
//| Method         | Mean       | Error     | StdDev    |
//| ---------------| -----------| ----------| ----------|
//| FindBenchmark  | 4.022 us   | 0.0353 us | 0.0331 us |
//| FindQBenchmark | 356.013 us | 1.2256 us | 0.9568 us |
//

var matrix = new List<string>
{
    "abcdc",
    "fgwio",
    "chill",
    "pqnsd",
    "uvdxy",
};

var wordFinder = new WordFinder(matrix);
var words = new List<string> { "cold", "wind", "chill" };
var foundWords = wordFinder.Find(words);

foreach (var word in foundWords)
{
    Console.WriteLine(word);
}

//Uncoment to run the benchmark (it will take a while) - Use release mode
//var summary = BenchmarkRunner.Run<WordFinderBenchmark>();
