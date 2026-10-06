using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_UpdateProfile : System.Web.UI.Page
{


    DataTable Mydt;
    DataSet Myds;
    SqlDataReader Mydr;
    clsMasterUser objUser;
    int UserId, RoleId, DepartmentId;
    string UserFullName, Userpwd;



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

            txtFirstName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtMiddleName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtLastName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            //txtDOB.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtEmailId.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtPhoneNo.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtMobileNo.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtFax.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (Page.IsPostBack != true)
            {
                DropDownDesigBind();
                objUser = new clsMasterUser();
                Myds = new DataSet();
                Mydt = new DataTable();
                DecryptEncrypt objDecrypt = new DecryptEncrypt();

                //TO FETCH ROLE-ID AND USERNAME
                Myds = objUser.MasterUser_Fetch_DataSet(UserId);

                //TO STORE PASSWORD
                Userpwd=Myds.Tables[0].Rows[0]["fGPtbl_MU_Pwd"].ToString();

                //TO FETCH ALL DATA RELATED TO USER
                Mydt = objUser.MasterUser_FetchByRoleUserName_DataSet(Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MU_RoleId"]), Myds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString()).Tables[0];
                int Role_id_user = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MU_RoleId"]);

                if (Role_id_user != 1 && Role_id_user != 5)
                {//------------1
                    if (Mydt.Rows.Count > 0)
                    {
                        //if (Role_id_user != 1)
                        //{
                        //    lblRole.Text = Mydt.Rows[0]["RoleName"].ToString();
                        //    lblZone.Text = Mydt.Rows[0]["ZoneName"].ToString();
                        //    lblBranch.Text = Mydt.Rows[0]["BranchName"].ToString();
                        //}
                        lblRole.Text = Mydt.Rows[0]["RoleName"].ToString();
                        lblZone.Text = Mydt.Rows[0]["ZoneName"].ToString();
                        lblBranch.Text = Mydt.Rows[0]["BranchName"].ToString();
                        lblUserName.Text = Mydt.Rows[0]["fGPtbl_MU_Name"].ToString();
                        txtFirstName.Text = Mydt.Rows[0]["fGPtbl_MU_FName"].ToString();
                        txtMiddleName.Text = Mydt.Rows[0]["fGPtbl_MU_MName"].ToString();
                        txtLastName.Text = Mydt.Rows[0]["fGPtbl_MU_LName"].ToString();
                        //txtDOB.Text = Mydt.Rows[0]["DOB"].ToString();
                        txtEmailId.Text = Mydt.Rows[0]["fGPtbl_MU_Email"].ToString();
                        txtPhoneNo.Text = Mydt.Rows[0]["fGPtbl_MU_Phone"].ToString();
                        txtMobileNo.Text = Mydt.Rows[0]["fGPtbl_MU_Mobile"].ToString();
                        txtFax.Text = Mydt.Rows[0]["fGPtbl_MU_Fax"].ToString();
                        ddldept.SelectedValue = Mydt.Rows[0]["fGPtbl_MU_DepartmentId"].ToString();
                    }
                }//-----1
                else if (Role_id_user == 1 ||  Role_id_user==5)
                {//-------2
                    if (Myds.Tables[0].Rows.Count > 0)
                    {
                        if (Myds.Tables[0].Rows[0]["RoleName"].ToString() != "")
                        {
                            lblRole.Text = Myds.Tables[0].Rows[0]["RoleName"].ToString();
                        }

                        lblUserName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString();
                        txtFirstName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_FName"].ToString();
                        txtMiddleName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_MName"].ToString();
                        txtLastName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_LName"].ToString();
                        //txtDOB.Text = Myds.Tables[0].Rows[0]["DOB"].ToString();
                        txtEmailId.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                        txtPhoneNo.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Phone"].ToString();
                        txtMobileNo.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Mobile"].ToString();
                        txtFax.Text = Myds.Tables[0].Rows[0]["fGPtbl_MU_Fax"].ToString();
                        //ddldept.SelectedValue = Mydt.Rows[0]["fGPtbl_MU_DepartmentId"].ToString();
                        if (Myds.Tables[0].Rows[0]["fGPtbl_MU_DepartmentId"].ToString() != "" || Myds.Tables[0].Rows[0]["fGPtbl_MU_DepartmentId"].ToString() != "0")
                        {
                            ddldept.SelectedValue = Myds.Tables[0].Rows[0]["fGPtbl_MU_DepartmentId"].ToString();
                        }
                    }
                }//-----2
                
            }//-----POSTBACK
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }

    }

    private void DropDownDesigBind()
    {
        DataTable mydt = new DataTable();
        objUser = new clsMasterUser();
        mydt = objUser.Master_FETCH_Department(RoleId  );
        ddldept.DataSource = mydt;
        ddldept.DataBind();
        ddldept.Items.Insert(0, new ListItem("--Select Department--", "0"));
     
        
    }   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objUser = new clsMasterUser();
            int success = 3;

            Mydr = objUser.MasterUser_InsertUpdate(UserId, lblUserName.Text, "0", 0, Convert.ToInt32(ddldept.SelectedValue),
                    txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, string.Empty, txtEmailId.Text,
                    txtPhoneNo.Text, txtMobileNo.Text, txtFax.Text, UserId);
            while (Mydr.Read())
            {
                success = Convert.ToInt32(Mydr["Output"]);
            }
            Mydr.Close();

            if (success == 4)
            {

                lblMessage.Text = "Profile updated successfully";
                lblMessage.Focus();
            }
            else
            {
                lblMessage.Text = "Server is busy please try later";
                lblMessage.Focus();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx", false);
    }
}
