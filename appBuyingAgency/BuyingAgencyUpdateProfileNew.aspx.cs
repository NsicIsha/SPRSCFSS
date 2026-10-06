using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;

public partial class appBuyingAgency_BuyingAgencyUpdateProfileNew :  System.Web.UI.Page
{
    clsMasterInspectionAgency objAgency;
    DataTable Mydt;
    DataSet Myds;

    int UserId, RoleId;
    string UserFullName;
    string Email = string.Empty;
    string Remark = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());

            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();


            txtEmailId.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txt_remark.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");


            if (Page.IsPostBack != true)
            {
                objAgency = new clsMasterInspectionAgency();
                Myds = new DataSet();
                Mydt = new DataTable();
                DecryptEncrypt objDecrypt = new DecryptEncrypt();

                //TO FETCH ROLE-ID AND USERNAME
                Myds = objAgency.MasterBuyingAgency_Fetch_DataSet(UserId);
                //TO STORE PASSWORD
                Mydt = Myds.Tables[0];


                if (Mydt.Rows.Count > 0)
                {
                    txtEmailId.Text = Mydt.Rows[0]["Email"].ToString();
                    txt_remark.Text = Mydt.Rows[0]["Remark"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;

            UserFullName = "";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("BuyingAgencySearchGem.aspx", false);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objAgency = new clsMasterInspectionAgency();
            Email = txtEmailId.Text.ToString();
            Remark = txt_remark.Text.ToString();
            int j = objAgency.MasterBuyingAgency_UpdateProfile(UserId, Email, Remark);
            if (j == 1)
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
}