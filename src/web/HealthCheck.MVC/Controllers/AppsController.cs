using HealthCheck.Domain;
using HealthCheck.Model;
using HealthCheck.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheck.MVC.Controllers
{
    [Authorize]
    public class AppsController : Controller
    {
        private readonly IAppService _appService;


        public AppsController(IAppService appService)
        {

            _appService = appService;
        }

        // GET: Apps
        public async Task<IActionResult> Index()
        {
            return View(await _appService.Apps());

        }

        // GET: Apps/Details/5
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            return View(_appService.App(id.Value));

        }

        // GET: Apps/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Interval,UserId,URL,Id")] AppDTO app)
        {
            if (ModelState.IsValid)
            {
                await _appService.SaveApp(app);
                return RedirectToAction(nameof(Index));
            }

            return View(app);
        }

        // GET: Apps/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = _appService.App(id.Value);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Interval,UserId,URL,Id")] AppDTO app)
        {
            if (id != app.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _appService.SaveApp(app);
                return RedirectToAction(nameof(Index));
            }

            return View(app);
        }

        // GET: Apps/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = _appService.App(id.Value);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appService.RemoveApp(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
