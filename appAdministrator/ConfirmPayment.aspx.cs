using System;
using System.Data.SqlClient ;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_ConfirmPayment : System.Web.UI.Page
{
    clsCompany objcompany;
    clsDDL objddl;
    clsAmendment objAmendment;
   // DataSet Myds;
    int accessId;
    int count = 1;

    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;

    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int monetaryAction = 0;
    int successId, urlId, amendmentid = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

           // if (RoleId == 4)
           // {
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
            //}

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
    public void GridViewDataBind()
    {
        try
        {
            if (urlId == 1)
            {
                objcompany = new clsCompany();

                gvUnconfirmedCompanies.DataSource = objcompany.Company_UnconfirmedTransaction_Fetch_Dataset(UserId,1);
                gvUnconfirmedCompanies.DataBind();
            }
            else if (urlId == 2)
            {
                objcompany = new clsCompany();

                gvUnconfirmedCompanies.DataSource = objcompany.Company_UnconfirmedTransaction_Fetch_Dataset(UserId, 2);
                gvUnconfirmedCompanies.DataBind();
            }
            else if (urlId == 3)
            {
                objcompany = new clsCompany();

                gvUnconfirmedCompanies.DataSource = objcompany.Company_UnconfirmedTransaction_Fetch_Dataset(UserId, 3);
                gvUnconfirmedCompanies.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

       }

    protected void gvUnconfirmedCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex > 0)
       // {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblindx = (Label)e.Row.FindControl("lblsn");
            lblindx.Text = (e.Row.RowIndex + 1).ToString();
        }
        //}
    }
    protected void gvUnconfirmedCompanies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "trans")
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptPayid = objDecEnc.Encrypt(e.CommandArgument.ToString());
            string EncryptUrlid = objDecEnc.Encrypt(Request.QueryString["i"].ToString());         
            Response.Redirect("ConfirmPaymentDetails.aspx?pd=" + EncryptPayid.ToString() +"&i="+EncryptUrlid.ToString(), false); 
        }

    }
}
