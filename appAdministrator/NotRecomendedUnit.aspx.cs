using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
public partial class appAdministrator_NotRecomendedUnit : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int monetaryAction = 0;
    int successId, urlId, amendmentid = 0;
    string ToMail = string.Empty;
    string FromMail = string.Empty;
    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    string UserEmailID = string.Empty;

    Hashtable hashtbl = new Hashtable();

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 4)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
                }

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvMonetary.SetActiveView(ViewMonetaryDetail);
                    GridViewDataBind();
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
            ErrHandler.WriteError("Error in Page load() " + ex.StackTrace);
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }



    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        try
        {
            if (urlId == 1)
            {
                objCompany = new clsCompany();

                gvMonetaryDetails.DataSource = Company_NotRecomended_Fetch_Dataset(UserId);
                gvMonetaryDetails.DataBind();
            }
            else if (urlId == 2)
            {
                clsRenewal objrenewal = new clsRenewal();
                gvMonetaryDetails.DataSource = objrenewal.Company_MonetaryLimit_Fetch_Datasetrenew(UserId);
                gvMonetaryDetails.DataBind();
            }
            else if (urlId == 3)
            {
                objAmendment = new clsAmendment();

                gvMonetaryDetails.DataSource = objAmendment.CompanyAmendment_MonetaryLimit_Fetch_Dataset(UserId);
                gvMonetaryDetails.DataBind();
            }

            if (gvMonetaryDetails.Rows.Count == 0)
            {
                mvMonetary.SetActiveView(ViewMonetaryNotFound);
                lblMessage.Text = "No Monetary limit to be issued!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["monetaryAction"] != null)
            {
                LabelAction.Text = Session["monetaryAction"].ToString();
                monetaryAction = 1;
                Session["monetaryAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                monetaryAction = 0;
                Session["monetaryAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["monetaryAction"] = null;
        }
    }


    public DataSet Company_NotRecomended_Fetch_Dataset(int UserId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_NotRecomended_Registration_Fetch_All", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;

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

    public DataSet Company_NotRecomended_Fetch_DatasetIndividualunit(int UnitId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_NotRecomended_Registration_Fetch_IndividualUnit", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = UnitId;

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
    //AVOPS1570Q
    protected void gvMonetaryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName== "Close")
        {
            int Unitid =Convert.ToInt32(e.CommandArgument);
            DataSet ds = new DataSet();
            ds = Company_NotRecomended_Fetch_DatasetIndividualunit(Unitid);

            tableRemarks.Visible = true;
            lblCompanyName.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            lblPAN.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
            lblNsicFee.Text = ds.Tables[0].Rows[0]["fGPtbl_PD_UnitAmount"].ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        tableRemarks.Visible = false;
    }

    protected void btnSubmitUpload_Click(object sender, EventArgs e)
    {

    }
}