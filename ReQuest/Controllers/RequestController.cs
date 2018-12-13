using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReQuest.Models;

namespace ReQuest.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        // Creating DbContext to access the tables and properties of the database. 
        private ApplicationDbContext _context = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public RequestController()
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        // GET: Request
        // Get the actual user and return a list of the request for the actual user.
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            return View(_context.Requests.ToList().Where(req => req.RequestUserID == currentUser.Id));
        }

        // GET: Request/Create
        public ActionResult Create()
        {
            LoadBuildingRoomViewBag();
            return View();
        }

        // POST: Request/Create
        // Creating a request and validating the information with the model. 
        [HttpPost]
        public ActionResult Create([Bind(Include = "RequestName,RequestDescription,RequestStartTime,RequestEndTime,RoomNumber")] Request request)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                var currentUser = manager.FindById(User.Identity.GetUserId());
                request.RequestUserID = currentUser.Id;
                request.RequestStatus = "Pending";
                _context.Requests.Add(request);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            LoadBuildingRoomViewBag();  
            return View(request);
        }

        // GET: Request/Edit/5 -----> Future Implementation!!
        public ActionResult Edit(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = _context.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Request/Edit/5 -----> Future Implementation!!
        [HttpPost]
        public ActionResult Edit([Bind(Include = "RequestName,RequestDescription")]Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(request).State = EntityState.Modified;
                //_context.SaveChanges(); Generates error when saving changes
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = _context.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Request/Delete/5
        // Remove a request
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection,Request request)
        {
            request = _context.Requests.Find(id);
            _context.Requests.Remove(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Upload a list of rooms.
        private void LoadBuildingRoomViewBag()
        {
            var room = (from m in _context.Rooms select new { roomNumber = m.RoomNumber, buildingID = m.BuildingID }).ToList();
            ViewBag.Room = room;
        }
    }
}
