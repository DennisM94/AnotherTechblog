using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AnotherTechblog.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnotherTechblog.Controllers
{
    public class WordCountController : Controller
    {
        private readonly HttpClient _client = new HttpClient();

        public IActionResult CountWords()
        {
            var model = new WordCountViewModel();
            ViewBag.Title = "Wordcounter for Wikipedia Articles";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CountWords(string url)
        {
            if (!url.StartsWith("https://en.wikipedia.org/wiki/"))
            {
                ModelState.AddModelError("", "Invalid Wikipedia article URL.");
                return View();
            }

            // Download the Wikipedia article content
            var html = await _client.GetStringAsync(url);

            // Parse the HTML using HtmlAgilityPack
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Get the article text
            var articleNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='mw-content-text']");
            var articleText = HttpUtility.HtmlDecode(articleNode.InnerText.Trim());

            // Split the article text into words
            var words = articleText.Split(' ');

            // Count the occurrence of each word
            var wordCount = words.GroupBy(w => w.ToLower())
                .ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(w => w.Value)
                .Take(15)
                .ToDictionary(x => x.Key, x => x.Value);

            // Serialize the word count to JSON
            var wordCountJson = JsonConvert.SerializeObject(wordCount);

            // Create the view model
            var model = new WordCountViewModel
            {
                ArticleUrl = url,
                WordCounts = wordCount
            };

            return View(model);
        }

    }
}
