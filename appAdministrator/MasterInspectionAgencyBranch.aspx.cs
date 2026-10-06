using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_MasterInspectionAgencyBranch : System.Web.UI.Page
{ 
    clsMasterInspectionAgency objInspbr;
    clsTrans_UserBranch objTUB;
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    DataSet Myds;
    //int accessId;
    int successId;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int inspbrAction = 0;

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        Myds = new DataSet();
        objInspbr = new clsMasterInspectionAgency();
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 1 || RoleId == 2 || RoleId ==3)
            {
                if (Page.IsPostBack != true)
                {

                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvInspbr.SetActiveView(ViewInspbrDetail);
                    GridViewDataBind();

                    /*
                    if (RoleId == 1)
                    {
                        DropDownZoneBind(DropDownListZone);
                        DropDownBranchBind();
                        DropDownInspBind();
                    }

                    else if (RoleId == 2)  //AS ZONAL HEAD
                    {
                        DropDownZoneDataBind(DropDownListZone, UserId);
                        //DropDownZoneDataBind(DropDownListZone, 10);
                    }

                    else if (RoleId == 3) //AS BRANCH HEAD
                    {
                        //disable ddl zone and branch show only branch head location
                        DropDownBranchBind(DropDownListBranch, UserId);
                    }
                      */

                }
            }
            else
            {
                btnAddInspbr.Visible = false;
            }


        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    public void GridViewDataBind()
    {
        objInspbr = new clsMasterInspectionAgency();
        Myds = new DataSet();
        try
        {
            Myds = objInspbr.Trans_InspectionagecnyBranch_Fetch_DataSet_byuserId(UserId);
            if (Myds.Tables[0].Rows.Count > 0)
            {
                gvInspbr.DataSource = Myds;
                gvInspbr.DataBind();
            }
            else
            {
                mvInspbr.SetActiveView(ViewInspbrNotFound);
                lblMessage.Text = "No InspectionAgency Branch found!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        try
        {
            if (Session["inspbrAction"] != null)
            {
                LabelAction.Text = Session["inspbrAction"].ToString();
                inspbrAction = 1;
                Session["inspbrAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                inspbrAction = 0;
                Session["inspbrAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["inspbrAction"] = null;
            Myds.Dispose();
        }
    }

  

    protected void gvInspbr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objInspbr = new clsMasterInspectionAgency();
            string emailid;
            string Encryptpwd, Decryptpwd;

            if (e.CommandName == "view")
            {
                Response.Redirect("MasterInspectionAgencyBranchAdd.aspx?act=" + e.CommandArgument);
            }
            else if (e.CommandName == "dte")
            {
                successId = objInspbr.Trans_InspectionagecnyBranch_DeleteById(Convert.ToInt32(e.CommandArgument), UserId);
                if (successId == 1)
                {
                    Session["inspbrAction"] = "Deleted successfully";
                }
                else
                {
                    Session["inspbrAction"] = "Server is busy, please try later";
                }
            }
            else if (e.CommandName == "reset")
            {
                Myds = new DataSet();
                clsMasterMailer objMailer = new clsMasterMailer();
                string FromMail, ccMail, bccMail, subject, bodyMail;

                Myds = objInspbr.Trans_InspectionagecnyBranch_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                emailid = Myds.Tables[0].Rows[0]["fGPtbl_TIAB_EmailId"].ToString();

                //DecryptEncrypt objDecEnc = new DecryptEncrypt();
                RandomKeyGenerator objRnd = new RandomKeyGenerator();
                Decryptpwd = objRnd.Generate();
                //Encryptpwd = objDecEnc.Encrypt(Decryptpwd).Trim();


                Myds = objMailer.MasterMailer_Fetch_DataSet(6);

                FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
                ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
                bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
                subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
                bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +"   " + Decryptpwd + ".";

                string bodystr = bodyMail.Replace("$$password$$", Decryptpwd);

                successId = 4;/* objInspbr.Trans_InspectionagecnyBranch_InsertUpdate(Convert.ToInt32(e.CommandArgument),
                                0, 0, 0, "0", Decryptpwd, "0", UserId);*/

                if (successId == 4)
                {
                   // SendMail.Mail_WithOUTAttachment(emailid, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
                   Session["inspbrAction"]="Password sent to your email";
                }
                else if (successId == 3)
                {
                    Session["inspbrAction"] = "Server is busy please try later";
                }
                else
                {
                    Session["inspbrAction"] = "Server is busy please try later";
                }

            }

            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


   protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

           // successId = objInspbr.Trans_InspectionagecnyBranch_InsertUpdate("0", Convert.ToInt32(DropDownListZone.SelectedValue), Convert.ToInt32(DropDownListBranch.SelectedValue), UserId);

            if (successId == 2)
            {
                Session["inspbrAction"] = "Updated successfully!";
            }
            else
            {
                Session["inspbrAction"] = "Server is busy, please try later!";
            }
            GridViewDataBind();
        }
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }  

    protected void btnAddInspbr_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterInspectionAgencyBranchAdd.aspx?act=axz", false);

    }

    

    
}
