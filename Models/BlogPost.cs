using System.ComponentModel.DataAnnotations;

namespace AnotherTechblog.Models
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

    }
}
