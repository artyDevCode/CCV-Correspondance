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
using System.Threading.Tasks;

namespace CCVCorrespondance.Controllers
{
    public class CorrespondanceController : Controller
    {
        private CorrespondanceDB db = new CorrespondanceDB();

        // GET: /Correspondance/
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

            return RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
        }

        [SharePointContextFilter]
        public ActionResult Unauthorised(string alertMessage)
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

            TempData["alertMessage"] = alertMessage;
            return View("Unauthorised");
        }

        [SharePointContextFilter]
        public ActionResult Alert(string alertMessage)
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
            TempData["alertMessage"] = alertMessage;
            return View();
        }

        // GET: /Correspondance/Details/5
        [SharePointContextFilter]
        public ActionResult Details(int? id, string ctype)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }
            Correspondance correspondance = db.Correspondances.Find(id);
            if (correspondance == null)
            {
                return HttpNotFound();
            }
            return View(correspondance);
        }

        // GET: /Correspondance/Create
        [SharePointContextFilter]
        public ActionResult Create(string ctype, int cyear)
        {
            var correspondanceTypeModel = getCorrespondanceTypes();
            var correspondanceYearModel = getCorrespondanceYears();
            var correspondanceFromToModel = getCorrespondanceFromTos();
            var correspondanceDepartmentModel = getCorrespondanceDepartments();

            SelectList CTNames = new SelectList(correspondanceTypeModel);
            ViewData["CTNames"] = CTNames;

            SelectList CYNames = new SelectList(correspondanceYearModel);
            ViewData["CYNames"] = CYNames;

            SelectList CFTNames = new SelectList(correspondanceFromToModel);
            ViewData["CFTNames"] = CFTNames;

            SelectList CDNames = new SelectList(correspondanceDepartmentModel);
            ViewData["CDNames"] = CDNames;

            var correspondance = new Correspondance 
            {
                CorrespondanceType = ctype,
                CorrespondanceYear = cyear,            
                DocumentDateCreated = DateTime.Now,
                DocumentDateModified = DateTime.Now,
                DocumentCreatedBy = User.Identity.Name,
                DocumentModifiedBy = User.Identity.Name,
                DocumentDeleted = false
            };

            return View(correspondance);
        }

        // POST: /Correspondance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SharePointContextFilter]
        public async Task<ActionResult> Create([Bind(Include = "ID,CorrespondanceType,CorrespondanceYear,CorrespondanceFromTo,CorrespondanceDepartment,CorrespondanceSubject,CorrespondanceDateOnLetter,CorrespondanceDateReceivedOrSent,CorrespondanceBody,CorrespondanceAdditionalComments,DocumentDateModified,DocumentModifiedBy,DocumentDateCreated,DocumentCreatedBy,DocumentDeleted,CorrespondanceDescription")] Correspondance correspondance)
        {

           if (ModelState.IsValid)
            {
                correspondance.DocumentDateCreated = DateTime.Now;
                correspondance.DocumentModifiedBy = User.Identity.Name;
                correspondance.CorrespondanceYear = DateTime.Now.Year;
                
                db.Correspondances.Add(correspondance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }

           var correspondanceTypeModel = getCorrespondanceTypes();
           var correspondanceYearModel = getCorrespondanceYears();
           var correspondanceFromToModel = getCorrespondanceFromTos();
           var correspondanceDepartmentModel = getCorrespondanceDepartments();


           SelectList CTNames = new SelectList(correspondanceTypeModel);
           ViewData["CTNames"] = CTNames;

           SelectList CYNames = new SelectList(correspondanceYearModel);
           ViewData["CYNames"] = CYNames;

           SelectList CFTNames = new SelectList(correspondanceFromToModel);
           ViewData["CFTNames"] = CFTNames;

           SelectList CDNames = new SelectList(correspondanceDepartmentModel);
           ViewData["CDNames"] = CDNames;

           //return RedirectToAction("Edit", correspondance.ID);
           return View(correspondance);
        }

        [SharePointContextFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correspondance correspondance = db.Correspondances.Find(id);
            if (correspondance == null)
            {
                return HttpNotFound();
            }

            var correspondanceTypeModel = getCorrespondanceTypes();
            var correspondanceYearModel = getCorrespondanceYears();
            var correspondanceFromToModel = getCorrespondanceFromTos();
            var correspondanceDepartmentModel = getCorrespondanceDepartments();

            SelectList CTNames = new SelectList(correspondanceTypeModel);
            ViewData["CTNames"] = CTNames;

            SelectList CYNames = new SelectList(correspondanceYearModel);
            ViewData["CYNames"] = CYNames;

            SelectList CFTNames = new SelectList(correspondanceFromToModel);
            ViewData["CFTNames"] = CFTNames;

            SelectList CDNames = new SelectList(correspondanceDepartmentModel);
            ViewData["CDNames"] = CDNames;

            return View(correspondance);
        }

        // POST: /Correspondance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CorrespondanceType,CorrespondanceYear,CorrespondanceFromTo,CorrespondanceDepartment,CorrespondanceSubject,CorrespondanceDateOnLetter,CorrespondanceDateReceivedOrSent,CorrespondanceBody,CorrespondanceAdditionalComments,DocumentDateModified,DocumentModifiedBy,DocumentDateCreated,DocumentCreatedBy,DocumentDeleted,CorrespondanceDescription")] Correspondance correspondance)
        {
            if (ModelState.IsValid)
            {
                correspondance.DocumentDateModified = DateTime.Now;
                correspondance.DocumentModifiedBy = User.Identity.Name;

                string sdate = correspondance.CorrespondanceDateOnLetter.ToString();
                DateTime date = Convert.ToDateTime(sdate); 
                int iyear = date.Year;

                correspondance.CorrespondanceYear = iyear;
                db.Entry(correspondance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }
            var correspondanceTypeModel = getCorrespondanceTypes();
            var correspondanceYearModel = getCorrespondanceYears();
            var correspondanceFromToModel = getCorrespondanceFromTos();
            var correspondanceDepartmentModel = getCorrespondanceDepartments();


            SelectList CTNames = new SelectList(correspondanceTypeModel);
            ViewData["CTNames"] = CTNames;

            SelectList CYNames = new SelectList(correspondanceYearModel);
            ViewData["CYNames"] = CYNames;

            SelectList CFTNames = new SelectList(correspondanceFromToModel);
            ViewData["CFTNames"] = CFTNames;

            SelectList CDNames = new SelectList(correspondanceDepartmentModel);
            ViewData["CDNames"] = CDNames;

            return View(correspondance);
        }

        // GET: /Correspondance/Delete/5
        [SharePointContextFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correspondance correspondance = db.Correspondances.Find(id);
            if (correspondance == null)
            {
                return HttpNotFound();
            }
            return View(correspondance);
        }

        // POST: /Correspondance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Correspondance correspondance = db.Correspondances.Find(id);

            if (correspondance != null)
            {
                correspondance.DocumentDeleted = true;
                correspondance.DocumentDateModified = DateTime.Now;
                correspondance.DocumentModifiedBy = User.Identity.Name;
                db.Entry(correspondance).State = EntityState.Modified;
            }
            //db.Correspondances.Remove(correspondance);
            db.SaveChanges();
            return RedirectToAction("Index", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
        }

        private List<string> getCorrespondanceTypes()
        {
            List<string> model = db.CorrespondanceTypes
                .Select(r => r.CorrespondanceTypeName).ToList();
            return model;
        }

        private List<string> getCorrespondanceYears()
        {
            List<string> model = db.CorrespondanceYears
                .Select(r => r.CorrespondanceYearNumber.ToString()).ToList();
            return model;
        }

        private List<string> getCorrespondanceFromTos()
        {
            List<string> model = db.Correspondances
                .GroupBy(a => a.CorrespondanceFromTo)
                .Select(r => r.FirstOrDefault().CorrespondanceFromTo).ToList();
            return model;
        }

        public JsonResult getCorrespondanceName(string term)
        {

            var model = db.Correspondances
               .Select(r => new
               {
                   r.CorrespondanceFromTo
               }).Distinct().ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCorrespondanceDepartment(string term)
        {

            var model = db.Correspondances
               .Select(r => new
               {
                   r.CorrespondanceDepartment
               }).Distinct().ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private List<string> getCorrespondanceDepartments()
        {

            List<string> model = db.Correspondances
                .GroupBy(a => a.CorrespondanceDepartment)
                .Select(r => r.FirstOrDefault().CorrespondanceDepartment).ToList();

            return model;
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
