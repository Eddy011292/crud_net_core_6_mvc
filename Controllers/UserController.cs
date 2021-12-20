using CRUD.Data;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
   
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(long? idUser)
        {
            List<User> users = new();

            try
            {
                if (idUser != null)
                {
                    TempData["Update"] = true;
                    TempData["Data"] = await Task.Run(() => _db.Users.Where(u => u.Id == idUser && u.Active == true).FirstOrDefault());
                }

                users = await Task.Run(() => _db.Users.Where(u => u.Active == true).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller : User | Action : Index | Error : " + ex.Message);
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User users)
        {
            try
            {
                if (await Task.Run(() => _db.Users.Where(x => x.Name.Equals(users.Name) && x.Active == true).Any()))
                {
                    TempData["Error"] = "User already exist!";
                }
                else
                {
                    users.Active = true;
                    await _db.Users.AddAsync(users);
                    var resp = await _db.SaveChangesAsync();
                    if (resp == 1)
                    {
                        TempData["Success"] = "User created successfully!";
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller : User | Action : Create | Error : " + ex.Message);
                TempData["Error"] = "Error creating user";
            }

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user, long idUser)
        {
            try
            {
                user.Id = idUser;
                user.Active = true;
                await Task.Run(() => _db.Users.Update(user));
                await _db.SaveChangesAsync();

                TempData["Success"] = "User updated successfully!";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller : User | Action : Update | Error : " + ex.Message);
                TempData["Error"] = "Error updating user";
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            User user = new();
            try
            {
                user = await _db.Users.FindAsync(id);
                if (user != null)
                {
                    user.Active = false;
                    await Task.Run(() => _db.Users.Update(user));
                    //await Task.Run(() => _db.Users.Remove(user));
                    await _db.SaveChangesAsync();

                    TempData["Success"] = "User deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller : User | Action : Delete | Error : " + ex.Message);
                TempData["Error"] = "Error delete user";
            }
            return RedirectToAction("Index", "User");
        }
    }
}
