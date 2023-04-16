using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnotherTechblog.Models
{
    public class WordCountViewModel
    {
        [Required(ErrorMessage = "Please enter a valid Wikipedia article URL.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [RegularExpression(@"^https:\/\/(?:en|de)\.wikipedia\.org\/wiki\/[\w-]+$", ErrorMessage = "Please enter a valid Wikipedia article URL.")]
        public string ArticleUrl { get; set; }

        public Dictionary<string, int> WordCounts { get; set; }
        public bool RemoveCommonWords { get; set; }

        public List<KeyValuePair<string, int>> TopWords
        {
            get { return WordCounts.OrderByDescending(x => x.Value).Take(15).ToList(); }
        }

        public WordCountViewModel()
        {
            WordCounts = new Dictionary<string, int>();
        }
    }
}
