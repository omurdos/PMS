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
    public class DevicesController : Controller
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public DevicesController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: DevicesController
        public async Task<ActionResult> Index(int? Page)
        {
            ViewBag.Title = "Devices";
            var devices = await _context.Devices
                .Include(d => d.Emirate)
                .Include(d => d.Model)
                .Include(d => d.Shop)
                .Include(d => d.User)
                .Include(d => d.Status)
                .ToListAsync();
            return View(devices.ToPagedList(Page ?? 1, 10));
        }

        // GET: DevicesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: DevicesController/Create
        public async Task<ActionResult> Create()
        {
            await PrepareLists();
            ViewBag.Title = "Add new Device";
            return View();
        }

        // POST: DevicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDeviceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var device = _mapper.Map<Device>(model);
                    await _context.Devices.AddAsync(device);
                    var result = await _context.SaveChangesAsync();
                    if (result >0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {device.IMEI1} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Title = "Add new device";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the device {device.IMEI1}, please contact Administrator";
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

        // GET: DevicesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                await PrepareLists();
                var device = await _context.Devices.FindAsync(id);
                var model = _mapper.Map<UpdateDeviceViewModel>(device);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We can't find the Device you're looking for";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: DevicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateDeviceViewModel model)
        {
            try
            {
                var device = await _context.Devices.FindAsync(id);
                if (device == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                _mapper.Map(model, device);
                _context.Devices.Update(device);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {model.IMEI1} has been saved successfully";
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

        // GET: DevicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DevicesController/Delete/5
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

        private async Task PrepareLists() {
            ViewBag.Emirates = await _context.Emirates.ToListAsync();
            ViewBag.DeviceModels = await _context.DeviceModels.ToListAsync();
            ViewBag.Shops = await _context.Shops.ToListAsync();
            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.Statuses = await _context.Statuses.ToListAsync();
        }

    }
}
