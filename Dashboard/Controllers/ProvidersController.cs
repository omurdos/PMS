using AutoMapper;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Providers";
            var providers = await  _context.Providers.ToListAsync();
            return View(providers);
        }

        // GET: ProvidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProvidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProvidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ProvidersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProvidersController/Edit/5
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
