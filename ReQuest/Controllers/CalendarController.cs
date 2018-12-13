using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReQuest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReQuest.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        // Creating a object for the database to access the tables and properties. 
        private ApplicationDbContext _context = new ApplicationDbContext();

        // Creating a manager object to use th identity function.
        private UserManager<ApplicationUser> manager;
        public CalendarController()
        {
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
        }

        // GET: Calendar
        public ActionResult Index()
        {
            return View(_context.Requests.ToList());
        }

        // GET: Calendar
        public ActionResult PendingRequests()
        {
            return View(_context.Requests.ToList());
        }

        // GET: Calendar/Edit/5 -----> Still non function (Future Works)!
        public ActionResult Edit(int? id, Request request)
        {
            //LoadUserRolesViewBag();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            request = _context.Requests.Find(id);

            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Calendar/Edit/5 -----> Still non function (Future Works)!
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult StatusAccept()
        {
            return View();
        }

        // POST: Calendar/StatusAccept/5
        // Admin accept the activities and the pass this values to Calendar. 
        [HttpPost]
        public ActionResult StatusAccept(int? id, Request request)
        {
            Calendar calendar = new Calendar();

            request = _context.Requests.Find(id);
            request.RequestStatus = "Accepted";
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            calendar.ActivityId = request.RequestId;
            calendar.ActivityName = request.RequestName;
            calendar.ActivityDescription = request.RequestDescription;
            calendar.RoomNumber = request.RoomNumber;
            calendar.ActivityUserID = request.RequestUserID;
            calendar.ActivityStartTime = request.RequestStartTime;
            calendar.ActivityEndTime = request.RequestEndTime;
            calendar.ActivityStatus = request.RequestStatus;

            _context.Calendars.Add(calendar);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult StatusDecline()
        {
            return View();
        }

        // POST: Calendar/StatusDecline/5
        // Sames procedure of the accepted function.
        [HttpPost]
        public ActionResult StatusDecline(int? id, Request request)
        {
            request = _context.Requests.Find(id);
            request.RequestStatus = "Declined";
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Working with JavaScript. Use the events variable to make a list of the calendar data.
        public JsonResult GetEvents()
        {
            try
            {
                var events = (from m in _context.Calendars select new { ActivityName = m.ActivityName, ActivityDescription = m.ActivityDescription, ActivityStartTime = m.ActivityStartTime, ActivityEndTime = m.ActivityEndTime, RoomNumber = m.RoomNumber }).ToList();


                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            finally
            {
                //throw new NotImplementedException();
            }

        }

    }
}
