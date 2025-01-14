using BenchmarkDotNet.Attributes;
using Code.Challenge.Word.Finder;

public class WordFinderBenchmark
{
    private WordFinder? _wordFinder;
    private List<string>? _wordstream;

    [GlobalSetup]
    public void Setup()
    {
        var matrix = new List<string>
        {
            "catdoga",
            "aldpher",
            "tloghaa",
            "ntgebrt",
            "girafff",
            "monkeys",
            "dramter"
        };
        _wordFinder = new WordFinder(matrix);
        _wordstream = new List<string> { "cat", "cat", "dog", "dog", "dog", "giraff", "elephant", "elephant", "elephant", "elephant", "monkey" };
    }

    [Benchmark]
    public void FindBenchmark()
    {
        _wordFinder.Find(_wordstream).ToList();
    }

    [Benchmark]
    public void FindQBenchmark()
    {
        _wordFinder.FindQ(_wordstream).ToList();
    }
}
