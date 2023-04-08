using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnotherTechblog.Models;

namespace AnotherTechblog.Controllers
{
    public class EncryptionController : Controller
    {
        private readonly EncryptionCipherViewModel _model = new EncryptionCipherViewModel();

        public EncryptionController()
        {
            _model.PlainText = "";
            _model.CipherText = "";
        }
        public IActionResult EncryptCipher()
        {
            var model = new EncryptionCipherViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult EncryptCipher(EncryptionCipherViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Algorithm)
                {
                    case "caesar":
                        model.CipherText = EncryptCaesarCipher(model.PlainText, 3);
                        break;

                    case "vigenere":
                        model.CipherText = EncryptVigenereCipher(model.PlainText, "KEY");
                        break;

                    case "railfence":
                        model.CipherText = EncryptRailFenceCipher(model.PlainText, 3);
                        break;

                    default:
                        break;
                }
            }

            ViewBag.Title = "Encrypt Cipher";

            return View(model);
        }

        private string EncryptCaesarCipher(string plainText, int shift)
        {
            string cipherText = "";

            foreach (char c in plainText)
            {
                if (char.IsLetter(c))
                {
                    char cipherChar = (char)((((c - 65) + shift) % 26) + 65);
                    cipherText += cipherChar;
                }
                else
                {
                    cipherText += c;
                }
            }

            return cipherText;
        }

        private string EncryptVigenereCipher(string plainText, string key)
        {
            string cipherText = "";
            int keyIndex = 0;

            foreach (char c in plainText)
            {
                if (char.IsLetter(c))
                {
                    int plainIndex = char.ToUpper(c) - 65;
                    int keyChar = char.ToUpper(key[keyIndex % key.Length]) - 65;
                    int cipherIndex = (plainIndex + keyChar) % 26;
                    char cipherChar = (char)(cipherIndex + 65);
                    cipherText += cipherChar;

                    keyIndex++;
                }
                else
                {
                    cipherText += c;
                }
            }

            return cipherText;
        }

        private string EncryptRailFenceCipher(string plainText, int rails)
        {
            int plainTextLength = plainText.Length;
            int fullCycleLength = rails * 2 - 2;

            string cipherText = "";

            // First row
            for (int i = 0; i < plainTextLength; i += fullCycleLength)
            {
                cipherText += plainText[i];
            }

            // Rows between first and last
            for (int r = 1; r < rails - 1; r++)
            {
                for (int i = r; i < plainTextLength; i += fullCycleLength)
                {
                    cipherText += plainText[i];

                    int secondIndex = i + (fullCycleLength - r * 2);

                    if (secondIndex < plainTextLength)
                    {
                        cipherText += plainText[secondIndex];
                    }
                }
            }

            // Last row
            for (int i = rails - 1; i < plainTextLength; i += fullCycleLength)
            {
                cipherText += plainText[i];
            }

            return cipherText;
        }
    }
}
