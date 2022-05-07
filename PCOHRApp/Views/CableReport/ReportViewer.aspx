<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>ReportViwer</title>    
    <script runat="server">
        void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                PCOHRApp.DA.ReportDA _da = new PCOHRApp.DA.ReportDA();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                if (Session["reportType"].ToString() == "AllDishCustomerDetails")
                {

                    bool? isActive = Session["isActive"] == null ? null : (bool?)Convert.ToBoolean(Session["isActive"]);
                    int zoneId = Convert.ToInt32(Session["zoneId"]);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "AllDishCustomerDetails.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("isActive", isActive == null ? null : isActive.ToString()));
                    reportParameters.Add(new ReportParameter("zoneId", Session["zoneId"] + ""));

                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    var dataList = _da.GetDishCustomerList(isActive, zoneId, 0) ?? new List<PCOHRApp.Models.CustomerVM>();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CustomerDataSet", dataList);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.PageCountMode = PageCountMode.Actual;

                }
                else if (Session["reportType"].ToString() == "AllInternetCustomerDetails")
                {

                    bool? isActive = Session["isActive"] == null ? null : (bool?)Convert.ToBoolean(Session["isActive"]);
                    int zoneId = Convert.ToInt32(Session["zoneId"]);


                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "AllInternetCustomerDetails.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;


                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("isActive", isActive == null ? null : isActive.ToString()));
                    reportParameters.Add(new ReportParameter("zoneId", Session["zoneId"] + ""));

                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    var dataList = _da.GetInternetCustomerList(isActive, zoneId, 0) ?? new List<PCOHRApp.Models.CustomerVM>();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CustomerDataSet", dataList);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();

                }
                else if (Session["reportType"].ToString() == "DishBillDetails")
                {

                    DateTime fromDate = DateTime.ParseExact(Session["fromDate"].ToString(), "dd/MM/yyyy", null);
                    DateTime toDate = DateTime.ParseExact(Session["toDate"].ToString(), "dd/MM/yyyy", null);
                    int cid = Convert.ToInt32(Session["cid"]);
                    int receivedBy = Convert.ToInt32(Session["receivedBy"]);
                    int collectedBy = Convert.ToInt32(Session["collectedBy"]);
                    string ctype = Session["ctype"].ToString();

                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), ctype == "0" ? "AllDishBillDetails.rdlc" : "AllDishBillDetailsByType.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;


                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("fromDate", fromDate.ToString()));
                    reportParameters.Add(new ReportParameter("toDate", toDate.ToString()));
                    reportParameters.Add(new ReportParameter("cid", cid.ToString()));
                    reportParameters.Add(new ReportParameter("receivedBy", receivedBy.ToString()));
                    reportParameters.Add(new ReportParameter("collectedBy", collectedBy.ToString()));
                    if (ctype != "0")
                    {
                        reportParameters.Add(new ReportParameter("ctype", ctype));
                    }
                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    if(ctype == "0")
                    {
                        var dataList = _da.GetDishBillCollections(fromDate, toDate, cid, receivedBy, collectedBy) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                    }
                    else
                    {
                        var dataList = _da.GetDishBillCollectionByType(fromDate, toDate, cid, receivedBy, collectedBy,ctype) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                    }
                    
                    ReportViewer1.LocalReport.Refresh();

                }
                else if (Session["reportType"].ToString() == "InternetBillDetails")
                {

                    DateTime fromDate = DateTime.ParseExact(Session["fromDate"].ToString(), "dd/MM/yyyy", null);
                    DateTime toDate = DateTime.ParseExact(Session["toDate"].ToString(), "dd/MM/yyyy", null);
                    int cid = Convert.ToInt32(Session["cid"]);
                    int receivedBy = Convert.ToInt32(Session["receivedBy"]);
                    int collectedBy = Convert.ToInt32(Session["collectedBy"]);
                    string ctype = Session["ctype"].ToString();

                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), ctype == "0" ? "AllInternetBillDetails.rdlc" : "AllInternetBillDetailsByType.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;


                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("fromDate", fromDate.ToString()));
                    reportParameters.Add(new ReportParameter("toDate", toDate.ToString()));
                    reportParameters.Add(new ReportParameter("cid", cid.ToString()));
                    reportParameters.Add(new ReportParameter("receivedBy", receivedBy.ToString()));
                    reportParameters.Add(new ReportParameter("collectedBy", collectedBy.ToString()));
                    if (ctype != "0")
                    {
                        reportParameters.Add(new ReportParameter("ctype", ctype));
                    }
                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    if (ctype == "0")
                    {
                        var dataList = _da.GetInternetBillCollections(fromDate, toDate, cid, receivedBy, collectedBy) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                    }
                    else
                    {
                        var dataList = _da.GetInternetBillCollectionByType(fromDate, toDate, cid, receivedBy, collectedBy, ctype) ?? new List<PCOHRApp.Models.BillCollectionVM>();
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                    }

                    ReportViewer1.LocalReport.Refresh();

                }
                else if (Session["reportType"].ToString() == "InternetBillDue")
                {

                    int zoneId = Convert.ToInt32(Session["zoneId"]);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "InternetCustomerDue.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("zoneId", Session["zoneId"] + ""));

                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    var dataList = _da.GetInternetCustomerDue(zoneId) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.PageCountMode = PageCountMode.Actual;

                }
                else if (Session["reportType"].ToString() == "DishBillDue")
                {

                    int zoneId = Convert.ToInt32(Session["zoneId"]);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishCustomerDue.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("zoneId", Session["zoneId"] + ""));

                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    var dataList = _da.GetDishCustomerDue(zoneId) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.PageCountMode = PageCountMode.Actual;

                }
                else if (Session["reportType"].ToString() == "DishPaymentHistory")
                {

                    int cid = Convert.ToInt32(Session["cid"]);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishPaymentHistory.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("cid", Session["cid"] + ""));

                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    var dataList = _da.GetDishPaymentHistory(cid) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.PageCountMode = PageCountMode.Actual;

                }
                else if (Session["reportType"].ToString() == "InternetPaymentHistory")
                {

                    int cid = Convert.ToInt32(Session["cid"]);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Reports"), "DishPaymentHistory.rdlc");
                    ReportViewer1.LocalReport.ReportPath = path;
                    ReportParameterCollection reportParameters = new ReportParameterCollection();
                    reportParameters.Add(new ReportParameter("cid", Session["cid"] + ""));

                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    var dataList = _da.GetDishPaymentHistory(cid) ?? new List<PCOHRApp.Models.BillCollectionVM>();

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("CollectionDataSet", dataList);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.PageCountMode = PageCountMode.Actual;

                }
                
            }
        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false"></asp:ScriptManager>
        <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="false" SizeToReportContent="true" PageCountMode="Actual" InteractivityPostBackMode="AlwaysSynchronous"></rsweb:ReportViewer>--%>
        <div class="row ">
                        <div class="col-md-12 table-responsive">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"
                                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
                                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="500px"
                                PageCountMode="Actual" CssClass="table" AsyncRendering="False" ShowFindControls="false"
                                InteractivityPostBackMode="AlwaysSynchronous">
                            </rsweb:ReportViewer>
                        </div>
                    </div>
    </div>
    </form>
</body>
</html>

