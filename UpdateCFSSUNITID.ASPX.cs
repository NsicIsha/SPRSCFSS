using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateCFSSUNITID : System.Web.UI.Page
{
    int CFSSUNITID;
    string strUAM;
    string strPAN;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    System.Data.DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        clsCompany objCompany = new clsCompany();
        DataTable dt = new DataTable();
        dt = Company_Fetch_Record();

        int CompanyId = 118208;// Convert.ToInt32(dt.Rows[0]["fGPtbl_Company_Id"].ToString());
        strUAM = "UDYAM-RJ-33-0006463";// dt.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
        strPAN = "ARIPV2366H";// dt.Rows[0]["fGPtbl_Company_PAN"].ToString();

        #region Retrive CFSSUnitId and Update in Company Table
        CFSSService obj = new CFSSService();
        CFSSUNITID = obj.CallApi_CFSSUNITID_Company(strUAM, "", strPAN);

        if (CFSSUNITID != 0)
        {
            bool isUpdated = obj.UpdateCFSSUnitID_IfNull(CompanyId, CFSSUNITID);
        }
        else
        {
            string json = CFSSService.GetUnitDetailJson_R_RA(CompanyId);

            string errorMessage;
            bool isSuccess = obj.PostUnitDetailsToCFSS(CompanyId, json, out errorMessage);

            if (!isSuccess)
            {
                lblMsg.Text = errorMessage;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
        #endregion
    }


    public DataTable Company_Fetch_Record()
    {
        MyDt = new DataTable();
        try
        {
            
            MyAdp = new SqlDataAdapter("CFSS_GPsp_Company_Update_CFSSUNITID", DBManager.ConnectToDB());
            // MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search_Online_New_CFSS", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
             
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
}