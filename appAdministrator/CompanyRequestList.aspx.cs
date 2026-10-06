using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;
public partial class appAdministrator_CompanyRequestList : System.Web.UI.Page
{
    clsCompany objCompany;
    int successId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int requestAction = 0;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    int ErrorId;

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
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            FunRequest();

            //fgfg

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvCertRequest.SetActiveView(ViewCertRequestDetail);
                    GridViewDataBind();
              //  Company_EditRequest_Fetch(UserId);

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

    //TO FETCH DATA OF CERTIFICATE REQUEST
    public DataTable Company_EditRequest_Fetch(int userId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_CompanyDetails_For_Edit", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        objCompany = new clsCompany();
        try
        {
            gvCertRequestDetails.DataSource = Company_EditRequest_Fetch(UserId);
            gvCertRequestDetails.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        if (gvCertRequestDetails.Rows.Count == 0)
        {
            mvCertRequest.SetActiveView(ViewCertRequestNotFound);
            lblMessage.Text = "No Certificate request found!";
        }
        try
        {
            if (Session["requestAction"] != null)
            {
                LabelAction.Text = Session["requestAction"].ToString();
                requestAction = 1;
                Session["requestAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                requestAction = 0;
                Session["requestAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["requestAction"] = null;
        }
    }


    protected void gvCertRequestDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objCompany = new clsCompany();

            if (e.CommandName == "view")//TO VIEW BY ADMIN
            {
                Response.Redirect("CertificateRequestList_view.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "accept")
            {
                //TO COLLECT TWO COMMAND ARGUMENT INTO STRING
                string cmd = e.CommandArgument.ToString();
                string[] cmdargument = cmd.Split(',');

                //INSERTED INTO CERTIFICATE REQUEST TABLE
                successId = objCompany.Company_CertificateRequest_Insert(Convert.ToInt32(cmdargument[0]), Convert.ToInt32(cmdargument[1]), "0", "0", 3, UserId);

                if (successId == 4)
                {
                    Session["requestAction"] = "Successfully updated";
                }
                else if (successId == 3)
                {
                    Session["requestAction"] = "Server is busy";
                }
                else
                {
                    Session["requestAction"] = "Server is busy";
                }
            }

            else if (e.CommandName == "acceptreject")//REJECTED BY ADMIN
            {
                string cmd = e.CommandArgument.ToString();
                string[] cmdargument = cmd.Split(',');

                //CAPTURING ID
                HiddenCompanyId.Value = cmdargument[0];//COMPANY ID
                HiddenCompanyName.Value = cmdargument[1];//COMPANY NAME
                ltlCertRequest.Text = cmdargument[1];//
                lblAddEdit.Text = "Accept/Reject Request";
                ModalPopupExtender1.Show();
            }


            //REBINDING GRIDVIEW
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public int Company_UpdateRequest(int CompanyId,int ActionId,   string Reason, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_CompanyDetailsEditRequest_Update", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@ActionId", SqlDbType.Int).Value = ActionId;
            MySqlComm.Parameters.Add("@Reason", SqlDbType.VarChar, 5000).Value = Reason;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            
           
            ErrorId = Convert.ToInt32(MySqlComm.ExecuteNonQuery());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }
        return ErrorId;
    }

    //TO SUBMIT DETAILS IN CERTIFICATE REQUEST TABLE
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            successId = Company_UpdateRequest(Convert.ToInt32(HiddenCompanyId.Value),Convert.ToInt32(rdbAction.SelectedValue),txtReason.Text,UserId);

            if (successId == 4)
            {
                Session["requestAction"] = "Updated successfully";
            }
            else if (successId == 3)
            {
                Session["requestAction"] = "Server is busy";
            }
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
}