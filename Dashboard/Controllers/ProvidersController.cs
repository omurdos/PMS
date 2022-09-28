using AutoMapper;
using Core;
using Core.Entities;
using Dashboard.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Dashboard.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ProvidersController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: ProvidersController
        public async Task<IActionResult> Index(int? Page)
        {
            ViewBag.Title = "Providers";
            var providers = await _context.Providers.ToListAsync();
            return View(providers.ToPagedList(Page ?? 1, 10));
        }

        // GET: ProvidersController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Provide";
            return View();
        }

        // GET: ProvidersController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add new Provider";
            return View();
        }

        // POST: ProvidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProviderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var provider = _mapper.Map<Provider>(model);
                    await _context.Providers.AddAsync(provider);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {provider.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Title = "Add new Provider";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the provider {provider.Name}, please contact Administrator";
                        return View(model);
                    }
                }
                else
                {
                    //Display Error Messages...
                    ViewBag.Title = "Add new Provider";
                    return View(model);
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: ProvidersController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            
            var result = await _context.Providers.FindAsync(id);
            var model = _mapper.Map<UpdateProviderViewModel>(result);
            ViewBag.Title = $"Edit {model.Name}";
            return View(model);
        }

        // POST: ProvidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateProviderViewModel model)
        {
            try
            {
                var provider = await _context.Providers.FindAsync(id);
                if (provider == null) {
                    return RedirectToAction(nameof(Index));
                }
                _mapper.Map(model, provider);
                _context.Providers.Update(provider);
                var result = await _context.SaveChangesAsync();
                if (result > 0) {
                    TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {provider.Name} has been saved successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ProvidersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProvidersController/Delete/5
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
