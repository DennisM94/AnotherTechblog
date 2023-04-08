using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnotherTechblog.Models
{
    public class EncryptionCipherViewModel
    {
        public EncryptionCipherViewModel()
        {
            PlainText = "";
        }
        public string PlainText { get; set; }
        public string Cipher { get; set; }
        public string Algorithm { get; set; }
    }
}