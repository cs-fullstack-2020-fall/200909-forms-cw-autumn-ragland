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
        // ref to db 
        private readonly StarterDbContext _context;
        public Primary(StarterDbContext context)
        {
            _context = context;
        }
        // initial view for sanity check
        public IActionResult Index()
        {
            return View(); // render default view
        }
        // view all user from db
        public IActionResult ViewAll()
        { 
            return View(_context); // render default view and pass ref to database to use as view model
        }
        // view user by ID
        public IActionResult ViewOne(int userID)
        {
            // find user by ID in users db set
            UserModel founduser = _context.users.FirstOrDefault(user => user.id == userID);
            // if user is found
            if(founduser != null)
            {
                // return default view and pass found user to use as view model
                return View(founduser);
            } else 
            // if user is not found
            {
                // add message to view data dictionary
                ViewData["error"] = "No user found";
                // return error view
                return View("Error");
            }
        }
        // add user to db
        [HttpPost]
        public IActionResult AddUser(UserModel newUser)
        {
            // if model created from form passes validation set using data annotations
            if(ModelState.IsValid)
            {
                // add the object created from form to db set
                _context.users.Add(newUser);
                // save changes to db
                _context.SaveChanges();
                // redirect to the ViewAll method in the Primary Controller
                return RedirectToAction("ViewAll", "Primary");                
            } else 
            // if model created from form does not pass validation set using data annotations
            {
                string displayErr = ""; // string to build
                // method to return errors from inValid model
                List<string> errors = GetErrorListFromModelState(ModelState);
                // iterate through errors from invalid model and append each to display string
                errors.ForEach(err => displayErr += $" {err} ");
                // add message to view data dictionary
                ViewData["errors"] = displayErr;
                // render UserForm view and pass invalid model
                return View("userForm", newUser);
            }
        }
        // display user form view
        public IActionResult UserForm()
        {
            // display default view
            return View();
        }
        // update user in db by ID
        [HttpPost]
        public IActionResult UpdateUser(UserModel updateUser)
        {
            // find user by ID
            UserModel founduser = _context.users.FirstOrDefault(user => user.id == updateUser.id);
            // if model created from form passes validation set using data annotations
            if(ModelState.IsValid)
            {
                // update the value of the found user in the database to the values from the object passed in (from form)
                founduser.fName = updateUser.fName;
                founduser.lName = updateUser.lName;
                founduser.userName = updateUser.userName;
                // save changes to the db
                _context.SaveChanges();
                // redirect to the VIewAll method in the Primary Controller
                return RedirectToAction("ViewAll", "Primary");        
            } else
            // if model created from form does not pass validation set using data annotations
            {
                string displayErr = ""; // string to build
                // method to return errors from inValid model
                List<string> errors = GetErrorListFromModelState(ModelState);
                // iterate through errors from invalid model and append each to display string
                errors.ForEach(err => displayErr += $" {err} ");
                // add message to view data dictionary
                ViewData["errors"] = displayErr;
                // render EditForm view and pass invalid model
                return View("EditForm", updateUser);               
            }

        }
        // display Edit Form view
        public IActionResult EditForm(int userID)
        {
            // find user by ID
            UserModel founduser = _context.users.FirstOrDefault(user => user.id == userID);
            // if user is found
            if(founduser != null)
            {
                // display default view and pass found user to use as view model
                return View(founduser);
            } else
            // if user is not found
            {
                // add message to view data dictionary
                ViewData["error"] = "No user found";
                // return error view
                return View("Error");
            }
        }
        // delete user in db by ID
        [HttpGet]
        public IActionResult DeleteUser(int userID)
        {
            // find user by ID
            UserModel foundUser = _context.users.FirstOrDefault(user => user.id == userID);
            // if user is found
            if(foundUser != null)
            {
                // remove user form db
                _context.Remove(foundUser);
                // save changes to db
                _context.SaveChanges();
                // redirect to ViewAll method in Primary controller
                return RedirectToAction("ViewAll", "Primary");
            } else
            // is user is not found
            {
                // add message to view data dictionary
                ViewData["error"] = "No user found";
                // return error view
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