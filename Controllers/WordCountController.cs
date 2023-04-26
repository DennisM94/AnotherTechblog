using AnotherTechblog.Data;
using AnotherTechblog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Web;

namespace AnotherTechblog.Controllers
{
    public class WordCountController : Controller
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly AnotherTechblogDbContext _context;

        public WordCountController(AnotherTechblogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _client = new HttpClient();
        }

        public IActionResult WordCount()
        {
            var model = new WordCountViewModel();
            ViewBag.Title = "Wordcounter for Wikipedia Articles";
            return View(model);
        }

        public string StripPunctuation(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }

        [HttpPost]
        public async Task<IActionResult> WordCount(WordCountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Download article content
            var html = await _client.GetStringAsync(model.ArticleUrl);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            var articleNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='mw-content-text']");
            var unwantedNodes = articleNode.SelectNodes("//script|//style|//table");
            if (unwantedNodes != null)
            {
                foreach (var unwantedNode in unwantedNodes)
                {
                    unwantedNode.Remove();
                }
            }

            var articleText = StripPunctuation(HttpUtility.HtmlDecode(articleNode.InnerText.Trim()));

            // Split article text into words
            var words = articleText.Split(' ');

            if (model.RemoveCommonWords)
            {
                words = FilterCommonWords(words).ToArray();
            }

            // Count occurrence of each word
            var wordCount = words.GroupBy(w => w.ToLower())
            .ToDictionary(g => g.Key, g => g.Count())
            .OrderByDescending(w => w.Value)
            .Take(15)
            .ToDictionary(x => x.Key, x => x.Value);

            // Save the top 15 words to the database
            foreach (var wc in wordCount)
            {
                var topWords = new WordCountResult
                {
                    ArticleUrl = model.ArticleUrl,
                    Word = wc.Key,
                    Count = wc.Value
                };
                _context.WordCountResults.Add(topWords);
            }
            await _context.SaveChangesAsync();
            // Serialize word count to JSON
            var wordCountJson = JsonConvert.SerializeObject(wordCount);

            model.WordCounts = wordCount;

            return View(model);
        }

        public async Task<string> GetRandomWikipediaArticleUrl()
        {
            var randomPageUrl = "https://en.wikipedia.org/wiki/Special:Random";
            var request = new HttpRequestMessage(HttpMethod.Get, randomPageUrl);
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

            HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            return response.RequestMessage!.RequestUri!.ToString();
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomWikipediaUrl()
        {
            var randomUrl = await GetRandomWikipediaArticleUrl();
            return Json(new { url = randomUrl });
        }


        private IEnumerable<string> FilterCommonWords(IEnumerable<string> words)
        {
            var commonWordsGerman = new HashSet<string> { "der", "die", "das", "und", "am", "bei", "in", "ist", "zu", "mit", "auf", "für", "ein", "eine", "einer", "nicht", "auch", "sich", "aus", "dem", "den", "im", "als", "wie", "oder", "aber", "dass", "nach", "wenn", "dann", "noch", "über", "vor", "zum", "einen", "zur", "bis", "an", "durch", "um", "von", "haben", "sein", "werden", "kann", "muss", "wird", "sind", "war", "es", "ich", "du", "er", "sie", "es", "wir", "ihr", "sie", "mein", "dein", "sein", "ihr", "unser", "euer", "ihre", "wurde", "vom", "mal", "des", "s", "l", "-" };
            var commonWordsEnglish = new HashSet<string> { "the", "it", "a", "and", "to", "in", "is", "you", "that", "of", "for", "on", "with", "at", "as", "by", "from", "about", "this", "an", "are", "be", "or", "which", "one", "have", "has", "but", "all", "your", "we", "if", "they", "will", "can", "not", "was", "what", "there", "out", "when", "up", "get", "so", "more", "see", "other", "some", "do", "would", "like", "these", "only", "its", "also", "how", "them", "then", "well", "should", "use", "just", "many", "make", "may", "any", "now", "new", "had", "he", "p", "pp", "were", "such", "where", "first", "who", "most", "way", "need", "know", "much", "could", "want", "find", "than", "been", "come", "every", "two", "too", "their", "p.", "iii", "through", "even", "into", "because", "over", "before", "pp.", "go", "work", "between", "must", "our", "after", "think", "best", "each", "never", "under", "right", "going", "own", "around", "still", "look", "might", "provide", "example", "those", "without", "end", "does", "here", "three", "my", "very", "same", "back", "say", "part", "take", "while", "his", "long", "little", "help", "something", "next", "much", "better", "anyone", "always", "though", "off", "us", "given", "last", "let", "great", "being", "less", "several", "away", "thing", "old", "year", "point", "however", "few", "yet", "down", "day", "another", "during", "things", "both", "around", "put", "different", "again", "able", "give", "once", "least", "why", "own", "keep", "done", "called", "number", "ask", "high", "line", "change", "mean", "large", "came", "tell", "group", "become", "important", "every", "include", "became", "run", "life", "order", "course", "fact", "far", "hand", "really", "small", "set", "often", "big", "four", "five", "seem", "felt", "late", "place", "didn't", "someone", "second", "move", "heard", "enough", "took", "person", "got", "nothing" };

            var commonWords = new HashSet<string> { string.Empty };
            commonWords.UnionWith(commonWordsEnglish);
            commonWords.UnionWith(commonWordsGerman);

            return words.Where(word => !commonWords.Contains(word.ToLower())).ToList();
        }
    
    }
}