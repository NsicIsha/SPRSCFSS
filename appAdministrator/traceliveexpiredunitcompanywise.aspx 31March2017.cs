using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;

public partial class appAdministrator_traceliveexpiredunitcompanywise : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    int successId;
    int Unitid, Formtypeid, count = 0;
    string UserFullName = "";
    StringBuilder sb;

    string RefrenceNumber="";
    string CompanyName = "";
    string CompanyStatus = "";
    int ZoneId;
    int BranchId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId != 1 && RoleId != 10)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                ZoneId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
            }
            txt_Ref_no.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btn_status_track');");
            txt_Ref_no.Focus();
            Bind_tabel_liveunitandexpiredunit();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            ErrHandler.WriteError("" + ex.StackTrace);
        }

    }
    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        Bind_tabel_liveunitandexpiredunit();
    }


    public void Bind_tabel_liveunitandexpiredunit()
    {
        try
        {
            Myds = new DataSet();
            if (ddl_list.SelectedValue == "1")
            {
                CompanyName = txt_Ref_no.Text.ToString().Trim();
            }
            if (ddl_list.SelectedValue == "2")
            {
                RefrenceNumber = txt_Ref_no.Text.ToString().Trim();
            }
            sb = new StringBuilder();
            objCompany = new clsCompany();
            BranchId = Convert.ToInt32(Request.QueryString["id"].ToString());
            CompanyStatus = Convert.ToString(Request.QueryString["Status"].ToString());
            Myds = objCompany.Fetch_Company_status_track_Live_Unit_Expired_unit(RefrenceNumber, CompanyName, CompanyStatus, ZoneId, BranchId);
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptID = objDecEnc.Encrypt("1");
            string EncryptCompany_Name = "";
            string EncryptCompanyID = "";
            string EncryptBranchName = "";
            string imageid = string.Empty;
           // imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
            HyperLink hyper =new HyperLink();
            if (Myds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i <= Myds.Tables[0].Rows.Count-1; i++)
                {
                    sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; min-width:800px; ' >");  // main table we have closed it
                    sb.Append("<tr class='GrdviewHeader'><th >Company Name</th><th >Reference Number /<br/>Registration Number </th>");
                    sb.Append("<th > Zone Branch </th> <th> Registration Date /<br/> Validity</th>  <th> </th></tr>");
                    sb.Append("<tr><td align='left'  valign='top'> " + Myds.Tables[0].Rows[i]["ComapnyAddress"].ToString() + " </td>");
                    sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["GPRefrenceNo"].ToString()  +"<br/>" + Myds.Tables[0].Rows[i]["RegistrationNumber"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["ZoneBranchName"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                   // sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["ContactDetails"].ToString() + "</td>");
                  //  sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["RegistrationNumber"].ToString() + "</td>");
                    EncryptCompany_Name =objDecEnc.Encrypt( Myds.Tables[0].Rows[i]["CompanyName"].ToString());
                    EncryptCompanyID = objDecEnc.Encrypt(Myds.Tables[0].Rows[i]["CompanyId"].ToString());
                    EncryptBranchName = objDecEnc.Encrypt(Myds.Tables[0].Rows[i]["BranchName"].ToString());
                    hyper.NavigateUrl="ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                     imageid="ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    hyper.Text = "View";
                    sb.Append("<td align='left' valign='middle' style='text-decoration:underline; color:blue;'>" + "<a href=" + imageid + " target='_blank'>" + "<b>" + hyper.Text + "</b>" + "</a>" + "</td>");
                    sb.Append("</tr>");
                }
               
                sb.Append("</table>");
                lbl_report_status_track.Text = sb.ToString();
            }

           

            else
            {
                Myds.Dispose();
                lbl_report_status_track.Text ="<span style='color:red'>"+"<b>" +"No record Found!" +"</b></span>";
                  
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }


}
