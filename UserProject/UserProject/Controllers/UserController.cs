
using Microsoft.AspNetCore.Mvc;
using UserProject.Data;
using UserProject.Models;
using System.Linq;
using System.Web;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;


namespace UserProject.Controllers
{
    public class UserController : Controller
    {
        private readonly bool UseDb = true;
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = new List<User>();

            if (UseDb)
            {
                users = _context.Users.ToList();
            }

            else {
                string filePath = "..\\UserProject\\Data\\UsersDB.csv";
                string[] lines = System.IO.File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    // פיצול השורה לפי פסיקים
                    string[] values = line.Split(',');
                    User user = new User();
                    user.Id = int.Parse(values[0]);
                    user.FirstName = values[1];
                    user.LastName = values[2];
                    user.IDNumber = values[3];

                    users.Add(user);

                }
            
            }
            return View(users);
        }

        [HttpGet]
        public IActionResult Ella(string name)
        {
            var users = new List<User>();
            if (UseDb)
            {
                users = users = _context.Users
                .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
                .ToList();
            }
            else
            {
                string filePath = "..\\UserProject\\Data\\UsersDB.csv";
                string[] lines = System.IO.File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    // פיצול השורה לפי פסיקים
                    string[] values = line.Split(',');
                    if (values[1] == name || values[2] == name)
                    {
                        User user = new User();
                        user.Id = int.Parse(values[0]);
                        user.FirstName = values[1];
                        user.LastName = values[2];
                        user.IDNumber = values[3];

                        users.Add(user);
                    }

                }
            }
           
            return View("Index", users);
        }
    }
}
