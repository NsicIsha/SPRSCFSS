using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Provisional_MonetaryLimit : System.Web.UI.Page
{
    public static DataTable Table = new DataTable();
    int UserId, RoleId, DepartmentId;
    string UserFullName;


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
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            int companyid;
            companyid = Convert.ToInt32(Request.QueryString["id"]);

             int isprovisional;
             clsCompany objCompany = new clsCompany();
             isprovisional = objCompany.Company_Provisional_Status(companyid);
               if (isprovisional == 0 )
               {
                   Response.Redirect("Home.aspx");
               }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;

        }
        if (RoleId == 4 || RoleId == 1)
        {

        }
        else
        {
            Response.Redirect("Home.aspx");
        }
      
        if (!Page.IsPostBack)
        {
            f_bindgrid();
        }
    }

    private void f_bindgrid()
    {
        DataTable dt = new DataTable();
        dt = F_create_dataTable();
        gvmonetary.DataSource = dt;
        gvmonetary.DataBind();
    }

    private DataTable F_create_dataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Year");
        dt.Columns.Add("Turnover");
        for (int i = 1; i <= 4; i++)
        {
            DataRow oItem = dt.NewRow();
            int index = 0;
            foreach (DataColumn dcol in dt.Columns)
            {
                if (i == 4 && index == 0)
                {
                    oItem[dcol] = " &nbsp;&nbsp;&nbsp; <b>Monetary Limit</b>";
                }
                    else if((i == 4 && index == 1))
                {
                    oItem[dcol] = " &nbsp;&nbsp;&nbsp; <b>  5 </b>  ";
                    }
                else
                {
                    oItem[dcol] = " &nbsp;&nbsp;&nbsp; 0 ";
                }

                index = index + 1;
            }
            dt.Rows.Add(oItem);
        }
        Table = dt;
        return Table;

    }


    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            clsCompany objcom = new clsCompany();
            int companyid;
            companyid = Convert.ToInt32(Request.QueryString["id"]);
            int urlid;
            urlid = Convert.ToInt32(Request.QueryString["urlid"]);
            int errorId = objcom.F_insert_provisional_monetary_turnover(companyid, UserId,urlid);

            if (errorId == 1)
            {


                if (RoleId == 1)
                {
                    int type = Convert.ToInt32(Request.QueryString["type"]);
                    Response.Redirect("Convertprovisonaltofreshregistration.aspx?i=" + Convert.ToInt32(type), true);
                }
                else
                {
                    Response.Redirect("BAPendingMonetaryLimit.aspx");
                }
            }

        }
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.StackTrace + " " + ex.Message.ToString());
        }
      
    }
}
