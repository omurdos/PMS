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
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<User> userManager, IMapper mapper, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager=roleManager;
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
        public async Task<ActionResult> Create()
        {
            ViewBag.Title = "Add new user";
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles;
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

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {user.FullName} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Title = "Add new user";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the user {user.FullName}, please contact Administrator";
                        var roles = await _roleManager.Roles.ToListAsync();
                        ViewBag.Roles = roles;
                        return View(model);
                    }
                }
                else
                {
                    //Display Error Messages...
                    ViewBag.Title = "Add new user";
                    var roles = await _roleManager.Roles.ToListAsync();
                    ViewBag.Roles = roles;
                    return View(model);
                }

            }
            catch(Exception)
            {
                throw;
                
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
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles;
            var model = _mapper.Map<UpdateUserViewModel>(user);
            ViewBag.Title = "Update " + user.FullName;
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
                        var IsInRole = await _userManager.IsInRoleAsync(user, model.Role);
                        if (IsInRole) {
                        }
                        else { 
                            var userRole = await _userManager.GetRolesAsync(user);
                            if (userRole.Count > 0) {
                                await _userManager.RemoveFromRoleAsync(user, userRole[0]);
                            }
                            
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }
                        TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {user.FullName} has been saved successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else {
                        var roles = await _roleManager.Roles.ToListAsync();
                        ViewBag.Roles = roles;
                        return View(model);
                    }
                }
            }
            catch(Exception e)
            {
                throw;
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
