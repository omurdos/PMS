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
    public class EmiratesController : Controller
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public EmiratesController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: EmiratesController
        public async Task<ActionResult> Index(int? Page)
        {
            ViewBag.Title = "Emirates";
            var Emirates = await _context.Emirates.ToListAsync();
            return View(Emirates.ToPagedList(Page ?? 1, 10));
        }

        // GET: EmiratesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: EmiratesController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add new Emirate";
            return View();
        }

        // POST: EmiratesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmirateViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var emirate = _mapper.Map<Emirate>(model);
                    await _context.Emirates.AddAsync(emirate);
                    var result = await _context.SaveChangesAsync();
                    if (result >0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {emirate.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Title = "Add new Emirate";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the Emirate {emirate.Name}, please contact Administrator";
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

        // GET: EmiratesController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var emirate = await _context.Emirates.FindAsync(id);
                var model = _mapper.Map<UpdateEmirateViewModel>(emirate);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We can't find the Emirate you're looking for";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: EmiratesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateEmirateViewModel model)
        {
            try
            {
                var emirate = await _context.Emirates.FindAsync(id);
                if (emirate == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                _mapper.Map(model, emirate);
                _context.Emirates.Update(emirate);
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

        // GET: EmiratesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmiratesController/Delete/5
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
