namespace AnotherTechblog.Models
{
    public class DecryptCipherViewModel
    {
        public DecryptCipherViewModel()
        {
            Cipher = "";
        }

        public string Cipher { get; set; }
        public string Algorithm { get; set; }
        public string DecryptedCipher { get; set; }
    }
}
