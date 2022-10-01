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
    public class ShopsController : Controller
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ShopsController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ProvidersController
        public async Task<IActionResult> Index(int? Page)
        {
            ViewBag.Title = "Shops";
            var shops = await _context.Shops.OrderByDescending(s => s.CreatedAt).ToListAsync();
            return View(shops.ToPagedList(Page ?? 1, 10));
        }

        //// GET: ShopsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ShopsController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add new shop";
            return View();
        }

        // POST: ShopsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateShopViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var shop = _mapper.Map<Shop>(model);
                    await _context.Shops.AddAsync(shop);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, {shop.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Title = "Add new shop";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the shop {shop.Name}, please contact Administrator";
                        return View(model);
                    }
                }
                else
                {
                    //Display Error Messages...
                    ViewBag.Title = "Add new shop";
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopsController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var shop = await _context.Shops.FindAsync(id);
                if (shop == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    var model = _mapper.Map<UpdateShopViewModel>(shop);
                    return View(model);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: ShopsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateShopViewModel model)
        {
            try
            {
                var shop = await _context.Shops.FindAsync(id);
                if (shop == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                _mapper.Map(model, shop);
                _context.Shops.Update(shop);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["SuccessMessage"] = $"<strong>Success!</strong>, Changes on {shop.Name} has been saved successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: ShopsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopsController/Delete/5
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
