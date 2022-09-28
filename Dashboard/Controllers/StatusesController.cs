using AutoMapper;
using Core;
using Core.Entities;
using Dashboard.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Dashboard.Controllers
{
    public class StatusesController : Controller
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public StatusesController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: StatusesController
        public async Task<ActionResult> Index(int? Page)
        {
            ViewBag.Title = "Statuses";
            var Manufactureres = await _context.Statuses.ToListAsync();
            return View(Manufactureres.ToPagedList(Page ?? 1, 10));
        }

        // GET: StatusesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: StatusesController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add new status";
            return View();
        }

        // POST: StatusesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStatusViewModel model)
        {
            try
            {
                if (ModelState.IsValid) {
                    var status = _mapper.Map<Status>(model);
                    await _context.Statuses.AddAsync(status);
                    var result = await _context.SaveChangesAsync();
                    if (result >0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {status.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else {
                        ViewBag.Title = "Add new status";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the status {status.Name}, please contact Administrator";
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

        // GET: StatusesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var status = await _context.Statuses.FindAsync(id);
                var model = _mapper.Map<UpdateStatusViewModel>(status);
                return View(model);
            }
            catch (Exception) {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: StatusesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateStatusViewModel model)
        {
            try
            {
                var status = await _context.Statuses.FindAsync(id);
                if (status == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                _mapper.Map(model, status);
                _context.Statuses.Update(status);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {model.Name} has been saved successfully";
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

        // GET: StatusesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StatusesController/Delete/5
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
