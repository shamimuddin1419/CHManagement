using PCOHRApp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using PCOHRApp.DA;
using PCOHRApp.Models;

namespace PCOHRApp.Controllers
{
    public class CableReportController : Controller
    {
        private ReportDA _da;
        private HostDA _hostDa;
        private DishBillGenerateDA _dishBillGenerateDA;
        private InternetBillGenerateDA _internetBillGenerateDA;
        private ReportDataSource rd;
        public CableReportController()
        {
            _da = new ReportDA();
            _hostDa = new HostDA();
            _dishBillGenerateDA = new DishBillGenerateDA();
            _internetBillGenerateDA = new InternetBillGenerateDA();
        }
        // GET: CableReport
        [CustomSessionFilterAttribute]
        public ActionResult AllDishCustomerDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult AllInternetCustomerDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult AllDishCollectionDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult AllInternetCollectionDetails()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishPaymentHistory()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult InternetPaymentHistory()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult InternetBillDue()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishBillDue()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult HostList()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult MonthWiseDishBill()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult MonthWiseInternetBill()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishCollectionPageWise()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult InternetCollectionPageWise()
        {
            return View();
        }
        //------------- Report 

       [CustomSessionFilterAttribute]
        public ActionResult IntCollPageBetween()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult IntCollPageGroup()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishCollPageBetween()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult DishCollPageGroup()
        {
            return View();
        }
        [CustomSessionFilterAttribute]
        public ActionResult IntBillCard()
        {
            return View();
        }
         [CustomSessionFilterAttribute]
        public ActionResult DishBillCard()
        {
            return View();
        }
        
        public void ReportView(string reportType, int? cid,int? receivedBy, int? collectedBy, bool? isActive, int? zoneId, string fromDate = "", string toDate = "",string ctype = "",int pageNo = 0)
        {
            if (reportType == "AllDishCustomerDetails" || reportType == "AllInternetCustomerDetails")
            {
                Session["reportType"] = reportType;
                Session["isActive"] = isActive;
                Session["zoneId"] = zoneId;
            }
            else if (reportType == "DishBillDetails" || reportType == "InternetBillDetails")
            {
                Session["reportType"] = reportType;
                Session["fromDate"] = fromDate;
                Session["toDate"] = toDate;
                Session["cid"] = cid;
                Session["receivedBy"] = receivedBy;
                Session["collectedBy"] = collectedBy;
                Session["ctype"] = ctype;
            }
            else if (reportType == "DishBillDue" || reportType == "InternetBillDue")
            {
                Session["reportType"] = reportType;
                Session["zoneId"] = zoneId;
            }
            else if (reportType == "DishPaymentHistory" || reportType == "InternetPaymentHistory")
            {
                Session["reportType"] = reportType;
                Session["cid"] = cid;
            }
            
        }
        public ActionResult ReportViewer()
        {
            return View();
        }

