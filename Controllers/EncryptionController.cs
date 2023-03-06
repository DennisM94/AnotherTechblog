using AnotherTechblog.Data;
using AnotherTechblog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AnotherTechblog.Controllers
{
    public class EncryptionController : Controller
    {
        private readonly ILogger<EncryptionController> _logger;
        private string _encryptionKey;
        private string input;
        private readonly AnotherTechblogDbContext _context; 


        public EncryptionController(ILogger<EncryptionController> logger, AnotherTechblogDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Encryption()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
