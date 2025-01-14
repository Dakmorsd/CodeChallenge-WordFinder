namespace Code.Challenge.Word.Finder
{
    /// <summary>
    /// The WordFinder class provides functionality to search for words in a given matrix.
    /// </summary>
    public class WordFinder
    {
        private readonly char[,] _matrix;
        private readonly int _rows;
        private readonly int _cols;

        /// <summary>
        /// Initializes a new instance of the WordFinder class with the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix of characters to search within.</param>
        public WordFinder(IEnumerable<string> matrix)
        {
            var matrixList = matrix.ToList();
            _rows = matrixList.Count;
            _cols = matrixList[0].Length;
            _matrix = new char[_rows, _cols];

            // Initialize the matrix
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    _matrix[i, j] = matrixList[i][j];
                }
            }
        }

        /// <summary>
        /// Finds the top 10 most frequently occurring words from the wordstream in the matrix.
        /// </summary>
        /// <param name="wordstream">The stream of words to search for.</param>
        /// <returns>An enumerable of the top 10 most frequently found words.</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordSet = new HashSet<string>(wordstream);
            var foundWords = new Dictionary<string, int>();

            // Search for each word in the wordstream
            Parallel.ForEach(wordSet, word =>
            {
                if (SearchWord(word))
                {
                    lock (foundWords)
                    {
                        foundWords[word] = foundWords.TryGetValue(word, out int value) ? ++value : 1;
                    }
                }
            });

            // Return the top 10 most frequently found words
            return foundWords.OrderByDescending(kv => kv.Value)
                             .Take(10)
                             .Select(kv => kv.Key);
        }

        /// <summary>
        /// Finds the top 10 most frequently occurring words from the wordstream in the matrix using LINQ.
        /// </summary>
        /// <param name="wordstream">The stream of words to search for.</param>
        /// <returns>An enumerable of the top 10 most frequently found words.</returns>
        public IEnumerable<string> FindQ(IEnumerable<string> wordstream)
        {
            var wordSet = new HashSet<string>(wordstream);
            var foundWords = wordSet.AsParallel()
                                    .Where(word => SearchWord(word))
                                    .GroupBy(word => word)
                                    .ToDictionary(g => g.Key, g => g.Count());

            return foundWords.OrderByDescending(kv => kv.Value)
                             .Take(10)
                             .Select(kv => kv.Key);
        }

        private bool SearchWord(string word)
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    // Check both horizontally and vertically
                    if (SearchHorizontally(i, j, word) || SearchVertically(i, j, word))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool SearchHorizontally(int row, int col, string word)
        {
            if (col + word.Length > _cols) return false;

            for (int k = 0; k < word.Length; k++)
            {
                if (_matrix[row, col + k] != word[k])
                {
                    return false;
                }
            }
            return true;
        }

        private bool SearchVertically(int row, int col, string word)
        {
            if (row + word.Length > _rows) return false;

            for (int k = 0; k < word.Length; k++)
            {
                if (_matrix[row + k, col] != word[k])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
