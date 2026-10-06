using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

//1=Add, 2=Edit, 3=View


public partial class appAdministrator_MasterUserProfile : System.Web.UI.Page
{

    DataTable dt;
    DataSet Myds;
    SqlDataReader Mydr;
    clsMasterUser objUser;
    clsTrans_UserBranch objTub;
    clsMasterMailer objMailer;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int userAction = 0;
    int successId;
    int act, actid;


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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
       FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();


            if (RoleId == 1)
            {
                if (Request.QueryString["act"] != null)
                {
                    act = Convert.ToInt32(Request.QueryString["act"]);
                    actid = Convert.ToInt32(Request.QueryString["actid"]);
                }

                if (!IsPostBack)
                {

                    HideShow(act, actid);

                    //Enter button as Default button
                    DropDownListRole.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    DropDownListZone.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    DropDownListBranch.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtUserName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtFirstName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtMiddleName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtLastName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                   // txtDOB.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtEmailId.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtPhoneNo.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtMobileNo.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
                    txtFax.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");


                    DropDownRoleBind();
                    DropDownZoneBind();
                    DropDownBranchBind(0);

                }
            }
            else
            {

                lblRole.Visible = false;
                lblZone.Visible = false;
                lblBranch.Visible = false;
                lblUserName.Visible = false;
                lblFirstName.Visible = false;
                lblMiddleName.Visible = false;
                lblLastName.Visible = false;
            //    lblDOB.Visible = false;
                lblEmailId.Visible = false;
                lblPhoneNo.Visible = false;
                lblMobileNo.Visible = false;
                lblFax.Visible = false;

                DropDownListRole.Visible = false;
                DropDownListZone.Visible = false;
                DropDownListBranch.Visible = false;
                txtUserName.Visible = false;
                txtFirstName.Visible = false;
                txtMiddleName.Visible = false;
                txtLastName.Visible = false;
             //   txtDOB.Visible = false;
               // ltlDOB.Visible = false;
                txtEmailId.Visible = false;
                txtPhoneNo.Visible = false;
                txtMobileNo.Visible = false;
                txtFax.Visible = false;
                btnSubmit.Visible = false;
                btnBack.Visible = false;
            }


        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }

    }

    public void DropDownRoleBind()
    {
        dt=new DataTable();
        dt=clsDDL.ddlRoleExceptAdmin();
        try
        {
            DropDownListRole.DataSource = dt;
            DropDownListRole.DataTextField = dt.Columns["fGPtbl_MR_Name"].ToString();
            DropDownListRole.DataValueField = dt.Columns["fGPtbl_MR_Id"].ToString();
            DropDownListRole.DataBind();
            DropDownListRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

   

    public void DropDownZoneBind()
    {
        dt = new DataTable();
        dt = clsDDL.ddlZone();
        try
        {
            DropDownListZone.DataSource = dt;
            DropDownListZone.DataTextField = dt.Columns["fGPtbl_MZ_Name"].ToString();
            DropDownListZone.DataValueField = dt.Columns["fGPtbl_MZ_Id"].ToString();
            DropDownListZone.DataBind();
            DropDownListZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
            DropDownListZone.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void DropDownBranchBind(int zoneid)
    {
        dt = new DataTable();
        
        try
        {
            dt=clsDDL.ddlBranch(zoneid);
            //REBINDING WITH NEW VALUE AS 'ALL'
            if (DropDownListRole.SelectedValue == "2")
            {
                DropDownListBranch.Items.Clear();
                DropDownListBranch.DataBind();
                DropDownListBranch.Items.Insert(0, new ListItem("All", "0"));
                DropDownListZone.AutoPostBack = false;
            }
            else
            {

                DropDownListBranch.DataSource = dt;
                DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
                DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
                DropDownListBranch.DataBind();
                DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                DropDownListZone.AutoPostBack = true;
            }
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string encryptpwd;
        int Useridentity=0, outputuser=0, outputtub=0, checkexist;
        objUser = new clsMasterUser();
        
        //password send in encrypted mode
        DecryptEncrypt objencrypt = new DecryptEncrypt();
        RandomKeyGenerator objGen = new RandomKeyGenerator();
        try
        {
            if (Convert.ToInt32(Request.QueryString["act"]) == 1) //TO ADD
            {

                checkexist = CheckZoneBranchHead(Convert.ToInt32(DropDownListRole.SelectedValue), Convert.ToInt32(DropDownListZone.SelectedValue), Convert.ToInt32(DropDownListBranch.SelectedValue));
                if (checkexist == 1)
                {
                    lblMessage.Text = "Zonal head exist!";
                    lblMessage.Focus();
                }
                else if (checkexist == 2)
                {
                    lblMessage.Text = "Branch head exist!";
                }
                else if (checkexist == 4)
                {
                    lblMessage.Text = "Username exists for branch";
                }
                else if (checkexist == 3)
                {

                    //TO GENERATE PASSWORD WITHOUT ENCRYPTION 7/6/2012
                    encryptpwd = objGen.Generate();

                    //TO INSERT INTO DATABASE
                    Mydr = objUser.MasterUser_InsertUpdate(0, txtUserName.Text, encryptpwd, Convert.ToInt32(DropDownListRole.SelectedValue), 0,
                        txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, string.Empty, txtEmailId.Text, txtPhoneNo.Text, txtMobileNo.Text, txtFax.Text, UserId);
                    while (Mydr.Read())
                    {
                        Useridentity = Convert.ToInt32(Mydr["UserId"]);
                        outputuser = Convert.ToInt32(Mydr["Output"]);
                    }
                    Mydr.Close();
                    if (Useridentity == 0 && outputuser == 1)
                    {
                        lblMessage.Text = "User name already exists";
                        lblMessage.Focus();
                    }
                    // message for success or failure
                    else if (Useridentity != 0)
                    {
                        objTub = new clsTrans_UserBranch();
                        outputtub = objTub.Trans_UserBranch_InsertUpdate(0, Useridentity, Convert.ToInt32(DropDownListRole.SelectedValue), Convert.ToInt32(DropDownListZone.SelectedValue), Convert.ToInt32(DropDownListBranch.SelectedValue), UserId);
                        if (outputtub == 3)
                        {
                            Session["userAction"] = "Server is busy!";
                            Response.Redirect("MasterUser.aspx", false);
                        }
                    }
                    else
                    {
                        Session["userAction"] = "Server is busy!";
                        Response.Redirect("MasterUser.aspx", false);
                    }

                    if (outputtub == 3 || Useridentity == 0)
                    {
                        Session["userAction"] = "Server is busy!";
                        Response.Redirect("MasterUser.aspx", false);
                    }
                    else
                    {
                       // sendmailtouser(txtUserName.Text);
                        Session["userAction"] = "Inserted successfully!";
                        Response.Redirect("MasterUser.aspx", false);
                    }
                }
            }
            else
            {
                Mydr = objUser.MasterUser_InsertUpdate(Convert.ToInt32(Request.QueryString["actid"]), lblUserName.Text.Trim(), "0", 0, 0,
                       txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, string.Empty, txtEmailId.Text, txtPhoneNo.Text, txtMobileNo.Text, txtFax.Text, UserId);
                while (Mydr.Read())
                {
                    Useridentity = Convert.ToInt32(Mydr["UserId"]);
                    outputuser = Convert.ToInt32(Mydr["Output"]);
                }
                Mydr.Close();

                if (outputuser == 3)
                {
                    Session["userAction"] = "Server is busy!";
                    Response.Redirect("MasterUser.aspx", false);
                }
                else
                {
                    Session["userAction"] = "Updated successfully!";
                    Response.Redirect("MasterUser.aspx", false);
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
   
    protected void DropDownListZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownBranchBind(Convert.ToInt32(DropDownListZone.SelectedValue));
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterUser.aspx", false);
    }
   
    protected void DropDownListRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        //REBINDING WITH NEW VALUE AS 'ALL'
        if (DropDownListRole.SelectedValue == "2")
        {
            DropDownListBranch.Items.Clear();
            DropDownListBranch.DataBind();
            DropDownListBranch.Items.Insert(0, new ListItem("All", "0"));
            DropDownListZone.AutoPostBack = false;
        }
        else
        {
            DropDownBranchBind(0);
            DropDownListZone.AutoPostBack = true;
        }
    }

    //TO CHECK ZONE-HEAD BRANCH-HEAD EXIST
    public int CheckZoneBranchHead(int RoleId, int ZoneId, int BranchId)
    {
        int check; // 1 FOR ZONAL-HEAD EXIST 2 FOR BRANCH-HEAD EXIST 3 FOR NOT EXIST 4 USER NAME EXIST FOR BRANCH
        Myds = new DataSet();
        objUser = new clsMasterUser();
        if (BranchId == 0)
        {
            //TO CHECK ZONE-HEAD AND BRANCH-HEAD EXIST OR NOT
            
            Myds = objUser.MasterUser_FetchUserByRoleZone_DataSet(RoleId,ZoneId);
            if (Myds.Tables[0].Rows.Count != 0)
            {
                lblMessage.Text = "Zonal head already exists!";
                check=1; //FOR ZONAL-HEAD EXIST
            }
            else
                check=3; //FOR NOT EXIST
        }
        else
        {

            //TO CHECK BRANCH-HEAD FOR THAT ZONE IS EXIST OR NOT
            successId = objUser.MasterUser_FetchUserByRoleZoneBranchId(RoleId, ZoneId, BranchId, txtUserName.Text);
            if (successId==1)
            {
                lblMessage.Text = "Usename already exist for Branch!";
                check=4; //FOR USERNAME EXIST FOR BRANCH
            }
            else if (successId == 3)
            {
                lblMessage.Text = "Branch head already exists!";
                check = 2;
            }
            else
            {
                check = 3; //FOR NOT EXIST
            }
        }

        return check;
    }

    public void sendmailtouser(string UserName)
    {
        objUser = new clsMasterUser();
        objMailer = new clsMasterMailer();
        int id;
        string FromMail, ccMail, bccMail, subject, bodyMail;
        ArrayList arl = new ArrayList();
        Myds = new DataSet();

        try
        {

            arl = objUser.MasterUser_ForgotPassword(UserName);
            id = Convert.ToInt32(arl[0]);

            //FETCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(6);

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +arl[2].ToString() + ".";

            string str = bodyMail.Replace("$$username$$", UserName);
            str = str.Replace("$$url$$", "www.nsic.co.in");
            string bodystr = str.Replace("$$password$$", arl[2].ToString());

            //Password successfully changed
            SendMail.Mail_WithOUTAttachment(arl[1].ToString(), 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void HideShow(int action, int actionid)
    {
        try
        {

            if (action == 1) //FOR ADD NEW USER
            {
                lblRole.Visible = false;
                lblZone.Visible = false;
                lblBranch.Visible = false;
                lblUserName.Visible = false;
                lblFirstName.Visible = false;
                lblMiddleName.Visible = false;
                lblLastName.Visible = false;
               // lblDOB.Visible = false;
                lblEmailId.Visible = false;
                lblPhoneNo.Visible = false;
                lblMobileNo.Visible = false;
                lblFax.Visible = false;

                DropDownListRole.Visible = true;
                DropDownListZone.Visible = true;
                DropDownListBranch.Visible = true;

                       
                txtUserName.Visible = true;
                txtFirstName.Visible = true;
                txtMiddleName.Visible = true;
                txtLastName.Visible = true;
             //   txtDOB.Visible = true;
            //    ltlDOB.Visible = true;
                txtEmailId.Visible = true;
                txtPhoneNo.Visible = true;
                txtMobileNo.Visible = true;
                txtFax.Visible = true;
                btnSubmit.Visible = true;
            }

            else if (action == 2) //FOR EDIT
            {
                lblRole.Visible = true;
                lblZone.Visible = true;
                lblBranch.Visible = true;
                lblUserName.Visible = true;
                lblFirstName.Visible = false;
                lblMiddleName.Visible = false;
                lblLastName.Visible = false;
              //  lblDOB.Visible = false;
                lblEmailId.Visible = false;
                lblPhoneNo.Visible = false;
                lblMobileNo.Visible = false;
                lblFax.Visible = false;

                DropDownListRole.Visible = false;
                DropDownListZone.Visible = false;
                DropDownListBranch.Visible = false;
                txtUserName.Visible = false;
                txtFirstName.Visible = true;
                txtMiddleName.Visible = true;
                txtLastName.Visible = true;
                //txtDOB.Visible = true;
               // ltlDOB.Visible = true;
                txtEmailId.Visible = true;
                txtPhoneNo.Visible = true;
                txtMobileNo.Visible = true;
                txtFax.Visible = true;
                btnSubmit.Visible = true;

                //TO FETCH ROLEID AND USERNAME
                Myds = new DataSet();
                dt = new DataTable();
                objUser = new clsMasterUser();

                dt = objUser.MasterUser_Fetch_DataSet(actionid).Tables[0];


                //TO FETCH ALL DATA

                Myds = objUser.MasterUser_FetchByRoleUserName_DataSet(Convert.ToInt32(dt.Rows[0]["fGPtbl_MU_RoleId"].ToString()), dt.Rows[0]["fGPtbl_MU_Name"].ToString());

                lblRole.Text = Myds.Tables[0].Rows[0]["RoleName"].ToString();
                lblZone.Text = Myds.Tables[0].Rows[0]["ZoneName"].ToString();
                lblBranch.Text = Myds.Tables[0].Rows[0]["BranchName"].ToString();
                lblUserName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString();
                txtFirstName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_FName"].ToString();
                txtMiddleName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_MName"].ToString();
                txtLastName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_LName"].ToString();
              //  txtDOB.Text = Myds.Tables[0].Rows[0]["DOB"].ToString();
                txtEmailId.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                txtPhoneNo.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Phone"].ToString();
                txtMobileNo.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Mobile"].ToString();
                txtFax.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Fax"].ToString();

            }

            else if (action == 3)  //VIEW
            {
                lblRole.Visible = true;
                lblZone.Visible = true;
                lblBranch.Visible = true;
                lblUserName.Visible = true;
                lblFirstName.Visible = true;
                lblMiddleName.Visible = true;
                lblLastName.Visible = true;
               // lblDOB.Visible = true;
                lblEmailId.Visible = true;
                lblPhoneNo.Visible = true;
                lblMobileNo.Visible = true;
                lblFax.Visible = true;

                DropDownListRole.Visible = false;
                DropDownListZone.Visible = false;
                DropDownListBranch.Visible = false;
                txtUserName.Visible = false;
                txtFirstName.Visible = false;
                txtMiddleName.Visible = false;
                txtLastName.Visible = false;
               // txtDOB.Visible = false;
               // ltlDOB.Visible = false;
                txtEmailId.Visible = false;
                txtPhoneNo.Visible = false;
                txtMobileNo.Visible = false;
                txtFax.Visible = false;
                btnSubmit.Visible = false;

                //TO FETCH ROLEID AND USERNAME
                Myds = new DataSet();
                dt = new DataTable();
                objUser = new clsMasterUser();

                dt = objUser.MasterUser_Fetch_DataSet(actionid).Tables[0];
                

                //TO FETCH ALL DATA

                Myds = objUser.MasterUser_FetchByRoleUserName_DataSet(Convert.ToInt32(dt.Rows[0]["fGPtbl_MU_RoleId"].ToString()), dt.Rows[0]["fGPtbl_MU_Name"].ToString());
                DecryptEncrypt obj = new DecryptEncrypt();
                lblRole.Text = Myds.Tables[0].Rows[0]["RoleName"].ToString();
                lblZone.Text = Myds.Tables[0].Rows[0]["ZoneName"].ToString();
                lblBranch.Text = Myds.Tables[0].Rows[0]["BranchName"].ToString();
                lblUserName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString();
                lblFirstName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_FName"].ToString();
                lblMiddleName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_MName"].ToString();
                lblLastName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_LName"].ToString();
             //   lblDOB.Text = Myds.Tables[0].Rows[0]["DOB"].ToString();
                lblEmailId.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                lblPhoneNo.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Phone"].ToString();
                lblMobileNo.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Mobile"].ToString();
                lblFax.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Fax"].ToString();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }


}
