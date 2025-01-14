using Code.Challenge.Word.Finder;

namespace CodeChallenge_QU_WordFinder.UnitTests;

public class WordFinderQTests
{
    private WordFinder _wordFinder;

    [SetUp]
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
    }

    [Test]
    public void FindQ_WordExistsHorizontally_ReturnsWord()
    {
        var wordstream = new List<string> { "cat", "dog", "giraff" };
        var result = _wordFinder.FindQ(wordstream);
        Assert.Contains("cat", result.ToList());
        Assert.Contains("dog", result.ToList());
        Assert.Contains("giraff", result.ToList());
    }

    [Test]
    public void FindQ_WordExistsVertically_ReturnsWord()
    {
        var wordstream = new List<string> { "cat", "rat", "dog" };
        var result = _wordFinder.FindQ(wordstream);
        Assert.Contains("cat", result.ToList());
        Assert.Contains("rat", result.ToList());
        Assert.Contains("dog", result.ToList());
    }

    [Test]
    public void FindQ_WordDoesNotExist_ReturnsEmpty()
    {
        var wordstream = new List<string> { "lion", "tiger" };
        var result = _wordFinder.FindQ(wordstream);
        Assert.IsEmpty(result);
    }

    [Test]
    public void FindQ_Top10MostFrequentWords_ReturnsCorrectWords()
    {
        var wordstream = new List<string> { "cat", "cat", "dog", "dog", "dog", "giraff", "elephant", "elephant", "elephant", "elephant", "monkey" };
        var result = _wordFinder.FindQ(wordstream).OrderBy(x => x).ToList();
        var expected = new List<string> { "cat", "dog", "giraff", "monkey" };
        CollectionAssert.AreEqual(expected, result);
    }
}