using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            try
            {
                var urls = new[]
                {
                    "https://www.ime.usp.br/~pf/dicios/br-sem-acentos.txt",
                    "https://raw.githubusercontent.com/pythonprobr/palavras/refs/heads/master/palavras.txt"
                };

                Console.WriteLine("Insira a letra central obrigatória:");
                char centerLetter = char.ToLowerInvariant(Console.ReadLine()?.FirstOrDefault() ?? 'z');

                Console.WriteLine("Insira as letras permitidas (separadas por espaço):");
                string? allowedLettersInput = Console.ReadLine();
                var allowedLetters = new HashSet<char>(
                    allowedLettersInput?
                        .ToLowerInvariant()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .SelectMany(s => s.ToCharArray()) ?? Array.Empty<char>());
                

                if (allowedLetters.Count == 0)
                {
                    Console.WriteLine("Nenhuma letra permitida informada.");
                    return;
                }

                allowedLetters.Add(centerLetter);

                IWordLoader wordLoader = new MultiUrlWordLoader(urls);
                List<string> words = await wordLoader.LoadWordsAsync();

                if (words.Count > 0)
                {
                    IWordValidator wordValidator = new WordValidator(centerLetter, allowedLetters);
                    var validWords = wordValidator.FindValidWords(words);

                    Console.WriteLine($"Possibilidades de palavras válidas: {validWords.Count}");
                    foreach (var word in validWords)
                    {
                        Console.WriteLine(word);
                    }
                }
                else
                {
                    Console.WriteLine("Nenhuma palavra carregada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }

    public interface IWordLoader
    {
        Task<List<string>> LoadWordsAsync();
    }

    public class MultiUrlWordLoader : IWordLoader
    {
        private readonly IEnumerable<string> _urls;

        public MultiUrlWordLoader(IEnumerable<string> urls)
        {
            _urls = urls;
        }

        public async Task<List<string>> LoadWordsAsync()
        {
            try
            {
                var uniqueWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using HttpClient client = new HttpClient();
                foreach (var url in _urls)
                {
                    try
                    {
                        string content = await client.GetStringAsync(url);
                        var words = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            if (!string.IsNullOrWhiteSpace(word))
                            {
                                uniqueWords.Add(word.Trim());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao baixar ou processar o arquivo {url}: {ex.Message}");
                    }
                }

                return uniqueWords.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao baixar ou processar o arquivo: {ex.Message}");
                return new List<string>();
            }
        }
    }

    public interface IWordValidator
    {
        List<string> FindValidWords(List<string> words);
    }

    public class WordValidator : IWordValidator
    {
        private readonly char _centerLetter;
        private readonly HashSet<char> _allowedLetters;

        public WordValidator(char centerLetter, HashSet<char> allowedLetters)
        {
            _centerLetter = centerLetter;
            _allowedLetters = allowedLetters;
        }

        public List<string> FindValidWords(List<string> words)
        {
            return words.Where(IsValidWord).OrderBy(w => w.Length).ToList();
        }

        private bool IsValidWord(string word)
        {
            string lowerWord = word.ToLower();
            lowerWord = lowerWord
                .Replace('ã', 'a')
                .Replace('á', 'a')
                .Replace('à', 'a')
                .Replace('â', 'a')
                .Replace('ä', 'a')
                .Replace('é', 'e')
                .Replace('è', 'e')
                .Replace('ê', 'e')
                .Replace('ê', 'e')
                .Replace('ë', 'e')
                .Replace('í', 'i')
                .Replace('ì', 'i')
                .Replace('î', 'i')
                .Replace('ï', 'i')
                .Replace('ó', 'o')
                .Replace('ò', 'o')
                .Replace('õ', 'o')
                .Replace('ô', 'o')
                .Replace('ö', 'o')
                .Replace('ú', 'u')
                .Replace('ù', 'u')
                .Replace('ü', 'u')
                .Replace('û', 'u');
            lowerWord = lowerWord.Replace('ç', 'c');
            return lowerWord.Length >= 4 && lowerWord.Length <= 20 &&
                   lowerWord.Contains(_centerLetter) &&
                   lowerWord.All(_allowedLetters.Contains);
        }
    }
}
