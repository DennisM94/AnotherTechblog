using System.ComponentModel.DataAnnotations;

namespace AnotherTechblog.Models
{
    public class WordCountViewModel
    {
        [Required(ErrorMessage = "Please enter a valid Wikipedia article URL.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string ArticleUrl { get; set; }

        public Dictionary<string, int> WordCounts { get; set; }

        public List<KeyValuePair<string, int>> TopWords
        {
            get { return WordCounts.OrderByDescending(x => x.Value).Take(15).ToList(); }
        }
    }
}
