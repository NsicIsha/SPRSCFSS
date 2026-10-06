using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.Globalization;

public partial class appAdministrator_ICRegistration : System.Web.UI.Page
{

    clsCompany objCompany;
    clsAmendment objAmendment;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int urlid = 0;
    public int icregAction = 0;
    int Companyid = 0;
    int UnitId = 0;

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

            if (RoleId == 8)
            {
                if (Request.QueryString["urlid"] != null)
                {
                    urlid = Convert.ToInt32(Request.QueryString["urlid"].ToString());
                }
                if (Page.IsPostBack != true)
                {
                    //FILLING GRIDVIEW
                    if (urlid == 1)
                    {
                        GridViewDataBind(1);
                    }
                    else if (urlid == 3)
                    {
                        lblRegistration.Text = "Inspection Complete Amendment";

                        GridViewDataBind(3);
                    }
                    
                }
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


    //BINDING GRIDVIEW
    public void GridViewDataBind(int formtype)
    {
        try
        {
            objCompany = new clsCompany();
            objAmendment = new clsAmendment();
            if (formtype == 1)
            {
                gvICRegistration.DataSource = objCompany.Company_ICompleteRegistrationCompany_Dataset(UserId);
                gvICRegistration.DataBind();
            }
            else if (formtype == 3)
            {
                gvICRegistration.DataSource = objAmendment.CompanyAmendment_ICompleteAmendment_Dataset(UserId);
                gvICRegistration.DataBind();
            }
            if (gvICRegistration.Rows.Count == 0)
            {
                lblMessage.Text = "No company found!";
            }
            else
                lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["icregAction"] != null)
            {
                LabelAction.Text = Session["icregAction"].ToString();
                icregAction = 1;
                Session["icregAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                icregAction = 0;
                Session["icregAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["icregAction"] = null;
        }
    }

    protected void gvICRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnaccept = (Button)e.Row.FindControl("btnAccept");
                divReject.Visible = false;
                if (urlid == 1)
                {
                    btnaccept.PostBackUrl = "ICRegistration_View.aspx?id=" + btnaccept.CommandArgument + "&urlid=1";
                }
                else if (urlid == 3)
                {
                    btnaccept.PostBackUrl = "ICRegistration_View.aspx?id=" + btnaccept.CommandArgument + "&urlid=3";
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    SqlCommand MySqlComm;
    public int InspectionReject(int Companyid, int UnitId, int formtype, string reason, DateTime IADate)
    {
        int ErrorId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_InspectionReject_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CUnitId", SqlDbType.Int).Value = UnitId;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@Reason", SqlDbType.VarChar, 5000).Value = reason;
            MySqlComm.Parameters.Add("@IAdate", SqlDbType.DateTime).Value = IADate;
            MySqlComm.Parameters.Add("@FormType", SqlDbType.Int).Value = formtype;
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
    protected void btnReject_Command(object sender, CommandEventArgs e)
    {
        divReject.Visible = true;

        string[] company = e.CommandArgument.ToString().Split(',');
        hdCMY.Value = company[1];
        hdCUI.Value = company[0];        
    }

    protected void btnInnerReject_Click(object sender, EventArgs e)
    {
        DateTime parsed;
        bool valid = DateTime.TryParseExact(txtDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed);
        if (!valid)
        {
            lblMessage.Text = "Please Date format.It should be i mm/dd/yyyy!!";
        }
        else
        {
            lblMessage.Text = "";
        }
        if (Convert.ToInt32(hdCMY.Value) != 0 && Convert.ToInt32(hdCUI.Value) != 0 && !string.IsNullOrEmpty(txtReason.Text) && !string.IsNullOrEmpty(txtDate.Text) && valid)
        {
            int successId = InspectionReject(Convert.ToInt32(hdCMY.Value), Convert.ToInt32(hdCUI.Value), urlid, txtReason.Text, Convert.ToDateTime(txtDate.Text));

            if (successId == 2)
            {
                divReject.Visible = false;
                if (urlid == 1)
                {
                    Response.Redirect("~/appAdministrator/ICRegistration.aspx?urlid=1", false);
                }
            }
        }
    }
}
