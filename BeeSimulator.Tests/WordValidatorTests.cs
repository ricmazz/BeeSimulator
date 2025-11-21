using System.Collections.Generic;
using System.Linq;
using WordFinder;
using Xunit;

namespace BeeSimulator.Tests
{
    public class WordValidatorTests
    {
        private static WordValidator CreateValidator(char center, params char[] allowed)
        {
            var allowedSet = new HashSet<char>(allowed) { center };
            return new WordValidator(center, allowedSet);
        }

        [Fact]
        public void FiltersWordsWithoutCenterLetter()
        {
            var validator = CreateValidator('z', 'a', 'r', 'o');
            var input = new List<string> { "arroz", "aroma", "zorro" };

            var result = validator.FindValidWords(input);

            Assert.DoesNotContain("aroma", result); // missing center letter
            Assert.Contains("arroz", result);
            Assert.Contains("zorro", result);
        }

        [Fact]
        public void RejectsWordsWithForbiddenLetters()
        {
            var validator = CreateValidator('z', 'a', 'r', 'o');
            var input = new List<string> { "arroz", "azedo" };

            var result = validator.FindValidWords(input);

            Assert.DoesNotContain("azedo", result); // contains forbidden letters e/d
            Assert.Contains("arroz", result);
        }

        [Fact]
        public void RejectsWordsOutsideLengthLimits()
        {
            var validator = CreateValidator('z', 'a', 'r', 'o');
            var input = new List<string> { "zoo", "zoologistas" };

            var result = validator.FindValidWords(input);

            Assert.Empty(result); // one too short, one too long
        }

        [Fact]
        public void OrdersByLengthAscending()
        {
            var validator = CreateValidator('z', 'a', 'r', 'o');
            var input = new List<string> { "arroz", "zara", "zoar" };

            var result = validator.FindValidWords(input);

            Assert.True(result.SequenceEqual(new[] { "zara", "zoar", "arroz" }));
        }

        [Fact]
        public void NormalizesAccentedLetters()
        {
            var validator = CreateValidator('a', 'r', 'z', 'o');
            var input = new List<string> { "razão", "árvore", "zorra" };

            var result = validator.FindValidWords(input);

            Assert.Contains("razão", result); // razão -> razao
            Assert.Contains("zorra", result);
            Assert.DoesNotContain("árvore", result); // árvore contains v
        }
    }
}
