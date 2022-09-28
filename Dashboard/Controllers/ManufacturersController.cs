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
    public class ManufacturersController : Controller
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ManufacturersController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: ManufacturersController
        public async Task<ActionResult> Index(int? Page)
        {
            ViewBag.Title = "Manufactureres";
            var Manufactureres = await _context.Manufacturers
                .Include(m => m.Provider)
                .ToListAsync();
            return View(Manufactureres.ToPagedList(Page ?? 1, 10));
        }

        // GET: ManufacturersController/Details/5
        //public ActionResult Details(string id)
        //{
        //    return View();
        //}

        // GET: ManufacturersController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View();
        }

        // POST: ManufacturersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateManufacturerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manufacturer = _mapper.Map<Manufacturer>(model);
                    await _context.Manufacturers.AddAsync(manufacturer);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {manufacturer.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Providers = await _context.Providers.ToListAsync();
                        ViewBag.Title = "Add new Manufacturer";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the manufacturer {manufacturer.Name}, please contact Administrator";
                        return View(model);
                    }
                }
                ViewBag.Providers = await _context.Providers.ToListAsync();
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.Providers = await _context.Providers.ToListAsync();
                ViewBag.ErrorMessage = $"<strong>OOOP!</strong>, We had some trouble adding the manufacturer, please contact Administrator";
                return View(model);
            }
        }

        // GET: ManufacturersController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var model = _mapper.Map<UpdateManufacturerViewModel>(manufacturer);
            ViewBag.Providers = await _context.Providers.ToListAsync();
            ViewBag.Title = $"Edit {manufacturer.Name}";
            return View(model);
        }

        // POST: ManufacturersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateManufacturerViewModel model)
        {
            try
            {
                var manufacturer = await _context.Manufacturers.FindAsync(id);
                if (manufacturer == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                _mapper.Map(model, manufacturer);
                _context.Update(manufacturer);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {manufacturer.Name} has been saved successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ManufacturersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManufacturersController/Delete/5
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
