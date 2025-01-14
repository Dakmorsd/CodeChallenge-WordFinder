using Code.Challenge.Word.Finder;

namespace CodeChallenge_QU_WordFinder.UnitTests;

public class WordFinderTests
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
    public void Find_WordExistsHorizontally_ReturnsWord()
    {
        var wordstream = new List<string> { "cat", "dog", "giraff" };
        var result = _wordFinder.Find(wordstream);
        Assert.Contains("cat", result.ToList());
        Assert.Contains("dog", result.ToList());
        Assert.Contains("giraff", result.ToList());
    }

    [Test]
    public void Find_WordExistsVertically_ReturnsWord()
    {
        var wordstream = new List<string> { "cat", "rat", "dog" };
        var result = _wordFinder.Find(wordstream);
        Assert.Contains("cat", result.ToList());
        Assert.Contains("rat", result.ToList());
        Assert.Contains("dog", result.ToList());
    }

    [Test]
    public void Find_WordDoesNotExist_ReturnsEmpty()
    {
        var wordstream = new List<string> { "lion", "tiger" };
        var result = _wordFinder.Find(wordstream);
        Assert.IsEmpty(result);
    }

    [Test]
    public void Find_Top10MostFrequentWords_ReturnsCorrectWords()
    {
        var wordstream = new List<string> { "cat", "cat", "dog", "dog", "dog", "giraff", "elephant", "elephant", "elephant", "elephant", "monkey" };
        var result = _wordFinder.Find(wordstream).OrderBy(x => x).ToList();
        var expected = new List<string> { "cat", "dog", "giraff", "monkey" };
        CollectionAssert.AreEqual(expected, result);
    }
}