using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCVCorrespondance.Models;
using CCVCorrespondance.DAL;
using System.Configuration;

namespace CCVCorrespondance.Controllers
{
    public class CorrespondanceReportController : Controller
    {
        private CorrespondanceDB db = new CorrespondanceDB();

        // GET: /CorrespondanceReport/
        [HttpGet]
        [SharePointContextFilter]
        public ActionResult Index()
        {
            // Check access levels and pass to view
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);
            List<Access> AccessGroupsModel = db.tblAccess
                             .Where(r => r.UserId == user)
                             .ToList();

            ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
            ViewData["InUsersRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("user")).Count() > 0 ? "true" : "false";

            if ((ViewData["InUsersRole"] != "true") && ViewData["InOwnerRole"] != "true")
            {
                return RedirectToAction("Unauthorised", "Correspondance", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }

            var correspondanceTypeModel = getCorrespondanceTypes();
            SelectList CTRNames = new SelectList(correspondanceTypeModel);
            ViewData["CTRNames"] = CTRNames;

            return View(db.CorrespondanceReport.ToList());
        }


        [HttpPost]
        [SharePointContextFilter]
        public ActionResult Index(DateTime SSDate1, DateTime SEDate1, string CorrespondanceReportTypeName)
        {

            return RedirectToAction("Report", "Report", new { SSDate = SSDate1, SEDate = SEDate1, SType = CorrespondanceReportTypeName, SFlag = "Generate", SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
           // return View();
        }

        [SharePointContextFilter]
        public ActionResult ShowCorrespondanceReport(DateTime startDate, DateTime endDate)
        {

            return View(db.CorrespondanceReport.ToList());
        }

        // GET: /CorrespondanceReport/Details/5
        [SharePointContextFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorrespondanceReport correspondancereport = db.CorrespondanceReport.Find(id);
            if (correspondancereport == null)
            {
                return HttpNotFound();
            }
            var correspondanceTypeModel = getCorrespondanceTypes();
            SelectList CTRNames = new SelectList(correspondanceTypeModel);
            ViewData["CTRNames"] = CTRNames;
            return View(correspondancereport);
        }


        private List<string> getCorrespondanceTypes()
        {
            List<string> model = db.CorrespondanceTypes
                .Select(r => r.CorrespondanceTypeName).ToList();
            model.Add("All");
            return model;
        }


        // GET: /CorrespondanceReport/Create
        [SharePointContextFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CorrespondanceReport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,CorrespondanceReportUser,CorrespondanceReportStartDate,CorrespondanceReportEndDate,CorrespondanceReportTypeName,CorrespondanceReceivedOrSentDate,CorrespondanceReportFromTo,CorrespondanceReportDepartment,CorrespondanceReportSubject")] CorrespondanceReport correspondancereport)
        {
            if (ModelState.IsValid)
            {
                db.CorrespondanceReport.Add(correspondancereport);
                db.SaveChanges();
                return RedirectToAction("Index", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }

            return View(correspondancereport);
        }

        // GET: /CorrespondanceReport/Edit/5
        [SharePointContextFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorrespondanceReport correspondancereport = db.CorrespondanceReport.Find(id);
            if (correspondancereport == null)
            {
                return HttpNotFound();
            }
            return View(correspondancereport);
        }

        // POST: /CorrespondanceReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,CorrespondanceReportUser,CorrespondanceReportStartDate,CorrespondanceReportEndDate,CorrespondanceReportTypeName,CorrespondanceReceivedOrSentDate,CorrespondanceReportFromTo,CorrespondanceReportDepartment,CorrespondanceReportSubject")] CorrespondanceReport correspondancereport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correspondancereport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }
            return View(correspondancereport);
        }

        // GET: /CorrespondanceReport/Delete/5
        [SharePointContextFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorrespondanceReport correspondancereport = db.CorrespondanceReport.Find(id);
            if (correspondancereport == null)
            {
                return HttpNotFound();
            }
            return View(correspondancereport);
        }

        // POST: /CorrespondanceReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CorrespondanceReport correspondancereport = db.CorrespondanceReport.Find(id);
            db.CorrespondanceReport.Remove(correspondancereport);
            db.SaveChanges();
            return RedirectToAction("Index", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
