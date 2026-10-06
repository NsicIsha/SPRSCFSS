using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.IO;
using System.Web.Mvc;

public partial class appAdministrator_BugDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.RegisterPostBackControl(grdBugDetails);
            sm.RegisterPostBackControl(ddlStatus);
        }
        if (!IsPostBack)
        {
           
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            int UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Session["BranchID"] = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MB_Id"].ToString());
            FillBugTable(grdBugDetails,"Open");
            if (grdBugDetails.HeaderRow != null)
                grdBugDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void lnkDownload_Command(object sender, CommandEventArgs e)
    {
        string fileName = ExtractFileName(e);
        string path =  System.Configuration.ConfigurationManager.AppSettings["MasterPath"].ToString();       
        if (path != string.Empty && !string.IsNullOrEmpty(fileName))
        {
            try
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                string filePath = path + "\\\\" + fileName;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=document.pdf");
                byte[] data = req.DownloadData((filePath));
                response.BinaryWrite(data);               
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this.Page, "No document attached");
            }

        }
        else
        {
            MessageBox.Show(this.Page, "No document attached");
        }
        if (grdBugDetails.HeaderRow != null)
            grdBugDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    private string ExtractFileName(CommandEventArgs e)
    {
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        string fileName = string.Empty;
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select FileName from BugTracker where Bug_ID='" + e.CommandArgument + "'", con))
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                try
                {
                    while (rdr.Read())
                    {
                        fileName = Convert.ToString(rdr["FileName"]);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    rdr.Close();
                    con.Close();
                }
                return fileName;
            }
        }
    }

    private void FillBugTable(GridView grd, string status)
    {
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        string query = "select top 200 Bug_ID,Bug_Title,ProjectName,Bug_Reporter,CloseComments,Bug_Status,Bug_ClosedOn,Bug_LoggedOn,Bug_Priority,CloseComments, Bug_STR,ExpectedResult,ActualResult,Notes,Bug_PAN,Bug_Severity,Bug_AssignedTo,targetEmailID from BugTracker where BranchID='" + Session["BranchID"] + "' and Bug_Status='"+status+ "' order by Bug_LoggedOn desc ";//: "select top 1000 Bug_ID,Bug_Title,ProjectName,Bug_Reporter,Bug_Status,Bug_ClosedOn,Bug_LoggedOn,Bug_Priority,CloseComments, Bug_STR,ExpectedResult,ActualResult,Notes,Bug_PAN,Bug_Severity,Bug_AssignedTo,targetEmailID from BugTracker where Bug_Status='" + status + "' order by Bug_LoggedOn desc ";
        using (SqlConnection con = new SqlConnection(constr))
        {

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ds = new DataSet();
                da.Fill(ds);
                grd.DataSource = ds;
                grd.DataBind();
                grd.HeaderRow.TableSection = TableRowSection.TableHeader;
                con.Close();
            }
        }
    }

    protected void btnStatusChange_Command(object sender, CommandEventArgs e)
    {
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        TextBox txtclose = (sender as LinkButton).FindControl("txtCloseComment") as TextBox;
        txtclose.Visible = true;
        string query = "update BugTracker set Bug_Status='Close' where Bug_ID='" + e.CommandArgument + "'";


        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        FillBugTable();
        //    }
        //}
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if ((sender as DropDownList).SelectedIndex == 1)
        {
            FillBugTable( grdBugDetails, "Open");
            //grdBugDetails.Visible = true;
            //grdViewClose.Visible = false;
        }
        if ((sender as DropDownList).SelectedIndex == 2)
        {
            FillBugTable(grdBugDetails, "Close");
            //grdBugDetails.Visible = false;
            //grdViewClose.Visible = true;
        }
        if (grdBugDetails.HeaderRow != null)
            grdBugDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
   

    protected void btnReopen_Command(object sender, CommandEventArgs e)
    {
        GridViewRow grdRow = ((sender as LinkButton).Parent).Parent as GridViewRow;
        grdRow.BackColor = System.Drawing.Color.Orchid;


    }

    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        
    }

    protected void btnCloseBug_Click(object sender, EventArgs e)
    {
       
    }
    
}

public static class MessageBox
{
    public static void Show(this Page Page, String Message)
    {
        Page.ClientScript.RegisterStartupScript(
           Page.GetType(),
           "MessageBox",
           "<script language='javascript'>alert('" + Message + "');</script>"
        );
    }
}
