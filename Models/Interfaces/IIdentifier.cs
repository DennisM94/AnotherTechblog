using System.ComponentModel.DataAnnotations;

namespace AnotherTechblog.Models.Interfaces
{
    public interface IIdentifier
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}