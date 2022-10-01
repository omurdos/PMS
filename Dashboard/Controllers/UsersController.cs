using AutoMapper;
using Core.Entities;
using Dashboard.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        // GET: ProvidersController
        public async Task<IActionResult> Index(int? Page)
        {
            ViewBag.Title = "Users";
            var users = await _userManager.Users.ToListAsync();
            return View(users.ToPagedList(Page ?? 1, 10));
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProvidersController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add new user";
            return View();
        }

        // POST: ProvidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _mapper.Map<User>(model);
                    user.UserName = model.PhoneNumber;

                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {user.FullName} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Title = "Add new user";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the user {user.FullName}, please contact Administrator";
                        return View(model);
                    }
                }
                else
                {
                    //Display Error Messages...
                    ViewBag.Title = "Add new user";
                    return View(model);
                }

            }
            catch(Exception)
            {
                throw;
                return View(model);
            }
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, User not found";

                return RedirectToAction(nameof(Index));
            }
            var model = _mapper.Map<UpdateUserViewModel>(user);
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateUserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, User not found";

                    return RedirectToAction(nameof(Index));
                }
                else {
                    _mapper.Map(model, user);
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {user.FullName} has been saved successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else {
                        return View(model);
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
