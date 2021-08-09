using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ReportViewerForMvc.Report.Controllers;
//using CCVCorrespondance.Reports;
using CCVCorrespondance.DAL;
using CCVCorrespondance.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;
//using CCVCorrespondance.

namespace ReportViewerForMvc.Report.Controllers
{
    public class ReportController : Controller
    {

        //private CorrespondanceDataSet cor = new CorrespondanceDataSet();
        //private CDataSet cor = new CDataSet();

        CorrespondanceDB _db = new CorrespondanceDB();
        //string _sString = "";

        [CCVCorrespondance.SharePointContextFilter]
        public ActionResult Index()
        {
            return View();
            //return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        // POST: /Document/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CorrespondanceReportUser,CorrespondanceReportTypeName,CorrespondanceReceivedOrSentDate,CorrespondanceReportFromTo,CorrespondanceReportDepartment,CorrespondanceReportSubject")] CorrespondanceReport correspondancereport)
        {
            if (ModelState.IsValid)
            {
                _db.CorrespondanceReport.Add(correspondancereport);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { SPHostUrl = CCVCorrespondance.SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
                
            }

            return View(correspondancereport);
        }

      
        /// <summary>
        /// Creates a ReportViewer control and stores it on the ViewBag
        /// </summary>
        /// <returns></returns>
        //public ActionResult Report(DateTime startDate, DateTime endDate)
        [HttpGet]
        [CCVCorrespondance.SharePointContextFilter]
        public ActionResult Report(DateTime SSDate, DateTime SEDate, string SType, string SFlag = "Generate")
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
                return RedirectToAction("Unauthorised", "Correspondance", new { SPHostUrl = CCVCorrespondance.SharePointContext.GetSPHostUrl(HttpContext.Request).AbsoluteUri });
            }

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            var CRdata = _db.CorrespondanceReport
              .ToList()
              .Select(r => new
              {
                  r.ID,
                  r.CorrespondanceReportTypeName,
                  r.CorrespondanceReportFromTo ,
                  r.CorrespondanceReportReceivedOrSentDate ,
                  r.CorrespondanceReportDepartment ,
                  r.CorrespondanceReportSubject,
                  r.CorrespondanceReportStartDate,
                  r.CorrespondanceReportEndDate,
                  r.CorrespondanceReportUser 
              });


                if (SType == "All")
                {
                    var data = _db.Correspondances
                    .ToList()
                    .Where(r => (r.CorrespondanceDateReceivedOrSent >= SSDate && r.CorrespondanceDateReceivedOrSent <= SEDate))
                    .Select(r => new CorrespondanceReport
                    {
                        ID = r.ID,
                        CorrespondanceReportStartDate = r.CorrespondanceDateOnLetter.ToString(),
                        CorrespondanceReportEndDate = r.CorrespondanceDateOnLetter.ToString(),
                        CorrespondanceReportUser = r.DocumentCreatedBy,
                        CorrespondanceReportTypeName = r.CorrespondanceType,
                        CorrespondanceReportReceivedOrSentDate = r.CorrespondanceDateReceivedOrSent,
                        CorrespondanceReportFromTo = r.CorrespondanceFromTo,
                        CorrespondanceReportDepartment = r.CorrespondanceDepartment,
                        CorrespondanceReportSubject = r.CorrespondanceSubject
                    });
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CorrespondanceReport.rdlc";
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data));
                    reportViewer.LocalReport.SetParameters(GetParametersLocal(SSDate, SEDate, SType));
                }
                else if (SType == "Sortable")
                {
                    var data = _db.Correspondances
                    .ToList()
                    .OrderBy(r => r.CorrespondanceDateReceivedOrSent)
                    .Select(r => new CorrespondanceReport
                    {
                        ID = r.ID,
                        CorrespondanceReportStartDate = r.CorrespondanceDateOnLetter.ToString(),
                        CorrespondanceReportEndDate = r.CorrespondanceDateOnLetter.ToString(),
                        CorrespondanceReportUser = r.DocumentCreatedBy,
                        CorrespondanceReportTypeName = r.CorrespondanceType,
                        CorrespondanceReportReceivedOrSentDate = r.CorrespondanceDateReceivedOrSent,
                        CorrespondanceReportFromTo = r.CorrespondanceFromTo,
                        CorrespondanceReportDepartment = r.CorrespondanceDepartment,
                        CorrespondanceReportSubject = r.CorrespondanceSubject
                    });
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CorrespondanceReport.rdlc";
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data));
                    reportViewer.LocalReport.SetParameters(GetParametersLocal(SSDate, SEDate, SType));
                }
                else
                {
                    var data = _db.Correspondances
                    .ToList()
                    .Where(r => ((r.CorrespondanceDateReceivedOrSent >= SSDate && r.CorrespondanceDateReceivedOrSent <= SEDate) && r.CorrespondanceType == SType))
                    .Select(r => new CorrespondanceReport
                    {
                        //SearchSDate = SSDate,
                        //SearchEDate = SEDate,
                        //SearchType = SType,
                        ID = r.ID,
                        CorrespondanceReportStartDate = r.CorrespondanceDateOnLetter.ToString(),
                        CorrespondanceReportEndDate = r.CorrespondanceDateOnLetter.ToString(),
                        CorrespondanceReportUser = r.DocumentCreatedBy,
                        CorrespondanceReportTypeName = r.CorrespondanceType,
                        CorrespondanceReportReceivedOrSentDate = r.CorrespondanceDateReceivedOrSent,
                        CorrespondanceReportFromTo = r.CorrespondanceFromTo,
                        CorrespondanceReportDepartment = r.CorrespondanceDepartment,
                        CorrespondanceReportSubject = r.CorrespondanceSubject
                    });
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CorrespondanceReport.rdlc";
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data));
                    reportViewer.LocalReport.SetParameters(GetParametersLocal(SSDate, SEDate, SType));

                }

    
                
            ////Update Correspondance Report with report generation values
            //if (SFlag == "Generate")
            //{
            //    if (ModelState.IsValid)
            //    {
            //        CorrespondanceReport NewCR = new CorrespondanceReport();
            //        NewCR.CorrespondanceReportUser = User.Identity.Name;
            //        NewCR.CorrespondanceReportDepartment = "";
            //        NewCR.CorrespondanceReportSubject = "";
            //        NewCR.CorrespondanceReportReceivedOrSentDate = SSDate;
            //        NewCR.CorrespondanceReportStartDate = SSDate.ToString();
            //        NewCR.CorrespondanceReportEndDate = SEDate.ToString();
            //        NewCR.CorrespondanceReportFromTo = "";
            //        NewCR.CorrespondanceReportTypeName = SType;

            //        _db.CorrespondanceReport.Add(NewCR);
            //        _db.SaveChanges();
            //    }
            //}

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        private ReportParameter[] GetParametersLocal(DateTime SSDate, DateTime SEDate, string SType)
        {
            if (SType == "Sortable")
            {
                ReportParameter p1 = new ReportParameter("ReportTitle", "Correspondance: Report (Sortable (ALL))");
                return new ReportParameter[] { p1 };
            }
            else
            { 
                ReportParameter p1 = new ReportParameter("ReportTitle", "Correspondance: Report (" + SSDate.ToShortDateString() + " - " + SEDate.ToShortDateString() + ")");
                return new ReportParameter[] { p1 };
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }

}