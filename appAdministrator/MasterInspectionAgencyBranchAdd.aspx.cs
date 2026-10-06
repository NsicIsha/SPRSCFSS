using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

using System.Data.SqlClient;

public partial class appAdministrator_MasterInspectionAgencyBranchAdd : System.Web.UI.Page
{
    DataSet Myds;
    clsMasterZone objZone;
    clsMasterBranch objBranch;
    clsMasterInspectionAgency objInspbr;
    clsTrans_UserBranch objTUB;
    
    int UserId, RoleId, DepartmentId;
    string UserFullName, branchcode,InspAgency, usrname;
    int zoneid, branchid,successId;
    //DecryptEncrypt obj=new DecryptEncrypt();


    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

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
        //txtInstr.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        

         //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            FunRequest();
            if (RoleId == 1 || RoleId == 2 || RoleId == 3)
            {
                if (Page.IsPostBack != true)
                {
                    if (Request.QueryString["act"].ToString() == "axz")
                    {
                        showhide(1);
                    }
                    else
                    {
                        showhide(2);
                    }
                    if (RoleId == 1)
                    {
                        DropDownZoneBind(DropDownListZone);
                        DropDownBranchBind();
                        DropDownInspBind();
                    }
                    else if (RoleId == 2) //AS ZONAL HEAD
                    {
                        DropDownListZone.Visible = false;
                        lblZone.Visible = true;

                        ZoneHead();
                    }
                    else if (RoleId == 3) //AS BRANCH HEAD
                    {
                        DropDownListZone.Visible = false;
                        DropDownListBranch.Visible = false;
                        lblZone.Visible = true;
                        lblBranch.Visible = true;

                        BranchHead();
                    }
                }
            }
            else
            {
                DropDownListZone.Visible = false;
                DropDownListBranch.Visible = false;
                DropDownListInsp.Visible = false;
                txtInstr.Visible = false;
                btnSubmit.Visible = false;
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

    public void DropDownZoneBind(DropDownList ddl)
    {
        try
        {
            Myds = new DataSet();
            objZone = new clsMasterZone();
            Myds = objZone.MasterZone_Fetch_DataSet();

            ddl.DataTextField = Myds.Tables[0].Columns[1].Caption;
            ddl.DataValueField = Myds.Tables[0].Columns[0].Caption;
            ddl.DataSource = Myds.Tables[0];
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select Zone--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void DropDownBranchBind()
    {
        try
        {
            Myds = new DataSet();
            objBranch = new clsMasterBranch();
            objTUB = new clsTrans_UserBranch();

            Myds = objBranch.MasterBranch_Fetch_DataSet();

            DropDownListBranch.DataSource = Myds.Tables[0];
            DropDownListBranch.DataTextField = Myds.Tables[0].Columns[4].Caption;
            DropDownListBranch.DataValueField = Myds.Tables[0].Columns[0].Caption;
            DropDownListBranch.DataBind();
            DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void DropDownInspBind()
    {
        DataSet MydsInsp = new DataSet();
        try
        {
            objInspbr = new clsMasterInspectionAgency();
           

            MydsInsp = objInspbr.MasterInspectionAgency_Fetch_DataSet();

            DropDownListInsp.DataSource = MydsInsp.Tables[0];
            DropDownListInsp.DataTextField = MydsInsp.Tables[0].Columns[1].Caption;
            DropDownListInsp.DataValueField = MydsInsp.Tables[0].Columns[0].Caption;
            DropDownListInsp.DataBind();
            DropDownListInsp.Items.Insert(0, new ListItem("--Select agency--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MydsInsp.Dispose();
        }
    }

    protected void DropDownListZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objBranch = new clsMasterBranch();
            Myds = new DataSet();

            Myds = objBranch.MasterBranch_FetchByZoneId_DataSet(Convert.ToInt32(DropDownListZone.SelectedValue));

            DropDownListBranch.DataSource = Myds.Tables[0];
            DropDownListBranch.DataTextField = Myds.Tables[0].Columns[4].Caption;
            DropDownListBranch.DataValueField = Myds.Tables[0].Columns[0].Caption;
            DropDownListBranch.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int id=0;
        string Decryptpwd = string.Empty;
        string FromMail, ccMail, bccMail, subject, bodyMail;
        clsMasterMailer objMailer = new clsMasterMailer();
        try
        {
            objInspbr = new clsMasterInspectionAgency();
            objBranch = new clsMasterBranch();
            Myds = new DataSet();
            objTUB = new clsTrans_UserBranch();

            Myds = objMailer.MasterMailer_Fetch_DataSet(6);

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString() + "   " + Decryptpwd + ".";

           /* if (RoleId == 2)
            {
                Myds = objTUB.Trans_UserBranch_FetchZoneId_DataSet(UserId);
                zoneid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MZ_Id"]);

               /* Myds = objBranch.MasterBranch_Fetch_DataSet(Convert.ToInt32(DropDownListBranch.SelectedValue));
                branchcode = Myds.Tables[0].Rows[0]["fGPtbl_MB_Code"].ToString();
                InspAgency = DropDownListInsp.SelectedItem.ToString().Substring(0, 4).Trim();
                usrname = InspAgency + branchcode;

                successId = objInspbr.Trans_InspectionagecnyBranch_InsertUpdate(0, Convert.ToInt32(DropDownListInsp.SelectedValue),
                    zoneid, Convert.ToInt32(DropDownListBranch.SelectedValue), txtInstr.Text, Decryptpwd, txtEmail.Text, UserId);
            }
            else*/ if (RoleId == 3)
            {
                Myds = objTUB.Trans_UserBranch_FetchZoneId_DataSet(UserId);
                zoneid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
                branchid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MB_Id"]);

                if (Request.QueryString["act"].ToString() != "axz")
                { //FOR UPDATING EXISTING INSPECTION AGENCY USER
                    int inspid = Convert.ToInt32(Request.QueryString["act"]);
                    // successId = objInspbr.Trans_InspectionagecnyBranch_InsertUpdate(inspid, Convert.ToInt32(DropDownListInsp.SelectedValue), zoneid, branchid,
                    //txtUserid.Text.Trim(), txtPassword.Text.Trim(), txtAddress.Text.Trim(), txtInstr.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), rblservicetax.SelectedValue, UserId,txtIAN_Location.Text.Trim());


                    successId = Trans_InspectionagecnyBranch_InsertUpdate(inspid, Convert.ToInt32(DropDownListInsp.SelectedValue), zoneid, branchid,
                       txtUserid.Text.Trim(), txtPassword.Text.Trim(), txtAddress.Text.Trim(), txtInstr.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), rblservicetax.SelectedValue, UserId, txtIAN_Location.Text.Trim());

                }
                else  //FOR ADDING NEW INSPECTION AGENCY USER
                {
                    //successId = objInspbr.Trans_InspectionagecnyBranch_InsertUpdate(0, Convert.ToInt32(DropDownListInsp.SelectedValue), zoneid, branchid,
                    //    txtUserid.Text.Trim(), txtPassword.Text.Trim(), txtAddress.Text.Trim(), txtInstr.Text.Trim(), txtEmail.Text, txtPhone.Text.Trim(), Convert.ToString(rblservicetax.SelectedValue), UserId, txtIAN_Location.Text.Trim());

                    successId = Trans_InspectionagecnyBranch_InsertUpdate(0, Convert.ToInt32(DropDownListInsp.SelectedValue), zoneid, branchid,
                            txtUserid.Text.Trim(), txtPassword.Text.Trim(), txtAddress.Text.Trim(), txtInstr.Text.Trim(), txtEmail.Text, txtPhone.Text.Trim(), Convert.ToString(rblservicetax.SelectedValue), UserId, txtIAN_Location.Text.Trim());

                }
            }
          /*  else if (RoleId == 1)
            {
                successId = objInspbr.Trans_InspectionagecnyBranch_InsertUpdate(0, Convert.ToInt32(DropDownListInsp.SelectedValue),
                    Convert.ToInt32(DropDownListZone.SelectedValue), Convert.ToInt32(DropDownListBranch.SelectedValue), txtInstr.Text, Decryptpwd, txtEmail.Text, UserId);
            }*/
            switch (successId)
            {
                case 1://data already exist
                    Session["inspbrAction"] = "User Name already exist!";
                    break;
                case 2://data inserted successfully
                    Session["inspbrAction"] = "Added successfully!";
                    break;
                case 3:// error in operation
                    Session["inspbrAction"] = "Server is busy, please try later!";
                    break;
                case 4://data updated successfully
                    Session["inspbrAction"] = "Updated successfully!";
                    break;
                default: //error in operation
                    Session["inspbrAction"] = "Server is busy, please try later!";
                    break;
            }

            Response.Redirect("MasterInspectionAgencyBranch.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    //TO INSERT OR UPDATE TABLE
    public int Trans_InspectionagecnyBranch_InsertUpdate(int Inspbrid, int fGPtbl_TIAB_AgencyId, int fGPtbl_TIAB_ZoneId, int fGPtbl_TIAB_BranchId,
                string username, string passwd, string address, string fGPtbl_TIAB_Instructions, string fGPtbl_TIAB_EmailId, string IAphone, string servicetax, int UserId, string InspectionAgencyName_Location)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_TransInspectionagencyBranch", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_TIAB_Id", SqlDbType.Int).Value = Inspbrid;
            MySqlComm.Parameters.Add("@fGPtbl_TIAB_AgencyId", SqlDbType.Int).Value = fGPtbl_TIAB_AgencyId;
            MySqlComm.Parameters.Add("@fGPtbl_TIAB_ZoneId", SqlDbType.Int).Value = fGPtbl_TIAB_ZoneId;
            MySqlComm.Parameters.Add("@fGPtbl_TIAB_BranchId", SqlDbType.Int).Value = fGPtbl_TIAB_BranchId;
            MySqlComm.Parameters.Add("@fGPtbl_TIAB_UserName", SqlDbType.VarChar, 200).Value = username;


            MySqlComm.Parameters.Add("@fGPtbl_TIAB_Name", SqlDbType.VarChar, 200).Value = InspectionAgencyName_Location;

            MySqlComm.Parameters.Add("@fGPtbl_TIAB_Pwd", SqlDbType.NVarChar, 200).Value = passwd;
            if (address.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_TIAB_Address", SqlDbType.VarChar, 2000).Value = address;
            if (fGPtbl_TIAB_Instructions.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_TIAB_Instructions", SqlDbType.VarChar, 5000).Value = fGPtbl_TIAB_Instructions;
            if (fGPtbl_TIAB_EmailId.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_TIAB_EmailId", SqlDbType.VarChar, 500).Value = fGPtbl_TIAB_EmailId;
            if (IAphone != "") MySqlComm.Parameters.Add("@Phone", SqlDbType.VarChar, 50).Value = IAphone;
            MySqlComm.Parameters.Add("@Servicetax", SqlDbType.VarChar, 5).Value = servicetax;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }



    //SHOWING ZONAL HEAD AND BRANCH HEAD IN LABELS
    public void BranchHead()
    {
        try
        {
            Myds = new DataSet();
            objTUB = new clsTrans_UserBranch();

            Myds = objTUB.Trans_UserBranch_FetchZoneId_DataSet(UserId);
            lblZone.Text = Myds.Tables[0].Rows[0]["ZoneName"].ToString();
            lblBranch.Text = Myds.Tables[0].Rows[0]["BranchName"].ToString();

            zoneid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
            branchid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MB_Id"]);

            DropDownInspBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //TO SHOW ZONAL HEAD IN LABEL AND BRANCH IN DDL
    public void ZoneHead()
    {
        try
        {
            
            Myds = new DataSet();
            objBranch = new clsMasterBranch();
            objTUB = new clsTrans_UserBranch();

            Myds = objTUB.Trans_UserBranch_FetchZoneId_DataSet(UserId);
            lblZone.Text = Myds.Tables[0].Rows[0]["ZoneName"].ToString();
            zoneid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MZ_Id"]);

            Myds = objBranch.MasterBranch_FetchByZoneId_DataSet(zoneid);

            DropDownListBranch.DataSource = Myds.Tables[0];
            DropDownListBranch.DataTextField = Myds.Tables[0].Columns[4].Caption;
            DropDownListBranch.DataValueField = Myds.Tables[0].Columns[0].Caption;
            DropDownListBranch.DataBind();

            DropDownInspBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterInspectionAgencyBranch.aspx", false);
    }

    public void showhide(int showid)
    {
        objInspbr = new clsMasterInspectionAgency();
        Myds = new DataSet();
        try
        {
            if (showid == 1)//TO SHOW IN ADD MODE
            {
                lblZone.Visible = false;
                lblBranch.Visible = false;
                lblInspagency.Visible = false;
                lblInstr.Visible = false;
                lblEmail.Visible = false;

                DropDownListZone.Visible = true;
                DropDownListBranch.Visible = true;
                DropDownListInsp.Visible = true;
                txtInstr.Visible = true;
                txtEmail.Visible = true;
            }
            else if (showid == 2) //TO SHOW IN VIEW MODE
            {
                lblZone.Visible = true;
                lblBranch.Visible = true;
                lblInspagency.Visible = true;
                lblInstr.Visible = true;
                lblEmail.Visible = true;
                txtUserid.Enabled = false;

              //  txtIAN_Location.Enabled = false;

                DropDownListZone.Visible = false;
                DropDownListBranch.Visible = false;
                DropDownListInsp.Enabled = false;
              /*  txtInstr.Visible = false;
                txtEmail.Visible = false;
                btnSubmit.Visible = false;*/


                Myds = Trans_InspectionagecnyBranch_Fetch_DataSet(Convert.ToInt32(Request.QueryString["act"].ToString()));
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    lblZone.Text = Myds.Tables[0].Rows[0]["ZoneName"].ToString();
                    lblBranch.Text = Myds.Tables[0].Rows[0]["BranchName"].ToString();
                    DropDownInspBind();
                    DropDownListInsp.SelectedValue = Convert.ToString(Myds.Tables[0].Rows[0]["fGPtbl_TIAB_AgencyId"]);
                    //lblInspagency.Text = Myds.Tables[0].Rows[0]["AgencyName"].ToString();
                    txtUserid.Text = Myds.Tables[0].Rows[0]["fGPtbl_TIAB_UserName"].ToString();
                    txtPassword.Text = Myds.Tables[0].Rows[0]["password"].ToString();
                    txtAddress.Text = Myds.Tables[0].Rows[0]["address"].ToString();
                    txtInstr.Text = Myds.Tables[0].Rows[0]["fGPtbl_TIAB_Instructions"].ToString();
                    txtEmail.Text = Myds.Tables[0].Rows[0]["fGPtbl_TIAB_EmailId"].ToString();
                    txtPhone.Text = Myds.Tables[0].Rows[0]["AgencyPhone"].ToString();
                    rblservicetax.SelectedValue = Myds.Tables[0].Rows[0]["servicetax"].ToString();

                    txtIAN_Location.Text= Myds.Tables[0].Rows[0]["fGPtbl_TIAB_Name"].ToString(); 
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }



    // TO FETCH SELECTIVE DATA FROM TABLE USING ID
    public DataSet Trans_InspectionagecnyBranch_Fetch_DataSet(int TransInspAgenBranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TransInspectionagencyBranch_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TIAB_Id", SqlDbType.Int).Value = TransInspAgenBranchId;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

}
