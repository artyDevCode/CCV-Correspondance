using CCVCorrespondance.DAL;
using CCVCorrespondance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CCVCorrespondance.Controllers
{
    public class HomeDepartmentController : Controller
    {

        CorrespondanceDB _db;

        public HomeDepartmentController()
        {
            _db = new CorrespondanceDB();

        }

        public HomeDepartmentController(CorrespondanceDB db)
        {
            _db = db;
        }

        public class JQueryDataTableParamModel
        {
            public string sEcho { get; set; }
            public string sSearch { get; set; }
            public int iDisplayLength { get; set; }
            public int iDisplayStart { get; set; }
            public string first_data { get; set; }
            public string second_data { get; set; }
        }

        public JsonResult GetAjaxData(JQueryDataTableParamModel param)
        {

            var model = _db.Correspondances
               .Where(r => r.DocumentDeleted == false)
               .Select(r => new CorrespondanceDepartmentVM { VM_Type = r.CorrespondanceType, VM_Year = r.CorrespondanceYear, VM_FromTo = r.CorrespondanceFromTo, VM_Department = r.CorrespondanceDepartment, VM_Subject = r.CorrespondanceSubject, VM_DateOnLetter = r.CorrespondanceDateOnLetter, VM_DateReceivedOrSent = r.CorrespondanceDateReceivedOrSent, VM_Id = r.ID })
                .ToList();

            List<CorrespondanceDepartmentVM> modelsorted;

            int totalRowsCount = 0;
            int filteredRowsCount = 0;
            string[][] aaData;

            if (param.first_data != null && param.second_data != null)
            {
                List<CorrespondanceDepartmentVM> aa = new List<CorrespondanceDepartmentVM>();
                foreach (var group in model)
                {
                    var test = group.VM_Department.Replace(" ", "").Trim('-').ToLower();
                    if (test.Contains(param.first_data) &&
                        group.VM_Type.ToLower().Contains(param.second_data.Trim()))
                        aa.Add(group);
                }

                modelsorted = aa.OrderBy(r => r.VM_Department).ThenBy(e => e.VM_Type).ToList();
                aa = null;
                aaData = modelsorted.Select(d => new string[] { d.VM_Department, d.VM_Type, d.VM_Year.ToString(), d.VM_FromTo, d.VM_Subject, (d.VM_DateOnLetter.HasValue ? d.VM_DateOnLetter.Value.ToString("dd-MM-yyyy") : d.VM_DateOnLetter.ToString()), d.VM_DateReceivedOrSent.ToString("dd-MM-yyyy"), d.VM_Id.ToString() }).ToArray();

                totalRowsCount = modelsorted.Count();
                filteredRowsCount = modelsorted.Count();

                return Json(new
                {
                    sEcho = param.sEcho,
                    aaData = aaData,
                    iTotalRecords = Convert.ToInt32(totalRowsCount),
                    iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
                }, JsonRequestBehavior.AllowGet);


            }

            if (param.sSearch != null)
            {
                if (param.sSearch.Length > 2)
                {

                    List<CorrespondanceDepartmentVM> aa = new List<CorrespondanceDepartmentVM>();
                    foreach (var group in model)
                    {
                        if (group.VM_Type.ToLower().Contains(param.sSearch.ToLower()) || group.VM_Year.ToString().Contains(param.sSearch.ToLower()) || group.VM_Department.ToLower().Contains(param.sSearch.ToLower()) ||
                            group.VM_Subject.ToLower().Contains(param.sSearch.ToLower()))

                            aa.Add(group);

                    }

                    modelsorted = aa.OrderBy(r => r.VM_Department).ThenBy(e => e.VM_Type).ToList();
                    aa = null;
                    aaData = modelsorted.Select(d => new string[] { d.VM_Department, d.VM_Type, d.VM_Year.ToString(), d.VM_FromTo, d.VM_Subject, (d.VM_DateOnLetter.HasValue ? d.VM_DateOnLetter.Value.ToString("dd-MM-yyyy") : d.VM_DateOnLetter.ToString()), d.VM_DateReceivedOrSent.ToString("dd-MM-yyyy"), d.VM_Id.ToString() }).ToArray();

                    totalRowsCount = modelsorted.Count();
                    filteredRowsCount = modelsorted.Count();

                    return Json(new
                    {
                        sEcho = param.sEcho,
                        aaData = aaData,
                        iTotalRecords = Convert.ToInt32(totalRowsCount),
                        iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            totalRowsCount = model.Count();
            filteredRowsCount = model.Count();
            var vv = model.Select(r => new { r.VM_Department, r.VM_Type }).Distinct().OrderBy(r => r.VM_Department).ThenBy(e => e.VM_Type).ToList();

            aaData = vv.Select(d => new string[] { d.VM_Department, d.VM_Type, "", "", "", "", "", "" }).ToArray();


            return Json(new
            {
                sEcho = param.sEcho,
                aaData = aaData,
                iTotalRecords = Convert.ToInt32(totalRowsCount),
                iTotalDisplayRecords = Convert.ToInt32(filteredRowsCount)
            }, JsonRequestBehavior.AllowGet);

        }
        [SharePointContextFilter]
        public ActionResult Index()
        {

            // Check access levels and pass to view
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);
            List<Access> AccessGroupsModel = _db.tblAccess
                             .Where(r => r.UserId == user)
                             .ToList();

            ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
            ViewData["InUsersRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("user")).Count() > 0 ? "true" : "false";

            if ((ViewData["InUsersRole"] != "true") && ViewData["InOwnerRole"] != "true")
            {
                return RedirectToAction("Unauthorised", "Correspondance", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }

            var model = _db.Correspondances
                 .Where(a => a.DocumentDeleted == false)
                .ToList();

            return View(model);
        }

        [SharePointContextFilter]
        public ActionResult About()
        {
            // Check access levels and pass to view
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);
            List<Access> AccessGroupsModel = _db.tblAccess
                             .Where(r => r.UserId == user)
                             .ToList();

            ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
            ViewData["InUsersRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("user")).Count() > 0 ? "true" : "false";

            if ((ViewData["InUsersRole"] != "true") && ViewData["InOwnerRole"] != "true")
            {
                return RedirectToAction("Unauthorised", "Correspondance", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }
            ViewBag.Message = "CCV Correspondance";

            return View();
        }

        [SharePointContextFilter]
        public ActionResult Contact()
        {
            // Check access levels and pass to view
            int index = User.Identity.Name.IndexOf("\\");
            string user = User.Identity.Name.Substring(index + 1);
            List<Access> AccessGroupsModel = _db.tblAccess
                             .Where(r => r.UserId == user)
                             .ToList();

            ViewData["InOwnerRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("owner")).Count() > 0 ? "true" : "false";
            ViewData["InUsersRole"] = AccessGroupsModel.Where(r => r.AccessGroup.ToLower().Contains("user")).Count() > 0 ? "true" : "false";

            if ((ViewData["InUsersRole"] != "true") && ViewData["InOwnerRole"] != "true")
            {
                return RedirectToAction("Unauthorised", "Correspondance", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Report()
        //{
        //    ViewBag.Message = "Report";

        //    return View();
        //}


        [HttpGet]
        [SharePointContextFilter]
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //, new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
        public async Task<ActionResult> Create([Bind(Include = "DocumentId,SubCategoryId,DocumentName,BusinessUnit,Body,DateCreated,CreatedBy")] Correspondance correspondance)
        {
            if (ModelState.IsValid)
            {
                _db.Correspondances.Add(correspondance);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { SPHostUrl = SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }

            return View(correspondance);
        }


        [SharePointContextFilter]
        public ActionResult Details() //list all the categories for edit or delete
        {

            var document = _db.Correspondances
               .Where(a => a.DocumentDeleted == false)
               .ToList();
            //.GroupBy(a => a.CategoryName)
            //.Select(r => new CategoryList
            //{
            //    CategoryName = r.First().CategoryName,
            //    GroupId = r.First().GroupId,
            //});


            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}