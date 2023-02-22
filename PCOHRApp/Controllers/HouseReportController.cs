﻿using Microsoft.Reporting.WebForms;
using PCOHRApp.DA;
using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCOHRApp.Controllers
{
    public class HouseReportController : Controller
    {
        private readonly HouseBillGenerateDA _houseBillGenerateDA;
        private readonly HouseBillCollectionDA _houseBillCollectionDA;
        public HouseReportController()
        {
            _houseBillGenerateDA = new HouseBillGenerateDA();
            _houseBillCollectionDA = new HouseBillCollectionDA();
        }
        public FileResult ShowReport(string fileType, string reportType, int? billDetailId)
        {
            try
            {
                LocalReport lr = new LocalReport();
                if (reportType == "HouseBillInformation")
                {
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "HouseBill.rdlc");
                    lr.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    HouseBillInformationVM result = _houseBillGenerateDA.GetHouseBillInformation(billDetailId ?? 0,true);
                    reportParameters.Add(new ReportParameter("houseName", result.houseName));
                    reportParameters.Add(new ReportParameter("meterNo", result.meterNo));
                    reportParameters.Add(new ReportParameter("unPaidMonth", result.unPaidMonth));
                    reportParameters.Add(new ReportParameter("renterName", result.renterName));
                    reportParameters.Add(new ReportParameter("renterNID", result.renterNID));
                    reportParameters.Add(new ReportParameter("renterPhone", result.renterPhone));
                    reportParameters.Add(new ReportParameter("renterEmail", result.renterEmail));
                    reportParameters.Add(new ReportParameter("billAmount", result.billAmount.ToString()));
                    reportParameters.Add(new ReportParameter("gasCharge", result.gasCharge.ToString()));
                    reportParameters.Add(new ReportParameter("electricityCharge", result.electricityCharge.ToString()));
                    reportParameters.Add(new ReportParameter("waterCharge", result.waterCharge.ToString()));
                    reportParameters.Add(new ReportParameter("serviceCharge", result.serviceCharge.ToString()));
                    reportParameters.Add(new ReportParameter("otherCharge", result.otherCharge.ToString()));
                    reportParameters.Add(new ReportParameter("houseType", result.houseType));
                    reportParameters.Add(new ReportParameter("companyName", result.companyName));
                    reportParameters.Add(new ReportParameter("companyAddress", result.companyAddress));
                    lr.SetParameters(reportParameters);
                }
                else if(reportType == "HouseReceipt")
                {
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "HouseReceipt.rdlc");
                    lr.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    HouseRenterBillCollectionVM result = _houseBillCollectionDA.GetHouseRenterBillReceipt(billDetailId ?? 0);


                    reportParameters.Add(new ReportParameter("houseName", result.houseName));
                    reportParameters.Add(new ReportParameter("meterNo", result.meterNo));

                    reportParameters.Add(new ReportParameter("billMonth", result.billMonth));
                    reportParameters.Add(new ReportParameter("renterName", result.renterName));
                    reportParameters.Add(new ReportParameter("renterPhone", result.renterPhone));
                    reportParameters.Add(new ReportParameter("renterEmail", result.renterEmail));
                    reportParameters.Add(new ReportParameter("renterNID", result.renterNID));

                    reportParameters.Add(new ReportParameter("gasCharge", result.gasCharge.ToString()));
                    reportParameters.Add(new ReportParameter("electricityCharge", result.electricityCharge.ToString()));
                    reportParameters.Add(new ReportParameter("waterCharge", result.waterCharge.ToString()));
                    reportParameters.Add(new ReportParameter("otherCharge", result.otherCharge.ToString()));
                    reportParameters.Add(new ReportParameter("serviceCharge", result.serviceCharge.ToString()));
                    reportParameters.Add(new ReportParameter("payable", result.payableAmount.ToString()));
                    reportParameters.Add(new ReportParameter("adjustAdvance", result.adjustAdvance.ToString()));
                    reportParameters.Add(new ReportParameter("discount", result.discount.ToString()));


                    reportParameters.Add(new ReportParameter("companyName", result.companyName));
                    reportParameters.Add(new ReportParameter("companyAddress", result.companyAddress));
                    
                    reportParameters.Add(new ReportParameter("rcvAmount", result.rcvAmount.ToString()));
                    reportParameters.Add(new ReportParameter("billAmount", result.rentAmount.ToString()));
                    reportParameters.Add(new ReportParameter("paymentRef", result.paymentRef.ToString()));
                    
                    lr.SetParameters(reportParameters);
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}