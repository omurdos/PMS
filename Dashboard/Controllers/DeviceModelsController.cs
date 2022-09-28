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
    public class DeviceModelsController : Controller
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public DeviceModelsController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ModelsController
        public async Task<ActionResult> Index(int? Page)
        {
            ViewBag.Title = "Device Models";
            var providers = await _context.DeviceModels.ToListAsync();
            return View(providers.ToPagedList(Page ?? 1, 10));
        }

        // GET: ModelsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: ModelsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Title = "Add new Device Model";
            var manufacturers = await _context.Manufacturers.ToListAsync();
            ViewBag.Manufacturers = _mapper.Map<List<ManufacturerViewModel>>(manufacturers);
            return View();
        }

        // POST: ModelsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDeviceModelViewModel model)
        {
            try
            {
                if (ModelState.IsValid) {
                    var deviceModel = _mapper.Map<DeviceModel>(model);
                    
                    await _context.AddAsync(deviceModel);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0) {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {deviceModel.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        
                        ViewBag.Title = "Add new Device Model";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the manufacturer {deviceModel.Name}, please contact Administrator";
                        var manufacturers = await _context.Manufacturers.ToListAsync();
                        ViewBag.Manufacturers = _mapper.Map<List<ManufacturerViewModel>>(manufacturers);
                        return View(model);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModelsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ModelsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModelsController/Delete/5
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
