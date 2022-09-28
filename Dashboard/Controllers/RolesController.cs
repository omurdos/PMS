using AutoMapper;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(DBContext context, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        // GET: RolesController
        public async Task<ActionResult> Index(int? Page)
        {
            ViewBag.Title = "Roles";
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles.ToPagedList(Page ?? 1, 10));
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add new role";
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid) {
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {role.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else {
                        ViewBag.Title = "Add new role";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the role {role.Name}, please contact Administrator";
                        return View(role);
                    }
                }
                return View(role);
            }
            catch
            {
                return View(role);
            }
        }

        // GET: RolesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) {
                ViewBag.ErrorMessage = $"The role your tryin to edit is not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Title = "Edit role";
            return View(role);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, IdentityRole model)
        {
            try
            {
                if (ModelState.IsValid) {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = model.Name;
                    role.NormalizedName = model.Name.ToUpper();
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded) {
                        TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {role.Name} has been saved successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else{
                        return View(model);
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: RolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolesController/Delete/5
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
