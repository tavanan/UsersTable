using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;
using Users.Repository;

namespace Users.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: UserController
        public ActionResult Index()
        {
            var users = _userRepository.GetAllUsers().ToList();
            return View(users);
        }


        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (_userRepository.UserExists(user))
            {
                ModelState.AddModelError("CustomError","User already exists!");
            }
            if ((DateTime.Now.Year - user.DateOfBirth.Year) < 1)
            {
                ModelState.AddModelError("dateofbirth", "Only User older than one year can register! ");
            }
            if(ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);

        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            if(!_userRepository.IdExists(id))
            {
                return NotFound();
            }

            var user = _userRepository.GetSingleUser(id);
            
            if(user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_userRepository.IdExists(id))
            {
                return NotFound();
            }

            var user = _userRepository.GetSingleUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePOST(int id)
        {
            if(!_userRepository.IdExists(id))
            {
                return NotFound();
            }

            var user = _userRepository.GetSingleUser(id);

            if(user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
