using Microsoft.AspNetCore.Mvc;
using UserProject.Data;
using UserProject.Models;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly bool UseDb = true;
    public ApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = new List<User>();

        if (UseDb)
        {
            return Ok(_context.Users.ToList());
        }
        else
        {
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
            return Ok(users);
        }

        
    }

    [HttpGet("{name}")]
    public IActionResult GetUsersByName(string name)
    {
        var users = new List<User>();
        if (UseDb)
        {
            users = _context.Users
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
        return Ok(users);
    }
}
