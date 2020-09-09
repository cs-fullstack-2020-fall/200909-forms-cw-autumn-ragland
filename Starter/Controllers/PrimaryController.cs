using System.Collections.Generic;
using System.Linq;
using Starter.DAO;
using Starter.Models;
using Microsoft.AspNetCore.Mvc;

namespace Starter.Controllers
{
    public class Primary : Controller
    {
        private readonly StarterDbContext _context;
        public Primary(StarterDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}