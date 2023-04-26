using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace AnotherTechblog.Models
{
    public class WordCountResult
    {
        public WordCountResult()
        {
            Id = new Guid();
        }
        [Key]
        public Guid Id { get; set; }
        public string ArticleUrl { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
