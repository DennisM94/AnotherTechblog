using System.ComponentModel.DataAnnotations;
using AnotherTechblog.Models.Interfaces;

namespace AnotherTechblog.Models
{
    public class Encryption : IIdentifier
    {
        public Encryption(string encName, string inp)
        {
            this.Id= Guid.NewGuid();
            this.Created = DateTime.Now;
            this.Updated = DateTime.MinValue;
            this.EncryptionName = encName;
            this.InputMessage = inp;
            this.OutputMessage = string.Empty;
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string EncryptionName;
        public string InputMessage;
        public string OutputMessage;
    }
}
