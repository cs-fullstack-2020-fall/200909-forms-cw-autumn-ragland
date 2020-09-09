using System.Collections.Generic;
using System.Linq;
using Starter.DAO;
using Starter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        // view all user
        public IActionResult ViewAll()
        {
            return View(_context);
        }
        // view user by ID
        public IActionResult ViewOne(int userID)
        {
            UserModel founduser = _context.users.FirstOrDefault(user => user.id == userID);
            if(founduser != null)
            {
                return View(founduser);
            } else 
            {
                ViewData["error"] = "No user found";
                return View("Error");
            }
        }
        // add user to db
        [HttpPost]
        public IActionResult AddUser(UserModel newUser)
        {
            if(ModelState.IsValid)
            {
                _context.users.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("ViewAll", "Primary");                
            } else 
            {
                string displayErr = "";
                List<string> errors = GetErrorListFromModelState(ModelState);
                errors.ForEach(err => displayErr += $" {err} ");
                ViewData["errors"] = displayErr;
                return View("userForm", newUser);
            }

        }
        public IActionResult UserForm()
        {
            return View();
        }
        // update user in db by ID
        [HttpPost]
        public IActionResult UpdateUser(UserModel updateUser)
        {
            UserModel founduser = _context.users.FirstOrDefault(user => user.id == updateUser.id);
            if(ModelState.IsValid)
            {
                founduser.fName = updateUser.fName;
                founduser.lName = updateUser.lName;
                founduser.userName = updateUser.userName;
                _context.SaveChanges();
                return RedirectToAction("ViewAll", "Primary");        
            } else
            {
                string displayErr = "";
                List<string> errors = GetErrorListFromModelState(ModelState);
                errors.ForEach(err => displayErr += $" {err} ");
                ViewData["errors"] = displayErr;
                return View("EditForm", updateUser);               
            }

        }
        public IActionResult EditForm(int userID)
        {
            UserModel founduser = _context.users.FirstOrDefault(user => user.id == userID);
            if(founduser != null)
            {
                return View(founduser);
            } else
            {
                ViewData["error"] = "No user found";
                return View("Error");
            }
        }
        // delete user in db by ID
        [HttpGet]
        public IActionResult DeleteUser(int userID)
        {
            UserModel foundUser = _context.users.FirstOrDefault(user => user.id == userID);
            if(foundUser != null)
            {
                _context.Remove(foundUser);
                _context.SaveChanges();
                return RedirectToAction("ViewAll", "Primary");
            } else
            {
                ViewData["error"] = "No user found to delete";
                return View("Error");
            }
        }
        // method to capture model state validation errors
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<string> query = from state in modelState.Values
                from error in state.Errors
                select error.ErrorMessage;

            List<string> errorList = query.ToList();
            return errorList;
        }
    }
}