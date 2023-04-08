using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnotherTechblog.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnotherTechblog.Controllers
{
    public class DecryptionController : Controller
    {
        // GET: /<controller>/
        public IActionResult DecryptCipher()
        {
            var model = new DecryptCipherViewModel
            {
                Cipher = ""
            };
            ViewBag.Title = "Decrypt Cipher";
            return View(model);
        }

        // GET: /<controller>/DecryptCaesar
        public IActionResult DecryptCaesar(string cipher, int shift)
        {
            string decryptedCipher = DecryptCaesarCipher(cipher, shift);

            ViewBag.DecryptedCipher = decryptedCipher;

            return View();
        }

        // GET: /<controller>/DecryptVigenere
        public IActionResult DecryptVigenere(string cipher, string key)
        {
            string decryptedCipher = DecryptVigenereCipher(cipher, key);

            ViewBag.DecryptedCipher = decryptedCipher;

            return View();
        }

        // GET: /<controller>/DecryptRailFence
        public IActionResult DecryptRailFence(string cipher, int rails)
        {
            string decryptedCipher = DecryptRailFenceCipher(cipher, rails);

            ViewBag.DecryptedCipher = decryptedCipher;

            return View();
        }

        private string DecryptCaesarCipher(string cipher, int shift)
        {
            string decryptedCipher = "";

            foreach (char c in cipher)
            {
                if (char.IsLetter(c))
                {
                    char decryptedChar = (char)((((c - 65) + shift) % 26) + 65);
                    decryptedCipher += decryptedChar;
                }
                else
                {
                    decryptedCipher += c;
                }
            }

            return decryptedCipher;
        }

        private string DecryptVigenereCipher(string cipher, string key)
        {
            string decryptedCipher = "";
            int keyIndex = 0;

            foreach (char c in cipher)
            {
                if (char.IsLetter(c))
                {
                    int cipherIndex = char.ToUpper(c) - 65;
                    int keyChar = char.ToUpper(key[keyIndex % key.Length]) - 65;
                    int decryptedIndex = (cipherIndex - keyChar + 26) % 26;
                    char decryptedChar = (char)(decryptedIndex + 65);
                    decryptedCipher += decryptedChar;

                    keyIndex++;
                }
                else
                {
                    decryptedCipher += c;
                }
            }

            return decryptedCipher;
        }

        private string DecryptRailFenceCipher(string cipher, int rails)
        {
            int cipherLength = cipher.Length;
            int fullCycleLength = rails * 2 - 2;

            string decryptedCipher = "";

            // First row
            for (int i = 0; i < cipherLength; i += fullCycleLength)
            {
                decryptedCipher += cipher[i];
            }

            // Rows between first and last
            for (int r = 1; r < rails - 1; r++)
            {
                for (int i = r; i < cipherLength; i += fullCycleLength)
                {
                    decryptedCipher += cipher[i];

                    int secondIndex = i + (fullCycleLength - r * 2);

                    if (secondIndex < cipherLength)
                    {
                        decryptedCipher += cipher[secondIndex];
                    }
                }
            }

            // Last row
            for (int i = rails - 1; i < cipherLength; i += fullCycleLength)
            {
                decryptedCipher += cipher[i];
            }

            return decryptedCipher;
        }
    }
}
