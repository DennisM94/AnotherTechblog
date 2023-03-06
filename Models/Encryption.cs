using System.ComponentModel.DataAnnotations;
using AnotherTechblog.Models.Interfaces;

namespace AnotherTechblog.Models
{
    public class Encryption : IIdentifier
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string EncryptionName;
        public string InputMessage;
        public string OutputMessage;
    }
}
