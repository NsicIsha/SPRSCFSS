using System;
using System.Collections.Generic;
using System.Data;
using NSICGP_VBClassLibrary;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class appAdministrator_FeesAwaited :  System.Web.UI.Page
{
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
        FunRequest();

        //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        GridViewDataBind();
    }

    public void GridViewDataBind()
    {
        clsCompany objCompany; 
        DataSet Myds;

        try
            {
                objCompany = new clsCompany();
                DataSet ds_waitingcompanies = new DataSet();
                ds_waitingcompanies = FillGrid_Dataset_Online(UserId);
                if (ds_waitingcompanies.Tables[0].Rows.Count > 0)
                {
                    gvRegistrationDetails.DataSource = ds_waitingcompanies.Tables[0];

                    gvRegistrationDetails.DataBind();
                }
                else
                {
                    gvRegistrationDetails.Dispose();
                    gvRegistrationDetails.DataBind();
                    ds_waitingcompanies.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
       
    }
    SqlDataAdapter MyAdp;
    DataSet MyDs;
    public DataSet FillGrid_Dataset_Online(int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_PendingFees_Online", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;

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