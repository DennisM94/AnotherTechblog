using System.ComponentModel.DataAnnotations;
using AnotherTechblog.Models.Interfaces;

namespace AnotherTechblog.Models
{
    public class BlogPost : IIdentifier
    {
        public BlogPost()
        {
            this.Id = Guid.NewGuid();
            this.Created= DateTime.Now;
            this.Updated = DateTime.MinValue;
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

    }
}