        public ActionResult ShowReport(string fileType, string reportType, int? cid, int? receivedBy, int? collectedBy, bool? isActive, int? zoneId, int? custSerialPrefixId, int? assignedUserId, int? hostId, int? dueReportStatus, int? activeStatus, string fromDate = "", string toDate = "", string ctype = "", string month = "", int year = 0, int pageNo = 0, int topageNo = 0, string yearName = "", string receivedByString="")
        {
            LocalReport lr = new LocalReport();
            if (reportType == "AllDishCustomerDetails")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "AllDishCustomerDetails.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("isActive", isActive == null ? null : isActive.ToString()));
                reportParameters.Add(new ReportParameter("zoneId",zoneId + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCustomerList(isActive, zoneId?? 0, 0, custSerialPrefixId ?? 0, assignedUserId ?? 0, hostId ?? 0) ?? new List<CustomerVM>();
                rd = new ReportDataSource("CustomerDataSet", dataList);
                lr.DataSources.Add(rd);

            }
            else if (reportType == "AllInternetCustomerDetails")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "AllInternetCustomerDetails.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("isActive", isActive == null ? null : isActive.ToString()));
                reportParameters.Add(new ReportParameter("zoneId", zoneId + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetCustomerList(isActive, zoneId ?? 0, 0, custSerialPrefixId ?? 0, assignedUserId ?? 0, hostId ?? 0) ?? new List<CustomerVM>();
                rd = new ReportDataSource("CustomerDataSet", dataList);
                lr.DataSources.Add(rd);
            }
            else if (reportType == "DishBillDetails")
            {
                DateTime rptFromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                DateTime rptToDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), ctype == "0" ? "AllDishBillDetails.rdlc" : "AllDishBillDetailsByType.rdlc");
                lr.ReportPath = path;


                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromDate", rptFromDate.ToString()));
                reportParameters.Add(new ReportParameter("toDate", rptToDate.ToString()));
                reportParameters.Add(new ReportParameter("cid", cid.ToString()));
                reportParameters.Add(new ReportParameter("receivedBy", receivedBy.ToString()));
                reportParameters.Add(new ReportParameter("collectedBy", collectedBy.ToString()));
                if (ctype != "0")
                {
                    reportParameters.Add(new ReportParameter("ctype", ctype));
                }
                lr.SetParameters(reportParameters);
                if (ctype == "0")
                {
                    var dataList = _da.GetDishBillCollections(rptFromDate, rptToDate, cid ?? 0, receivedBy ?? 0, collectedBy ?? 0) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    lr.DataSources.Add(rds);
                }
                else
                {
                    var dataList = _da.GetDishBillCollectionByType(rptFromDate, rptToDate, cid ?? 0, receivedBy ?? 0, collectedBy ?? 0, ctype) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    lr.DataSources.Add(rds);
                }
            }
            else if (reportType == "InternetBillDetails")
            {
                DateTime rptFromDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                DateTime rptToDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), ctype == "0" ? "AllInternetBillDetails.rdlc" : "AllInternetBillDetailsByType.rdlc");
                lr.ReportPath = path;


                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromDate", rptFromDate.ToString()));
                reportParameters.Add(new ReportParameter("toDate", rptToDate.ToString()));
                reportParameters.Add(new ReportParameter("cid", cid.ToString()));
                reportParameters.Add(new ReportParameter("receivedBy", receivedBy.ToString()));
                reportParameters.Add(new ReportParameter("collectedBy", collectedBy.ToString()));
                if (ctype != "0")
                {
                    reportParameters.Add(new ReportParameter("ctype", ctype));
                }
                lr.SetParameters(reportParameters);
                if (ctype == "0")
                {
                    var dataList = _da.GetInternetBillCollections(rptFromDate, rptToDate, cid ?? 0, receivedBy ?? 0, collectedBy ?? 0) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    lr.DataSources.Add(rds);
                }
                else
                {
                    var dataList = _da.GetInternetBillCollectionByType(rptFromDate, rptToDate, cid ?? 0, receivedBy ?? 0, collectedBy ?? 0, ctype) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    lr.DataSources.Add(rds);
                }
            }
            else if (reportType == "DishBillDue")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishCustomerDue.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("zoneId", zoneId + ""));

                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCustomerDue(zoneId ?? 0, custSerialPrefixId ?? 0, assignedUserId ?? 0, hostId??0,activeStatus??0,dueReportStatus??0) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
                lr.Refresh();
              
            }
            else if (reportType == "InternetBillDue")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "InternetCustomerDue.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("zoneId", zoneId + ""));

                lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetCustomerDue(zoneId ?? 0, custSerialPrefixId ?? 0, assignedUserId ?? 0, hostId ?? 0,activeStatus??0,dueReportStatus??0) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
                lr.Refresh();
            }
            else if (reportType == "DishPaymentHistory")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishPaymentHistory.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("cid", cid + ""));

                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishPaymentHistory(cid ?? 0) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "InternetPaymentHistory")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "InternetPaymentHistory.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("cid", cid + ""));

                lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetPaymentHistory(cid ?? 0) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "HostList")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "HostList.rdlc");
                lr.ReportPath = path;
                var dataList = _hostDa.GetHostList() ?? new List<HostVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("HostDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "MonthWiseDishBillGenerate")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "MonthWiseDishBillGenerate.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("month", month));
                reportParameters.Add(new ReportParameter("year", year + ""));
                lr.SetParameters(reportParameters);
                var dataList = _dishBillGenerateDA.GetBillList(month,year) ?? new List<BillGenerateVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("BillDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "MonthWiseInternetBillGenerate")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "MonthWiseInternetBillGenerate.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("month", month));
                reportParameters.Add(new ReportParameter("year", year + ""));
                lr.SetParameters(reportParameters);
                var dataList = _internetBillGenerateDA.GetBillList(month, year) ?? new List<BillGenerateVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("BillDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "DishBillCollectionPageWise")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishBillDetails.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCollectionPageWise(year, pageNo, receivedBy ?? 0) ?? new List<BillCollectionVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "DishBillCollectionPageBetween") // Report
            {
                IDictionary<string, string> UserFullNames = new Dictionary<string, string>();
                UserFullNames.Add("Amirul", "Alhaj Md Amirul Islam Lingkon");
                UserFullNames.Add("Yousof", "Hazi Yousof Ahmed");
                UserFullNames.Add("ALL", "ALL");
                receivedByString = receivedByString == "Select" ? "ALL" : receivedByString;
                receivedByString = UserFullNames[receivedByString];

                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishBillCollectionPageBetween.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromPage", pageNo + ""));
                reportParameters.Add(new ReportParameter("topage", topageNo + ""));
                reportParameters.Add(new ReportParameter("yearName", yearName + ""));
                reportParameters.Add(new ReportParameter("receivedByString", receivedByString + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCollectionPageBetween(year, pageNo, receivedBy ?? 0, topageNo) ?? new List<BillCollectionVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "DishBillCollectionPageGroup") // Report
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishCollectionPageGroup.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromPage", pageNo+""));
                reportParameters.Add(new ReportParameter("topage", topageNo + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCollectionPageGroup(year, pageNo, receivedBy ?? 0, topageNo) ?? new List<BillCollectionVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "InternetBillCollectionPageWise")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "InternetBillDetails.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetCollectionPageWise(year, pageNo, receivedBy ?? 0) ?? new List<BillCollectionVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "InternetBillCollectionPageBetween") // Report
            {
                IDictionary<string, string> UserFullNames = new Dictionary<string, string>();
                UserFullNames.Add("Amirul", "Alhaj Md Amirul Islam Lingkon");
                UserFullNames.Add("Yousof", "Hazi Yousof Ahmed");
                UserFullNames.Add("ALL", "ALL");
                receivedByString = receivedByString == "Select" ? "ALL" : receivedByString;
                receivedByString = UserFullNames[receivedByString];

                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "InternetBillCollectionPageBetween.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromPage", pageNo + ""));
                reportParameters.Add(new ReportParameter("topage", topageNo + ""));
                reportParameters.Add(new ReportParameter("yearName", yearName + ""));
                reportParameters.Add(new ReportParameter("receivedByString", receivedByString + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetCollectionPageBetween(year, pageNo, receivedBy ?? 0, topageNo) ?? new List<BillCollectionVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "InternetBillCollectionPageGroup") // Report
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "InternetCollectionPageGroup.rdlc");
                lr.ReportPath = path;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("fromPage", pageNo + ""));
                reportParameters.Add(new ReportParameter("topage", topageNo + ""));
                lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetCollectionPageGroup(year, pageNo, receivedBy ?? 0, topageNo) ?? new List<BillCollectionVM>();
                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "DishBillCard")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "BillCard.rdlc");
                lr.ReportPath = path;
                //ReportParameterCollection reportParameters = new ReportParameterCollection();
                //reportParameters.Add(new ReportParameter("cid", cid + ""));

                //lr.SetParameters(reportParameters);
                var dataList = _da.GetDishCustomerCardPrintInfo(cid ?? 0) ?? new List<PCOHRApp.Models.CardBillPrintVM>();

                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dataList);
                lr.DataSources.Add(rds);
            }
            else if (reportType == "InternethBillCard")
            {
                string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "BillCard.rdlc");
                lr.ReportPath = path;
                //ReportParameterCollection reportParameters = new ReportParameterCollection();
                //reportParameters.Add(new ReportParameter("cid", cid + ""));

                //lr.SetParameters(reportParameters);
                var dataList = _da.GetInternetCustomerCardPrintInfo(cid ?? 0) ?? new List<PCOHRApp.Models.CardBillPrintVM>();

                lr.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", dataList);
                lr.DataSources.Add(rds);
            }

            string rptType = fileType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + fileType + "</OutputFormat>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                rptType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);

        }
    }
}