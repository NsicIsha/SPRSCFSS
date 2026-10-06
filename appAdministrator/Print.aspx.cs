using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.IO;
using System.Text;
using System.Diagnostics;

public partial class appAdministrator_Print : System.Web.UI.Page
{

    clsMasterBranch objBranch;
    clsMasterZone objZone;
    clsCompany objcompany;
    clsDDL objddl;
    DataSet Myds;
    int accessId;
    int count = 1;
    string FormType;
    DataTable DTZone;

    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
       // Control ctrl = (Control)Session["ctrl"];
      //  PrintHelper.PrintWebControl(ctrl);

        try
        {
            bindgridview2();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public void bindgridview2()
    {
        try
        {

            string dateFrom;
            string dateto;
            int BranchID;
            int ZoneID;
            string StatusText;
            int State_ID = 0;
            int City_ID = 0;
            string Turn_over_from = "";
            string Turn_over_to = "";
            string Monetarylimit_from = "";
            string Monetarylimit_to = "";
            int CategoryID;
            int SubCategoryID;
            int CASTID;
            int provoisinal;
            string Orderby="";
            string SearchCompany_name = "";
            //dateFrom = txt_21.Text.ToString();
            if (Request.QueryString["dt"].ToString() == "")
            {
                dateFrom = "";
            }
            else
            {
                dateFrom = Request.QueryString["dt"].ToString();
            }
            if (Request.QueryString["CID"].ToString() == "0")
            {
                CategoryID = 0;
            }
            else
            {
                CategoryID = Convert.ToInt32(Request.QueryString["CID"].ToString());
            }
            if (Request.QueryString["SID"].ToString() == "0")
            {
                SubCategoryID = 0;
            }
            else
            {
                SubCategoryID = Convert.ToInt32(Request.QueryString["SID"].ToString());
            }
            if (Request.QueryString["CastID"].ToString() == "0")
            {
                CASTID = 0;
            }
            else
            {
                CASTID = Convert.ToInt32(Request.QueryString["CastID"].ToString());

            }

            if (Request.QueryString["dto"].ToString() == "")
            {
                dateto = "";
            }
            else
            {
                dateto = Request.QueryString["dto"].ToString();
            }
            if (Convert.ToInt32(Request.QueryString["brid"].ToString()) == 0)
            {
                BranchID = 0;
            }
            else
            {
                BranchID = Convert.ToInt32(Request.QueryString["brid"].ToString());
            }
            if (Convert.ToInt32(Request.QueryString["zid"].ToString()) == 0)
            {
                ZoneID = 0;
            }
            else
            {
                ZoneID = Convert.ToInt32(Request.QueryString["zid"].ToString());
            }
            if(Request.QueryString["orderby"].ToString()=="1")
            {
                Orderby = "1";
            }
            if (Request.QueryString["orderby"].ToString() == "2")
            {
                Orderby = "2"; 
            }
            if (Request.QueryString["orderby"].ToString() == "3")
            {
                Orderby = "3";
            }
            if (Request.QueryString["SatateID"].ToString() == "0")
            {
                State_ID = Convert.ToInt32(Request.QueryString["SatateID"].ToString());
            }
            else
            {
                State_ID = Convert.ToInt32(Request.QueryString["SatateID"].ToString());
            }
            if (Request.QueryString["City_ID"].ToString() == "0")
            {
                City_ID = 0;
            }
            else
            {
                City_ID = Convert.ToInt32(Request.QueryString["City_ID"].ToString());
            }
            if ((Request.QueryString["turn_over_one"].ToString())== "" || (Request.QueryString["turn_over_one"].ToString())==" ")
            {
                Turn_over_from = " ";
            }
            else
            {
                Turn_over_from =Convert.ToString(Request.QueryString["turn_over_one"].ToString());
            }
            if ((Request.QueryString["turn_over_two"].ToString()) == "" || (Request.QueryString["turn_over_two"].ToString()) == " ")
            {
                Turn_over_to = " ";
            }
            else
            {
                Turn_over_to = Convert.ToString(Request.QueryString["turn_over_two"].ToString());
            }
            if (Request.QueryString["Monetery_limit_one"].ToString() == " ")
            {
                Monetarylimit_from = " ";
            }
            else
            {
                Monetarylimit_from = Convert.ToString(Request.QueryString["Monetery_limit_one"].ToString());
            }
            if (Request.QueryString["Monetary_limit_two"].ToString() == " " || Request.QueryString["Monetary_limit_two"].ToString()=="")
            {
                Monetarylimit_to = " ";
            }
            else
            {
                Monetarylimit_to = Request.QueryString["Monetary_limit_two"].ToString();
            }
            lblReportName.Text = Request.QueryString["Lblreport"].ToString();
            if (lblReportName.Text == "Amendment")
            {
                FormType = "3";
            }
            else if (lblReportName.Text =="Registration Forms Filled" || lblReportName.Text=="Companies List" )
            {
                FormType = "1";
            }
            else if (lblReportName.Text =="Renewal")
            {
                FormType = "4";
            }
            if (Request.QueryString["statusid"].ToString() == "undefined" || Request.QueryString["statusid"].ToString() == "--Selctone--")
            {
                StatusText = "";
            }
            else
            {

                StatusText = Request.QueryString["statusid"].ToString();
                if (StatusText == "Registered")
                {
                    DetailsView2.Columns[1].HeaderText = "Registration Date";
                }
                else
                {
                    DetailsView2.Columns[1].HeaderText = StatusText;
                }
            }
            if (Request.QueryString["provisinal"].ToString() == "undefined" || Request.QueryString["provisinal"].ToString() == "--Selctone--")
            {
               provoisinal=0;   
            }
            else
            {
                provoisinal = Convert.ToInt32(Request.QueryString["provisinal"].ToString());
            }
            objcompany = new clsCompany();
            DataSet dsgridview = new DataSet();
            DataTable dt_print = new DataTable();
            //   dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Dataset(dateFrom, dateto, BranchID);
            if (FormType == "3")
            {
                //dsgridview=objcompany.
                dsgridview = objcompany.Company_WaitingCompany_Amedment_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_ID, City_ID, Turn_over_from, Turn_over_to, Monetarylimit_from, Monetarylimit_to, CategoryID, SubCategoryID, CASTID, SearchCompany_name, Orderby);
            }
            else if(FormType=="1")
            {
                if (lblReportName.Text == "Companies List")
                {
                    dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_ID, City_ID, Turn_over_from, Turn_over_to, Monetarylimit_from, Monetarylimit_to, CategoryID, SubCategoryID, CASTID, SearchCompany_name, Orderby);
                    var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == provoisinal).Count();
                    if (count1 > 0)
                    {
                        dt_print = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == provoisinal).CopyToDataTable();                  
                    }
                    else
                    {
                        dsgridview.Dispose();
                    }
                }
                else
                {
                    dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_ID, City_ID, Turn_over_from, Turn_over_to, Monetarylimit_from, Monetarylimit_to, CategoryID, SubCategoryID, CASTID, SearchCompany_name, Orderby);
                }
            }
            else if (FormType == "4")
            {
                dsgridview = objcompany.Company_WaitingCompany_Renewal_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_ID, City_ID, Turn_over_from, Turn_over_to, Monetarylimit_from, Monetarylimit_to, CategoryID, SubCategoryID, CASTID, SearchCompany_name, Orderby);
            }
            dsgridview.Tables[0].DefaultView.Sort = dsgridview.Tables[0].Columns["fGPtbl_Company_Name"].ToString() + " " + "ASC";
            DataTable dtgrid = new DataTable();
            dtgrid = dsgridview.Tables[0];


            if (StatusText == "undefined" || StatusText == " " || StatusText==null || StatusText=="")
            {
                if (dsgridview.Tables[0].Rows.Count > 0)
                {
                    //lblMessage.Text = "";

                    DetailsView2.DataSource = dtgrid;
                    DetailsView2.DataBind();





                    // grdRepor.DataSource = dsgridview.Tables[0];
                    //grdRepor.DataBind();
                }
                else
                {


                    DetailsView2.Dispose();
                    DetailsView2.DataBind();
                    dsgridview.Dispose();

                }
            }
            else
            {

                DataTable dtprintGrid = new DataTable();

                //DataTable dt_print = new DataTable();

                if (StatusText == " ")
                {
                    if (dsgridview.Tables[0].Rows.Count > 0)
                    {
                        //lblMessage.Text = "";

                        DetailsView2.DataSource = dtgrid;
                        DetailsView2.DataBind();





                        // grdRepor.DataSource = dsgridview.Tables[0];
                        //grdRepor.DataBind();
                    }

                }
                else
                {
                    dt_print = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == provoisinal).CopyToDataTable();
                    var count = dt_print.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == StatusText).Count();
                    int countStatus = Convert.ToInt32(count);
                    if (count > 0)
                    {

                        dtprintGrid = dt_print.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == StatusText).OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                        DetailsView2.DataSource = dtprintGrid;
                        DetailsView2.DataBind();
                    }
                    else
                    {
                        // Changes Is Here 


                        DataTable dtFilterReg = new DataTable();
                        dtFilterReg = dsgridview.Tables[0];
                        if (dtFilterReg.Rows.Count > 0)
                        {
                              var count1=0;
                            if (FormType == "3")
                            {

                                count1 = dtFilterReg.AsEnumerable().Count();
                            }
                            else
                            {
                           count1 = dtFilterReg.AsEnumerable().Where(a => Convert.ToString(a["fGPtbl_CUnit_FormtypeId"].ToString()) == FormType).Count();
                            }
                            int countStatus1 = Convert.ToInt32(count1);
                            if (count1 > 0)
                            {
                                DataTable dtFilterRegFilter = new DataTable();
                                if (FormType == "3")
                                {
                                    dtFilterRegFilter = dtFilterReg.AsEnumerable().OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                                }
                                else
                                {
                                    dtFilterRegFilter = dtFilterReg.AsEnumerable().Where(a => Convert.ToString(a["fGPtbl_CUnit_FormtypeId"].ToString()) == FormType).OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                                }
                                DetailsView2.DataSource = dtFilterRegFilter;
                                DetailsView2.DataBind();

                            }
                            else
                            {

                                DetailsView2.Dispose();
                                DetailsView2.DataBind();


                            }
                        }
                        else
                        {
                            dtFilterReg.Dispose();
                        }

                    }


                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
          //  ex.Message("Server Busy!!!");
        }
    }
    
    protected void MainPrint_Databound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (lblReportName.Text == "Registration Forms Filled" || lblReportName.Text == "Companies List")
                {
                    // Application Date/Validity
                    e.Row.Cells[2].Text = "Registration No/Validity";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
                e.Row.Cells[0].Text = decodedText;
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Width, "250px");
                string Decodetext_contact_details = HttpUtility.HtmlDecode(e.Row.Cells[1].Text);
                e.Row.Cells[1].Text = Decodetext_contact_details;
                string Decodetext_ZoneBranch = HttpUtility.HtmlDecode(e.Row.Cells[3].Text);
                e.Row.Cells[3].Text = Decodetext_ZoneBranch;
                e.Row.Cells[2].Text = HttpUtility.HtmlDecode(e.Row.Cells[2].Text);
                this.intCurrentRecordNumber++;
                if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
                {
                    e.Row.Attributes.Add("style", @"page-break-after:avoid;");
                    this.intCurrentRecordNumber = 0;
                }
                if (lblReportName.Text == "Renewal Forms Filled")
                {
                    DetailsView2.Columns[4].Visible = false;
                }
                else
                {
                    DetailsView2.Columns[4].Visible = true;
                }

                //DetailsView2.Columns[1].HeaderText = "Ravish";

            }



            //if(DetailsView2.Rows.Count>0)
            //{
            //    DetailsView2.CssClass = "ui-widget-content";
            //     DetailsView2.UseAccessibleHeader=true;

            //    DetailsView2.HeaderRow.TableSection=TableRowSection.TableHeader;

            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    //protected void PageBreakRender(HtmlTextWriter output, Control container)
    //{
    //    HtmlTextWriter cellsWriter = new HtmlTextWriter(new StringWriter());
    //    foreach (Control c in container.Controls)
    //    {
    //        TableCell cell = (TableCell)c;
    //        cell.RenderControl(cellsWriter);
    //    }

    //    output.Write("<tr>");
    //    output.Write(cellsWriter.InnerWriter.ToString());
    //    output.Write("</tr></table>");
    //    output.Write(GetPageFooterHtml());

    //    //If it is the last row, don't show the next header
    //    if (_currentPrintPage == PrintPageCount && Rows.Count %
    //        (_currentPrintPage) == 0)
    //        return;

    //    output.Write("<div style=\"page-break-after:always;\"></div>");
    //    output.Write(GetHeaderHtml());
    //    _currentPrintPage++;
    //}
    protected void detailsview_prerender(object sender, EventArgs e)
    {
        try
        {
            DetailsView2.CssClass = "ui-widget-content";

            DetailsView2.UseAccessibleHeader = false;

            DetailsView2.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    //protected void Print(object sender, EventArgs e)
    //{
        
 
    //    DetailsView2.UseAccessibleHeader = true;
    //    DetailsView2.HeaderRow.TableSection = TableRowSection.TableHeader;

    //    DetailsView2.Attributes["style"] = "border-collapse:separate";
    //    foreach (GridViewRow row in DetailsView2.Rows)
    //    {
    //        if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
    //        {
    //            row.Attributes["style"] = "page-break-after:always;";
    //        }
    //    }
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    DetailsView2.RenderControl(hw);
    //    string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script type = 'text/javascript'>");
    //    sb.Append("window.onload = new function(){");
        
    //    sb.Append("var printWin = window.open('', '', 'left=0");
    //  //  sb.Append("var prtContent = document.getElementById(Div_Main)");
    //    sb.Append(",top=0,width=1000,height=600,status=0');");
    //    sb.Append("printWin.document.write(\"");
    //   // sb.Append(";");
    //    string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
    //    sb.Append(style + gridHTML);
    //    sb.Append("\");");
    //    sb.Append("printWin.document.close();");
    //    sb.Append("printWin.focus();");
    //    sb.Append("printWin.print();");
    //    sb.Append("printWin.close();");
    //    sb.Append("};");
    //    sb.Append("</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "Div_Main", sb.ToString());  //GridPrint
    //    DetailsView2.DataBind();
    //}

   




    protected void btnword_Click(object sender, EventArgs e)
    {
        try
        {
            Response.AddHeader("content-disposition", "attachment;filename=FileName.doc");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.word";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();

            DetailsView2.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(DetailsView2);
            frm.RenderControl(htmlWrite);

            //GridView1.RenderControl(htw);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError("" + EX.Message.ToString());
        }
    }


    protected void btnexcel_Click(object sender, EventArgs e)
    {
        try
        {
            string attachment = "attachment; filename=Contacts.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();
            DetailsView2.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(DetailsView2);
            frm.RenderControl(htw);

            //GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError("" + EX.Message.ToString());
        }
    }
    protected void btnpdf_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            DetailsView2.RenderControl(htw);

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=MypdfFile.pdf");
            Response.Write(sw.ToString());
            Response.End(); 
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError("" + EX.Message.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

}
