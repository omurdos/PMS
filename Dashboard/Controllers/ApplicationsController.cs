using Core;
using Core.Entities;
using Dashboard.ViewModels;
using JassimAPIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Dashboard.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IFileUploader _fileUploader;
        private readonly DBContext _context;

        public ApplicationsController(DBContext context, IFileUploader fileUploader)
        {
            _context=context;
            _fileUploader=fileUploader;
        }


        // GET: ApplicationsController
        public async Task<ActionResult> Index(int? Page)
        {

            try
            {
                var applications = await _context.Applicaions.Include(ap => ap.ApplicationCategory).OrderByDescending(a => a.Version).ToListAsync();
                return View(applications.ToPagedList(Page ?? 1, 10));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: ApplicationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationsController/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _context.ApplicaionCategories.ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }

        // POST: ApplicationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm]CreateApplicationViewModel model)
        {
            try
            {
                var apps = await _context.Applicaions
                    .Where(c => c.Version == model.Version && c.ApplicationCategoryId == model.ApplicationCategoryId && c.IsDeleted == false)
                    .ToListAsync();
                if (apps.Any())
                {

                    var categories = await _context.ApplicaionCategories.ToListAsync();
                    ViewBag.Categories = categories;
                    ViewBag.Title = "Add new Application Version";
                    ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding version {model.Version} of {model.Name}, please contact Administrator";
                    return View(model);

                }
                var category = await _context.ApplicaionCategories.FindAsync(model.ApplicationCategoryId);
                var uploadResult = await _fileUploader.Upload(model.File, category.Name, model.Version);

                if (uploadResult.Succeed)
                {
                    var application = new Application
                    {
                        FileName = uploadResult.FileName,
                        Version = model.Version,
                        ApplicationCategoryId = model.ApplicationCategoryId,
                    };
                    await _context.Applicaions.AddAsync(application);
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = $"<strong>Congrats!</strong>, version {model.Version} for {category.Name} has been added successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var categories = await _context.ApplicaionCategories.ToListAsync();
                        ViewBag.Categories = categories;
                        ViewBag.Title = "Add new Version";
                        ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the new version, please contact Administrator";
                        return View(model);
                    }
                }
                else
                {
                    var categories = await _context.ApplicaionCategories.ToListAsync();
                    ViewBag.Categories = categories;
                    ViewBag.Title = "Add new Version";
                    ViewBag.ErrorMessage = $"<strong>Sorry!</strong>, We had some trouble adding the new version, please contact Administrator";
                    return View(model);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: ApplicationsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationsController/Edit/5
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

        // GET: ApplicationsController/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: ApplicationsController/Delete/5
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
