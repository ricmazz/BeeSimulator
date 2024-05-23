using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordFinder
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                Console.WriteLine("Insira a URL com as palavras:");
                string? urlWords = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(urlWords))
                {
                    Console.WriteLine("URL inválida.");
                    return;
                }

                Console.WriteLine("Insira a letra central obrigatória:");
                char centerLetter = Console.ReadLine()?.FirstOrDefault() ?? 'z';

                Console.WriteLine("Insira as letras permitidas (separadas por espaço):");
                string? allowedLettersInput = Console.ReadLine();
                var allowedLetters = new HashSet<char>(allowedLettersInput?.Split(' ').SelectMany(s => s.ToCharArray()) ?? new char[0]);

                if (allowedLetters.Count == 0)
                {
                    Console.WriteLine("Nenhuma letra permitida informada.");
                    return;
                }

                allowedLetters.Add(centerLetter);

                IWordLoader wordLoader = new UrlWordLoader(urlWords);
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

    public class UrlWordLoader : IWordLoader
    {
        private readonly string _url;

        public UrlWordLoader(string url)
        {
            _url = url;
        }

        public async Task<List<string>> LoadWordsAsync()
        {
            try
            {
                using HttpClient client = new HttpClient();
                string content = await client.GetStringAsync(_url);
                return content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
            return lowerWord.Length >= 4 && lowerWord.Length <= 8 &&
                   lowerWord.Contains(_centerLetter) &&
                   lowerWord.All(_allowedLetters.Contains);
        }
    }
}
